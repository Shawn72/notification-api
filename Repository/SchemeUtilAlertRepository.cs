using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class SchemeUtilAlertRepository : ISchemeUtilAlert
    {
        private readonly DatabaseContext _context;

        public SchemeUtilAlertRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task CreateSchemeUtilAlert(SchemeUtilAlertModel utilAlert)
        {
            _context.Add(utilAlert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSchemeUtilAlert(SchemeUtilAlertModel utilAlert)
        {
            _context.Remove(utilAlert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SchemeUtilAlertModel>> GetAllSchemeUtilAlert()
        {
            //get full list
            return await _context.SchemeUtilAlert.ToListAsync();
        }
      

        public SchemeUtilAlertModel GetSchemeUtilAlert(int id) =>
            _context.SchemeUtilAlert.FirstOrDefault(p => p.id.Equals(id));

        public async Task UpdateSchemeUtilAlert(SchemeUtilAlertModel utilAlert, SchemeUtilAlertModel dbUtilAlert)
        {
            dbUtilAlert.Allocation = utilAlert.Allocation;
            dbUtilAlert.Expenditure = utilAlert.Expenditure;
            dbUtilAlert.benefit_type = utilAlert.benefit_type;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
