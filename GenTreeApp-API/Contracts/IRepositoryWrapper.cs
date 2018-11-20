using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenTreeApp_API.Contracts
{
    interface IRepositoryWrapper
    {
        ITreeRepository  TreeRepository { get; }
    }
}
