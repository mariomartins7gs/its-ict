using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO.NET.MVC_GAM.Models;

public partial class Opera
{
    public int Id { get; set; }

    [Display(Name = "Inventario")]
    public string? Inventario { get; set; }

    public int? IdAutore { get; set; }

    [Display(Name = "Ambito culturale")]
    public string? Ambito_culturale { get; set; }

    [Display(Name = "Datazione")]
    public string? Datazione { get; set; }

    [Display(Name = "Titolo / Soggetto")]
    public string? Titolo_soggetto { get; set; }

    [Display(Name = "Immagine")]
    public string? Immagine { get; set; }

    public string? lsreferenceby { get; set; }

    [ForeignKey(nameof(IdAutore))]
    [Display(Name = "Autore")]
    public virtual Autore? IdAutoreNavigation { get; set; }

    public virtual ICollection<OperaMateriale> OperaMateriali { get; set; } = new List<OperaMateriale>();
}
