using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class CustomerAlertSettingRepository: ICustomerAlertSettings
    {
        private readonly DatabaseContext _context;

        public CustomerAlertSettingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAlertSetting(CustomerAlertSetting customerAlertsetting)
        {
            _context.Add(customerAlertsetting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAlertSetting(CustomerAlertSetting customerAlertsetting)
        {
            _context.Remove(customerAlertsetting);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerAlertSetting>> GetAllCustomerAlertSetting()
        {
            //get full list
            return await _context.Customer_Alert_Setting.ToListAsync();
        }

        public  CustomerAlertSetting GetCustomerAlertSetting(string id) =>
            _context.Customer_Alert_Setting.FirstOrDefault(p => p.id.Equals(id));

        public async Task UpdateCustomerAlertSetting(CustomerAlertSetting customerAlertsetting, CustomerAlertSetting dBcustomerAlertsetting)
        {
            dBcustomerAlertsetting.customer_name = customerAlertsetting.customer_name;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
