using System.Text;

namespace _12_FileCSV_Scrittura;

public static class ServizioFile
{
    private const string Separatore = ";";

    public static void ScriviPersona(string percorsoFile, Persona persona)
    {
        bool fileEsiste = File.Exists(percorsoFile);

        using var sw = new StreamWriter(percorsoFile, append: true, Encoding.UTF8);

        if (!fileEsiste)
        {
            sw.WriteLine("Cognome;Nome;DataNascita;LuogoNascita;Sesso;Eta");
        }

        sw.WriteLine($"{persona.Cognome}{Separatore}" +
                     $"{persona.Nome}{Separatore}" +
                     $"{persona.DataDiNascita:dd/MM/yyyy}{Separatore}" +
                     $"{persona.LuogoDiNascita}{Separatore}" +
                     $"{persona.Sesso}{Separatore}" +
                     $"{persona.Età}");
    }

    public static List<string> LeggiFile(string percorsoFile)
    {
        var righe = new List<string>();

        if (!File.Exists(percorsoFile))
        {
            Console.WriteLine($"[AVVISO] Il file '{percorsoFile}' non esiste.");
            return righe;
        }

        using var sr = new StreamReader(percorsoFile, Encoding.UTF8);
        string? linea;
        while ((linea = sr.ReadLine()) is not null)
        {
            righe.Add(linea);
        }

        return righe;
    }

    public static void MostraFile(string percorsoFile)
    {
        var righe = LeggiFile(percorsoFile);

        if (righe.Count == 0)
        {
            Console.WriteLine("Nessun dato presente.");
            return;
        }

        Console.WriteLine("\n═══════════════════════════════════════════");
        Console.WriteLine("  📄 Contenuto di Persone.csv");
        Console.WriteLine("═══════════════════════════════════════════");

        foreach (var riga in righe)
        {
            Console.WriteLine($"  {riga}");
        }

        Console.WriteLine("═══════════════════════════════════════════");
    }
}
