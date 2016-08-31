using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.RefreshTokenDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RefreshTokenService : BaseService, IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public RefreshToken GetRefreshToken(string id)
        {
            return Execute(_refreshTokenRepository, r => r.GetRefreshToken(id));
        }

        public BaseResponse Save(RefreshToken entity)
        {
            return Execute(_refreshTokenRepository, r => r.Save(entity));
        }

        public BaseResponse Delete(string id)
        {
            return Execute(_refreshTokenRepository, r => r.Delete(id));
        }
    }
}
