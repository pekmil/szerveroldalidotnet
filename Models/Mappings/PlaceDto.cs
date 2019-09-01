using System;

namespace EventApp.Models.Mappings {
    public class PlaceDto {
        public string Name { get; set; }

        public string Address { get; set; }
    }

    public class PlaceCreateDto : PlaceDto { }

    public class PlaceUpdateDto : PlaceDto { 
        public int Id { get; set; }
    }

    public class PlaceReadDto : PlaceDto { 
        public int Id { get; set; }
    }
}