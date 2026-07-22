using System.ComponentModel.DataAnnotations;

namespace ADO.NET.MVC_GAM.Models;

public partial class Autore
{
    public int Id { get; set; }

    [Display(Name = "Nominativo")]
    public string? Nominativo { get; set; }

    public virtual ICollection<Opera> Opere { get; set; } = new List<Opera>();
}
