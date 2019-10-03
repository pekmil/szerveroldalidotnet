using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class InvitationService : IInvitationService
    {

        //private readonly EventAppDbContext _context;

        private readonly IUnitOfWork _unitOfWork;
        
        // public InvitationService(EventAppDbContext context){
        //     _context = context;
        // }

        public InvitationService(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public async Task<Invitation> CreateInvitationAsync(Invitation invitation)
        {
            // await _context.AddAsync(invitation);
            // await _context.SaveChangesAsync();
            // return invitation;

            await _unitOfWork.GetRepository<Invitation>().Create(invitation);
            await _unitOfWork.SaveChangesAsync();
            return invitation;
        }

        public IQueryable<Invitation> GetInvitations()
        {
            // var invitations = _context.Invitations.Include(i => i.Person).Include(i => i.Event).AsNoTracking();
            // return invitations;

            return _unitOfWork.GetRepository<Invitation>().GetAsQueryable(null, source => source
                        .Include(i => i.Person)
                        .Include(i => i.Event)
                    );
        }

        public async Task UpdateInvitationAsync(int personId, int eventId, InvitationStatus status)
        {
            // var invitation = await _context.Invitations.Where(i => i.PersonId == personId && i.EventId == eventId).FirstOrDefaultAsync();
            // if(invitation != null){
            //     invitation.Status = status;
            //     _context.Update(invitation);
            //     await _context.SaveChangesAsync();
            // }

            var invitation = _unitOfWork.GetRepository<Invitation>()
                    .GetAsQueryable(i => i.PersonId == personId && i.EventId == eventId).FirstOrDefault();
            if(invitation != null)
            {
                invitation.Status = status;
                await _unitOfWork.GetRepository<Invitation>().Update(invitation.Id, invitation);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}