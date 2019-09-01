using System.Collections.Generic;

namespace EventApp.Models {
    public class Place {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public IList<Event> Events { get; set; }
    }
}