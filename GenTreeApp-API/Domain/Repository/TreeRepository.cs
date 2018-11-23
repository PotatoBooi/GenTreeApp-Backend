using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Data;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.Domain.Repository
{
    public class TreeRepository : RepositoryBase<TreeDbContext,Tree>, ITreeRepository
    {
        // TODO : create EF entities for DbContextClass
    }
}