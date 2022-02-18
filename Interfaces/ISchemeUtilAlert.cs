using NotificationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.Interfaces
{
    public interface ISchemeUtilAlert
    {
        Task CreateSchemeUtilAlert(SchemeUtilAlertModel utilAlert);
        SchemeUtilAlertModel GetSchemeUtilAlert(int id);
        Task<IEnumerable<SchemeUtilAlertModel>> GetAllSchemeUtilAlert();
        Task UpdateSchemeUtilAlert(SchemeUtilAlertModel utilAlert, SchemeUtilAlertModel dbUtilAlert);
        Task DeleteSchemeUtilAlert(SchemeUtilAlertModel utilAlert);
    }
}
