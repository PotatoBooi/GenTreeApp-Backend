using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services.Repository.Contracts;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Services.Repository
{
    public class UserRepository : RepositoryBase<TreeDbContext,User>, IUserRepository
    {
        public UserRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}