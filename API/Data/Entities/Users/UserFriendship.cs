using API.Core.Enums;
using API.Data.Entities;
using API.Data.Entities.Users;

namespace API.Data.Entities.Users
{
    public class UserFriendship : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int User2Id { get; set; }
        public User User2 { get; set; }
        public RelationshipStatus Status { get; set; } = RelationshipStatus.Pending;
    }
}