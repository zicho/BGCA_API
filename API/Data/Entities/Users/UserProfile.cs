using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities.Users
{
    public class UserProfile : BaseEntity
    {
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Desc { get; set; }
        public string Country { get; set; }
        public string State{ get; set; }
        public string City { get; set; }

        //public GeoCoordinate ExactPosition { get; set; }

        //public TimeZoneInfo TimeZone { get; set; }
    }
}
