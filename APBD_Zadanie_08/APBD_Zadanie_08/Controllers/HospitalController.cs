using APBD_Zadanie_08.DTO.Request;
using APBD_Zadanie_08.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APBD_Zadanie_08.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IDBHospitalService _dbService;
        public HospitalController(IDBHospitalService dBHospitalService)
        {
            _dbService = dBHospitalService;
        }

        [HttpGet("doctor")]
        public async Task<IActionResult> GetDoctor()
        {
            return await _dbService.GetDoctors();
        }

        [HttpPost("doctor")]
        public async Task<IActionResult> AddDoctor(RequestAddDoctor request)
        {
            return await _dbService.AddDoctor(request);
        }

        [HttpDelete("doctor/{idDoctor}")]
        public async Task<IActionResult> DeleteDoctor(int IdDoctor)
        {
            return await _dbService.DeleteDoctor(IdDoctor);
        }

        [HttpPut("doctor/{idDoctor}")]
        public async Task<IActionResult> ModifyDoctor(RequestModifyDoctor request)
        {
            return await _dbService.ModifyDoctor(request);
        }

        [HttpGet("prescription")]
        public async Task<IActionResult> GetPrescription(RequestDownloadPrescription request)
        {
            return await _dbService.GetPrescription(request);
        }
        
    }
}
