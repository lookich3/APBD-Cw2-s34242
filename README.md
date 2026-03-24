# System wypożyczania sprzętu

## Opis projektu
Projekt przedstawia konsolową aplikację w C# do zarządzania systemem wypożyczania sprzętu na uczelni

Program umożliwia:
- dodawanie sprzętu
- dodawanie użytkowników
- wypożyczanie sprzętu
- zwracanie sprzętu
- oznaczanie sprzętu jako niedostępny
- naliczanie kar za opóźniony zwrot
- generowanie raportu końcowego

## Struktura projektu
Projekt został podzielony na:
- `Models` – klasy domenowe
- `Services` – logika biznesowa
- `Program.cs` – scenariusz demonstracyjny działania programu

## Najważniejsze klasy
- `Equipment` – klasa bazowa dla sprzętu
- `Laptop`, `Projector`, `Camera` – typy sprzętu
- `User` – klasa bazowa użytkownika
- `Student`, `Employee` – typy użytkowników
- `Rental` – model wypożyczenia
- `EquipmentService` – zarządzanie sprzętem
- `UserService` – zarządzanie użytkownikami
- `RentalService` – obsługa wypożyczeń i zwrotów
- `RentalPolicy` – zasady limitów i kar
- `ReportService` – generowanie raportu

## Decyzje projektowe
Zastosowano podział na warstwę modeli oraz warstwę serwisów

Przykłady:
- `Equipment`, `User` i `Rental` reprezentują model domeny
- `EquipmentService` odpowiada za zarządzanie sprzętem
- `UserService` odpowiada za zarządzanie użytkownikami
- `RentalService` obsługuje wypożyczenia i zwroty
- `RentalPolicy` przechowuje zasady limitów oraz naliczania kar
- `ReportService` odpowiada za generowanie raportu

Takie podejście zwiększa kohezję, ponieważ każda klasa ma jedną odpowiedzialność
Jednocześnie zmniejsza coupling, ponieważ logika została rozdzielona na osobne klasy


## W projekcie wykorzystano gałęzie:
- `main`
- `feature/models`
- `feature/services`
- `feature/final`

Zmiany były dodawane etapami i łączone do `main` po zakończeniu kolejnych części projektu
