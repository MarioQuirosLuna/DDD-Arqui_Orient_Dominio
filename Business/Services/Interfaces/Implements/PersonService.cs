using Data.Data;
using Entities.Models;
using Repositories.Repositories;
using Repositories.Repositories.Implements;
using System.Threading.Tasks;

namespace Services.Services.Implements
{
    public class PersonService : GenericService<Person>, IPersonService
    {
        private readonly static PersonRepository _Repository = new PersonRepository(SISCOA_Context.Create());
        private readonly IPersonRepository usuarioRepository;
        public PersonService() : base(_Repository)
        {
            this.usuarioRepository = new PersonRepository(SISCOA_Context.Create());
        }

        public async Task<Person> GetOldestPerson()
        {
            return await usuarioRepository.GetOldestPerson();
        }
    }
}
