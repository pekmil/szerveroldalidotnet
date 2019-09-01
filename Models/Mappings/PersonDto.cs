using System;

namespace EventApp.Models.Mappings {
    public class PersonDto {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }

    public class PersonCreateDto : PersonDto { }

    public class PersonUpdateDto : PersonDto { 
        public int Id { get; set; }
    }

    public class PersonReadDto : PersonDto { 
        public int Id { get; set; }
    }
}