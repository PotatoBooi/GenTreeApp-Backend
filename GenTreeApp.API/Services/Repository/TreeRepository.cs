using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services.Repository.Contracts;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Services.Repository
{
    public class TreeRepository : RepositoryBase<TreeDbContext,Tree>, ITreeRepository
    {
        public TreeRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}