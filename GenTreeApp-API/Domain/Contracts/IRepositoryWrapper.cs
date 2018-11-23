using System.Data.Entity.Core.Objects.DataClasses;

namespace GenTreeApp_API.Domain.Contracts
{
    public interface IRepositoryWrapper
    {
        ITreeRepository  TreeRepository { get; }
        IPersonRepository PersonRepository { get; }
        IUserRepository UserRepository { get; }
        IRelationRepository RelationRepository { get; }
        IDetailsRepository DetailRepository { get; }

    }
}
