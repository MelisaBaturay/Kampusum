using System.ComponentModel.DataAnnotations;

namespace Kampusum.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        // Genel Bilgiler
        public string Title { get; set; }              // Annual Science Exhibition
        public string Description { get; set; }        // Açıklama paragrafı
        public string ImagePath { get; set; }          // Etkinlik görseli

        // Tarih ve Saat
        public DateTime Date { get; set; }             // 10/24/2023
        public TimeSpan StartTime { get; set; }        // 3:00 PM
        public TimeSpan EndTime { get; set; }          // 6:00 PM
        public string Location { get; set; }           // İTÜ / İTÜ Ayazağa Kampüsü gibi

        // Etkinlik Programı (alt tablo olarak)
        public virtual List<EventSchedule> Schedules { get; set; }

        // Organizator Bilgileri
        public string OrganizerName { get; set; }      // Prof. Michael Anderson
        public string OrganizerTitle { get; set; }     // Head of Science Department
        public string OrganizerEmail { get; set; }
        public string OrganizerPhone { get; set; }
        public string OrganizerImagePath { get; set; }
    }
}
