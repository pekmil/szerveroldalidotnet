using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;

namespace EventApp.Services {
    public interface IApiLogService
    {
        void Log(ApiLogEntry entry);

        IQueryable<ApiLogEntry> GetEntries();
    }

}