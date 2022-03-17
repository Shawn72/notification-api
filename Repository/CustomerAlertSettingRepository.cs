using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class CustomerAlertSettingRepository : ICustomerAlertSettings
    {
        private readonly DatabaseContext _context;

        private readonly IRedisCacheService _redisCacheService;

        public CustomerAlertSettingRepository(DatabaseContext context, IRedisCacheService redisCacheService)
        {
            _context = context;
            _redisCacheService = redisCacheService;
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
            var redCachedId = _redisCacheService.Get<IEnumerable<CustomerAlertSetting>>("alert_settn_list");
            if (redCachedId != null) return redCachedId; //return saved id on redis cache

            else
            {
                //get full list
                var alldata = await _context.Customer_Alert_Setting.ToListAsync();
                //cache the object retrieved
                if (alldata != null)
                    _redisCacheService.Set<IEnumerable<CustomerAlertSetting>>("alert_settn_list", alldata);
                //return the same object if accessed for the first time within the set timeout
                return alldata;

            }
        }

        public CustomerAlertSetting GetCustomerAlertSetting(int id) { 

            var redCachedId = _redisCacheService.Get<CustomerAlertSetting>(id.ToString());

            if (redCachedId != null) return redCachedId; //return saved id on redis cache
            else
            {
                CustomerAlertSetting alertSett = _context.Customer_Alert_Setting.FirstOrDefault(p => p.id ==Convert.ToInt32(id) );

                //cache the object retrieved
                if (alertSett != null)
                    _redisCacheService.Set<CustomerAlertSetting>(id.ToString(), alertSett);
                //return the same object if accessed for the first time within the set timeout
                return alertSett;
            }
            
        }
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
