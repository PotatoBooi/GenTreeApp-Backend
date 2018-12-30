using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services.Repository.Contracts;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Services.Repository
{
    public class DetailsRepository : RepositoryBase<TreeDbContext,Details>,IDetailsRepository
    {
        public DetailsRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}