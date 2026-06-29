namespace Atleti;

public class Atleta : IAtletaUniversale
{
    // Proprietà
    public required string Nome { get; set; }
    public required string Cognome { get; set; }
    public int Pettorina { get; set; }
    public string Disciplina { get; set; } = string.Empty;

    // IAtleta
    public string Corro() => $"{Nome} {Cognome} sta correndo! 🏃";
    public string Salto() => $"{Nome} {Cognome} sta saltando! 🤸";

    // ITennista
    public string Dritto() => $"{Nome} {Cognome} colpisce di dritto! 🎾";
    public string Rovescio() => $"{Nome} {Cognome} colpisce di rovescio! 🎾";

    // INuotatore
    public string Rana() => $"{Nome} {Cognome} nuota a rana! 🏊";
    public string Dorso() => $"{Nome} {Cognome} nuota a dorso! 🏊";

    // IAtletaUniversale
    public string Mangio() => $"{Nome} {Cognome} sta mangiando! 🍝";
    public string Bevo() => $"{Nome} {Cognome} sta bevendo! 🥤";

    public override string ToString()
    {
        return $"{GetType().Name}: {Nome} {Cognome} | Pett. {Pettorina} | {Disciplina}";
    }
}
