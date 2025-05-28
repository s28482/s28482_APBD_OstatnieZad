using Microsoft.EntityFrameworkCore;
using s28482_OstatnieZadaniePunktowane.Data;
using s28482_OstatnieZadaniePunktowane.DTOs;
using s28482_OstatnieZadaniePunktowane.Models;

namespace s28482_OstatnieZadaniePunktowane.Services;


public interface IDbService
{
    public Task AddPrescriptionAsync(PrescriptionCreateRequestDTO dto, int doctorId = 1);
    public Task<PatientDetailsDTO> GetPatientDetailsAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task AddPrescriptionAsync(PrescriptionCreateRequestDTO dto, int doctorId = 1)
    {
        // Recepta może obejmować maksymalnie 10 leków. W innym
        // wypadku zwracamy błąd.
        if (dto.Medicaments.Count > 10)
            throw new Exception("Recepta nie może zawierać więcej niż 10 leków.");

        // Musimy sprawdzić czy DueData>=Date
        if (dto.DueDate < dto.Date)
            throw new Exception("DueDate nie może być wcześniejszy niż Date.");

        
        
        // Sprawdź, czy pacjent istnieje
        // Jeśli pacjent przekazany w żądaniu nie istnieje, wstawiamy
        // nowego pacjenta do tabeli Pacjent.

        var patient = await data.Patients.FindAsync(dto.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = dto.Patient.IdPatient,
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                BirthDate = dto.Patient.BirthDate
            };
            data.Patients.Add(patient);
        }

        
        
        
        // Sprawdź, czy wszystkie leki istnieją
        // tu beda id leków z dto
        var medicamentIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
        // tu beda leki istneijace
        var existingMedicaments = await data.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        // Jeśli lek podany na recepcie nie istnieje, zwracamy błąd.
        var missingMedicaments = medicamentIds.Except(existingMedicaments).ToList();
        
        if (missingMedicaments.Any())
            throw new Exception($"Nie znaleziono leków o ID: {string.Join(", ", missingMedicaments)}");

        // Utwórz receptę
        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdDoctor = doctorId,
            Patient = patient
        };

        // Dodaj leki do recepty
        foreach (var m in dto.Medicaments)
        {
            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            });
        }

        data.Prescriptions.Add(prescription);
        await data.SaveChangesAsync();
    }
    
    
    // Asynchroniczna metoda, która zwraca dane szczegółowe pacjenta na podstawie jego ID
    public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int id) 
{
    var patient = await data.Patients 
        .Include(p => p.Prescriptions) // Ładujemy również przypisane do pacjenta recepty
        .ThenInclude(pr => pr.Doctor) // Dla każdej recepty pobieramy również lekarza, który ją wystawił
        .Include(p => p.Prescriptions) // Ponownie ładujemy recepty
        .ThenInclude(pr => pr.PrescriptionMedicaments) // Do każdej recepty pobieramy przypisane leki
        .ThenInclude(pm => pm.Medicament) // Dla każdego wpisu w tabeli pośredniczącej ładujemy szczegóły leku
        .FirstOrDefaultAsync(p => p.IdPatient == id); // Filtrujemy pacjenta po ID 

    if (patient == null) // Jeśli pacjent nie istnieje, rzucamy wyjątek
        throw new Exception("Nie znaleziono pacjenta");

    return new PatientDetailsDTO // Tworzymy DTO z danymi pacjenta
    {
        IdPatient = patient.IdPatient, 
        FirstName = patient.FirstName, 
        LastName = patient.LastName, 
        BirthDate = patient.BirthDate, 
        Prescriptions = patient.Prescriptions // Lista recept przypisanych do pacjenta
            // "Dane na temat recept powinny być posortowane po polu DueDate."
            .OrderBy(p => p.DueDate) // Sortujemy recepty po dacie ważności (rosnąco)
            .Select(p => new PrescriptionDetailsDTO 
            {
                IdPrescription = p.IdPrescription, 
                Date = p.Date, 
                DueDate = p.DueDate, 
                Doctor = new DoctorDTO // Tworzymy obiekt DTO dla lekarza
                {
                    IdDoctor = p.Doctor.IdDoctor, 
                    FirstName = p.Doctor.FirstName, 
                    LastName = p.Doctor.LastName, 
                    Email = p.Doctor.Email 
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDTO 
                {
                    IdMedicament = pm.Medicament.IdMedicament, 
                    Name = pm.Medicament.Name, 
                    Description = pm.Medicament.Description, 
                    Type = pm.Medicament.Type, 
                    Dose = pm.Dose, 
                    Details = pm.Details 
                }).ToList() // Konwertujemy na listę
            }).ToList() // Konwertujemy wszystkie recepty na listę
    };
}
    
}