using System;

namespace APBD_Zadanie_08.DTO
{
    public class ResponseDownloadPrescription
    {
        public string Medicament { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }
}
