using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mshop.Models
{
    public partial class MCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
