namespace FileTestoLettura;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ESERCIZIO — FileTesto Lettura ===\n");

        string filePath = @"C:\file\Frase.txt";

        // Controllo se il file esiste
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"❌ File non trovato: {filePath}");
            Console.WriteLine("Esegui prima l'esercizio di scrittura per crearlo.");
            return;
        }

        // 1) Lettura con StreamReader
        Console.WriteLine("📖 Contenuto del file:");
        Console.WriteLine(new string('─', 40));

        using (StreamReader sr = new StreamReader(filePath))
        {
            string? line;
            int lineNum = 0;
            while ((line = sr.ReadLine()) != null)
            {
                lineNum++;
                Console.WriteLine($"   {lineNum,2}. {line}");
            }
        }

        Console.WriteLine(new string('─', 40));

        // 2) Rilettura per analisi statistica
        string fullText = File.ReadAllText(filePath);
        string[] words = fullText.Split([' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':'],
                                       StringSplitOptions.RemoveEmptyEntries);

        int charCount = fullText.Length;
        int wordCount = words.Length;
        int lineCount = fullText.Split('\n').Length;

        Console.WriteLine($"\n📊 Statistiche del file:");
        Console.WriteLine($"   • Caratteri:    {charCount}");
        Console.WriteLine($"   • Parole:       {wordCount}");
        Console.WriteLine($"   • Righe:        {lineCount}");

        // 3) Parola più lunga
        if (words.Length > 0)
        {
            string longest = words.OrderByDescending(w => w.Length).First();
            Console.WriteLine($"   • Parola più lunga: \"{longest}\" ({longest.Length} caratteri)");
        }

        Console.WriteLine("\n--- FINE ---");
    }
}
