using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Helper
{
    class UserSingleton
    {
        public User User { set; get; }
        private UserSingleton()
        {
            // Prevent outside instantiation
        }

        private static readonly UserSingleton _singleton = new UserSingleton();

        public static UserSingleton GetUser()
        {
            return _singleton;
        }
        public static long GetUserId()
        {
            return _singleton.User.Id;
        }
    }
}
