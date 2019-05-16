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
        [Required]
        public string Name { get; set; }
        public decimal? Factor { get; set; }
        public long? ParentUnitId { get; set; }
        public Unit ParentUnit { get; set; }
        public bool IsVoid { get; set; }
        public string VoidReason { get; set; }
    }
}
