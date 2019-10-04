using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.UnitOfWork
{
    public interface IEventUnitOfWork
    {
        object GetEventsAndPlaces();
        Task<Event> CreateEventWithPlace(Event evt, Place place);
    }
    public class EventUnitOfWork<TContext> : UnitOfWork<TContext>, IEventUnitOfWork where TContext : DbContext
    {
        public EventUnitOfWork(TContext context) : base(context)
        {
        }

        public object GetEventsAndPlaces()
        {
            var events = GetRepository<Event>().GetAll();
            var places = GetRepository<Place>().GetAll();
            return new {
                Events = events,
                Places = places
            };
        }

        public async Task<Event> CreateEventWithPlace(Event evt, Place place)
        {
            bool existPlace = GetRepository<Place>().Exists(p => p.Address == place.Address);
            if(!existPlace)
            {
                await GetRepository<Place>().Create(place);
            }

            evt.PlaceIdentity = place.Id;
            await GetRepository<Event>().Create(evt);
            return evt;
        }
    }
}