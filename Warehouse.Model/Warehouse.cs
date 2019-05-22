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
        public long Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string LatinName { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Code { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Location { get; set; }
        public long? ParentWarehouseId { get; set; }
        public long? BranchId { get; set; }
        public long OrganizationID { get; set; }
        public byte ParentType { get; set; }

        public bool IsVoid { get; set; }
        public string VoidReason { get; set; }
    }
}
