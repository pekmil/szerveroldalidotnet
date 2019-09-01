using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class PersonService : IPersonService
    {
        private readonly EventAppDbContext _context;

        public PersonService(EventAppDbContext context){
            _context = context;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task DeletePersonAsync(int personId)
        {
            var person = await _context.FindAsync<Person>(personId);
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPersonAsync(int personId)
        {
            var person = await _context.FindAsync<Person>(personId);
            return person;
        }

        public IQueryable<Person> GetPeople()
        {
            return _context.People.AsNoTracking();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}