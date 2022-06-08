using APBD_Zadanie_08.Configurations;
using APBD_Zadanie_08.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace APBD_Zadanie_08.Models
{
    public class DBHospitalContext : DbContext, IDBHospitalContext
    {
        private IConfiguration _configuration;
        public DBHospitalContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DBHospitalContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=codefirst; User Id = AH; Password=<String>");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DocConfiguration());
            modelBuilder.ApplyConfiguration(new MedConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new PresConfiguration());
            modelBuilder.ApplyConfiguration(new PresMedConfiguration());

            modelBuilder.Seed();
        }


        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicamentes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptiones { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }

    public interface IDBHospitalContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicamentes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptiones { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
