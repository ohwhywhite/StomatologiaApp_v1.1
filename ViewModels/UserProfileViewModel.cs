using System.ComponentModel.DataAnnotations;

namespace Stomatologia.ViewModels;

public class UserProfileViewModel
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

    [Required(ErrorMessage = "Pole {0} jest wymagane")]
    public string PESEL { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;
}
