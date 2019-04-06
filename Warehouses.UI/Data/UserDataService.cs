using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class UserDataService : IUserDataService
    {
        public IEnumerable<User> GetAllUsers()
        {
            yield return new User { Id = 1, Username = "Abd", FirstName = "Abdalla", LastName = "Abuamsha", Email = "a@b.d", Password = "123456" };
            yield return new User { Id = 2, Username = "baraa", FirstName = "Baraa", LastName = "Salhani", Email = "b@s.n", Password = "654321" };
            yield return new User { Id = 3, Username = "mhd", FirstName = "Mohammad", LastName = "Jzmaty", Email = "m@j.z", Password = "313313" };
            yield return new User { Id = 4, Username = "aya", FirstName = "Aya", LastName = "HajAli", Email = "a@h.a", Password = "919191" };
            yield return new User { Id = 5, Username = "Ihsan", FirstName = "Ihssan", LastName = "Wardah", Email = "i@w.d", Password = "515151" };
            yield return new User { Id = 6, Username = "khaled", FirstName = "Khaled", LastName = "Wardah", Email = "k@w.d", Password = "317913" };
            
        }
      public User Login(string username, string password)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
        }
    }
}
