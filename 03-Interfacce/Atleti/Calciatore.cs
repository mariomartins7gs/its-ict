namespace Atleti;

public class Calciatore : Atleta, IComparable<Calciatore>, ICloneable
{
    public int GoalSegnati { get; set; }
    public int PartiteGiocate { get; set; }

    public double MediaGoal()
    {
        if (PartiteGiocate == 0) return 0;
        return (double)GoalSegnati / PartiteGiocate;
    }

    // Equals — confronto per contenuto (stesso cognome e nome)
    public override bool Equals(object? obj)
    {
        if (obj is not Calciatore other) return false;
        return Cognome == other.Cognome && Nome == other.Nome;
    }

    public override int GetHashCode() => HashCode.Combine(Cognome, Nome);

    // Clone — bloccato se PartiteGiocate == 0
    public object Clone()
    {
        if (PartiteGiocate == 0)
            throw new InvalidOperationException(
                $"Impossibile clonare {Nome} {Cognome}: partite giocate pari a zero.");

        return new Calciatore
        {
            Nome = this.Nome,
            Cognome = this.Cognome,
            Pettorina = this.Pettorina,
            Disciplina = this.Disciplina,
            GoalSegnati = this.GoalSegnati,
            PartiteGiocate = this.PartiteGiocate
        };
    }

    // CompareTo — basato sulla media goal (IComparable<Calciatore>)
    public int CompareTo(Calciatore? other)
    {
        if (other is null) return 1;
        return MediaGoal().CompareTo(other.MediaGoal());
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Goal: {GoalSegnati}/{PartiteGiocate} (media: {MediaGoal():F2})";
    }
}
