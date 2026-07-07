namespace QuadrilateriEquivalenti;

/// <summary>
/// Classe astratta che rappresenta un quadrilatero generico.
/// Il perimetro è calcolabile (somma dei 4 lati),
/// ma l'area è astratta — ogni forma ha la sua formula.
/// </summary>
public abstract class Quadrilatero
{
    // ─── Proprietà ───────────────────────────────────
    public double Lato1 { get; protected set; }
    public double Lato2 { get; protected set; }
    public double Lato3 { get; protected set; }
    public double Lato4 { get; protected set; }

    // ─── Proprietà calcolate ─────────────────────────
    /// <summary>Perimetro = somma dei 4 lati (concreto).</summary>
    public double Perimetro => Lato1 + Lato2 + Lato3 + Lato4;

    /// <summary>Area — astratta, ogni forma implementa la propria formula.</summary>
    public abstract double Area { get; }

    /// <summary>Nome descrittivo del quadrilatero — astratto.</summary>
    public abstract string Nome { get; }

    // ─── Costruttore protetto ────────────────────────
    protected Quadrilatero(double l1, double l2, double l3, double l4)
    {
        Lato1 = l1;
        Lato2 = l2;
        Lato3 = l3;
        Lato4 = l4;
    }

    // ─── ToString ────────────────────────────────────
    public override string ToString()
        => $"[{Nome}] Perimetro: {Perimetro:F2} | Area: {Area:F2} | "
         + $"Lati: {Lato1:F2}, {Lato2:F2}, {Lato3:F2}, {Lato4:F2}";
}
