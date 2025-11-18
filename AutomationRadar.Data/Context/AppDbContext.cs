using Microsoft.EntityFrameworkCore;
using AutomationRadar.Model.Entities;

namespace AutomationRadar.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Occupation> Occupations => Set<Occupation>();
        public DbSet<AutomationRisk> AutomationRisks => Set<AutomationRisk>();
        public DbSet<CareerTransition> CareerTransitions => Set<CareerTransition>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupation>(e =>
            {
                e.ToTable("OCCUPATIONS");

                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);

                e.Property(x => x.Sector)
                    .HasMaxLength(200);

                e.Property(x => x.IsActive)
                    .HasColumnType("NUMBER(1)");
                
                e.Property(x => x.CreatedAt);
                e.Property(x => x.UpdatedAt);
            });

            modelBuilder.Entity<AutomationRisk>(e =>
            {
                e.ToTable("AUTOMATION_RISKS");

                e.HasKey(x => x.Id);

                e.Property(x => x.RiskLevel)
                    .IsRequired();

                e.Property(x => x.HorizonYears);

                e.Property(x => x.Justification)
                    .HasMaxLength(2000);

                e.Property(x => x.Source)
                    .HasMaxLength(500);

                e.Property(x => x.CreatedAt);
                e.Property(x => x.UpdatedAt);

                e.HasOne(x => x.Occupation)
                    .WithMany(o => o.AutomationRisks)
                    .HasForeignKey(x => x.OccupationId)
                    .HasConstraintName("FK_AUTOMATIONRISK_OCCUPATION");
            });

            modelBuilder.Entity<CareerTransition>(e =>
            {
                e.ToTable("CAREER_TRANSITIONS");

                e.HasKey(x => x.Id);

                e.Property(x => x.RecommendedActions)
                    .HasMaxLength(2000);

                e.Property(x => x.Priority)
                    .HasDefaultValue(1);

                e.Property(x => x.CreatedAt);
                e.Property(x => x.UpdatedAt);

                e.HasOne(x => x.FromOccupation)
                    .WithMany(o => o.SourceTransitions)
                    .HasForeignKey(x => x.FromOccupationId)
                    .HasConstraintName("FK_TRANSITION_FROM");

                e.HasOne(x => x.ToOccupation)
                    .WithMany(o => o.TargetTransitions)
                    .HasForeignKey(x => x.ToOccupationId)
                    .HasConstraintName("FK_TRANSITION_TO");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
