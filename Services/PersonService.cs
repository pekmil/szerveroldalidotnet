using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Repository;
using EventApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class PersonService : IPersonService
    {
        //private readonly EventAppDbContext _context;
        private readonly IPersonRepository _personRepository;

        private readonly IUnitOfWork _unitOfWork;

        // public PersonService(EventAppDbContext context, IPersonRepository personRepository){
        //     _context = context;
        //     _personRepository = personRepository;
        // }

        public PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository){
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            // await _context.AddAsync(person);
            // await _context.SaveChangesAsync();
            // return person;

            await _unitOfWork.GetRepository<Person>().Create(person);
            await _unitOfWork.SaveChangesAsync();
            return person;
        }

        public async Task DeletePersonAsync(int personId)
        {
            // var person = await _context.FindAsync<Person>(personId);
            // _context.Remove(person);
            // await _context.SaveChangesAsync();

            await _unitOfWork.GetRepository<Person>().Delete(personId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Person> GetPersonAsync(int personId)
        {
            // var person = await _context.FindAsync<Person>(personId);
            // return person;

            return await _unitOfWork.GetRepository<Person>().GetById(personId);
        }

        public IQueryable<Person> GetPeople()
        {
            // return _context.People.AsNoTracking();

            return _unitOfWork.GetRepository<Person>().GetAll();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            // _context.Update(person);
            // await _context.SaveChangesAsync();

            await _unitOfWork.GetRepository<Person>().Update(person.Id, person);
            await _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<Person> GetAfter1990()
        {
            return _personRepository.GetAfter1990People();
        }
    }
}