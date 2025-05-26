using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace s28482_OstatnieZadaniePunktowane.Migrations
{
    /// <inheritdoc />
    public partial class ADDTABLESANDmoredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "adam@gmail.com", "Adam", "Adamski" },
                    { 2, "barbara.borkowska@medmail.com", "Barbara", "Borkowska" },
                    { 3, "cezary.cz@clinic.org", "Cezary", "Czarnecki" },
                    { 4, "dorota.dabrowska@healthmail.pl", "Dorota", "Dąbrowska" },
                    { 5, "edward.edelman@hospital.net", "Edward", "Edelman" },
                    { 6, "franciszek.f@medcenter.com", "Franciszek", "Fijałkowski" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Lek przeciwbólowy i przeciwgorączkowy", "Paracetamol", "Tabletka" },
                    { 2, "Antybiotyk z grupy penicylin", "Amoxicillin", "Kapsułka" },
                    { 3, "Lek przeciwzapalny i przeciwbólowy", "Ibuprofen", "Tabletka" },
                    { 4, "Lek rozszerzający oskrzela", "Salbutamol", "Aerozol" },
                    { 5, "Lek stosowany w leczeniu cukrzycy typu 2", "Metformina", "Tabletka" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "Doe" },
                    { 2, new DateTime(1985, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anna", "Kowalska" },
                    { 3, new DateTime(1978, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piotr", "Nowak" },
                    { 4, new DateTime(1995, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria", "Wiśniewska" },
                    { 5, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomasz", "Lewandowski" },
                    { 6, new DateTime(1992, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ewa", "Zielińska" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 4, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "1 tabletka co 8 godzin", 500 },
                    { 1, 4, "W razie bólu głowy", 500 },
                    { 2, 2, "2 kapsułki dziennie przez 7 dni", 250 },
                    { 3, 1, "1 tabletka rano i wieczorem", 200 },
                    { 4, 3, "Stosować w razie duszności", null },
                    { 5, 4, "1 tabletka dziennie przed śniadaniem", 850 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 5);
        }
    }
}
