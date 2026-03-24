using APBDcw2.Models;
using APBDcw2.Services;

namespace APBDcw2;

class Program
{
    static void Main(string[] args)
    {
        var equipmentIdGenerator = new IdGenerator();
        var userIdGenerator = new IdGenerator();
        var rentalIdGenerator = new IdGenerator();

        var equipmentService = new EquipmentService();
        var userService = new UserService();
        var rentalPolicy = new RentalPolicy();
        var rentalService = new RentalService(rentalPolicy, rentalIdGenerator);
        var reportService = new ReportService();

        var laptop1 = new Laptop(equipmentIdGenerator.GetNextId(), "Dell Latitude", 16, "Intel i7");
        var laptop2 = new Laptop(equipmentIdGenerator.GetNextId(), "Lenovo ThinkPad", 8, "Intel i5");
        var projector1 = new Projector(equipmentIdGenerator.GetNextId(), "Epson X500", 3200, "Full HD");
        var camera1 = new Camera(equipmentIdGenerator.GetNextId(), "Canon EOS", 24, true);

        equipmentService.AddEquipment(laptop1);
        equipmentService.AddEquipment(laptop2);
        equipmentService.AddEquipment(projector1);
        equipmentService.AddEquipment(camera1);

        var student1 = new Student(userIdGenerator.GetNextId(), "Jan", "Kowalski", "s12345");
        var student2 = new Student(userIdGenerator.GetNextId(), "Anna", "Nowak", "s54321");
        var employee1 = new Employee(userIdGenerator.GetNextId(), "Piotr", "Wiśniewski", "IT");

        userService.AddUser(student1);
        userService.AddUser(student2);
        userService.AddUser(employee1);

        Console.WriteLine("WSZYSTKIE SPRZĘTY");
        foreach (var equipment in equipmentService.GetAllEquipment())
        {
            Console.WriteLine(equipment);
        }

        Console.WriteLine();
        Console.WriteLine("DOSTĘPNE SPRZĘTY");
        foreach (var equipment in equipmentService.GetAvailableEquipment())
        {
            Console.WriteLine(equipment);
        }

        Console.WriteLine();
        Console.WriteLine("POPRAWNE WYPOŻYCZENIE");
        Console.WriteLine(rentalService.RentEquipment(student1, laptop1, 7));

        Console.WriteLine();
        Console.WriteLine("NIEPOPRAWNE WYPOŻYCZENIE - TEN SAM SPRZĘT");
        Console.WriteLine(rentalService.RentEquipment(student2, laptop1, 5));

        Console.WriteLine();
        Console.WriteLine("TEST LIMITU STUDENTA");
        Console.WriteLine(rentalService.RentEquipment(student1, laptop2, 5));
        Console.WriteLine(rentalService.RentEquipment(student1, projector1, 5));
        Console.WriteLine(rentalService.RentEquipment(student1, camera1, 5));

        Console.WriteLine();
        Console.WriteLine("ZWROT NA CZAS");
        var rental1 = rentalService.GetById(1);
        if (rental1 != null)
        {
            Console.WriteLine(rentalService.ReturnEquipment(rental1.Id, rental1.DueDate));
        }

        Console.WriteLine();
        Console.WriteLine("OZNACZENIE SPRZĘTU JAKO NIEDOSTĘPNY");
        var marked = equipmentService.MarkAsUnavailable(camera1.Id);
        Console.WriteLine(marked
            ? $"Sprzęt {camera1.Name} został oznaczony jako niedostępny."
            : $"Nie udało się oznaczyć sprzętu {camera1.Name} jako niedostępnego.");

        Console.WriteLine();
        Console.WriteLine("PRÓBA WYPOŻYCZENIA NIEDOSTĘPNEGO SPRZĘTU");
        Console.WriteLine(rentalService.RentEquipment(employee1, camera1, 3));

        camera1.Status = EquipmentStatus.Available;

        Console.WriteLine();
        Console.WriteLine("OPÓŹNIONY ZWROT");
        Console.WriteLine(rentalService.RentEquipment(employee1, camera1, 3));

        var lateRental = rentalService.GetById(3);
        if (lateRental != null)
        {
            var lateReturnDate = lateRental.DueDate.AddDays(4);
            Console.WriteLine(rentalService.ReturnEquipment(lateRental.Id, lateReturnDate));
        }

        Console.WriteLine();
        Console.WriteLine("AKTYWNE WYPOŻYCZENIA STUDENTA");
        foreach (var rental in rentalService.GetActiveRentalsByUser(student1.Id))
        {
            Console.WriteLine(rental);
        }

        Console.WriteLine();
        Console.WriteLine("PRZETERMINOWANE WYPOŻYCZENIA");
        foreach (var overdueRental in rentalService.GetOverdueRentals())
        {
            Console.WriteLine(overdueRental);
        }

        Console.WriteLine();
        Console.WriteLine(reportService.GenerateReport(equipmentService.GetAllEquipment(), userService.GetAllUsers(), rentalService.GetAllRentals()));
    }
}