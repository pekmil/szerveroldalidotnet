using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class EventService : IEventService
    {
        private readonly EventAppDbContext _context;

        public EventService(EventAppDbContext context){
            _context = context;
        }

        public async Task<Event> CreateEventAsync(Event evt)
        {
            await _context.AddAsync(evt);
            await _context.SaveChangesAsync();
            return evt;
        }

        public async Task DeleteEventAsync(int evtId)
        {
            var evt = await _context.FindAsync<Event>(evtId);
            _context.Remove(evt);
            await _context.SaveChangesAsync();
        }

        public async Task<Event> GetEventAsync(int evtId)
        {
            var evt = await _context.Events.Where(e => e.Id == evtId).Include(e => e.Place).FirstOrDefaultAsync();
            return evt;
        }

        public IQueryable<Event> GetEvents()
        {
            return _context.Events.Include(evt => evt.Place).AsNoTracking();
        }

        public async Task UpdateEventAsync(Event evt)
        {
            _context.Update(evt);
            await _context.SaveChangesAsync();
        }
    }
}