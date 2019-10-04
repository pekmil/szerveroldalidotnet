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
            return evt;
        }

        public async Task DeleteEventAsync(int evtId)
        {
            // var evt = await _context.FindAsync<Event>(evtId);
            // _context.Remove(evt);
            // await _context.SaveChangesAsync();
            
            await _unitOfWork.GetRepository<Event>().Delete(evtId);
        }

        public async Task<Event> GetEventAsync(int evtId)
        {
            // var evt = await _context.Events.Where(e => e.Id == evtId).Include(e => e.Place).FirstOrDefaultAsync();
            // return evt;

            var evt = _unitOfWork.GetRepository<Event>().GetByIdWithInclude(evtId, e => e.Include(e => e.Place));
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
        }

        public object GetEventsAndPlaces()
        {
            return _eventUnitOfWork.GetEventsAndPlaces();
        }

        public async Task<Event> CreateEventWithPlace(Event evt, Place place)
        {
            if(evt == null || place == null)
            {
                return null;
            }

            var obj = await _eventUnitOfWork.CreateEventWithPlace(evt, place);

            var people = _unitOfWork.GetRepository<Person>().GetAll();

            foreach(var person in people)
            {
                Invitation invitation = new Invitation
                {
                    EventId = obj.Id,
                    PersonId = person.Id,
                    Status = InvitationStatus.Created
                };

                await _unitOfWork.GetRepository<Invitation>().Create(invitation, false);
            }

            await _unitOfWork.SaveChangesAsync();

            return obj;
        }
    }
}