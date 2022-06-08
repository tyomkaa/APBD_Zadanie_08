using APBD_Zadanie_08.DTO;
using APBD_Zadanie_08.DTO.Request;
using APBD_Zadanie_08.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Zadanie_08.Services
{
    public class DBHospital : IDBHospitalService
    {
        private IDBHospitalContext _context;

        public DBHospital(IDBHospitalContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> AddDoctor(RequestAddDoctor request)
        {
            var doctor = new Doctor()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return new OkObjectResult($"Doctor {request.FirstName} {request.LastName} added to database");
        }

        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            if(!await CheckDoctor(idDoctor))
            {
                return new BadRequestResult();
            }
            _context.Doctors.Remove(await _context.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == idDoctor));
            await _context.SaveChangesAsync();
            return new OkObjectResult($"Doctor {idDoctor} has been deleted from database");
        }

        public async Task<IActionResult> GetDoctors()
        {
            return new OkObjectResult(await _context.Doctors.ToListAsync());
        }

        public async Task<IActionResult> GetPrescription(RequestDownloadPrescription request)
        {
            if(!await CheckDoctor(request.IdDoctor))
            {
                return new BadRequestResult();
            }
            if (!await CheckPatient(request.IdPatient))
            {
                return new BadRequestResult();
            }
            if (!await CheckMedicament(request.Medicament))
            {
                return new BadRequestResult();
            }

            var prescription = await _context.Prescriptiones.Where(p => p.IdDoctor == request.IdDoctor && p.IdPatient == request.IdPatient).SingleOrDefaultAsync();

            var medicament = await _context.Medicamentes.Where(m => m.Name == request.Medicament).SingleOrDefaultAsync();

            var response = await _context.Prescriptiones.Where(p => p.IdDoctor == request.IdDoctor && p.IdPatient == request.IdPatient).Select(p => new ResponseDownloadPrescription
            {
                Medicament = medicament.Name,
                Date = prescription.Date,
                DueDate = prescription.DueDate
            }).ToListAsync();

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> ModifyDoctor(RequestModifyDoctor request)
        {
            if(!await CheckDoctor(request.IdDoctor))
            {
                return new BadRequestResult();
            }
            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == request.IdDoctor);
            doctor.FirstName = request.FirstName;
            doctor.LastName = request.LastName;
            doctor.Email = request.Email;
            await _context.SaveChangesAsync();
            return new OkObjectResult($"Doctor {request.IdDoctor} has been modified");
        }

        private async Task<bool> CheckDoctor(int id)
        {
            return await _context.Doctors.AnyAsync(p => p.IdDoctor == id);
        }
        private async Task<bool> CheckPatient(int id)
        {
            return await _context.Patients .AnyAsync(p => p.IdPatient == id);
        }
        private async Task<bool> CheckMedicament(string name)
        {
            return await _context.Medicamentes.AnyAsync(p => p.Name == name);
        }
    }

    public interface IDBHospitalService
    {
        public Task<IActionResult> GetDoctors();
        public Task<IActionResult> AddDoctor(RequestAddDoctor request);
        public Task<IActionResult> DeleteDoctor(int idDoctor);
        public Task<IActionResult> ModifyDoctor(RequestModifyDoctor request);
        public Task<IActionResult> GetPrescription(RequestDownloadPrescription request);
    }
}
