using APBDcw2.Models;

namespace APBDcw2.Services;

public class ReportService
{
    public string GenerateReport(List<Equipment> equipmentList, List<User> users, List<Rental> rentals)
    {
        var report = "RAPORT KOŃCOWY\n";

        report += "Liczba sprzętów: " + equipmentList.Count + "\n";
        report += "Dostępne sprzęty: " + equipmentList.Count(e => e.Status == EquipmentStatus.Available) + "\n";
        report += "Wypożyczone sprzęty: " + equipmentList.Count(e => e.Status == EquipmentStatus.Rented) + "\n";
        report += "Niedostępne sprzęty: " + equipmentList.Count(e => e.Status == EquipmentStatus.Unavailable) + "\n\n";

        report += "Liczba użytkowników: " + users.Count + "\n";
        report += "Liczba wszystkich wypożyczeń: " + rentals.Count + "\n";
        report += "Aktywne wypożyczenia: " + rentals.Count(r => r.IsActive) + "\n";
        report += "Zwrócone wypożyczenia: " + rentals.Count(r => r.IsReturned) + "\n";
        report += "Suma kar: " + rentals.Sum(r => r.Penalty) + "\n\n";

        var overdueRentals = rentals
            .Where(r => r.IsActive && r.DueDate < DateTime.Today)
            .ToList();

        report += "Przeterminowane aktywne wypożyczenia:\n";

        if (overdueRentals.Count == 0)
        {
            report += "Brak\n";
        }
        else
        {
            foreach (var rental in overdueRentals)
            {
                report += rental + "\n";
            }
        }

        return report;
    }
}