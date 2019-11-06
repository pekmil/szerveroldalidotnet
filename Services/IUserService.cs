using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Models.Communication;

namespace EventApp.Services {

    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest data);

        Task InitAsync();

        IQueryable<ApplicationUser> GetUsers();
    }

}