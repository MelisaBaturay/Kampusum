namespace Kampusum.Models
{
    public class EventRegistrationsForAdminViewModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public List<EventRegistration> Registrations { get; set; }
    }
}
