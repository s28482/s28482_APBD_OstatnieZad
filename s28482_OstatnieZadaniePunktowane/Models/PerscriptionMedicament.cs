using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace s28482_OstatnieZadaniePunktowane.Models;

[Table(nameof(PrescriptionMedicament))]
[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [Column(nameof(IdMedicament))]
    public int IdMedicament { get; set; }
    [Column(nameof(IdPrescription))]
    public int IdPrescription { get; set; }
    [Column(nameof(Dose))]

    public int? Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;
    
    
    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
}