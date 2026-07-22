using System.ComponentModel.DataAnnotations.Schema;

namespace ADO.NET.MVC_GAM.Models;

public partial class OperaMateriale
{
    public int IdOpera { get; set; }

    public int IdMateriale { get; set; }

    [ForeignKey(nameof(IdMateriale))]
    public virtual Materiale IdMaterialeNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdOpera))]
    public virtual Opera IdOperaNavigation { get; set; } = null!;
}
