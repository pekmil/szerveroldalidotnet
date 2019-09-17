using System.Collections.Generic;

namespace EventApp.Models {
    public class Place : AbstractEntity {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<Event> Events { get; set; }
    }
}