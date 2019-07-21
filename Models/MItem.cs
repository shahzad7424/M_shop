using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mshop.Models
{
    public partial class MItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double? Quantity { get; set; }
        [Required]
        public decimal? CostPrice { get; set; }
        [Required]
        public decimal? SalePrice { get; set; }
        [Required]
        public string MainImage { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public int? ItemCategory { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
