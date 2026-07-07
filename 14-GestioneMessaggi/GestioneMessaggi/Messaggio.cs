namespace GestioneMessaggi;

/// <summary>
/// Implementazione concreta di un messaggio standard.
/// </summary>
public class Messaggio : MessaggioBase
{
    public Messaggio(
        string mittente,
        string destinatario,
        string oggetto,
        string testo,
        DateTime dataOra,
        Priorita priorita)
        : base(mittente, destinatario, oggetto, testo, dataOra, priorita)
    {
    }

    /// <summary>
    /// Riepilogo breve: mittente → destinatario | oggetto [priorità]
    /// </summary>
    public override string ToSummary()
    {
        string icon = Priorita switch
        {
            Priorita.Alta    => "🔴",
            Priorita.Normale => "🟡",
            Priorita.Bassa   => "🟢",
            _                => "❓"
        };

        return $"{icon} {Mittente} → {Destinatario} | {Oggetto} [{Priorita}]";
    }
}
