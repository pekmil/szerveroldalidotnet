using System.Collections.Generic;

namespace EventApp.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<EventStaff> Events { get; set; }
    }
}