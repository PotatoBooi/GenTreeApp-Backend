using GenTreeApp_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.MockObject
{
}
//{
//    public class TestObjects
//    {
//        public Relation relation1 { get; set; }
//        public Person person1 { get; set; }
//        public Event event1 { get; set; }
//        public Details detail1 { get; set; }
//        public Comment comment1 { get; set; }
//        public Tree tree1 { get; set; }

//        public TestObjects()
//        {
//            event1 = new Event
//            {
//                UUID = Guid.NewGuid().ToString(),
//                Date = DateTime.Now.ToString(),
//                Type = "Birth",
//                Description = "Urodził się"
//            };
//            person1 = new Person
//            {
//                UUID = Guid.NewGuid().ToString(),
//                RelationList = new HashSet<Relation> { relation1 },
//                Details_UUID = Guid.NewGuid().ToString()

//            };
//            comment1 = new Comment { UUID = Guid.NewGuid().ToString(), Body = "No fajny chłop" };
//            relation1 = new Relation
//            {
//                UUID = Guid.NewGuid().ToString(),
//                Child_UUID = person1.UUID,
//                Parents_UUID = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
//            };
//            detail1 = new Details
//            {
//                UUID = Guid.NewGuid().ToString(),
//                Name = "Janek",
//                Surname = "Kos",
//                CommentList = new HashSet<Comment> { comment1 },
//                Sex = "Chłop",
//                EventList = new HashSet<Event>(),
//                MediaList = new HashSet<Media>()

//            };
//            tree1 = new Tree { UUID = Guid.NewGuid().ToString(), Editable = false, Person_UUID = person1.UUID };
//        }
//    }
