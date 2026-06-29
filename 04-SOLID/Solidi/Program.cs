namespace Solidi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solidi!");

            //Link a tabella per pesi specifici:
            //https://,oppo.it/tabelle/pesi_specifici.html

            // link al file per il calcolo dei volumi: 
            //https://www.matematika.it/pubblica/allegati/50/Volumi_figure_solide_1_3.pdf

            double pesoSpecificoAcciaio = 7.85;

            Solido s;
            //s = new Solido(pesoSpecificoAcciaio);

            Cubo c1 = new Cubo(pesoSpecificoAcciaio, 2);
            Console.WriteLine(c1);

        }
    }
}
