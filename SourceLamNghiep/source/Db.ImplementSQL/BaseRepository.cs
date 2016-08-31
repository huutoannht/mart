using System;
using Db.ImplementSQL.Entity;
using ML.Common.Log;

namespace Db.ImplementSQL
{
    public abstract class BaseRepository
    {
        protected readonly ILogger Log;

        protected BaseRepository()
        {
            Log = LogManager.GetLogger(GetType());
        }

        internal SqlDataContext DbContext
        {
            get { return new SqlDataContext(); }
        }
    }
}
