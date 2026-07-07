namespace QuadrilateriEquivalenti;

/// <summary>
/// Rombo: Area = (D × d) / 2   (diagonale maggiore × diagonale minore ÷ 2)
/// </summary>
public class Rombo : Quadrilatero
{
    public double DiagonaleMaggiore { get; }
    public double DiagonaleMinore { get; }

    public Rombo(double diagonaleMaggiore, double diagonaleMinore, double lato)
        : base(lato, lato, lato, lato)
    {
        DiagonaleMaggiore = diagonaleMaggiore;
        DiagonaleMinore = diagonaleMinore;
    }

    public override double Area => (DiagonaleMaggiore * DiagonaleMinore) / 2.0;
    public override string Nome => "Rombo";
}
