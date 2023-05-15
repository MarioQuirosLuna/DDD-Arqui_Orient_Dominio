using Data.Data;
using Entities.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories.Implements
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly SISCOA_Context siscoa_context;
        public PersonRepository(SISCOA_Context siscoa_contex) : base(siscoa_contex)
        {
            this.siscoa_context = siscoa_contex;
        }

        public async Task<Person> GetOldestPerson()
        {
            var oldestPerson = await siscoa_context.Persons.OrderByDescending(p => p.Age).FirstOrDefaultAsync();
            return oldestPerson;
        }

    }
}
