namespace EventApp.Models {

    public enum InvitationStatus {
        Created, Accepted, Declined
    }

    public class Invitation {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public InvitationStatus Status { get; set; }

    }
}