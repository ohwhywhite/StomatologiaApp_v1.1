using System.ComponentModel.DataAnnotations;

namespace Stomatologia.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Imię")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Potwierdź hasło")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Display(Name = "PESEL")]
    public string PESEL { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; } = string.Empty;
}
