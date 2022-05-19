using System.Collections.Generic;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.BusinessLogic
{
    public interface ITenatProcessor
    {
        void addTenant(string name, string address, string phonenumber);
        void deleteTenant(int id);
        List<TenatModel> getAllTenants();
        TenatModel getSpecificTenant(int id);
        void updateTenant(int id, string name, string address, string phonenumber);
    }
}