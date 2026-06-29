namespace FileTesto;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ESERCIZIO — FileTesto Scrittura ===\n");

        // Usa la cartella Documenti dell'utente (nessun permesso admin richiesto)
        string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string folderPath = Path.Combine(docsPath, "file");
        string filePath = Path.Combine(folderPath, "Frase.txt");

        Directory.CreateDirectory(folderPath);
        Console.WriteLine($"📁 Cartella: {folderPath}");

        // Input frase dall'utente
        Console.Write("Inserisci la frase da salvare: ");
        string? frase = Console.ReadLine();

        if (string.IsNullOrEmpty(frase))
        {
            Console.WriteLine("❌ Nessuna frase inserita.");
            return;
        }

        // Scrittura su file con StreamWriter
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(frase);
        }

        Console.WriteLine($"✅ Frase salvata in: {filePath}");

        // Lettura di verifica con StreamReader
        Console.WriteLine("\n📖 Lettura file di verifica:");
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? contenuto = sr.ReadToEnd();
            Console.WriteLine(contenuto);
        }

        Console.WriteLine("\n--- FINE ---");
    }
}
