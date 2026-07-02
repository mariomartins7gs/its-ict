namespace _13_VenditaProdotti;

public abstract class Prodotto
{
    public int Codice { get; set; }
    public string Nome { get; set; }
    public decimal Prezzo { get; set; }
    public int Giacenza { get; set; }
    public DateOnly DataProduzione { get; set; }

    protected Prodotto(int codice, string nome, decimal prezzo, int giacenza, DateOnly dataProduzione)
    {
        Codice = codice;
        Nome = nome;
        Prezzo = prezzo;
        Giacenza = giacenza;
        DataProduzione = dataProduzione;
    }

    public override string ToString()
        => $"[{Codice}] {Nome} - €{Prezzo} - Giacenza: {Giacenza} - Prod: {DataProduzione:dd/MM/yyyy}";
}
