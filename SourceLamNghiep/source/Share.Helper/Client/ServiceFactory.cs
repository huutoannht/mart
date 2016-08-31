using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Web.Configuration;
using log4net;

namespace Share.Helper.Client
{
    internal class ServiceFactory
    {
		private readonly ILog _log = LogManager.GetLogger(typeof(ServiceFactory));

        private readonly ServiceModelSectionGroup _serviceModelSectionGroup;

        public ServiceFactory(bool webFlatform = true, string consoleExeName = "")
        {
			_serviceModelSectionGroup = webFlatform 
                ? ServiceModelSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("~"))
                : ServiceModelSectionGroup.GetSectionGroup(ConfigurationManager.OpenExeConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, consoleExeName)));
        }

        public ChannelFactory<T> CreateFactory<T>(string serviceUrl, string bindingType = "basicHttpBinding", string bindingConfiguration = "", string behaviorConfiguration = "") where T : class
        {
            var binding = CreateBinding(bindingType, bindingConfiguration);
            var behaviors = CreateEndpointBehaviors(behaviorConfiguration);

            return CreateFactory<T>(serviceUrl, binding, behaviors);
        }

        public ChannelFactory<T> CreateFactory<T>(string serviceUrl, Binding binding, IEnumerable<IEndpointBehavior> behaviors) where T : class
        {
            var service = new ChannelFactory<T>(binding, serviceUrl);

            foreach (var behavior in behaviors)
            {
                service.Endpoint.Behaviors.Add(behavior);
            }

            return service;
        }

        public Binding CreateBinding(string bindingType = "basicHttpBinding", string bindingConfiguration = "")
        {
            Binding resolveBinding;

            var bindingElement = _serviceModelSectionGroup.Bindings.BindingCollections.Single(s => s.BindingName == bindingType);

            var config = bindingElement.ConfiguredBindings.FirstOrDefault(x => x.Name == bindingConfiguration);

            if (config != null)
            {
                resolveBinding = (Binding)Activator.CreateInstance(bindingElement.BindingType, config.Name);
                config.ApplyConfiguration(resolveBinding);
            }
            else
            {
                resolveBinding = (Binding)Activator.CreateInstance(bindingElement.BindingType);
            }

            return resolveBinding;
        }

        public IEnumerable<IEndpointBehavior> CreateEndpointBehaviors(string behaviorConfiguration = "")
        {
            EndpointBehaviorElement endpointBehaviorsElement = null;

            if (!string.IsNullOrEmpty(behaviorConfiguration))
            {
                endpointBehaviorsElement = _serviceModelSectionGroup.Behaviors.EndpointBehaviors[behaviorConfiguration];
            }

            if (endpointBehaviorsElement == null && _serviceModelSectionGroup.Behaviors.EndpointBehaviors.Count > 0)
            {
                endpointBehaviorsElement = _serviceModelSectionGroup.Behaviors.EndpointBehaviors[0];
            }

            if (endpointBehaviorsElement != null)
            {
                foreach (var behavior in endpointBehaviorsElement)
                {
                    var behaviorType = behavior.GetType();
                    var extension = (IEndpointBehavior)behaviorType.InvokeMember("CreateBehavior", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, behavior, null);

                    if (extension != null)
                    {
                        yield return extension;
                    }
                }
            }
        }
    }
}
