using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class EventService : IEventService
    {
        //private readonly EventAppDbContext _context;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventUnitOfWork _eventUnitOfWork;

        // public EventService(EventAppDbContext context){
        //     _context = context;
        // }

        public EventService(IUnitOfWork unitOfWork, IEventUnitOfWork eventUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventUnitOfWork = eventUnitOfWork;
        }

        public async Task<Event> CreateEventAsync(Event evt)
        {
            // await _context.AddAsync(evt);
            // await _context.SaveChangesAsync();
            // return evt;

            await _unitOfWork.GetRepository<Event>().Create(evt);
            await _unitOfWork.SaveChangesAsync();
            return evt;
        }

        public async Task DeleteEventAsync(int evtId)
        {
            // var evt = await _context.FindAsync<Event>(evtId);
            // _context.Remove(evt);
            // await _context.SaveChangesAsync();
            
            await _unitOfWork.GetRepository<Event>().Delete(evtId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Event> GetEventAsync(int evtId)
        {
            // var evt = await _context.Events.Where(e => e.Id == evtId).Include(e => e.Place).FirstOrDefaultAsync();
            // return evt;

            var evt = _unitOfWork.GetRepository<Event>().GetByIdWithInclude(evtId, src => src.Include(e => e.Place));
            return evt;
        }

        public IQueryable<Event> GetEvents()
        {
            //return _context.Events.Include(evt => evt.Place).AsNoTracking();

             return _unitOfWork.GetRepository<Event>().GetAll();
        }

        public async Task UpdateEventAsync(Event evt)
        {
            // _context.Update(evt);
            // await _context.SaveChangesAsync();

            await _unitOfWork.GetRepository<Event>().Update(evt.Id, evt);
            await _unitOfWork.SaveChangesAsync();
        }

        public object GetEventsAndPlaces()
        {
            return _eventUnitOfWork.GetEventsAndPlaces();
        }
    }
}