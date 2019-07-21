using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mshop.Models
{
    public partial class MUser
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ProfilePicture { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
