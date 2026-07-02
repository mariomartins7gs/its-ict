namespace _13_VenditaProdotti;

public class Materiale
{
    public string Denominazione { get; set; }
    public int Percentuale { get; set; }

    public Materiale(string denominazione, int percentuale)
    {
        Denominazione = denominazione;
        Percentuale = percentuale;
    }

    public override string ToString()
        => $"{Denominazione} ({Percentuale}%)";
}
