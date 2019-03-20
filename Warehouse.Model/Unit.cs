using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Model
{
    public class Unit
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
    }
}
