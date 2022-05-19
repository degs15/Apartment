using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string GetConnectionString(string connectionName = "MyConnection")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public List<T> loadData<T>(string sql)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                return db.Query<T>(sql).ToList();
            }
        }

        public void SaveData<T>(T data, string sql)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Execute(sql, data);
            }
         }

        public void DeleteData(string sql)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Execute(sql);
            }

        }



    }
   
}
