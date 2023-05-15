using Data.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
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
    }
}
