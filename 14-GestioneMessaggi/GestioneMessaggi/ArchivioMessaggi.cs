namespace GestioneMessaggi;

/// <summary>
/// Archivio che gestisce la lista dei messaggi,
/// con operazioni di ricerca e conteggio.
/// </summary>
public class ArchivioMessaggi
{
    private readonly List<Messaggio> _messaggi = new();

    // ─── CRUD ────────────────────────────────────────

    /// <summary>Inserisce un nuovo messaggio nell'archivio.</summary>
    public void Inserisci(Messaggio messaggio)
    {
        _messaggi.Add(messaggio);
    }

    /// <summary>Restituisce tutti i messaggi.</summary>
    public List<Messaggio> ElencaTutti() => new(_messaggi);

    // ─── Ricerche ────────────────────────────────────

    /// <summary>Cerca messaggi per mittente (case-insensitive).</summary>
    public List<Messaggio> CercaPerMittente(string mittente)
    {
        return _messaggi
            .Where(m => m.Mittente.Contains(mittente, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>Cerca messaggi per destinatario (case-insensitive).</summary>
    public List<Messaggio> CercaPerDestinatario(string destinatario)
    {
        return _messaggi
            .Where(m => m.Destinatario.Contains(destinatario, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // ─── Statistiche ─────────────────────────────────

    /// <summary>Conta quanti messaggi sono stati inseriti dopo una certa data.</summary>
    public int ContaDopoData(DateOnly data)
    {
        return _messaggi.Count(m => DateOnly.FromDateTime(m.DataOra) > data);
    }

    // ─── Utility ─────────────────────────────────────

    public int TotaleMessaggi => _messaggi.Count;
}
