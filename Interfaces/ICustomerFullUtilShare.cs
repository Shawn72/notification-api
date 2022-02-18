using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ICustomerFullUtilShare
    {
        Task CreateCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting);
        CustomerFullUtilShareSetting GetCustomerFullUtilShare(string id);
        Task<IEnumerable<CustomerFullUtilShareSetting>> GetAllCustomerFullUtilShare();
        Task UpdateCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting, CustomerFullUtilShareSetting dBcustomerUtilsharesetting);
        Task DeleteCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting);
    }
}
