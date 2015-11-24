using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebService.Models.DataAccess.CommonDA
{

    public class DBManager
    {
        #region Variables
        static protected log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        private CommandType _DefaultCommandType = CommandType.StoredProcedure;

        public CommandType DefaultCommandType
        {
            get { return _DefaultCommandType; }
            set { _DefaultCommandType = value; }
        }


        #endregion

        /// <summary>
        /// SqlConnection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Connection(string contr)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[contr].ConnectionString);
                return connection;
            }
            catch (Exception ex)
            {
                logger.Info("Function get connection");
                logger.Error(ex.Message + " Connect string :" + contr);
                logger.Error(ex.StackTrace);
                throw new Exception();
            }
          
        }

        /// <summary>
        /// Method Update Data to database use Store procedure
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int UpdateData(string query, IDataParameter[] parameter, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand();

            command = AddParameter(parameter);
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = query;

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Method get Data From database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static IDataReader GetData(string query, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = conn;
            command.CommandText = query;

            return command.ExecuteReader();
        }

        /// <summary>
        /// Method Get Data from database use Store procedure
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static IDataReader GetData(CommandType commandType=CommandType.Text,string query="", IDataParameter[] parameter=null, SqlConnection conn=null,int connectTimeout=300)
        {
            SqlCommand command = new SqlCommand();

            command = AddParameter(parameter);
            command.Connection = conn;
            command.CommandType = commandType;
            command.CommandText = query;
            command.CommandTimeout = connectTimeout;
            return command.ExecuteReader();
        }

        /// <summary>
        /// Method add paramater into Store procedure
        /// </summary>
        /// <param name="objectParameter"></param>
        /// <returns></returns>
        private static SqlCommand AddParameter(IDataParameter[] objectParameter)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            if (objectParameter != null)
            {
                command.Parameters.AddRange(objectParameter);
            }
            return command;
        }

        /// <summary>
        /// ExecuteScalar With Parameter
        /// </summary>
        /// <param name="p">procedure name</param>
        /// <param name="parameter">user id</param>
        /// <param name="conn">connection string</param>
        /// <returns></returns>
        public static string ExecuteScalar(string p, SqlParameter[] parameter, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command = AddParameter(parameter);
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return command.ExecuteScalar().ToString();
        }
        /// <summary>
        /// ExecuteScalar 
        /// </summary>
        /// <param name="p">procedure name</param>
        /// <param name="parameter">user id</param>
        /// <param name="conn">connection string</param>
        /// <returns></returns>
        public static object ExecuteScalar(string p, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return command.ExecuteScalar();
        }
      
     
    }
}
