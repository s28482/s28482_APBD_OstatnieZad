using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s28482_OstatnieZadaniePunktowane.Models;

[Table(nameof(Prescription))]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}