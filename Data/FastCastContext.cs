using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FastCast.Models
{
    public class FastCastContext : DbContext
    {
        public FastCastContext (DbContextOptions<FastCastContext> options)
            : base(options)
        {
        }

        public DbSet<FastCast.Models.Session> Session { get; set; }
    }
}
