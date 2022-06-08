namespace APBD_Zadanie_08.DTO.Request
{
    public class RequestDownloadPrescription
    {
        public int IdDoctor { get; set; }
        public int IdPatient { get; set; }
        public string Medicament { get; set; }
    }
}
