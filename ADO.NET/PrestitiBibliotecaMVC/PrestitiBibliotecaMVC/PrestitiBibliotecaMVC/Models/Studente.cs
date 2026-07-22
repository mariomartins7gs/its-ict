using System;
using System.Collections.Generic;

namespace PrestitiBibliotecaMVC.Models;

public partial class Studente
{
    public int Matricola { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public string? Email { get; set; }

    public string? Classe { get; set; }

    public virtual ICollection<Prestito> Prestiti { get; set; } = new List<Prestito>();
}
