using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;

namespace EventApp.Services {
    public interface IInvitationService
    {
        Task<Invitation> CreateInvitationAsync(Invitation invitation);

        Task UpdateInvitationAsync(int personId, int eventId, InvitationStatus status);

        IQueryable<Invitation> GetInvitations();
    }
}