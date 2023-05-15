using Entities.Models;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IPersonService : IGenericService<Person>
    {
        Task<Person> GetOldestPerson();
    }
}
