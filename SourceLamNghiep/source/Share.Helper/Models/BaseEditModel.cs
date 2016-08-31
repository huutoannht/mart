using System;

namespace Share.Helper.Models
{
	[Serializable]
	public class BaseEditModel<T>
	{
		public BaseEditModel()
		{
			Entity = Activator.CreateInstance<T>();
		}

		public T Entity { get; set; }

		public bool IsEdit { get; set; }
	}
}