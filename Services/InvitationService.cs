using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class InvitationService : IInvitationService
    {

        private readonly EventAppDbContext _context;

        public InvitationService(EventAppDbContext context){
            _context = context;
        }

        public async Task<Invitation> CreateInvitationAsync(Invitation invitation)
        {
            await _context.AddAsync(invitation);
            await _context.SaveChangesAsync();
            return invitation;
        }

        public IQueryable<Invitation> GetInvitations()
        {
            var invitations = _context.Invitations.Include(i => i.Person).Include(i => i.Event).AsNoTracking();
            return invitations;
        }

        public async Task UpdateInvitationAsync(int personId, int eventId, InvitationStatus status)
        {
            var invitation = await _context.Invitations.Where(i => i.PersonId == personId && i.EventId == eventId).FirstOrDefaultAsync();
            if(invitation != null){
                invitation.Status = status;
                _context.Update(invitation);
                await _context.SaveChangesAsync();
            }
        }
    }
}