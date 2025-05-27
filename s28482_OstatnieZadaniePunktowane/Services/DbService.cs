using Microsoft.EntityFrameworkCore;
using s28482_OstatnieZadaniePunktowane.Data;
using s28482_OstatnieZadaniePunktowane.DTOs;
using s28482_OstatnieZadaniePunktowane.Models;

namespace s28482_OstatnieZadaniePunktowane.Services;


public interface IDbService
{
    public Task AddPrescriptionAsync(PrescriptionCreateRequestDTO dto, int doctorId = 1);
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
    
}