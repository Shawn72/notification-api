using NotificationApi.Models;
using NotificationApi.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ICustomerAlert: IRepositoryBase<CustomerAlertModel>
    {
        Task CreateCustomerAlert(CustomerAlertModel customerAlert);
        CustomerAlertModel GetCustomerAlert(Int64 id);
        Task<IEnumerable<CustomerAlertModel>> GetAllCustomerAlert();
        Task UpdateCustomerAlert(CustomerAlertModel customerAlert, CustomerAlertModel dBcustomerAlert);
        Task DeleteCustomerAlert(CustomerAlertModel customerAlert);

        // to test paging
        PagedList<CustomerAlertModel> GetCustomerAlertPagedlist(PagingParameters pgParameters);
        CustomerAlertModel GetCustomerAlertbySchemeName(string schemeName);
    }
}
