namespace GestioneImmobili;

/// <summary>
/// Classe astratta base per tutti gli immobili.
/// Contiene le proprietà comuni e un abstract ToString() specializzato.
/// </summary>
public abstract class Immobile
{
    // ─── Proprietà ───────────────────────────────────
    public string Codice { get; set; }
    public string Indirizzo { get; set; }
    public string CAP { get; set; }
    public string Città { get; set; }
    public int SuperficieMq { get; set; }
    public decimal PropostaEconomica { get; set; }

    /// <summary>Restituisce la tipologia descrittiva (es. "Box", "Appartamento", "Villa").</summary>
    public abstract string Tipologia { get; }

    // ─── Costruttore protetto ────────────────────────
    protected Immobile(string codice, string indirizzo, string cap,
                       string città, int superficieMq, decimal propostaEconomica)
    {
        Codice = codice;
        Indirizzo = indirizzo;
        CAP = cap;
        Città = città;
        SuperficieMq = superficieMq;
        PropostaEconomica = propostaEconomica;
    }

    // ─── ToString ────────────────────────────────────
    public override string ToString()
        => $"[{Tipologia}] {Codice} — {Indirizzo}, {CAP} {Città}"
         + $" | {SuperficieMq} mq | € {PropostaEconomica:N2}";
}
