namespace _13_VenditaProdotti;

public class ProdottoAlimentare : Prodotto
{
    public DateOnly DataScadenza { get; set; }

    public ProdottoAlimentare(int codice, string nome, decimal prezzo, int giacenza, DateOnly dataProduzione, DateOnly dataScadenza)
        : base(codice, nome, prezzo, giacenza, dataProduzione)
    {
        DataScadenza = dataScadenza;
    }

    public bool InScadenza()
    {
        var oggi = DateOnly.FromDateTime(DateTime.Today);
        var giorniRimanenti = (DataScadenza.ToDateTime(TimeOnly.MinValue) - oggi.ToDateTime(TimeOnly.MinValue)).Days;
        return giorniRimanenti < 10;
    }

    public override string ToString()
        => base.ToString() + $" - SCAD: {DataScadenza:dd/MM/yyyy} (Alimentare)";
}
