using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_Test.CM
{
    public class SqlExceptionAPI : Exception
    {
        public SqlExceptionAPI(string errorCode)
            : base(errorCode)
        {
        }
    }
}
