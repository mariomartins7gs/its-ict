using _12_FileCSV_Scrittura;

Console.WriteLine("═══════════════════════════════════════");
Console.WriteLine("  📝 Inserimento Nuova Persona");
Console.WriteLine("═══════════════════════════════════════");

Console.Write("Cognome . . . . . . . . . : ");
string cognome = Console.ReadLine()?.Trim() ?? "";

Console.Write("Nome . . . . . . . . . . . : ");
string nome = Console.ReadLine()?.Trim() ?? "";

Console.Write("Data di nascita (gg/mm/aaaa) : ");
DateOnly dataNascita = DateOnly.Parse(Console.ReadLine()!.Trim());

Console.Write("Luogo di nascita . . . . . . : ");
string luogo = Console.ReadLine()?.Trim() ?? "";

Console.Write("Sesso (M / F / Altro) . . . . : ");
Sesso sesso = Console.ReadLine()?.Trim().ToUpper() switch
{
    "M" => Sesso.M,
    "F" => Sesso.F,
    _   => Sesso.Altro
};

var persona = new Persona(cognome, nome, dataNascita, luogo, sesso);

Console.WriteLine("\n✅ Persona creata:");
Console.WriteLine($"   {persona}");
Console.WriteLine($"   Età calcolata: {persona.Età} anni");

string percorsoCsv = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "..", "..", "..", "File", "Persone.csv");

percorsoCsv = Path.GetFullPath(percorsoCsv);

ServizioFile.ScriviPersona(percorsoCsv, persona);

Console.WriteLine($"\n💾 Dati salvati in: {percorsoCsv}");

ServizioFile.MostraFile(percorsoCsv);

Console.WriteLine("\nPremi un tasto per uscire...");
Console.ReadKey();
