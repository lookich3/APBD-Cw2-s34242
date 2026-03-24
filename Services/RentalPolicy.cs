using APBDcw2.Models;

namespace APBDcw2.Services;

public class RentalPolicy
{
    private const decimal PenaltyPerDay = 10m;

    public int GetMaxActiveRentals(User user)
    {
        if (user.UserType == UserType.Student)
        {
            return 2;
        }

        if (user.UserType == UserType.Employee)
        {
            return 5;
        }

        return 0;
    }

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
        {
            return 0;
        }

        var lateDays = (returnDate.Date - dueDate.Date).Days;
        return lateDays * PenaltyPerDay;
    }
}