namespace s28482_OstatnieZadaniePunktowane.DTOs;

public class PrescriptionMedicamentCreateDTO
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; } = null!; 
}