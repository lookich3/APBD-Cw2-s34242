namespace APBDcw2.Models;

public class Employee : User
{
    public string Department { get; set; }

    public Employee(int id, string firstName, string lastName, string department)
        : base(id, firstName, lastName, UserType.Employee)
    {
        Department = department;
    }

    public override string ToString()
    {
        return base.ToString() + $", Dział: {Department}";
    }
}