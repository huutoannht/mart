using LamNghiep.DAL.DataServices;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.BUL.Bussiness
{
    public class KetHoachCTBussinessService 
    {
        public List<KeHoachCongTacModel> GetCurWeekKHCT()
        {
            KetHoachCTDataService keHoachCTDataService = new KetHoachCTDataService();
            List<KeHoachCT> listKeHoachCT = keHoachCTDataService.GetCurWeekKHCT();
            List<KeHoachCT> listKeHoachCTMonday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTTuesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTWednesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTThursday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTFriday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSaturday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSunday = new List<KeHoachCT>();

            List<KeHoachCongTacModel> listKeHoachCTModel = new List<KeHoachCongTacModel>();
            foreach (var item in listKeHoachCT)
            {
                KeHoachCongTacModel keHoachCongTacModel = new KeHoachCongTacModel();
                keHoachCongTacModel.Ngay = item.Ngay;
                if ("Monday".Equals(item.Ngay))
                {
                    listKeHoachCTMonday.Add(item);
                }
                if ("Tuesday".Equals(item.Ngay))
                {
                    listKeHoachCTTuesday.Add(item);
                }
                if ("Wednesday".Equals(item.Ngay))
                {
                    listKeHoachCTWednesday.Add(item);
                }
                if ("Thursday ".Equals(item.Ngay))
                {
                    listKeHoachCTThursday.Add(item);
                }
                if ("Friday".Equals(item.Ngay))
                {
                    listKeHoachCTFriday.Add(item);
                }
                if ("Saturday".Equals(item.Ngay))
                {
                    listKeHoachCTSaturday.Add(item);
                }
                if ("Sunday".Equals(item.Ngay))
                {
                    listKeHoachCTSunday.Add(item);
                }
            }
         
            KeHoachCongTacModel listKeHoachCTDayMondayModel = new KeHoachCongTacModel();
            listKeHoachCTDayMondayModel.Ngay = "Monday";
            listKeHoachCTDayMondayModel.ListCongTac = listKeHoachCTMonday;
            listKeHoachCTModel.Add(listKeHoachCTDayMondayModel);

            KeHoachCongTacModel listKeHoachCTDayTuesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayTuesdayModel.Ngay = "Tuesday";
            listKeHoachCTDayTuesdayModel.ListCongTac = listKeHoachCTTuesday;
            listKeHoachCTModel.Add(listKeHoachCTDayTuesdayModel);

            KeHoachCongTacModel listKeHoachCTDayWednesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayWednesdayModel.Ngay = "Wednesday";
            listKeHoachCTDayWednesdayModel.ListCongTac = listKeHoachCTWednesday;
            listKeHoachCTModel.Add(listKeHoachCTDayWednesdayModel);

            KeHoachCongTacModel listKeHoachCTDayThursdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayThursdayModel.Ngay = "Thursday";
            listKeHoachCTDayThursdayModel.ListCongTac = listKeHoachCTThursday;
            listKeHoachCTModel.Add(listKeHoachCTDayThursdayModel);

            KeHoachCongTacModel listKeHoachCTDayFridayModel = new KeHoachCongTacModel();
            listKeHoachCTDayFridayModel.Ngay = "Friday";
            listKeHoachCTDayFridayModel.ListCongTac = listKeHoachCTFriday;
            listKeHoachCTModel.Add(listKeHoachCTDayFridayModel);

            KeHoachCongTacModel listKeHoachCTDaySaturdayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySaturdayModel.Ngay = "Saturday";
            listKeHoachCTDaySaturdayModel.ListCongTac = listKeHoachCTSaturday;
            listKeHoachCTModel.Add(listKeHoachCTDaySaturdayModel);

            KeHoachCongTacModel listKeHoachCTDaySundayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySundayModel.Ngay = "Sunday";
            listKeHoachCTDaySundayModel.ListCongTac = listKeHoachCTSunday;
            listKeHoachCTModel.Add(listKeHoachCTDaySundayModel);

            return listKeHoachCTModel;
        }

        public int AddKeHoachCT(KeHoachCT keHoach)
        {
            KetHoachCTDataService ketHoachCTDataService = new KetHoachCTDataService();
            return ketHoachCTDataService.Add(keHoach);
        }

        public List<KeHoachCT> GetKeHoachCaNhan(string userName) {
            KetHoachCTDataService ketHoachCTDataService = new KetHoachCTDataService();
            return ketHoachCTDataService.GetKeHoachCaNhan(userName);
        }

        public List<KeHoachCT> GetKeHoachCanBo(string userName)
        {
            KetHoachCTDataService ketHoachCTDataService = new KetHoachCTDataService();
            return ketHoachCTDataService.GetKeHoachCanBo(userName);
        }
    }
}
