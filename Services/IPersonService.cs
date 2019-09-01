using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;

namespace EventApp.Services {
    public interface IPersonService
    {
        Task<Person> CreatePersonAsync(Person person);

        Task UpdatePersonAsync(Person person);

        Task DeletePersonAsync(int personId);

        IQueryable<Person> GetPeople();

        Task<Person> GetPersonAsync(int personId);
    }
}