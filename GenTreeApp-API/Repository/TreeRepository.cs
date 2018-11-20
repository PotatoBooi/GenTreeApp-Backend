using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using GenTreeApp_API.Contracts;
using GenTreeApp_API.Models;

namespace GenTreeApp_API.Repository
{
    public class TreeRepository : RepositoryBase<DbContext,Tree>, ITreeRepository
    {
        // TODO : create EF entities for DbContextClass
    }
}