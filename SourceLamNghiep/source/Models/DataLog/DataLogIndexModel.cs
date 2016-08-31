using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.Helper.Models;

namespace Models.DataLog
{
    public class DataLogIndexModel : BaseIndexModel<DataLogModel>
    {
        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
        public string TextSearch { get; set; }

        [Display(Name = "FromDate", ResourceType = typeof(Share.Messages.BeScreens.CustomerLog.CustomerLog))]
        public DateTime? FromDate { get; set; }

        [Display(Name = "ToDate", ResourceType = typeof(Share.Messages.BeScreens.CustomerLog.CustomerLog))]
        public DateTime? ToDate { get; set; }
    }
}
