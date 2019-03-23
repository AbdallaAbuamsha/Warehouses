using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Model
{
    public class Warehouse
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Code { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Location { get; set; }
        public int ParentId { get; set; }
    }
}
