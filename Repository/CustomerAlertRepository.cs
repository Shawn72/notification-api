using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Models;
using NotificationApi.Paging;
using System;
using NotificationApi.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class CustomerAlertRepository<T> : RepositoryBase<CustomerAlertModel>, ICustomerAlert
    {
       // protected DatabaseContext _context { get; set; }

        CultureInfo culture = new CultureInfo("en-US");

        private readonly IRedisCacheService _redisCacheService;

        //public CustomerAlertRepository(DatabaseContext context)
        //{
        //    _context = context;
        //}

        public CustomerAlertRepository(DatabaseContext context, IRedisCacheService redisCacheService)
        : base(context)
        {
            _redisCacheService = redisCacheService;
        }
        public async Task CreateCustomerAlert(CustomerAlertModel customerAlert)
        {
            _context.Add(customerAlert);
            await _context.SaveChangesAsync();
        }

        public  async Task DeleteCustomerAlert(CustomerAlertModel customerAlert)
        {
            _context.Remove(customerAlert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerAlertModel>> GetAllCustomerAlert()
        {
            var redCachedId = _redisCacheService.Get<IEnumerable<CustomerAlertModel>>("customer_alert_list");
            if (redCachedId != null) return redCachedId; //return saved id on redis cache
            else
            {
                //get full list
                var alldata = await _context.CustomerAlert.OrderByDescending(i => i.AlertID)
                       .Take(100).ToListAsync();
                //cache the object retrieved
                if (alldata != null)
                    _redisCacheService.Set<IEnumerable<CustomerAlertModel>>("customer_alert_list", alldata);
                //return the same object if accessed for the first time within the set timeout
                return alldata;
            }

        }

        public CustomerAlertModel GetCustomerAlert(Int64 id) =>
            _context.CustomerAlert.FirstOrDefault(p => p.AlertID.Equals(id));    

        public async Task UpdateCustomerAlert(CustomerAlertModel customerAlert, CustomerAlertModel dBcustomerAlert)
        {
            dBcustomerAlert.customer_id = customerAlert.customer_id;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();    
        }

        public PagedList<CustomerAlertModel> GetCustomerAlertPagedlist(PagingParameters pgParameters)
        {
            //no redis cache, brings issues, but can cache the rest
            try
            {
                //get full list
                var pageddata = PagedList<CustomerAlertModel>.ToPagedList(FindAllAlerts(), pgParameters.PageNumber,
              pgParameters.PageSize);
               
                //return the same object if accessed for the first time within the set timeout
                return pageddata;
            
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public CustomerAlertModel GetCustomerAlertbySchemeName(string schemeName)
        {
            return FindByCondition(alert => alert.Scheme.Equals(schemeName))
                  .DefaultIfEmpty(new CustomerAlertModel())
                  .FirstOrDefault();
        }
    }
}
