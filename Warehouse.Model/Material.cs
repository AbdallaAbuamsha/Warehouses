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
        //[MaxLength(50)]
        //[MinLength(3)]
        //[Required]
        //public string Name { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string LatinName { get; set; }
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
        public bool Serializable { get; set; }
        [Required]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$")]
        [Range(0, 99999999.99)]
        public decimal? MaximumSaleAmount { get; set; }
        [Required]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$")]
        [Range(0, 99999999.99)]
        public decimal? MinimumSaleAmount { get; set; }
        [Required]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$")]
        [Range(0, 99999999.99)]
        public decimal? DazonElementsCount { get; set; }
        [Required]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$")]  
        [Range(0, 99999999.99)]
        public decimal? FreeReferencesAmount { get; set; }

        public List<Unit> units;
        public long? ParentId { get; set; }
        public long OrganizationId { get; set; }
        public string VoidReason { get; set; }
        public bool IsVoid { get; set; }

        public Organization SelectedOrganization { get; set; }
    }
}
