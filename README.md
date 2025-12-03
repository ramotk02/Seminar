# Probe_Klausur

**Probe_Klausur** is a WPF application in C# that allows users to view seminars from a MySQL database and book them.

---

## Features

- Display a list of seminars from a MySQL database.
- Select a seminar and make a booking (insert into the `buchung` table).
- Show confirmation or error messages when booking.

---

## Technologies Used

- **C# / WPF**: for the graphical user interface.
- **MySQL**: for data management.
- **.NET Framework**: for Windows desktop development.
- **MySql.Data**: for connecting to MySQL.

---

## Project Structure

- `MainWindow.xaml`: UI layout, includes a `ListView` for displaying seminars and a button to book.
- `MainWindow.xaml.cs`: logic for loading seminars from the database and handling bookings.
- `DbConnection.cs`: utility class for managing the MySQL database connection.
- `Buchung`: class representing a booking.

---

## Database

### Main Tables

1. **seminar**
   - `SeminarId` (int, PK)
   - `Titel` (varchar)
   - `Datum` (datetime)

2. **buchung**
   - `BuchungId` (int, PK)
   - `SeminarId` (int, FK)
   - `TeilnehmerId` (int)
   - `Buchungsdatum` (datetime)

---

## Installation and Usage

1. Clone the project.
2. Set up a MySQL database (`probeklausur`) with the tables `seminar` and `buchung`.
3. Update the connection string in `DbConnection.cs` if needed:

```csharp
private static string connectionString = "server=localhost;port=3306;uid=root;pwd=root;database=probeklausur;";
