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
        public long Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Symbol { get; set; }
    }
}
