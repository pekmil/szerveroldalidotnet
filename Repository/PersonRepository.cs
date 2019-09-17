using System;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {         
        IQueryable<Person> GetAfter1990People();
    }

    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(EventAppDbContext ctx) : base(ctx)
        {

        }
        public IQueryable<Person> GetAfter1990People()
        {
            DateTime year1990 = new DateTime(1990,1,1);
            return DbSet.AsNoTracking().Where(p => p.DateOfBirth >= year1990);
        }
    }
}