using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class CustomerClaimAlertRepository : ICustomerClaimAlert
    {
        private readonly DatabaseContext _context;

        public CustomerClaimAlertRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerClaimAlert(CustomerClaimAlert customerclaimAlert)
        {
            _context.Add(customerclaimAlert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerClaimAlert(CustomerClaimAlert customerclaimAlert)
        {
            _context.Remove(customerclaimAlert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerClaimAlert>> GetAllCustomerClaimAlert()
        {
            //get full list
            return await _context.customer_claim_alert.ToListAsync();
        }

        public CustomerClaimAlert GetCustomerClaimAlert(int id) =>
            _context.customer_claim_alert.FirstOrDefault(p => p.id.Equals(id));

        public  async Task UpdateClaimAlertBySQL(CustomerClaimAlert c_alert, CustomerClaimAlert dBAlert)
        {
             _context.customer_claim_alert.FromSqlRaw("WITH cstAlertTable AS ( SELECT TOP 1 * FROM  dbo.customer_claim_alert   WHERE id = " + c_alert.id+ "ORDER BY id DESC ) UPDATE TOP(1) cstAlertTable SET alert_max_claim_amount = " + c_alert.alert_max_claim_amount + " WHERE id = " + c_alert.id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerClaimAlert(CustomerClaimAlert customerclaimAlert, CustomerClaimAlert dBcustomerclaimAlert)
        {
            dBcustomerclaimAlert.alert_max_claim_amount = customerclaimAlert.alert_max_claim_amount;
            dBcustomerclaimAlert.alert_max_amount = customerclaimAlert.alert_max_amount;
            dBcustomerclaimAlert.email_address = customerclaimAlert.email_address;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
