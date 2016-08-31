using System;
using Data.DataContract;
using Data.DataContract.AppSettingDC;

namespace Db.Interfaces
{
    public interface IAppSettingRepository
    {
        bool Delete(Guid id);

        bool CheckExist(AppSettingType settingType, string name);

        bool CheckExist(AppSettingType settingType, string name, Guid id);

        AppSetting GetAppSetting(Guid id);

        FindResponse FindAppSettings(FindRequest request);

        BaseResponse SaveAppSetting(SaveRequest request);
    }
}
