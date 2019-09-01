namespace EventApp.Models.Mappings {
    public class InvitationDto {
        public int PersonId { get; set; }

        public int EventId { get; set; }
    }

    public class InvitationReadDto : InvitationDto {
        public string EventName { get; set; }

        public string PersonName { get; set; }

        public InvitationStatus InvitationStatus { get; set; }
    }

    public class InvitationCreateDto : InvitationDto {}
}