using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class FandomContext : DbContext
    {
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<Characters> Characters { get; set; }
        public virtual DbSet<Casts> Casts { get; set; }
        public FandomContext(DbContextOptions<FandomContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
