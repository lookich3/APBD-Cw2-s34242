using APBDcw2.Models;

namespace APBDcw2.Services;

public class RentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly RentalPolicy _rentalPolicy;
    private readonly IdGenerator _rentalIdGenerator;

    public RentalService(RentalPolicy rentalPolicy, IdGenerator rentalIdGenerator)
    {
        _rentalPolicy = rentalPolicy;
        _rentalIdGenerator = rentalIdGenerator;
    }

    public string RentEquipment(User user, Equipment equipment, int days)
    {
        if (equipment.Status == EquipmentStatus.Unavailable)
        {
            return "Nie można wypożyczyć sprzętu: sprzęt jest niedostępny.";
        }

        if (equipment.Status == EquipmentStatus.Rented)
        {
            return "Nie można wypożyczyć sprzętu: sprzęt jest już wypożyczony.";
        }

        var activeRentalsCount = _rentals.Count(rental => rental.User.Id == user.Id && rental.IsActive);
        var maxAllowed = _rentalPolicy.GetMaxActiveRentals(user);

        if (activeRentalsCount >= maxAllowed)
        {
            return $"Nie można wypożyczyć sprzętu: użytkownik przekroczył limit ({maxAllowed}).";
        }

        var rentDate = DateTime.Today;
        var dueDate = rentDate.AddDays(days);

        var rental = new Rental(_rentalIdGenerator.GetNextId(), user, equipment, rentDate, dueDate);

        _rentals.Add(rental);
        equipment.Status = EquipmentStatus.Rented;

        return $"Sprzęt '{equipment.Name}' został wypożyczony użytkownikowi {user.FirstName} {user.LastName}.";
    }

    public string ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = GetById(rentalId);

        if (rental == null)
        {
            return "Nie znaleziono wypożyczenia.";
        }

        if (rental.IsReturned)
        {
            return "Sprzęt został już zwrócony.";
        }

        rental.ReturnDate = returnDate;
        rental.Penalty = _rentalPolicy.CalculatePenalty(rental.DueDate, returnDate);
        rental.Equipment.Status = EquipmentStatus.Available;

        return $"Sprzęt został zwrócony. Kara: {rental.Penalty}";
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public List<Rental> GetActiveRentalsByUser(int userId)
    {
        return _rentals
            .Where(rental => rental.User.Id == userId && rental.IsActive)
            .ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentals
            .Where(rental => rental.IsActive && rental.DueDate < DateTime.Today)
            .ToList();
    }

    public Rental? GetById(int rentalId)
    {
        return _rentals.FirstOrDefault(rental => rental.Id == rentalId);
    }
}