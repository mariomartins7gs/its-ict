namespace _12_FileCSV_Scrittura;

public enum Sesso
{
    M,
    F,
    Altro
}

public class Persona
{
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public DateOnly DataDiNascita { get; set; }
    public string LuogoDiNascita { get; set; }
    public Sesso Sesso { get; set; }

    public int Età
    {
        get
        {
            var oggi = DateOnly.FromDateTime(DateTime.Today);
            int eta = oggi.Year - DataDiNascita.Year;
            if (oggi.DayOfYear < DataDiNascita.DayOfYear)
                eta--;
            return eta;
        }
    }

    public Persona(string cognome, string nome, DateOnly dataDiNascita,
                   string luogoDiNascita, Sesso sesso)
    {
        Cognome = cognome;
        Nome = nome;
        DataDiNascita = dataDiNascita;
        LuogoDiNascita = luogoDiNascita;
        Sesso = sesso;
    }

    public override string ToString()
        => $"{Cognome} {Nome} | {DataDiNascita:dd/MM/yyyy} a {LuogoDiNascita} | {Sesso} | {Età} anni";
}
