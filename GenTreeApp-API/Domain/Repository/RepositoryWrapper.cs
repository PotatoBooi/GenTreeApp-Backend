using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenTreeApp_API.Domain.Contracts;

namespace GenTreeApp_API.Domain.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ITreeRepository _treeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRelationRepository _relationRepository;
        private readonly IDetailsRepository _detailRepository;

        public ITreeRepository TreeRepository => _treeRepository;

        public IPersonRepository PersonRepository => _personRepository;

        public IUserRepository UserRepository => _userRepository;

        public IRelationRepository RelationRepository => _relationRepository;

        public IDetailsRepository DetailRepository => _detailRepository;

        public RepositoryWrapper(ITreeRepository treeRepository, IPersonRepository personRepository,
            IUserRepository userRepository, IRelationRepository relationRepository, IDetailsRepository detailRepository)
        {
            _treeRepository = treeRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
            _relationRepository = relationRepository;
            _detailRepository = detailRepository;
        }
    }
}