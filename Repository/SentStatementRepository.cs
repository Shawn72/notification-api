using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Repository
{
    public class SentStatementRepository : ISentStatement
    {
        private readonly DatabaseContext _context;

        public SentStatementRepository(DatabaseContext context)
        {
            _context = context;
        }
        public  async Task CreateSentStatement(SentStatement sentStatement)
        {
            _context.Add(sentStatement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSentStatement(SentStatement sentStatement)
        {
            _context.Remove(sentStatement);
            await _context.SaveChangesAsync();
        }
        

        public async Task<IEnumerable<SentStatement>> GetAllSentStatement()
        {
            //get full list
            return await _context.send_statement.ToListAsync();
        }

        public SentStatement GetSentStatement(string id) =>
            _context.send_statement.FirstOrDefault(p => p.id.Equals(id));

        public async Task UpdateSentStatetement(SentStatement sentStatement, SentStatement dBsentStatement)
        {
            dBsentStatement.email_object = sentStatement.email_object;
            //update with coulumns you'd like to update
            await _context.SaveChangesAsync();
        }
    }
}
