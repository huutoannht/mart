using Service.Contract;
using StructureMap;

namespace Share.Helper.Client
{
	public class InternalServiceHelper : IServiceHelper
	{
		private readonly IContainer _container;

		public InternalServiceHelper(IContainer container)
		{
			_container = container;
		}

        public IHtmlContentService HtmlContent
        {
            get { return _container.GetInstance<IHtmlContentService>(); }
        }
	  
	    public IContactUsService ContactUs
        {
            get { return _container.GetInstance<IContactUsService>(); }
        }

        public IRequestPasswordService RequestPassword
        {
            get { return _container.GetInstance<IRequestPasswordService>(); }
        }

	    public IDataLogService DataLog
	    {
            get { return _container.GetInstance<IDataLogService>(); }
	    }

	    public IArticeService Artice
	    {
            get { return _container.GetInstance<IArticeService>(); }
	    }

	    public IFileService File
	    {
            get { return _container.GetInstance<IFileService>(); }
	    }

	    public ICategoryService Category
	    {
            get { return _container.GetInstance<ICategoryService>(); }
	    }

	    public IProductService Product
	    {
            get { return _container.GetInstance<IProductService>(); }
	    }

	    public IRefreshTokenService RefreshToken
	    {
            get { return _container.GetInstance<IRefreshTokenService>(); }
	    }

	    public IBeUserService BeUser
		{
			get { return _container.GetInstance<IBeUserService>(); }
		}

	    public ICustomerService Customer
	    {
            get { return _container.GetInstance<ICustomerService>(); }
	    }

	    public IRoleService Role
        {
            get { return _container.GetInstance<IRoleService>(); }
        }

        public IAppSettingService AppSetting
        {
            get { return _container.GetInstance<IAppSettingService>(); }
        }

        public ISystemEmailTemplateService SystemEmailTemplate
        {
            get { return _container.GetInstance<ISystemEmailTemplateService>(); }
        }
    }
}
