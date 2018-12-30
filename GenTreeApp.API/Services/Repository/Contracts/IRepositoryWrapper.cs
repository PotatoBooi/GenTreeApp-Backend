namespace GenTreeApp.API.Services.Repository.Contracts
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
