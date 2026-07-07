namespace GestioneImmobili;

/// <summary>
/// Archivio che gestisce la lista degli immobili con operazioni di ricerca e filtro.
/// </summary>
public class ArchivioImmobili
{
    private readonly List<Immobile> _immobili = new();

    // ─── CRUD ────────────────────────────────────────
    public void Aggiungi(Immobile immobile) => _immobili.Add(immobile);

    public int ConteggioTotale => _immobili.Count;

    public List<Immobile> ElencaTutti() => new(_immobili);

    // ─── Filtri per tipologia ────────────────────────
    public List<Box> ElencaBox()
        => _immobili.OfType<Box>().ToList();

    public List<Appartamento> ElencaAppartamenti()
        => _immobili.OfType<Appartamento>().Where(a => a is not Villa).ToList();

    public List<Villa> ElencaVille()
        => _immobili.OfType<Villa>().ToList();

    // ─── Ricerche ────────────────────────────────────
    public List<Immobile> CercaPerCittà(string città)
        => _immobili
           .Where(i => i.Città.Contains(città, StringComparison.OrdinalIgnoreCase))
           .ToList();

    public Immobile? CercaPerCodice(string codice)
        => _immobili
           .FirstOrDefault(i => i.Codice.Equals(codice, StringComparison.OrdinalIgnoreCase));
}
