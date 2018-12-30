using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services.Repository.Contracts;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Services.Repository
{
    public class PersonRepository : RepositoryBase<TreeDbContext,Person>,IPersonRepository
    {
        public PersonRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}