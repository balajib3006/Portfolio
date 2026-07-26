using Microsoft.EntityFrameworkCore;
using BalajiPortfolio.Models.Entity;

namespace BalajiPortfolio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<mstPersonal> MstPersonals { get; set; }
    }
}
