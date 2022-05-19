using System.Collections.Generic;

namespace DataAccessLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        void DeleteData(string sql);
        string GetConnectionString(string connectionName = "MyConnection");
        List<T> loadData<T>(string sql);
        void SaveData<T>(T data, string sql);
    }
}