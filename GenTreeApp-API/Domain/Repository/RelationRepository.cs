using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Data;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.Domain.Repository
{
    public class RelationRepository : RepositoryBase<TreeDbContext,Relation>,IRelationRepository
    {
        public RelationRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}