using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Kampusum.Models
{
    public class EventSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Etkinlik ile ilişkilendirme
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        [ValidateNever]
        public virtual Event Event { get; set; }
    }
}
