using _13_VenditaProdotti;

Console.WriteLine("Vendita Prodotti!");

// Riempire una lista di prodotti (Alimentari e Non alimentari) aiutandosi con l'AI
List<Prodotto> lista = new List<Prodotto>()
{
    new ProdottoAlimentare(1, "Latte", 1.50m, 100, new DateOnly(2026, 6, 1), new DateOnly(2026, 7, 5)),
    new ProdottoAlimentare(2, "Pane", 2.00m, 50, new DateOnly(2026, 6, 28), new DateOnly(2026, 7, 3)),
    new ProdottoAlimentare(3, "Yogurt", 3.20m, 80, new DateOnly(2026, 6, 15), new DateOnly(2026, 7, 15)),
    new ProdottoNonAlimentare(4, "Bottiglia", 5.00m, 200, new DateOnly(2026, 1, 10), 
        new List<Materiale> { new Materiale("Plastica", 80), new Materiale("Alluminio", 20) }),
    new ProdottoNonAlimentare(5, "Sedia", 45.00m, 30, new DateOnly(2026, 3, 20), 
        new List<Materiale> { new Materiale("Legno", 70), new Materiale("Metallo", 30) }),
    new ProdottoNonAlimentare(6, "Tazza", 8.50m, 150, new DateOnly(2026, 2, 15), 
        new List<Materiale> { new Materiale("Ceramica", 100) })
};

List<Materiale> listb = new List<Materiale>();
listb.Add(new Materiale("Plastica", 80));

string menu = "Scegli una tra le seguenti opzioni:" +
    "\n1 - visualizzare l'elenco dei prodotti" +
    "\n2 - visualizzare l'elenco dei prodotti in scadenza (minore di 10 giorni)" +
    "\n3 - visualizzare l'elenco delle materie prime con cui è fatto il prodotto" +
    "\n0 - termina il programma" +
    "\nScelta: ";

int scelta;
do
{
    Console.WriteLine("\n═══════════════════════════════════════");
    Console.Write(menu);
    
    if (!int.TryParse(Console.ReadLine(), out scelta))
    {
        Console.WriteLine("⚠️ Inserisci un numero valido!");
        continue;
    }

    switch (scelta)
    {
        case 1:
            Console.WriteLine("\n📦 Elenco prodotti:");
            foreach (var p in lista)
                Console.WriteLine($"  → {p}");
            break;

        case 2:
            Console.WriteLine("\n⏰ Prodotti in scadenza (< 10 giorni):");
            var inScadenza = lista.OfType<ProdottoAlimentare>().Where(p => p.InScadenza()).ToList();
            if (inScadenza.Count == 0)
                Console.WriteLine("  Nessun prodotto in scadenza.");
            else
                foreach (var p in inScadenza)
                    Console.WriteLine($"  → {p}");
            break;

        case 3:
            Console.WriteLine("\n🔧 Materie prime (prodotti non alimentari):");
            foreach (var p in lista.OfType<ProdottoNonAlimentare>())
                Console.WriteLine($"  → {p.Nome}: {string.Join(", ", p.Materiali)}");
            break;

        case 0:
            Console.WriteLine("\n👋 Programma terminato.");
            break;

        default:
            Console.WriteLine("⚠️ Scelta non valida!");
            break;
    }

} while (scelta != 0);
