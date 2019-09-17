namespace EventApp.Models
{
    public class EventStaff
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}