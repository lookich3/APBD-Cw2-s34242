namespace APBDcw2.Models;

public class Rental
{
    public int Id { get; set; }
    public User User { get; set; }
    public Equipment Equipment { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsActive => !ReturnDate.HasValue;

    public Rental(int id, User user, Equipment equipment, DateTime rentDate, DateTime dueDate)
    {
        Id = id;
        User = user;
        Equipment = equipment;
        RentDate = rentDate;
        DueDate = dueDate;
        ReturnDate = null;
        Penalty = 0;
    }

    public override string ToString()
    {
        string returnInfo = ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "nie zwrócono";

        return $"Id wypożyczenia: {Id}, Użytkownik: {User.FirstName} {User.LastName}, Sprzęt: {Equipment.Name}, Data wypożyczenia: {RentDate.ToShortDateString()}, Termin zwrotu: {DueDate.ToShortDateString()}, Data zwrotu: {returnInfo}, Kara: {Penalty}";
    }
}