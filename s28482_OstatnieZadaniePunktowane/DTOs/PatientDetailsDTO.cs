namespace s28482_OstatnieZadaniePunktowane.DTOs;

public class PatientDetailsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public List<PrescriptionDetailsDTO> Prescriptions { get; set; } = new();
    
}