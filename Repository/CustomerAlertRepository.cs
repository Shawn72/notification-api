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

        //public CustomerAlertRepository(DatabaseContext context)
        //{
        //    _context = context;
        //}

        public CustomerAlertRepository(DatabaseContext context)
        : base(context)
        {
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
            //get full list
            //default place holder
            return await _context.CustomerAlert.OrderByDescending(i => i.AlertID)
                        .Take(100).ToListAsync();
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
            return PagedList<CustomerAlertModel>.ToPagedList(FindAllAlerts(), pgParameters.PageNumber,
              pgParameters.PageSize);           

        }

        public CustomerAlertModel GetCustomerAlertbySchemeName(string schemeName)
        {
            return FindByCondition(alert => alert.Scheme.Equals(schemeName))
                  .DefaultIfEmpty(new CustomerAlertModel())
                  .FirstOrDefault();
        }
    }
}
