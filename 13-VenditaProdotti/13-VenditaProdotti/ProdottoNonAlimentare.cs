namespace _13_VenditaProdotti;

public class ProdottoNonAlimentare : Prodotto
{
    public List<Materiale> Materiali { get; set; }

    public ProdottoNonAlimentare(int codice, string nome, decimal prezzo, int giacenza, DateOnly dataProduzione, List<Materiale> materiali)
        : base(codice, nome, prezzo, giacenza, dataProduzione)
    {
        Materiali = materiali;
    }

    public override string ToString()
    {
        string mat = string.Join(", ", Materiali);
        return base.ToString() + $" - Materiali: {mat} (Non Alimentare)";
    }
}
