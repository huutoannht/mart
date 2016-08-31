﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Site.Backend.Library.DI
{
	/// <summary>
	/// The structure map dependency scope.
	/// </summary>
	public class StructureMapDependencyScope : ServiceLocatorImplBase, IDependencyScope
	{
		#region Constants and Fields

		/// <summary>
		/// The container.
		/// </summary>
		protected readonly IContainer Container;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureMapDependencyScope"/> class.
		/// </summary>
		/// <param name="container">
		/// The container.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// </exception>
		public StructureMapDependencyScope(IContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			Container = container;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The dispose.
		/// </summary>
		public void Dispose()
		{
			Container.Dispose();
		}

        public new object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                         ? Container.TryGetInstance(serviceType)
                         : Container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

		/// <summary>
		/// The get services.
		/// </summary>
		/// <param name="serviceType">
		/// The service type.
		/// </param>
		/// <returns>
		/// The System.Collections.Generic.IEnumerable`1[T -&gt; System.Object].
		/// </returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.GetAllInstances(serviceType).Cast<object>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// When implemented by inheriting classes, this method will do the actual work of
		///        resolving all the requested service instances.
		/// </summary>
		/// <param name="serviceType">
		/// Type of service requested.
		/// </param>
		/// <returns>
		/// Sequence of service instance objects.
		/// </returns>
		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			return Container.GetAllInstances(serviceType).Cast<object>();
		}

		/// <summary>
		/// When implemented by inheriting classes, this method will do the actual work of resolving
		///        the requested service instance.
		/// </summary>
		/// <param name="serviceType">
		/// Type of instance requested.
		/// </param>
		/// <param name="key">
		/// Name of registered service you want. May be null.
		/// </param>
		/// <returns>
		/// The requested service instance.
		/// </returns>
		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				var instance = serviceType.IsAbstract || serviceType.IsInterface
						   ? Container.TryGetInstance(serviceType)
						   : Container.GetInstance(serviceType);

			    return instance;
			}

			return Container.GetInstance(serviceType, key);
		}

		#endregion
	}
}