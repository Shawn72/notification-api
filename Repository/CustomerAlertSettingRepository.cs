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
            dBcustomerAlertsetting.percentage_alert = customerAlertsetting.percentage_alert;
            dBcustomerAlertsetting.email_address = customerAlertsetting.email_address;
            dBcustomerAlertsetting.pol_id = customerAlertsetting.pol_id;
            dBcustomerAlertsetting.pool_number= customerAlertsetting.pool_number;
            dBcustomerAlertsetting.country_code= customerAlertsetting.country_code;
            dBcustomerAlertsetting.inpatient_alert= customerAlertsetting.inpatient_alert;
            dBcustomerAlertsetting.inpatient_weekly_alert = customerAlertsetting.inpatient_weekly_alert;
            dBcustomerAlertsetting.customer_code= customerAlertsetting.customer_code;
            dBcustomerAlertsetting.member_perc_util=customerAlertsetting.member_perc_util;
            dBcustomerAlertsetting.scheme_util_index = customerAlertsetting.scheme_util_index;
            dBcustomerAlertsetting.ip_alert_email = customerAlertsetting.ip_alert_email;
            dBcustomerAlertsetting.full_member_util = customerAlertsetting.full_member_util;
            dBcustomerAlertsetting.full_member_util_shared = customerAlertsetting.full_member_util_shared;
            dBcustomerAlertsetting.full_member_util_email = customerAlertsetting.full_member_util_email;
            dBcustomerAlertsetting.scheme_expiry_alert_email = customerAlertsetting.scheme_expiry_alert_email;
            dBcustomerAlertsetting.age_cut_off_alert = customerAlertsetting.age_cut_off_alert;
            dBcustomerAlertsetting.split_report_into_multiple_sheets = customerAlertsetting.split_report_into_multiple_sheets;
            dBcustomerAlertsetting.perc_report_frequency = customerAlertsetting.perc_report_frequency;
            dBcustomerAlertsetting.include_cat_desc = customerAlertsetting.include_cat_desc;
            dBcustomerAlertsetting.include_util_summary = customerAlertsetting.include_util_summary;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
