using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Models.User
{
    public class ViewUserModel
    {
        public string Username { get; set; }
        public UserInfoModel Info { get; set; }
    }
}
