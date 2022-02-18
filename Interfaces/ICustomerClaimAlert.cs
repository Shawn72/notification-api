using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ICustomerClaimAlert
    {
        Task CreateCustomerClaimAlert(CustomerClaimAlert customerclaimAlert);
        CustomerClaimAlert GetCustomerClaimAlert(int id);
        Task<IEnumerable<CustomerClaimAlert>> GetAllCustomerClaimAlert();
        Task UpdateCustomerClaimAlert(CustomerClaimAlert customerclaimAlert, CustomerClaimAlert dBcustomerclaimAlert);
        Task DeleteCustomerClaimAlert(CustomerClaimAlert customerclaimAlert);
        Task UpdateClaimAlertBySQL(CustomerClaimAlert c_alert, CustomerClaimAlert dBAlert);
    }
}
