namespace EventApp.Models
{
    public class EventStaff : AbstractEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}