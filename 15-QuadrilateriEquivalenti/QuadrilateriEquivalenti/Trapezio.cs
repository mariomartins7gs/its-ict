namespace QuadrilateriEquivalenti;

/// <summary>
/// Trapezio: Area = ((B + b) × h) / 2
/// </summary>
public class Trapezio : Quadrilatero
{
    public double BaseMaggiore { get; }
    public double BaseMinore { get; }
    public double Altezza { get; }

    public Trapezio(double baseMaggiore, double baseMinore, double altezza,
                    double latoObliquo1, double latoObliquo2)
        : base(baseMaggiore, latoObliquo1, baseMinore, latoObliquo2)
    {
        BaseMaggiore = baseMaggiore;
        BaseMinore = baseMinore;
        Altezza = altezza;
    }

    public override double Area => ((BaseMaggiore + BaseMinore) * Altezza) / 2.0;
    public override string Nome => "Trapezio";
}
