// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - LINQ e Oggetti
// Studente: Mario Martins
// Data: 09/07/2026

namespace LinqObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LInQ e gli oggetti!");
            Console.WriteLine();

            var clienti = GeneraClienti(100);

            // ── 1. Codici Fiscali ──
            Console.WriteLine("=== CODICI FISCALI ===");

            // Classico foreach
            Console.WriteLine("Classico con foreach:");
            string txt = string.Empty;
            foreach (var c in clienti)
                txt += (txt.Length != 0 ? ", " : "") + c.CodiceFiscale;
            Console.WriteLine(txt);

            Console.WriteLine();

            // LINQ query
            var q1 = from codfisc in clienti select codfisc.CodiceFiscale;
            Console.WriteLine("LINQ query:");
            Console.WriteLine(string.Join(", ", q1));

            Console.WriteLine();

            // Lambda expression
            var q2 = clienti.Select(x => x.CodiceFiscale).ToList();
            Console.WriteLine("Lambda expression:");
            Console.WriteLine(string.Join(", ", q2));

            Console.WriteLine();

            // ── 2. Nominativi nati a partire dal 1980 ──
            Console.WriteLine("=== CLIENTI NATI DAL 1980 ===");

            // Classico foreach
            Console.WriteLine("Classico con foreach:");
            foreach (var c in clienti)
                if (c.DataNascita.Year >= 1980)
                    Console.WriteLine($"  {c.Nominativo()}");

            Console.WriteLine();

            // LINQ query
            var q3 = from nominativi in clienti
                     where nominativi.DataNascita.Year >= 1980
                     select nominativi.Nominativo();
            Console.WriteLine("LINQ query:");
            Console.WriteLine(string.Join("\n", q3.Select(n => "  " + n)));

            Console.WriteLine();

            // Lambda expression
            var q4 = clienti
                .Where(x => x.DataNascita.Year >= 1980)
                .Select(c => c.Nominativo())
                .ToList();
            Console.WriteLine("Lambda expression:");
            Console.WriteLine(string.Join("\n", q4.Select(n => "  " + n)));

            Console.WriteLine();
            Console.WriteLine($"Totale clienti: {clienti.Count}");
            Console.WriteLine($"Nati dal 1980: {q4.Count}");
        }

        // ── Genera 100 clienti di test ──
        static List<Cliente> GeneraClienti(int quanti)
        {
            string[] nomi = {
                "Mario", "Giulia", "Francesco", "Anna", "Luca", "Sara",
                "Andrea", "Laura", "Marco", "Elena", "Paolo", "Chiara",
                "Alessandro", "Martina", "Davide", "Valentina", "Simone",
                "Federica", "Riccardo", "Ilaria", "Giovanni", "Alice",
                "Lorenzo", "Beatrice", "Fabio", "Camilla", "Stefano",
                "Giorgia", "Roberto", "Arianna", "Claudio", "Veronica",
                "Massimo", "Serena", "Daniele", "Giada", "Alberto",
                "Noemi", "Vincenzo", "Rebecca", "Mattia", "Greta",
                "Tommaso", "Bianca", "Nicolò", "Viola", "Emanuele",
                "Eleonora", "Pietro", "Sofia", "Salvatore", "Maddalena"
            };

            string[] cognomi = {
                "Rossi", "Bianchi", "Ferrari", "Russo", "Colombo",
                "Esposito", "Ricci", "Marino", "Greco", "Bruno",
                "Gallo", "Conti", "Costa", "Mancini", "Barbieri",
                "Fontana", "Rinaldi", "Caruso", "Moretti", "Grassi",
                "Conte", "Pellegrini", "Fabbri", "Martini", "Leone",
                "Guerra", "Palmieri", "Serra", "Sanna", "Testa",
                "De Luca", "Parisi", "Villa", "Sala", "Farina",
                "Rizzi", "Damico", "Ferri", "Marchetti", "Barone",
                "Gatti", "Longo", "Giordano", "Morelli", "Mazza",
                "Monti", "Coppola", "Cattaneo", "Ferrara", "Cavallo"
            };

            Random rng = new Random();
            var clienti = new List<Cliente>(quanti);
            var usati = new HashSet<string>();

            for (int i = 0; i < quanti; i++)
            {
                string nome = nomi[rng.Next(nomi.Length)];
                string cognome = cognomi[rng.Next(cognomi.Length)];
                int anno = rng.Next(1950, 2010);
                int mese = rng.Next(1, 13);
                int giorno = rng.Next(1, 29);
                DateTime dataNascita = new DateTime(anno, mese, giorno);

                string cf = GeneraCF(cognome, nome, anno, mese, giorno, usati);
                usati.Add(cf);

                clienti.Add(new Cliente(cf, cognome, nome, dataNascita));
            }

            return clienti;
        }

        static string GeneraCF(string cognome, string nome, int anno, int mese, int giorno, HashSet<string> usati)
        {
            // Semplice generazione CF: 3 lettere cognome + 3 lettere nome + anno + mese + giorno + codice
            string cognomePulito = cognome.ToUpper().PadRight(3, 'X')[
                ..Math.Min(3, cognome.Length)];
            string nomePulito = nome.ToUpper().PadRight(3, 'X')[
                ..Math.Min(3, nome.Length)];
            string meseLettera = "ABCDEHLMPRST";
            char meseChar = meseLettera[mese - 1];
            // Giorno: per le donne si aggiunge 40 (es. nata il 5 -> 45)
            int giornoCF = giorno;

            string baseCF = $"{cognomePulito}{nomePulito}{(anno % 100):D2}{meseChar}{giornoCF:D2}";

            // Aggiungi suffisso finché unico
            string cf = baseCF;
            int suff = 0;
            while (usati.Contains(cf))
            {
                suff++;
                cf = baseCF + (char)('A' + (suff % 26));
            }

            return cf;
        }
    }
}
