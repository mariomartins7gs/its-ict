namespace GestioneImmobili;

/// <summary>
/// Villa: come un appartamento ma con giardino extra.
/// </summary>
public class Villa : Appartamento
{
    public int DimensioneGiardinoMq { get; set; }

    public Villa(string codice, string indirizzo, string cap, string città,
                 int superficieMq, decimal propostaEconomica,
                 int numeroVani, int numeroBagni, int dimensioneGiardinoMq)
        : base(codice, indirizzo, cap, città, superficieMq, propostaEconomica,
               numeroVani, numeroBagni)
    {
        DimensioneGiardinoMq = dimensioneGiardinoMq;
    }

    public override string Tipologia => "Villa";

    public override string ToString()
        => base.ToString() + $" | Giardino: {DimensioneGiardinoMq} mq";
}
