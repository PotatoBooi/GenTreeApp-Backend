using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenTreeApp_API.Models;
using GenTreeApp_API.Repository;

namespace GenTreeApp_API.Contracts
{
    public interface ITreeRepository : IRepositoryBase<Tree>
    {
    }
}
