using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;

namespace PA.ApplicationCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Etablissement> Etablissements { get; set; }
        public DbSet<Postes> Postes { get; set; }
        public DbSet<Salles> Salles { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Log> Log { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=PR3;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postes>()
                .HasOne<Salles>() // No Navigation property
                .WithMany(s => s.Postes)
                .HasForeignKey(p => p.SallesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Postes>()
                .HasOne<Status>() // No Navigation property
                .WithMany() 
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Postes>()
                .HasOne<Types>()
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Etablissement and Salles
            modelBuilder.Entity<Etablissement>()
                .HasMany(e => e.Salles)
                .WithOne()
                .HasForeignKey(s => s.EtablissementId);

            // Configure the relationship between Salles and Status
            modelBuilder.Entity<Salles>()
                .HasOne<Status>() // No Navigation property
                .WithMany()
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure the relationship between Postes and Salles
            modelBuilder.Entity<Postes>()
                .HasOne<Salles>() // No Navigation property
                .WithMany(s => s.Postes)
                .HasForeignKey(p => p.SallesId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Postes and Status
            modelBuilder.Entity<Postes>()
                .HasOne<Status>() // No Navigation property
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Postes and Types
            modelBuilder.Entity<Postes>()
                .HasOne<Types>() // No Navigation property
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure the relationship between Etablissement and Status
            modelBuilder.Entity<Etablissement>()
                .HasOne<Status>() // No Navigation property
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure the relationship between Utilisateur and Status
            modelBuilder.Entity<Utilisateur>()
                .HasOne<Status>(p => p.Status) // Navigation property
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
