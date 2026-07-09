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

            var clienti = new List<Cliente> {
                new Cliente("VRDFBA76A01L219J", "Verdi",   "Fabio",   DateTime.Parse("01/01/1976")),
                new Cliente("BNCMRA80L15C627M", "Bianchi", "Mario",   DateTime.Parse("15/07/1980")),
                new Cliente("MNNLRA91G52F335K", "Mannino", "Laura",   DateTime.Parse("12/06/1991")),
                new Cliente("RMTNTN58B05E050T", "Romito",  "Antonio", DateTime.Parse("05/02/1958"))
            };

            // ── 1. Codici Fiscali ──
            Console.WriteLine("=== CODICI FISCALI ===");

            // LINQ query
            var q1 = from codfisc in clienti
                     select codfisc.CodiceFiscale;
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
    }
}
