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
    public class PhongDataService : IPhongDataService
    {
        public List<Phong> GetPhongHop()
        {
            const string storeName = "st_getPhong";
            using (var conn = new AdoHelper())
            {
                 SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return  DataReaderExtensions.DataReaderToObjectList<Phong>(reader);
            }
        }

        public IList<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(User aggregate)
        {
            throw new NotImplementedException();
        }

        public int Save(User aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
