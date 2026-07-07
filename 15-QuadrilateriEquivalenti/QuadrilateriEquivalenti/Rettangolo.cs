namespace QuadrilateriEquivalenti;

/// <summary>
/// Rettangolo: Area = base × altezza
/// </summary>
public class Rettangolo : Quadrilatero
{
    public double Base { get; }
    public double Altezza { get; }

    public Rettangolo(double baseR, double altezza)
        : base(baseR, altezza, baseR, altezza)
    {
        Base = baseR;
        Altezza = altezza;
    }

    public override double Area => Base * Altezza;
    public override string Nome => "Rettangolo";
}
