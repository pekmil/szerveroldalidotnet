using System;
using System.Collections.Generic;

namespace EventApp.Models {
    public class Event : AbstractEntity {
        public string Name { set; get; }

        public string Description { set; get; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Place Place { get; set; }
        public int PlaceIdentity { get; set; }

        public List<EventStaff> Staff { get; set; }
    }
}