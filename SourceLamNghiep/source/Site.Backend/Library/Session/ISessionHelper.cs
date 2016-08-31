using System;
using System.Net.Mail;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Data.DataContract.RoleDC;
using System.Collections.Generic;

namespace Site.Backend.Library.Session
{
	public interface ISessionHelper
	{
        string CurrentTimeZone { get; set; }

		Guid CurrentUserId { get; set; }

		BeUser CurrentUser { get; }

        Role CurrentRole { get; }

		object Get(SessionKey key);

	    void Set(SessionKey key, object source);

		void Abandon();

        #region permissions

        bool CanView(BePage page);

	    bool CanAdd(BePage page);

        bool CanUpdate(BePage page);

	    bool CanDelete(BePage page);

        bool CanImport(BePage page);

        bool CanExport(BePage page);

        #endregion
        
	}

}
