namespace Stomatologia.Models;

public class Visit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Dentist Dentist { get; set; } = null!;
    public string DentistId { get; set; } = string.Empty;
    public ApplicationUser Client { get; set; } = null!;
    public string ClientId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
