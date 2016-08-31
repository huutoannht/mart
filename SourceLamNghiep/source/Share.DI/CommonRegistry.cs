using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.Common;
using ML.Utils.Email;
using StructureMap.Configuration.DSL;

namespace Share.DI
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
		{
			For<IEmailHelper>().Use<EmailHelper>();

			For<IDateTimeHelper>().Use<DateTimeHelper>();
		}
    }
}
