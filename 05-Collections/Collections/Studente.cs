namespace Collections;

public class Studente
{
    public int Matricola { get; set; }
    public required string Nome { get; set; }
    public required string Cognome { get; set; }
    public required string Email { get; set; }
    public required string Classe { get; set; }

    public override string ToString()
    {
        return $"[{Matricola}] {Nome} {Cognome} — {Classe} — {Email}";
    }
}
