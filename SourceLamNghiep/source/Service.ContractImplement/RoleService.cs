using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.RoleDC;
using Db.Interfaces;
using ML.Common;
using Service.Contract;
using Share.Helper.Cache;
using Share.Helper.Extension;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IBeUserRepository _repositoryBeUser;
        private readonly ICacheHelper _cacheHelper;

        public RoleService(IRoleRepository repository, IBeUserRepository repositoryBeUser,ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
            _repository = repository;
            _repositoryBeUser = repositoryBeUser;
        }

        public Role GetRole(Guid id)
        {
            return Execute(_repository, r => r.GetRole(id));
        }

        public BaseResponse SaveRole(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var isNew = !r.IsExists(request.Entity.Id);

                if (isNew && r.NameIsExist(request.Entity.Name.ToStr().Trim()))
                {
                    response.Success = false;
                    response.Messages.Add("NameIsExisted"); //resource key
                    return response;
                }

                if(r.NameIsExist(request.Entity.Name.ToStr().Trim(), request.Entity.Id))
                {
                    response.Success = false;
                    response.Messages.Add("NameIsExisted"); //resource key
                    return response;   
                }

                request.Entity.Permissions.ForEach(m =>
                {
                    if (m.IsNew) m.InitId();
                    m.RoleId = request.Entity.Id;
                });

                var pages = typeof(BePage).EnumToList();
                if (request.Entity.Permissions.Count > pages.Count)
                {
                    response.Success = false;
                    response.Messages.Add("DataIsInvalid"); //resource key
                    return response;
                }

                var res = r.SaveRole(request);

                _cacheHelper.ClearGetRole(request.Entity.Id);

                return res;
            });
        }

        public FindResponse FindRoles(FindRequest request)
        {
            return Execute(_repository, r => r.FindRoles(request));
        }

        public BaseResponse DeleteRole(Guid id)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                if (_repositoryBeUser.RoleIsUsed(id))
                {
                    res.Success = false;
                    res.Messages.Add("RoleIsUsed");//res key
                    return res;
                }

                res = r.DeleteRole(id);

                _cacheHelper.ClearGetRole(id);

                return res;
            });
        }

        public List<RolePermission> GetPermissions(Guid roleId)
        {
            return Execute(_repository, r => r.GetPermissions(roleId));
        }

        public List<Role> GetAll()
        {
            return Execute(_repository, r => r.GetAll());
        }
    }
}
