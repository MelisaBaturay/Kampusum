using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace Kampusum.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}