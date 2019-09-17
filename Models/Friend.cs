

namespace EventApp.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }        
        public int FriendPersonId { get; set; }
        public Person FriendPerson { get; set; }
    }
}