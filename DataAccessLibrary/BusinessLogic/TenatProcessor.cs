using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.BusinessLogic
{
    public class TenatProcessor : ITenatProcessor
    {
        ISqlDataAccess _sqlDataAccess;

        public TenatProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<TenatModel> getAllTenants()
        {
            string sql = @"Select * from dbo.Tenants";

            var output = _sqlDataAccess.loadData<TenatModel>(sql);

            return output;

        }

        public TenatModel getSpecificTenant(int id)
        {
            string sql = @"Select * from dbo.Tenants where TenantId="+id;

            var output = _sqlDataAccess.loadData<TenatModel>(sql).FirstOrDefault();

            return output;

        }

        public void addTenant(string name, string address, string phonenumber)
        {
            TenatModel data = new TenatModel();
            data.TenantName = name;
            data.TenantAddress = address;
            data.TenantPhoneNumber = phonenumber;

            string sql = @"Insert into dbo.Tenants (TenantName,TenantAddress,TenantPhoneNumber) values (@TenantName,@TenantAddress,@TenantPhoneNumber)";

            _sqlDataAccess.SaveData(data, sql);

        }

        public void updateTenant(int id,string name, string address, string phonenumber)
        {
            TenatModel data = new TenatModel();
            data.TenantId = id;
            data.TenantName = name;
            data.TenantAddress = address;
            data.TenantPhoneNumber = phonenumber;

            string sql = @"Update dbo.Tenants set TenantName = @TenantName, TenantAddress = @TenantAddress, TenantPhoneNumber = @TenantPhoneNumber where TenantId = @TenantId";

            _sqlDataAccess.SaveData(data, sql);

        }

        public void deleteTenant(int id)
        {
            string sql = @"Delete from [dbo].[Transaction] where TenantId=" + id;

            _sqlDataAccess.DeleteData(sql);

            string sql2 = @"Delete from dbo.Tenants where TenantId=" + id;

            _sqlDataAccess.DeleteData(sql2);
        }



    }
}
