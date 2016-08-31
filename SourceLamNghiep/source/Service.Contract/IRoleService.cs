using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.RoleDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "Role")]
    public interface IRoleService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        Role GetRole(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveRole(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindRoles(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteRole(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<RolePermission> GetPermissions(Guid roleId);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<Role> GetAll();
    }
}
