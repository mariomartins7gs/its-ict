namespace QuadrilateriEquivalenti;

/// <summary>
/// Parallelogramma: Area = base × altezza
/// </summary>
public class Parallelogramma : Quadrilatero
{
    public double Base { get; }
    public double Altezza { get; }
    public double LatoObliquo { get; }

    public Parallelogramma(double baseP, double altezza, double latoObliquo)
        : base(baseP, latoObliquo, baseP, latoObliquo)
    {
        Base = baseP;
        Altezza = altezza;
        LatoObliquo = latoObliquo;
    }

    public override double Area => Base * Altezza;
    public override string Nome => "Parallelogramma";
}
