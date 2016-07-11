using LamNghiep.Common;
using LamNghiep.DTO;
using LamNghiep.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DAL.DataServices
{
    public class KetHoachCTDataService : IKeHoachCTDataService
    {
        public List<KeHoachCT> GetCurWeekKHCT()
        {
            const string storeName = "st_GetKetHoachCongTacCurWeek";
            using (var conn = new AdoHelper())
            {
                 SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return  DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }

        public IList<KeHoachCT> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(KeHoachCT keHoachCT)
        {
            const string storeName = "st_InsertKeHoachCT";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@TenKeHoach",keHoachCT.TenKeHoach),
                     new SqlParameter("@UserName",keHoachCT.UserName),
                     new SqlParameter("@TuNgay",keHoachCT.TuNgay),
                     new SqlParameter("@DenNgay",keHoachCT.DenNgay),
                     new SqlParameter("@KeHoachCongTac",keHoachCT.KeHoachCongTac),
                     new SqlParameter("@ThamDu",keHoachCT.ThamDu),
                     new SqlParameter("@DiaDiem",keHoachCT.DiaDiem),
                     new SqlParameter("@MucDo",keHoachCT.MucDo),
                     new SqlParameter("@LapLai",keHoachCT.LapLai),
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }

        public int Save(KeHoachCT aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(KeHoachCT aggregate)
        {
            throw new NotImplementedException();
        }

        public List<KeHoachCT> GetKeHoachCaNhan(string userName)
        {
            const string storeName = "st_getKeHoachCaNhan";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@UserName",userName),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
    }
}
