using System;
using System.Collections.Generic;

namespace PrestitiBibliotecaMVC.Models;

public partial class Libro
{
    public int Codice { get; set; }

    public string? Autore { get; set; }

    public string? Titolo { get; set; }

    public string? Editore { get; set; }

    public string? Anno { get; set; }

    public string? Luogo { get; set; }

    public int? Pagine { get; set; }

    public string? Classificazione { get; set; }

    public string? Collocazione { get; set; }

    public int? Copie { get; set; }

    public virtual ICollection<Prestito> Prestiti { get; set; } = new List<Prestito>();
}
