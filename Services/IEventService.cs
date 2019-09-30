using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;

namespace EventApp.Services {
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event evt);

        Task UpdateEventAsync(Event evt);

        Task DeleteEventAsync(int evtId);

        IQueryable<Event> GetEvents();

        Task<Event> GetEventAsync(int evtId);

        object GetEventsAndPlaces();
    }
}