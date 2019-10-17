using System;

namespace EventApp.Models.Mappings {
    public class EventDto {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }

    public class EventCreateDto : EventDto { 
        public int PlaceId { get; set; }

        public bool AdultsOnly { get; set; }
    }

    public class EventUpdateDto : EventDto { 
        public int Id { get; set; }

        public int PlaceId { get; set; }
    }

    public class EventReadDto : EventDto { 
        public int Id { get; set; }

        public string PlaceName { get; set; }

        public string PlaceAddress { get; set; }

        public bool AdultsOnly { get; set; }
    }
}