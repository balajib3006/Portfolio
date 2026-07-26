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
        public DbSet<mstExperience> mstExperiences { get; set; }
        public DbSet<mstSkillCategory> mstSkillCategories { get; set; }
        public DbSet<mstSkill> mstSkills { get; set; }
        public DbSet<mstProject> mstProjects { get; set; }
        public DbSet<mstCertification> mstCertifications { get; set; }
        public DbSet<mstPublication> mstPublications { get; set; }
    }
}
