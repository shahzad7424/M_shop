using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mshop.Data
{
    public class MusersContext : IdentityDbContext
    {
        public MusersContext(DbContextOptions<MusersContext> options)
            : base(options)
        {
        }
    }
}
