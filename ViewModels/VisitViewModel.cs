using System.ComponentModel.DataAnnotations;

namespace Stomatologia.ViewModels;

public class VisitViewModel
{
    [Required]
    public string DentistId { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Data")]
    public DateTime Date { get; set; }
}
