using Stomatologia.Models;

namespace Stomatologia.Data;

internal static class InitialData
{
    public const string InitialPassword = "zaq1@WSX";

    public static readonly string[] Roles =
    {
        ApplicationRoles.User,
        ApplicationRoles.Dentist,
        ApplicationRoles.Admin
    };

    public static readonly ApplicationUser[] Users =
    {
        new Dentist()
        {
            FirstName = "Krzysztof",
            LastName = "Nowak",
            Specialization = DentistSpecializations.Dentist,
            Email = "krzysztof.nowak@clinic.pl",
            UserName = "krzysztof.nowak@clinic.pl"
        },
        new Dentist()
        {
            FirstName = "Monika",
            LastName = "Kowalska",
            Specialization = DentistSpecializations.PediatricDentist,
            Email = "monika.kowalska@clinic.pl",
            UserName = "monika.kowalska@clinic.pl"
        },
        new Dentist()
        {
            FirstName = "Katarzyna",
            LastName = "Grabowska",
            Specialization = DentistSpecializations.Periodontist,
            Email = "katarzyna.grabowska@clinic.pl",
            UserName = "katarzyna.grabowska@clinic.pl"
        },
        new Dentist()
        {
            FirstName = "Piotr",
            LastName = "Wawrzyniak",
            Specialization = DentistSpecializations.Prosthodontist,
            Email = "piotr.wawrzyniak@clinic.pl",
            UserName = "piotr.wawrzyniak@clinic.pl"
        },
        new Dentist()
        {
            FirstName = "Adam",
            LastName = "Leon",
            Specialization = DentistSpecializations.Orthodontist,
            Email = "adam.leon@clinic.pl",
            UserName = "adam.leon@clinic.pl"
        },
        new()
        {
            FirstName = "Marek",
            LastName = "Kowalski",
            Email ="marek.kowalski@example.pl",
            UserName ="marek.kowalski@example.pl"
        },
        new()
        {
            FirstName = "Joanna",
            LastName = "Nowak",
            Email = "joanna.nowak@example.pl",
            UserName = "joanna.nowak@example.pl"
        },
        new()
        {
            FirstName = "Adam",
            LastName = "Wiśniewski",
            Email = "adam.wisniewski@example.pl",
            UserName = "adam.wisniewski@example.pl"
        }
    };

    public static readonly Visit[] Visits =
    {
        new()
        {
            Dentist = (Dentist)Users[3],
            Client = Users[5],
            Date = DateTime.Today.AddDays(1).AddHours(12)
        },
        new()
        {
            Dentist = (Dentist)Users[1],
            Client = Users[6],
            Date = DateTime.Today.AddDays(3).AddHours(16)
        },
        new()
        {
            Dentist = (Dentist)Users[0],
            Client = Users[6],
            Date = DateTime.Today.AddDays(5).AddHours(14)
        },
        new()
        {
            Dentist = (Dentist)Users[4],
            Client = Users[5],
            Date = DateTime.Today.AddDays(10).AddHours(17)
        }
    };
}
