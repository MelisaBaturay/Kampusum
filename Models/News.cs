using System.ComponentModel.DataAnnotations;

namespace Kampusum.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
