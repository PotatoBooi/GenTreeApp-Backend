using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.MockObject
{
    public class FakeTreeRepository : ITreeRepository
    {
        private List<Tree> list;
        private static Details details1 = new Details
        {
            UUID = Guid.NewGuid(),
            Name = "Adam",
            Surname = "Kowalski",
            Sex = "M",
            CommentList = new HashSet<Comment>(),
            EventList = new HashSet<Event>()
        };
        private static Details details2 = new Details
        {
            UUID = Guid.NewGuid(),
            Name = "Grażyna",
            Surname = "Kowalski",
            Sex = "K",
            CommentList = new HashSet<Comment>(),
            EventList = new HashSet<Event>()
        };

        private static Person person2 = new Person
        {
            UUID = Guid.NewGuid(),
            Details = details2,
            RelationList = new List<Relation> {rel}

        };

       
        private static Person person1 = new Person {UUID = Guid.NewGuid(), Details = details1,};
        private static Relation rel = new Relation
        {
            UUID = Guid.NewGuid(),
            Parents = new List<Person> { person1, person2 }
        };
        public FakeTreeRepository()
        {
            
            list = new List<Tree> {new Tree{UUID = Guid.NewGuid(), Editable = false, Relations = new List<Relation>{rel} }};
        }
        public Task<IEnumerable<Tree>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Tree>>(list);
        }

        public Task<IEnumerable<Tree>> GetBy(Expression<Func<Tree, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(Tree entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Tree entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tree entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}