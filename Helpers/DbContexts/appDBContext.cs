using Microsoft.EntityFrameworkCore;
using BalajiPortfolio.Models.Entity;

namespace BalajiPortfolio.Helpers.DbContexts
{
    public class appDBContext : DbContext
    {
        public appDBContext(DbContextOptions<appDBContext> options)
            : base(options)
        {
        }

        public DbSet<mstPersonal> mstPersonals { get; set; }
    }
}
