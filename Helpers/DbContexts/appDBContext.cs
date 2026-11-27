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
        public DbSet<mstSkill> mstSkills { get; set; }
        public DbSet<mstProject> mstProjects { get; set; }
        public DbSet<mstCertification> mstCertifications { get; set; }
        public DbSet<mstPublication> mstPublications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<mstPersonal>().ToTable("mstpersonal");
            modelBuilder.Entity<mstExperience>().ToTable("mstexperience");
            modelBuilder.Entity<mstSkill>().ToTable("mstskill");
            modelBuilder.Entity<mstProject>().ToTable("mstproject");
            modelBuilder.Entity<mstCertification>().ToTable("mstcertification");
            modelBuilder.Entity<mstPublication>().ToTable("mstpublication");
        }
    }
}
