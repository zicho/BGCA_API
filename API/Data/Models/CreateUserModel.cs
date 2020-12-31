using API.Data.Static;

namespace API.Data.Models
{
    public class CreateUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = UserRoles.User;
    }
}