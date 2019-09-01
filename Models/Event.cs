using System;

namespace EventApp.Models {
    public class Event {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Place Place { get; set; }
        public int PlaceId { get; set; }
    }
}