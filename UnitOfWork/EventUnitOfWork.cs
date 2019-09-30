using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.UnitOfWork
{
    public interface IEventUnitOfWork
    {
        public object GetEventsAndPlaces();
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
    }
}