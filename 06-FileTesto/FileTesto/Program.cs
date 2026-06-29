using System.IO;

namespace FileTesto;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ESERCIZIO — FileTesto Scrittura ===\n");

        // Crea la cartella C:\file se non esiste
        string folderPath = @"C:\file";
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
