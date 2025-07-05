using Microsoft.EntityFrameworkCore;
using system_petshop.Models;

namespace system_petshop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Breed> Breed { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<ConsultationExam> ConsultationExam { get; set; }
        public DbSet<ConsultationMedicine> ConsultationMedicine { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Veterinarian> Veterinarian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Herança TPH (Table Per Hierarchy) para User
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Admin>("Admin")
                .HasValue<Veterinarian>("Veterinarian");

            // Exemplo de chave estrangeira explícita (caso necessário)
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Animal)
                .HasForeignKey(a => a.ClientId);

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    UserId = 1,
                    Name = "Administrador",
                    Email = "admin@admin.com",
                    PassHash = "admin@123"
                }
            );
        }
    }
}