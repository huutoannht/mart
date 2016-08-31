using Service.Contract;

namespace Share.Helper.Client
{
    public interface IServiceHelper
    {
        IDataLogService DataLog { get; }

        IArticeService Artice { get; }

        IFileService File { get; }

        ICategoryService Category { get; }

        IProductService Product { get; }

        IRefreshTokenService RefreshToken { get; }

        IBeUserService BeUser { get; }

        ICustomerService Customer { get; }

        IRoleService Role { get; }

        IAppSettingService AppSetting { get; }

        ISystemEmailTemplateService SystemEmailTemplate { get; }

        IRequestPasswordService RequestPassword { get; }

        IContactUsService ContactUs { get; }

        IHtmlContentService HtmlContent { get; }
    }
}
