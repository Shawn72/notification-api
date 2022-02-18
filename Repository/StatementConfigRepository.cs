using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class StatementConfigRepository : IStatementConfig
    {
        private readonly DatabaseContext _context;

        public StatementConfigRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task CreateCustomerStatConfig(IndividualStatementConfig statementConfig)
        {
            _context.Add(statementConfig);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerStatConfig(IndividualStatementConfig statementConfig)
        {
            _context.Remove(statementConfig);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndividualStatementConfig>> GetAllCustomerStatConfig()
        {
            //get full list
            return await _context.individual_statement_config.ToListAsync();
        }

        public IndividualStatementConfig GetCustomerStatConfig(string id) =>
            _context.individual_statement_config.FirstOrDefault(p => p.id.Equals(id));

        public async Task UpdateCustomerStatConfig(IndividualStatementConfig statementConfig, IndividualStatementConfig dBstatementConfig)
        {
            dBstatementConfig.customer_name = statementConfig.customer_name;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
