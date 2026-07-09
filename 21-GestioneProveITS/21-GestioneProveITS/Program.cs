// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Gestione Esami
// Studente: Mario Martins
// Data: 09/07/2026

namespace GestioneProveITS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gestione esami ITS!");
            Console.WriteLine();

            var esami = new List<Esame>
            {
                new Esame { Studente = "Mario Rossi",       ProvaTeorica = 28, ProvaPratica = 30, Colloquio = 27 },
                new Esame { Studente = "Laura Bianchi",     ProvaTeorica = 25, ProvaPratica = 22, Colloquio = 28 },
                new Esame { Studente = "Fabio Verdi",       ProvaTeorica = 30, ProvaPratica = 28, Colloquio = 30 },
                new Esame { Studente = "Sara Romano",       ProvaTeorica = 20, ProvaPratica = 25, Colloquio = 22 },
                new Esame { Studente = "Davide Gallo",      ProvaTeorica = 18, ProvaPratica = 20, Colloquio = 25 },
                new Esame { Studente = "Chiara Neri",       ProvaTeorica = 27, ProvaPratica = 30, Colloquio = 26 },
                new Esame { Studente = "Alessandro Costa",  ProvaTeorica = 22, ProvaPratica = 24, Colloquio = 20 },
                new Esame { Studente = "Giulia Marino",     ProvaTeorica = 30, ProvaPratica = 30, Colloquio = 30 },
            };

            EsamiBiz biz;
            try
            {
                biz = new EsamiBiz(esami);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            int scelta;
            do
            {
                string mnu = "\nScegliere una tra le seguenti operazioni: " +
                    "\n1 - Stampa elenco studenti e valutazioni" +
                    "\n2 - Stampa media degli esami" +
                    "\n3 - Stampa graduatoria" +
                    "\n4 - Esci dal programma" +
                    "\n\nScelta: ";

                Console.Write(mnu);
                scelta = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (scelta)
                {
                    case 1:
                        Console.WriteLine(biz.StampaElenco());
                        break;
                    case 2:
                        Console.WriteLine($"Media esami: {biz.MediaEsami():F2}");
                        break;
                    case 3:
                        Console.WriteLine("GRADUATORIA:");
                        Console.WriteLine(string.Join("\n", biz.Graduatoria().Select(e => $"  {e}")));
                        break;
                    case 4:
                        Console.WriteLine("Programma terminato");
                        break;
                    default:
                        Console.WriteLine("Errore! Scelta non valida");
                        break;
                }
            }
            while (scelta != 4);
        }
    }
}
