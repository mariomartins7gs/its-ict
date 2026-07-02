using _12_FileCSV_Lettura;

Console.WriteLine("═══════════════════════════════════════");
Console.WriteLine("  📖 Lettura Persone da CSV");
Console.WriteLine("═══════════════════════════════════════");

string percorsoCsv = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "..", "..", "..", "File", "Persone.csv");

percorsoCsv = Path.GetFullPath(percorsoCsv);

Console.WriteLine($"\n🔍 Cerco file: {percorsoCsv}\n");

var persone = ServizioFile.LeggiPersone(percorsoCsv);

if (persone.Count == 0)
{
    Console.WriteLine("Nessuna persona trovata nel file.");
}
else
{
    Console.WriteLine($"✅ Trovate {persone.Count} persone:\n");

    foreach (var persona in persone)
    {
        Console.WriteLine($"  → {persona}");
    }
}

Console.WriteLine("\n═══════════════════════════════════════");
Console.WriteLine("\nPremi INVIO per uscire...");
Console.ReadLine();
