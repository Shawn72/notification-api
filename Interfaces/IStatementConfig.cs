using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface IStatementConfig
    {
        Task CreateCustomerStatConfig(IndividualStatementConfig statementConfig);
        IndividualStatementConfig GetCustomerStatConfig(int id);
        Task<IEnumerable<IndividualStatementConfig>> GetAllCustomerStatConfig();
        Task UpdateCustomerStatConfig(IndividualStatementConfig statementConfig, IndividualStatementConfig dBstatementConfig);
        Task DeleteCustomerStatConfig(IndividualStatementConfig statementConfig);
    }
}
