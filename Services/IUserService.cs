using System.Threading.Tasks;
using EventApp.Models.Communication;

namespace EventApp.Services {

    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest data);

        Task InitAsync();
    }

}