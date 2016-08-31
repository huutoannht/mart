using System;
using System.Runtime.Serialization;

namespace Data.DataContract.AppSettingDC
{
    [Serializable]
    [DataContract]
    public class SaveRequest : BaseSaveRequest<AppSetting>
    {
    }
}
