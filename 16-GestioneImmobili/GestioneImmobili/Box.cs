namespace GestioneImmobili;

/// <summary>
/// Box: superficie ridotta, posti auto aggiuntivi.
/// </summary>
public class Box : Immobile
{
    public int NumeroPostiAuto { get; set; }

    public Box(string codice, string indirizzo, string cap, string città,
               int superficieMq, decimal propostaEconomica, int numeroPostiAuto)
        : base(codice, indirizzo, cap, città, superficieMq, propostaEconomica)
    {
        NumeroPostiAuto = numeroPostiAuto;
    }

    public override string Tipologia => "Box";

    public override string ToString()
        => base.ToString() + $" | {NumeroPostiAuto} post{(NumeroPostiAuto == 1 ? "o" : "i")} auto";
}
