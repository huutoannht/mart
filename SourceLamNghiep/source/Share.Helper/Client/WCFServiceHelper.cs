using System.Collections.Concurrent;
using System.Reflection;
using log4net;
using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Service.Contract;

namespace Share.Helper.Client
{
	public class WCFServiceHelper : IServiceHelper
	{
		private readonly ILog _log = LogManager.GetLogger(typeof(WCFServiceHelper));

        public IHtmlContentService HtmlContent
        {
            get { return CreateChannel<IHtmlContentService>(); }
        }
	 
        public IContactUsService ContactUs
        {
            get { return CreateChannel<IContactUsService>(); }
        }
	  
	    public IRequestPasswordService RequestPassword
        {
            get { return CreateChannel<IRequestPasswordService>(); }
        }

	    public IDataLogService DataLog
	    {
            get { return CreateChannel<IDataLogService>(); }
	    }

	    public IArticeService Artice
	    {
            get { return CreateChannel<IArticeService>(); }
	    }

	    public IFileService File
	    {
            get { return CreateChannel<IFileService>(); }
	    }

	    public ICategoryService Category
	    {
            get { return CreateChannel<ICategoryService>(); }
	    }

	    public IProductService Product
	    {
            get { return CreateChannel<IProductService>(); }
	    }

	    public IRefreshTokenService RefreshToken
	    {
            get { return CreateChannel<IRefreshTokenService>(); }
	    }

	    public IBeUserService BeUser
        {
            get { return CreateChannel<IBeUserService>(); }
        }

	    public ICustomerService Customer
	    {
            get { return CreateChannel<ICustomerService>(); }
	    }

	    public IRoleService Role
        {
            get { return CreateChannel<IRoleService>(); }
        }

        public IAppSettingService AppSetting
        {
            get { return CreateChannel<IAppSettingService>(); }
        }

        public ISystemEmailTemplateService SystemEmailTemplate
        {
            get { return CreateChannel<ISystemEmailTemplateService>(); }
        }

	    public WCFServiceHelper()
		{
			InitSettings();
		}

		public WCFServiceHelper(string consoleExeName)
		{
			InitSettings(false, consoleExeName);
		}

		#region Init Channel

		private static readonly ConcurrentDictionary<Type, ChannelFactory> ChannelFactories = new ConcurrentDictionary<Type, ChannelFactory>();

		private static readonly object LockObject = new object();

		private static ServiceFactory _serviceFactory;

		private static string _serviceRootUrl;

		private static Binding _serviveBinding;

		private static IEnumerable<IEndpointBehavior> _servicEndpointBehaviors;

		private void InitSettings(bool webFlatform = true, string consoleExeName = "")
		{
			if (_serviceFactory == null)
			{
				lock (LockObject)
				{
					_serviceRootUrl = std.AppSettings["ServiceRootUrl"];

					if (_serviceRootUrl.EndsWith("/"))
					{
						_serviceRootUrl = _serviceRootUrl.TrimEnd('/');
					}

					_serviceFactory = new ServiceFactory(webFlatform, consoleExeName);
					_serviveBinding = _serviceFactory.CreateBinding(std.AppSettings["ServiceBindingType"], std.AppSettings["ServiceBindingConfiguration"]);
					_servicEndpointBehaviors = _serviceFactory.CreateEndpointBehaviors(std.AppSettings["ServiceBehaviorConfiguration"]).ToList();

					//_log.Debug("Init FantasyAutoFiller ServiceFactory");
				}
			}
		}

		private T CreateChannel<T>() where T : class
		{
			var serviceType = typeof(T);

			var channel = ChannelFactories.GetOrAdd(serviceType, type =>
			{
				var serviceContractAttr = serviceType.GetCustomAttribute<ServiceContractAttribute>();

				var serviceUrl = _serviceRootUrl + "/"
								 + (!string.IsNullOrWhiteSpace(serviceContractAttr.Name)
									 ? serviceContractAttr.Name
									 : type.Name.StartsWith("I", StringComparison.Ordinal) ? type.Name.Remove(0, 1) : type.Name);

				//_log.Debug("Create FantasyAutoFiller Channel: " + serviceUrl);

				return _serviceFactory.CreateFactory<T>(serviceUrl, _serviveBinding, _servicEndpointBehaviors);
			});

			return ((ChannelFactory<T>)channel).CreateChannel();
		}

		#endregion

    }
}
