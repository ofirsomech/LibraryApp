using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public enum UserTypes { Admin, User }

    class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypes Type { get; set; } = UserTypes.User;

    }
}
