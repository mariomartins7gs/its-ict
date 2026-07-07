namespace GestioneMessaggi;

/// <summary>
/// Classe astratta base per un messaggio.
/// Definisce il contratto comune a tutti i tipi di messaggio.
/// </summary>
public abstract class MessaggioBase
{
    // ─── Proprietà ───────────────────────────────────
    public string Mittente { get; set; }
    public string Destinatario { get; set; }
    public string Oggetto { get; set; }
    public string Testo { get; set; }
    public DateTime DataOra { get; set; }
    public Priorita Priorita { get; set; }

    // ─── Costruttore ─────────────────────────────────
    protected MessaggioBase(
        string mittente,
        string destinatario,
        string oggetto,
        string testo,
        DateTime dataOra,
        Priorita priorita)
    {
        Mittente = mittente;
        Destinatario = destinatario;
        Oggetto = oggetto;
        Testo = testo;
        DataOra = dataOra;
        Priorita = priorita;
    }

    // ─── Metodo astratto (polimorfismo) ──────────────
    /// <summary>
    /// Restituisce un riepilogo breve del messaggio.
    /// Ogni classe derivata può implementarlo a modo suo.
    /// </summary>
    public abstract string ToSummary();

    // ─── Override ToString ───────────────────────────
    public override string ToString()
    {
        string prioritaIcon = Priorita switch
        {
            Priorita.Alta    => "🔴 Alta",
            Priorita.Normale => "🟡 Normale",
            Priorita.Bassa   => "🟢 Bassa",
            _                => "❓ Sconosciuta"
        };

        return $"┌─ Messaggio ──────────────────────────\n" +
               $"│ Da:      {Mittente}\n" +
               $"│ A:       {Destinatario}\n" +
               $"│ Oggetto: {Oggetto}\n" +
               $"│ Data:    {DataOra:dd/MM/yyyy HH:mm}\n" +
               $"│ Prio:    {prioritaIcon}\n" +
               $"├─ Testo ──────────────────────────────\n" +
               $"│ {Testo}\n" +
               $"└───────────────────────────────────────";
    }
}
