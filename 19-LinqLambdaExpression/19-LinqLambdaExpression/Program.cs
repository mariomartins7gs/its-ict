// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - LINQ e Lambda
// Studente: Mario Martins
// Data: 09/07/2026

namespace LinqLambdaExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LInQ e uso di funzioni Lambda!");

            //input
            Console.Write("Estremo inferiore: ");
            int inf = Convert.ToInt32(Console.ReadLine());

            int sup;
            //condizione: inf<sup
            do
            {
                Console.Write("Estremo superiore: ");
                sup = Convert.ToInt32(Console.ReadLine());

                if (inf >= sup)
                    Console.WriteLine("Estremo superiore inserito non valido");
                else 
                    break;

            } while (true);

            int tappo;
            do
            {
                Console.Write("Tappo terminatore: ");
                tappo = Convert.ToInt32(Console.ReadLine());

                if (tappo < inf || tappo > sup)
                    Console.WriteLine("Valore inserito non valido");
                else
                    break;

            } while (true);
            
            //uso l'oggetto per generare numeri interi casuali
            Random random = new Random();

            //creo la lista
            var numeri = new List<int>();

            int casuale;
            do
            {
                casuale = random.Next(inf, sup + 1);
                numeri.Add(casuale);
            } while (casuale != tappo);

            Console.WriteLine();
            Console.WriteLine("Caricamento dei dati avvenuta con successo!");
            Console.WriteLine();

            // ── Operazioni con LINQ (lambda / method syntax) ──
            Console.WriteLine("Elenco dei numeri generati");
            Console.WriteLine(string.Join(", ", numeri));
            Console.WriteLine();
            Console.WriteLine($"Numeri generati: {numeri.Count()}");
            Console.WriteLine();
            Console.WriteLine($"Max: {numeri.Max()}");
            Console.WriteLine();
            Console.WriteLine($"Min: {numeri.Min()}");
            Console.WriteLine();
            Console.WriteLine($"Somma: {numeri.Sum()}");

            Console.WriteLine();
            // ── Query expression syntax ──
            var query1 = from positivi in numeri
                         where positivi > 0
                         select positivi;
            Console.WriteLine($"Somma positivi (query): {query1.Sum()}");

            // ── Lambda expression syntax EQUIVALENTE ──
            var positiviLambda = numeri.Where(n => n > 0);
            Console.WriteLine($"Somma positivi (lambda): {positiviLambda.Sum()}");

            Console.WriteLine();
            // ── Query expression syntax ──
            var query2 = from dispari in numeri
                         where dispari % 2 != 0
                         select dispari;
            Console.WriteLine($"Dispari (query): {string.Join(", ", query2)}");

            // ── Lambda expression syntax EQUIVALENTE ──
            var dispariLambda = numeri.Where(n => n % 2 != 0);
            Console.WriteLine($"Dispari (lambda): {string.Join(", ", dispariLambda)}");

            Console.WriteLine();
            // ── Query expression syntax ──
            var query3 = from multipli3 in numeri
                         where multipli3 % 3 == 0
                         select multipli3;
            Console.WriteLine($"Multipli di 3 (query): {query3.Count()} ==> {string.Join(", ", query3)}");

            // ── Lambda expression syntax EQUIVALENTE ──
            var multipli3Lambda = numeri.Where(n => n % 3 == 0);
            Console.WriteLine($"Multipli di 3 (lambda): {multipli3Lambda.Count()} ==> {string.Join(", ", multipli3Lambda)}");
        }
    }
}
