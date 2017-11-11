using Microsoft.EntityFrameworkCore;
using Toggles.DataAccess.DbEntities;

namespace Toggles.DataAccess
{
    public class TogglesDbContext : DbContext
    {
        public DbSet<ToggleDbEntity> Toggles { get; set; }
        public DbSet<ToggleValueDbEntity> ToggleValues { get; set; }

        public TogglesDbContext()
            : base()
        {

        }

        public TogglesDbContext(DbContextOptions<TogglesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ConfigureTogglesDbMapping(modelBuilder);
            this.ConfigureToggleValuesDbMapping(modelBuilder);
        }

        private void ConfigureTogglesDbMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToggleDbEntity>(typeBuilder =>
            {
                typeBuilder.ToTable("Toggles");
                typeBuilder.HasKey(e => e.Id);
                typeBuilder.Property(e => e.CodeName).IsRequired();
            });
        }

        private void ConfigureToggleValuesDbMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToggleValueDbEntity>(typeBuilder =>
            {
                typeBuilder.ToTable("ToggleValues");
                typeBuilder.HasKey(e => e.Id);
                typeBuilder.Property(e => e.ToggleId).IsRequired();
                typeBuilder.Property(e => e.Value).IsRequired();
                typeBuilder.Property(e => e.ApplicationCodeName).IsRequired();
                typeBuilder.HasOne(e => e.Toggle)
                    .WithMany(t => t.Values)
                    .HasForeignKey(e => e.ToggleId);
            });
        }
    }
}
