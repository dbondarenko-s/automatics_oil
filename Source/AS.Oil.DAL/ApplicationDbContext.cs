using AS.Oil.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AS.Oil.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Storage> PaymentReceiver { get; set; }

        public virtual DbSet<Category> Category { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Entities.Storage>()
                        .HasOne(b => b.Category)
                        .WithMany(a => a.Storages)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelbuilder);
        }
    }
}
