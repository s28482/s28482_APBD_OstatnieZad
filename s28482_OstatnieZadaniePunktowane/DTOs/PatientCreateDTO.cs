namespace s28482_OstatnieZadaniePunktowane.DTOs;

public class PatientCreateDTO
{
    public int IdPatient { get; set; } // Umożliwia wskazanie istniejącego
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}