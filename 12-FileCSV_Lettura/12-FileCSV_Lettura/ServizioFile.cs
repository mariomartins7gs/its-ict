using System.Text;

namespace _12_FileCSV_Lettura;

public static class ServizioFile
{
    private const string Separatore = ";";

    public static List<Persona> LeggiPersone(string percorsoFile)
    {
        var persone = new List<Persona>();

        if (!File.Exists(percorsoFile))
        {
            Console.WriteLine($"[ERRORE] Il file '{percorsoFile}' non esiste.");
            return persone;
        }

        using var sr = new StreamReader(percorsoFile, Encoding.UTF8);

        string? linea;
        bool primaRiga = true;

        while ((linea = sr.ReadLine()) is not null)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            // Salta l'header
            if (primaRiga)
            {
                primaRiga = false;
                continue;
            }

            string[] colonne = linea.Split(Separatore);

            if (colonne.Length != 6)
            {
                Console.WriteLine($"[AVVISO] Riga ignorata (formato errato): {linea}");
                continue;
            }

            string cognome = colonne[0];
            string nome = colonne[1];
            DateOnly dataNascita = DateOnly.ParseExact(colonne[2], "dd/MM/yyyy");
            string luogo = colonne[3];
            Sesso sesso = Enum.Parse<Sesso>(colonne[4]);

            // Età non la leggiamo dal CSV — la calcola il modello
            var persona = new Persona(cognome, nome, dataNascita, luogo, sesso);
            persone.Add(persona);
        }

        return persone;
    }
}
