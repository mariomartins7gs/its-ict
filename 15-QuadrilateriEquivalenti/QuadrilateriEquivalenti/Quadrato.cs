namespace QuadrilateriEquivalenti;

/// <summary>
/// Quadrato: Area = lato²
/// </summary>
public class Quadrato : Quadrilatero
{
    public double Lato { get; }

    public Quadrato(double lato)
        : base(lato, lato, lato, lato)
    {
        Lato = lato;
    }

    public override double Area => Lato * Lato;
    public override string Nome => "Quadrato";
}
