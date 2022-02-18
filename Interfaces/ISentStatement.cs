using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ISentStatement
    {
        Task CreateSentStatement(SentStatement sentStatement);
        SentStatement GetSentStatement(string id);
        Task<IEnumerable<SentStatement>> GetAllSentStatement();
        Task UpdateSentStatetement(SentStatement sentStatement, SentStatement dBsentStatement);
        Task DeleteSentStatement(SentStatement sentStatement);
    }
}
