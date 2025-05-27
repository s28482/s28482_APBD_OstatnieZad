namespace s28482_OstatnieZadaniePunktowane.DTOs;

public class PrescriptionCreateRequestDTO
{
    public PatientCreateDTO Patient { get; set; } = null!;
    public List<PrescriptionMedicamentCreateDTO> Medicaments { get; set; } = new();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}