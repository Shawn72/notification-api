using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class CustomerFullUtilShareRepository : ICustomerFullUtilShare
    {
        private readonly DatabaseContext _context;

        public CustomerFullUtilShareRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting)
        {
            _context.Add(customerUtilsharesetting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting)
        {
            _context.Remove(customerUtilsharesetting);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerFullUtilShareSetting>> GetAllCustomerFullUtilShare()
        {
            //get full list
            return await _context.Customer_full_util_share_setting.ToListAsync();
        }

        public CustomerFullUtilShareSetting GetCustomerFullUtilShare(string id) =>
            _context.Customer_full_util_share_setting.FirstOrDefault(p => p.id.Equals(id));

        public async Task UpdateCustomerFullUtilShare(CustomerFullUtilShareSetting customerUtilsharesetting, CustomerFullUtilShareSetting dBcustomerUtilsharesetting)
        {

            dBcustomerUtilsharesetting.percentage_alert = customerUtilsharesetting.percentage_alert;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();

        }
    }
}
