using System;
using System.Collections.Generic;

namespace EventApp.Models {
    public class Person : AbstractEntity {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

       //public List<Person> Friends { get; set; }
       public List<Friend> Friends { get; set; }
    }
}