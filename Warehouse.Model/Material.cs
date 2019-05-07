using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Model
{
    public class Material
    {
        public long Id { get; set; }
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
        public string Barcode { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Serial { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public float MaximumSaleAmount { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public float MinimumSaleAmount { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public float DazonElementsCount { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public float FreeReferencesAmount { get; set; }

        public List<Unit> units;
        public long ParentId { get; set; }
        public Material Parent;
    }
}
