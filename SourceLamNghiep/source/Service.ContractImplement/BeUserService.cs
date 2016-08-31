using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Db.Interfaces;
using ML.Common;
using Service.Contract;
using Share.Helper;
using Share.Helper.Cache;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BeUserService : BaseService, IBeUserService
    {
        private readonly IBeUserRepository _repository;
        private readonly ICacheHelper _cacheHelper;
        private readonly IRequestPasswordRepository _requestPasswordRepository;

        public BeUserService(IBeUserRepository repository, IRequestPasswordRepository requestPasswordRepository, ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
            _repository = repository;
            _requestPasswordRepository = requestPasswordRepository;
        }

        public BaseResponse Delete(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var beUser = _cacheHelper.GetBeUser(id);
                var res = r.Delete(id);

                if (beUser != null)
                {
                    DeleteFile(dataFolderPath, beUser.ImagePath);
                }

                return res;
            });
        }

        public List<BeUser> GetAll()
        {
            return Execute(_repository, r => r.GetAll());
        }

        public FindResponse FindBeUsers(FindRequest request)
        {
            return Execute(_repository, r => r.FindBeUsers(request));
        }

        public BeUser GetBeUser(Guid id)
        {
            return Execute(_repository, r => r.GetBeUser(id));
        }

        public CheckLoginResponse CheckLogin(CheckLoginRequest request)
        {
            return Execute(_repository, r =>
            {
                request.Email = request.Email.ToStr().Trim().ToLower();
                var response = new CheckLoginResponse
                {
                    BeUser = _cacheHelper.GetAllBeUsers().FirstOrDefault(i => i.Email == request.Email)
                };

                if (response.BeUser == null)
                {
                    response.Messages.Add("UnableFindUser");//Return resouce key
                    return response;
                }

                if (!SimpleHash.VerifyHash(request.Password, SiteSettings.HashAlgorithm, response.BeUser.HashPassword))
                {
                    response.Messages.Add("PwdIncorrect");
                    return response;
                }

                if (response.BeUser.Status == BeUserStatus.Locked)
                {
                    response.Messages.Add("UserLocked");
                    return response;
                }

                r.UpdateUserLogin(new UpdateUserLoginRequest
                {
                    UserId = response.BeUser.Id
                });

                response.EntityId = response.BeUser.Id;

                return response;
            });
        }

        /// <summary>
        /// Hàm này gọi từ trang tôi quên mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse ChangePwd(ChangePwdResquest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var requestData = _requestPasswordRepository.GetByRefKey(request.KeyRef);

                if (requestData == null)
                {
                    response.Success = false;
                    response.Messages.Add("BadRequest");//Res key
                    return response;
                }

                var beUser = _cacheHelper.GetAllBeUsers().FirstOrDefault(i => i.Email == requestData.Email.ToStr().Trim().ToLower());
                if (beUser == null)
                {
                    response.Success = false;
                    response.Messages.Add("UnableFindUser");//Res key
                    return response;
                }

                request.UserId = beUser.Id;
                request.NewPassword = SimpleHash.ComputeHash(request.NewPassword, SiteSettings.HashAlgorithm, null);

                r.ChangePwd(request);

                _requestPasswordRepository.Delete(requestData.Id);

                _cacheHelper.ClearGetBeUser(beUser.Id);

                return response;
            });
        }

        public BaseResponse UpdateUserLogin(UpdateUserLoginRequest request)
        {
            return Execute(_repository, r => r.UpdateUserLogin(request));
        }

        public BaseResponse SaveBeUser(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var beUser = request.Entity;
                beUser.Email = beUser.Email.ToStr().Trim().ToLower();

                #region check

                if (beUser.IsNew)
                {
                    if (r.EmailExists(beUser.Email))
                    {
                        response.Success = false;
                        response.Messages.Add("EmailExisted"); //res key
                        return response;
                    }
                }
                else if (r.EmailExists(beUser.Email, beUser.Id))
                {
                    response.Success = false;
                    response.Messages.Add("EmailExisted");//res key
                    return response;
                }

                if (beUser.IsNew)
                {
                    if (r.CodeExists(beUser.Code))
                    {
                        response.Success = false;
                        response.Messages.Add("CodeExisted"); //res key
                        return response;
                    }
                }
                else if (r.CodeExists(beUser.Code, beUser.Id))
                {
                    response.Success = false;
                    response.Messages.Add("CodeExisted");//res key
                    return response;
                }
                #endregion

                if (beUser.IsNew)
                {
                    beUser.InitId();
                    beUser.CreatedDate = DateTime.UtcNow;
                    beUser.HashPassword = SimpleHash.ComputeHash(request.Password, SiteSettings.HashAlgorithm, null);
                }
                else
                {
                    var oldData = r.GetBeUser(beUser.Id);
                    if (oldData == null)
                    {
                        response.Success = false;
                        response.Messages.Add("CannotLoadData");//res key
                        return response;
                    }

                    beUser.CreatedDate = oldData.CreatedDate;
                    beUser.LastLoginDate = oldData.LastLoginDate;

                    beUser.HashPassword = !string.IsNullOrWhiteSpace(request.Password)
                        ? SimpleHash.ComputeHash(request.Password, SiteSettings.HashAlgorithm, null)
                        : oldData.HashPassword;

                    if (request.IsUpdateProfile)
                    {
                        beUser.RoleId = oldData.RoleId;
                        beUser.Type = oldData.Type;
                        beUser.Code = oldData.Code;
                        beUser.Status = oldData.Status;
                    }
                }

                response = r.SaveBeUser(request);

                response.EntityId = beUser.Id;

                _cacheHelper.ClearGetBeUser(beUser.Id);

                return response;
            });
        }

        /// <summary>
        /// Hàm này gọi từ trang change password khi user đã login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse SaveChangePassword(ChangePwdResquest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();
                var beUser = _cacheHelper.GetBeUser(request.UserId);

                if (beUser == null)
                {
                    response.Success = false;
                    response.Messages.Add("UnableFindUser");//Res key
                    return response;
                }

                if (!SimpleHash.VerifyHash(request.Password, SiteSettings.HashAlgorithm, beUser.HashPassword))
                {
                    response.Messages.Add("PwdIncorrect");
                    response.Success = false;
                    return response;
                }

                request.NewPassword = SimpleHash.ComputeHash(request.NewPassword, SiteSettings.HashAlgorithm, null);

                r.ChangePwd(request);

                _cacheHelper.ClearGetBeUser(beUser.Id);

                return response;
            });
        }
    }
}


