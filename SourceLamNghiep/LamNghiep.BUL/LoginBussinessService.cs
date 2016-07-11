using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DAL.DataServices
{
    public class LoginBussinessService 
    {
        public bool CheckLogin(User user)
        {
            LoginDataService loginData = new LoginDataService();
            return loginData.CheckLogin(user);
        }

       
    }
}
