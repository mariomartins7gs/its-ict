using QuadrilateriEquivalenti;

// ─── Seed: lista di quadrilateri con aree volutamente equivalenti ──
List<Quadrilatero> elenco = new()
{
    // Gruppo Area = 20
    new Rettangolo(5, 4),          // Area = 20, Perim = 18
    new Rettangolo(10, 2),         // Area = 20, Perim = 24 ← equivalente al precedente

    // Gruppo Area = 16
    new Quadrato(4),               // Area = 16, Perim = 16
    new Rettangolo(8, 2),          // Area = 16, Perim = 20 ← equivalente

    // Gruppo Area = 18 (tre forme diverse!)
    new Trapezio(8, 4, 3, 5, 5),  // Area = (12×3)/2 = 18, Perim = 22
    new Rombo(9, 4, 5.5),          // Area = (9×4)/2 = 18,  Perim = 22 ← equivalente
    new Parallelogramma(6, 3, 4),  // Area = 18,              Perim = 20 ← equivalente

    // Unici
    new Quadrato(5),               // Area = 25, Perim = 20
    new Rettangolo(3, 5),          // Area = 15, Perim = 16
};

// ─── Menu testuale ──────────────────────────────────────
bool esci = false;

while (!esci)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║     QUADRILATERI EQUIVALENTI        ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║  1. Visualizza tutti i quadrilateri  ║");
    Console.WriteLine("║  2. Verifica quadrilateri equivalenti ║");
    Console.WriteLine("║  0. Esci                             ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("Scelta: ");

    string? scelta = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (scelta)
    {
        case "1":
            VisualizzaTutti(elenco);
            break;

        case "2":
            VerificaEquivalenti(elenco);
            break;

        case "0":
            esci = true;
            Console.WriteLine("Arrivederci! 👋");
            break;

        default:
            Console.WriteLine("❌ Scelta non valida. Riprova.");
            break;
    }

    if (!esci)
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
}

// ═══════════════════════════════════════════════════════════
//  METODI DI SUPPORTO
// ═══════════════════════════════════════════════════════════

/// <summary>
/// 1) Stampa tutti i quadrilateri con Nome, Perimetro, Area, Lati.
/// </summary>
static void VisualizzaTutti(List<Quadrilatero> elenco)
{
    Console.WriteLine($"━━━ TUTTI I QUADRILATERI ({elenco.Count}) ━━━\n");

    for (int i = 0; i < elenco.Count; i++)
    {
        var q = elenco[i];
        Console.WriteLine($"[{i + 1}] {q.Nome}");
        Console.WriteLine($"    Perimetro: {q.Perimetro:F2}");
        Console.WriteLine($"    Area:      {q.Area:F2}");
        Console.WriteLine($"    Lati:      {q.Lato1:F2}, {q.Lato2:F2}, {q.Lato3:F2}, {q.Lato4:F2}");
        Console.WriteLine();
    }
}

/// <summary>
/// 2) Trova e stampa i gruppi di quadrilateri con la STESSA AREA (equivalenti).
/// Usa Math.Round( , 2) per evitare falsi negativi da floating-point.
/// </summary>
static void VerificaEquivalenti(List<Quadrilatero> elenco)
{
    // Raggruppa per area arrotondata a 2 decimali
    var gruppi = elenco
        .GroupBy(q => Math.Round(q.Area, 2))
        .Where(g => g.Count() >= 2)   // solo gruppi con almeno 2 elementi
        .OrderBy(g => g.Key)
        .ToList();

    if (gruppi.Count == 0)
    {
        Console.WriteLine("❌ Nessun quadrilatero equivalente trovato.");
        return;
    }

    int totaleGruppi = gruppi.Count;
    int totaleFig = gruppi.Sum(g => g.Count());

    Console.WriteLine($"━━━ {totaleFig} QUADRILATERI EQUIVALENTI in {totaleGruppi} gruppi ━━━\n");

    foreach (var gruppo in gruppi)
    {
        Console.WriteLine($"🔷 Area = {gruppo.Key:F2} ({gruppo.Count()} quadrilater{(gruppo.Count() == 1 ? "o" : "i")})");
        Console.WriteLine(new string('─', 40));

        foreach (var q in gruppo)
        {
            Console.WriteLine($"   • {q.Nome,-18} Perim: {q.Perimetro,7:F2}");
        }
        Console.WriteLine();
    }

    // Riepilogo: quanti gruppi, quanti equivalenti in totale
    int nonEquivalenti = elenco.Count - totaleFig;
    Console.WriteLine(new string('═', 40));
    Console.WriteLine($"📊 Riepilogo:");
    Console.WriteLine($"   {totaleGruppi} gruppo{(totaleGruppi == 1 ? "" : "i")} di equivalenti");
    Console.WriteLine($"   {totaleFig} quadrilater{(totaleFig == 1 ? "o" : "i")} equivalenti");
    Console.WriteLine($"   {nonEquivalenti} quadrilater{(nonEquivalenti == 1 ? "o" : "i")} non equivalenti");
}
