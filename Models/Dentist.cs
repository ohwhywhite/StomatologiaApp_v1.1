namespace Stomatologia.Models;

public class Dentist : ApplicationUser
{
    public DentistSpecializations Specialization { get; set; }
}
