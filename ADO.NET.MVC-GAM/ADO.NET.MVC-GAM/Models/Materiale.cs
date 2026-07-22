using System.ComponentModel.DataAnnotations;

namespace ADO.NET.MVC_GAM.Models;

public partial class Materiale
{
    public int Id { get; set; }

    [Display(Name = "Denominazione")]
    public string? Denominazione { get; set; }

    public virtual ICollection<OperaMateriale> OperaMateriali { get; set; } = new List<OperaMateriale>();
}
