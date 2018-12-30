using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services.Repository.Contracts;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Services.Repository
{
    public class RelationRepository : RepositoryBase<TreeDbContext,Relation>,IRelationRepository
    {
        public RelationRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}