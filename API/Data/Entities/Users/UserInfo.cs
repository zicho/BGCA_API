using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Entities.Users
{
    public class UserInfo : BaseEntity
    {
        [ForeignKey("User")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        //public GeoCoordinate ExactPosition { get; set; }
        // public TimeZoneInfo TimeZone { get; set; }
    }
}