using APBD_Zadanie_08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_Zadanie_08.Configurations
{
    public class PresConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescription");
            builder.HasKey(e => e.IdPrescription);
            builder.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
            builder.Property(e => e.Date).IsRequired().HasColumnType("datetime");
            builder.Property(e => e.DueDate).IsRequired().HasColumnType("datetime");
            builder.HasOne(e => e.Patient).WithMany(m => m.Prescriptions).HasForeignKey(m => m.IdPatient).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Doctor).WithMany(m => m.Prescriptions).HasForeignKey(m => m.IdDoctor).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
