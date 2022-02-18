using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ICustomerAlertSettings
    {
        Task CreateCustomerAlertSetting(CustomerAlertSetting customerAlertsetting);
        CustomerAlertSetting GetCustomerAlertSetting(string id);
        Task<IEnumerable<CustomerAlertSetting>> GetAllCustomerAlertSetting();
        Task UpdateCustomerAlertSetting(CustomerAlertSetting customerAlertsetting, CustomerAlertSetting dBcustomerAlertsetting);
        Task DeleteCustomerAlertSetting(CustomerAlertSetting customerAlertsetting);
    }
}
