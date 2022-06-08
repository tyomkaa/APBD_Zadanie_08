using APBD_Zadanie_08.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace APBD_Zadanie_08.Services
{
    public static class Expansions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { IdDoctor = 1, FirstName = "Artem", LastName = "Honcharenko", Email = "asfa@gmail.com" }
                );

            modelBuilder.Entity<Patient>().HasData(
                new Patient() { IdPatient = 1, FirstName = "Artem", LastName = "Honcharenko", BirthDate = DateTime.Now}
                );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription() { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now, IdDoctor = 1, IdPatient = 1 }
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament() { IdMedicament = 1, Name = "meds", Description = "so good", Type = "high" });

            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament() { IdMedicament = 1, IdPrescription = 1, Details = "mix with water" });
        }
    }
}
