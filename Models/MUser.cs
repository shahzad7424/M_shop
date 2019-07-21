using System;
using System.Collections.Generic;

namespace Mshop.Models
{
    public partial class MUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ProfilePicture { get; set; }
      
        public string Email { get; set; }

    }
}
