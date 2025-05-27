namespace s28482_OstatnieZadaniePunktowane.Tests;
using Xunit; // Framework testowy xUnit
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // EF Core
using s28482_OstatnieZadaniePunktowane.Models;
using s28482_OstatnieZadaniePunktowane.DTOs;
using s28482_OstatnieZadaniePunktowane.Services;
using s28482_OstatnieZadaniePunktowane.Data;

public class DbServiceTests
{
        // Tworzy kontekst EF Core oparty na pamięci (do testów jednostkowych)
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unikalna baza na potrzeby każdego testu
                .Options;

            return new AppDbContext(options); // zwraca kontekst z tą bazą
        }

        // Zwraca poprawny obiekt DTO do użycia w testach (można podać liczbę leków)
        private PrescriptionCreateRequestDTO GetValidDTO(int medicamentCount = 1)
        {
            return new PrescriptionCreateRequestDTO
            {
                Patient = new PatientCreateDTO
                {
                    IdPatient = 1, // identyfikator pacjenta (może już istnieć lub nie)
                    FirstName = "Jan", // dane pacjenta
                    LastName = "Nowak",
                    BirthDate = new DateTime(1990, 1, 1)
                },
                // Tworzymy listę leków zgodnie z `medicamentCount`
                Medicaments = Enumerable.Range(1, medicamentCount).Select(i => new PrescriptionMedicamentCreateDTO
                {
                    IdMedicament = i, // ID leku
                    Dose = 1, // dawka
                    Description = "Test" // opis
                }).ToList(),
                Date = new DateTime(2024, 1, 1), // data wystawienia
                DueDate = new DateTime(2024, 1, 5) // data realizacji
            };
        }

        //  Test sprawdza, czy recepta zostanie poprawnie dodana,
        // jeśli pacjent i leki istnieją, a dane są zgodne z wymaganiami.
        [Fact]
        public async Task AddPrescription_Success_WhenDataIsValid()
        {
            var db = GetInMemoryDbContext(); // tworzymy bazę testową

            // dodajemy pacjenta do kontekstu
            db.Patients.Add(new Patient
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Nowak",
                BirthDate = DateTime.Now
            });

            // dodajemy istniejący lek do kontekstu
            db.Medicaments.Add(new Medicament
            {
                IdMedicament = 1,
                Name = "Lek",
                Description = "Opis",
                Type = "typ"
            });

            db.SaveChanges(); // zapisujemy zmiany do bazy

            var service = new DbService(db); // tworzymy instancję serwisu
            var dto = GetValidDTO(); // pobieramy poprawne dane wejściowe

            await service.AddPrescriptionAsync(dto); // wywołujemy metodę

            // sprawdzamy, czy jedna recepta została dodana
            Assert.Equal(1, db.Prescriptions.Count());

            // sprawdzamy, czy jeden lek został powiązany z receptą
            Assert.Equal(1, db.PrescriptionMedicaments.Count());
        }

        // Test sprawdza, czy pacjent zostanie automatycznie dodany do bazy,
        // jeżeli nie istniał wcześniej.
        [Fact]
        public async Task AddPrescription_CreatesNewPatient_IfNotExists()
        {
            var db = GetInMemoryDbContext(); // baza testowa

            // dodajemy tylko lek, bez pacjenta
            db.Medicaments.Add(new Medicament
            {
                IdMedicament = 1,
                Name = "Lek",
                Description = "Opis",
                Type = "typ"
            });

            db.SaveChanges(); // zapisujemy lek

            var service = new DbService(db); // serwis
            var dto = GetValidDTO(); // DTO z nowym pacjentem

            await service.AddPrescriptionAsync(dto); // wykonujemy akcję

            Assert.Equal(1, db.Patients.Count()); // pacjent powinien zostać dodany
        }

        //  Test sprawdza, czy zostanie rzucony wyjątek,
        // gdy na recepcie znajduje się więcej niż 10 leków.
        [Fact]
        public async Task AddPrescription_Throws_WhenMoreThan10Medicaments()
        {
            var db = GetInMemoryDbContext();

            // dodajemy 11 leków do bazy (żeby wszystkie były "istniejące")
            for (int i = 1; i <= 11; i++)
            {
                db.Medicaments.Add(new Medicament
                {
                    IdMedicament = i,
                    Name = $"Lek{i}",
                    Description = "Opis",
                    Type = "typ"
                });
            }

            db.SaveChanges();

            var service = new DbService(db);
            var dto = GetValidDTO(11); // przekazujemy 11 leków

            // oczekujemy wyjątku z konkretnym komunikatem
            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddPrescriptionAsync(dto));
            Assert.Equal("Recepta nie może zawierać więcej niż 10 leków.", ex.Message);
        }

        // Test sprawdza, czy zostanie rzucony wyjątek,
        // gdy data realizacji (DueDate) jest wcześniejsza niż data wystawienia (Date).
        [Fact]
        public async Task AddPrescription_Throws_WhenDueDateIsBeforeDate()
        {
            var db = GetInMemoryDbContext();

            // dodajemy lek
            db.Medicaments.Add(new Medicament
            {
                IdMedicament = 1,
                Name = "Lek",
                Description = "Opis",
                Type = "typ"
            });

            db.SaveChanges();

            var service = new DbService(db);
            var dto = GetValidDTO();
            dto.DueDate = dto.Date.AddDays(-1); // ustawiamy niepoprawną datę

            // oczekujemy wyjątku z konkretną wiadomością
            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddPrescriptionAsync(dto));
            Assert.Equal("DueDate nie może być wcześniejszy niż Date.", ex.Message);
        }

        //  Test sprawdza, czy zostanie rzucony wyjątek,
        // jeżeli chociaż jeden z podanych leków nie istnieje w bazie danych.
        [Fact]
        public async Task AddPrescription_Throws_WhenMedicamentDoesNotExist()
        {
            var db = GetInMemoryDbContext(); // tworzymy kontekst, ale bez dodawania leków

            var service = new DbService(db);
            var dto = GetValidDTO(); // DTO zawiera lek z ID = 1

            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddPrescriptionAsync(dto));
            Assert.Contains("Nie znaleziono leków", ex.Message); // oczekujemy odpowiedniego komunikatu
        }
    }