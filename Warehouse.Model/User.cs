using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Model
{
    public class User
    {
        public long Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Username { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Phone]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
