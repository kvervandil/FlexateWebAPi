using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain
{
    public static class Entity
    {
        public static IList<Person> People { get; private set; } 

        public static IList<Person> InitializePeople()
        {
            People = new List<Person>();

            People.Add( new Person() { Id = 1, Name = "Piotr" });
            People.Add( new Person() { Id = 2, Name = "Marcin" });
            People.Add( new Person() { Id = 3, Name = "Johny" });
            People.Add( new Person() { Id = 4, Name = "Mariusz" });
            People.Add( new Person() { Id = 5, Name = "Sandra" });
            People.Add( new Person() { Id = 6, Name = "Iza" });
            People.Add( new Person() { Id = 7, Name = "Bartek" });
            People.Add( new Person() { Id = 8, Name = "Karola" });
            People.Add( new Person() { Id = 9, Name = "Michał" });
            People.Add( new Person() { Id = 10, Name = "Kuba" });
            People.Add( new Person() { Id = 11, Name = "James" });
            People.Add( new Person() { Id = 12, Name = "Ricardo" });
            People.Add( new Person() { Id = 13, Name = "Diego" });
            People.Add( new Person() { Id = 14, Name = "Enrique" });

            return People;
        }
    }
}
