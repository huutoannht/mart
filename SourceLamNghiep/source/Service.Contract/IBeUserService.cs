using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "BeUser")]
    public interface IBeUserService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse Delete(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<BeUser> GetAll();

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindBeUsers(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BeUser GetBeUser(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        CheckLoginResponse CheckLogin(CheckLoginRequest request);

        /// <summary>
        /// Hàm này gọi từ trang tôi quên mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse ChangePwd(ChangePwdResquest request);

        /// <summary>
        /// Hàm này gọi từ trang change password khi user đã login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveChangePassword(ChangePwdResquest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateUserLogin(UpdateUserLoginRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveBeUser(SaveRequest request);
    }
}
