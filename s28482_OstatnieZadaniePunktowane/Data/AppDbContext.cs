using Microsoft.EntityFrameworkCore;
using s28482_OstatnieZadaniePunktowane.Models;

namespace s28482_OstatnieZadaniePunktowane.Data;

public class AppDbContext : DbContext
{
    //Zarejestrowanie modelu w kontekscie bazy danych
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }





    // Klasa musi implementować konstruktor który umożliwia przekazywnie Opcji
    // 
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var patients = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 5, 27)
            },
            new()
            {
                IdPatient = 2,
                FirstName = "Anna",
                LastName = "Kowalska",
                BirthDate = new DateTime(1985, 3, 14)
            },
            new()
            {
                IdPatient = 3,
                FirstName = "Piotr",
                LastName = "Nowak",
                BirthDate = new DateTime(1978, 11, 2)
            },
            new()
            {
                IdPatient = 4,
                FirstName = "Maria",
                LastName = "Wiśniewska",
                BirthDate = new DateTime(1995, 7, 9)
            },
            new()
            {
                IdPatient = 5,
                FirstName = "Tomasz",
                LastName = "Lewandowski",
                BirthDate = new DateTime(2000, 1, 1)
            },
            new()
            {
                IdPatient = 6,
                FirstName = "Ewa",
                LastName = "Zielińska",
                BirthDate = new DateTime(1992, 10, 18)
            }
        };
        var doctors = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "Adam",
                LastName = "Adamski",
                Email = "adam@gmail.com"
            },
            new()
            {
                IdDoctor = 2,
                FirstName = "Barbara",
                LastName = "Borkowska",
                Email = "barbara.borkowska@medmail.com"
            },
            new()
            {
                IdDoctor = 3,
                FirstName = "Cezary",
                LastName = "Czarnecki",
                Email = "cezary.cz@clinic.org"
            },
            new()
            {
                IdDoctor = 4,
                FirstName = "Dorota",
                LastName = "Dąbrowska",
                Email = "dorota.dabrowska@healthmail.pl"
            },
            new()
            {
                IdDoctor = 5,
                FirstName = "Edward",
                LastName = "Edelman",
                Email = "edward.edelman@hospital.net"
            },
            new()
            {
                IdDoctor = 6,
                FirstName = "Franciszek",
                LastName = "Fijałkowski",
                Email = "franciszek.f@medcenter.com"
            }
        };
        var medicaments = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Lek przeciwbólowy i przeciwgorączkowy",
                Type = "Tabletka"
            },
            new()
            {
                IdMedicament = 2,
                Name = "Amoxicillin",
                Description = "Antybiotyk z grupy penicylin",
                Type = "Kapsułka"
            },
            new()
            {
                IdMedicament = 3,
                Name = "Ibuprofen",
                Description = "Lek przeciwzapalny i przeciwbólowy",
                Type = "Tabletka"
            },
            new()
            {
                IdMedicament = 4,
                Name = "Salbutamol",
                Description = "Lek rozszerzający oskrzela",
                Type = "Aerozol"
            },
            new()
            {
                IdMedicament = 5,
                Name = "Metformina",
                Description = "Lek stosowany w leczeniu cukrzycy typu 2",
                Type = "Tabletka"
            }
        };
        var prescriptions = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                Date = new DateTime(2024, 12, 1),
                DueDate = new DateTime(2025, 1, 15), // +45 dni
                IdPatient = 1,
                IdDoctor = 2
            },
            new()
            {
                IdPrescription = 2,
                Date = new DateTime(2025, 1, 15),
                DueDate = new DateTime(2025, 2, 28), // +44 dni
                IdPatient = 3,
                IdDoctor = 4
            },
            new()
            {
                IdPrescription = 3,
                Date = new DateTime(2025, 3, 10),
                DueDate = new DateTime(2025, 4, 20), // +41 dni
                IdPatient = 5,
                IdDoctor = 1
            },
            new()
            {
                IdPrescription = 4,
                Date = new DateTime(2025, 5, 1),
                DueDate = new DateTime(2025, 6, 5), // +35 dni
                IdPatient = 2,
                IdDoctor = 6
            }



        };
        var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new()
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 500,
                Details = "1 tabletka co 8 godzin"
            },
            new()
            {
                IdPrescription = 1,
                IdMedicament = 3,
                Dose = 200,
                Details = "1 tabletka rano i wieczorem"
            },
            new()
            {
                IdPrescription = 2,
                IdMedicament = 2,
                Dose = 250,
                Details = "2 kapsułki dziennie przez 7 dni"
            },
            new()
            {
                IdPrescription = 3,
                IdMedicament = 4,
                Dose = null, // brak konkretnej dawki – pole może być NULL
                Details = "Stosować w razie duszności"
            },
            new()
            {
                IdPrescription = 4,
                IdMedicament = 5,
                Dose = 850,
                Details = "1 tabletka dziennie przed śniadaniem"
            },
            new()
            {
                IdPrescription = 4,
                IdMedicament = 1,
                Dose = 500,
                Details = "W razie bólu głowy"
            }
        };
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicaments);
        
        
        



    }
}   
    
        
        