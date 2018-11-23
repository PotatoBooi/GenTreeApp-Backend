using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenTreeApp_API.Domain.Models;
using GenTreeApp_API.Domain.Repository;

namespace GenTreeApp_API.Domain.Contracts
{
    public interface IUserRepository:IRepositoryBase<User>
    {
    }
}
