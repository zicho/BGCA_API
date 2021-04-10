﻿namespace API.Data.Entities.Users
{
    public class UserInfo : BaseEntity
    {
        public string RealName { get; set; }
        public string Desc { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        //public GeoCoordinate ExactPosition { get; set; }
        // public TimeZoneInfo TimeZone { get; set; }
    }
}