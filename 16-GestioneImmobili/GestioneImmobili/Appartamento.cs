namespace GestioneImmobili;

/// <summary>
/// Appartamento: vani e bagni in aggiunta all'immobile base.
/// </summary>
public class Appartamento : Immobile
{
    public int NumeroVani { get; set; }
    public int NumeroBagni { get; set; }

    public Appartamento(string codice, string indirizzo, string cap, string città,
                        int superficieMq, decimal propostaEconomica,
                        int numeroVani, int numeroBagni)
        : base(codice, indirizzo, cap, città, superficieMq, propostaEconomica)
    {
        NumeroVani = numeroVani;
        NumeroBagni = numeroBagni;
    }

    public override string Tipologia => "Appartamento";

    public override string ToString()
        => base.ToString() + $" | {NumeroVani} van{(NumeroVani == 1 ? "o" : "i")}, {NumeroBagni} bagn{(NumeroBagni == 1 ? "o" : "i")}";
}
