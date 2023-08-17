
using Microsoft.EntityFrameworkCore;

namespace TaskManagementAppDataAccess
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Tasks");
                //entity.HasKey(e => e.Id);
                //entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                //entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
                //entity.Property(e => e.IsCompleted).HasColumnName("IsCompleted").HasDefaultValue(false);
                //entity.Property(e => e.Created_at).HasColumnName("Created_at").HasColumnType("DATE");
            });

            base.OnModelCreating(modelBuilder);
        }        

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
