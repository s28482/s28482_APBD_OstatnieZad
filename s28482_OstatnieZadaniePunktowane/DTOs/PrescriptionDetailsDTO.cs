namespace s28482_OstatnieZadaniePunktowane.DTOs;

public class PrescriptionDetailsDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDTO Doctor { get; set; } = null!;
    public List<MedicamentDTO> Medicaments { get; set; } = new();
}