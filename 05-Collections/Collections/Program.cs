namespace Collections;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# COLLECTIONS ===\n");

        // ──────────────────────────────────────────────
        // ESERCIZIO 1 — ListaGenerica
        // ──────────────────────────────────────────────
        Console.WriteLine("━━━ ESERCIZIO 1 — ListaGenerica ━━━\n");

        // Creare una lista di oggetti generici
        List<object> lista = new List<object>
        {
            42,          // int
            -7,          // int
            1024,        // int
            3.14,        // double
            2.718,       // double
            "Mela",      // string
            "Banana",    // string
            "Ciliegia",  // string
            "Dattero",   // string
            true         // bool
        };

        // 1) Visualizzare tutti gli elementi
        Console.WriteLine("1) Tutti gli elementi della lista:");
        for (int i = 0; i < lista.Count; i++)
            Console.WriteLine($"   [{i}] {lista[i]} ({(lista[i] is double ? "double" : lista[i].GetType().Name)})");

        // 2) Numero attuale di elementi
        Console.WriteLine($"\n2) Numero elementi: {lista.Count}");

        // 3) Solo i dati di tipo stringa
        Console.WriteLine("\n3) Elementi di tipo stringa:");
        var stringhe = lista.OfType<string>();
        foreach (var s in stringhe)
            Console.WriteLine($"   - {s}");

        Console.WriteLine("\n────────────────────────────────────\n");

        // ──────────────────────────────────────────────
        // ESERCIZIO 2 — ListaStudenti
        // ──────────────────────────────────────────────
        Console.WriteLine("━━━ ESERCIZIO 2 — ListaStudenti ━━━\n");

        List<Studente> elenco = new List<Studente>
        {
            new(){Matricola=12346, Nome="Diego",     Cognome="De Lillo",     Email="diego.delillo@its.net",     Classe="Mobile Developer"},
            new(){Matricola=12356, Nome="Marta",     Cognome="De Paoli",     Email="marta.depaoli@its.net",     Classe="Mobile Developer"},
            new(){Matricola=12126, Nome="Carlo",     Cognome="De Carlo",     Email="carlo.decarlo@its.net",     Classe="Mobile Developer"},
            new(){Matricola=12345, Nome="Pietro",    Cognome="De Lillo",     Email="pietro.delillo@its.net",    Classe="Cloud Specialist"},
            new(){Matricola=12350, Nome="Laura",     Cognome="De Paoli",     Email="laura.depaoli@its.net",     Classe="Backend Developer"},
            new(){Matricola=12121, Nome="Giulia",    Cognome="De Carlo",     Email="giulia.decarlo@its.net",    Classe="Cloud Specialist"},
            new(){Matricola=11123, Nome="Roberto",   Cognome="Di Freud",     Email="roberto.difreud@its.net",   Classe="Backend Developer"},
            new(){Matricola=10256, Nome="Andrea",    Cognome="Di Leo",       Email="andrea.dileo@its.net",      Classe="Cloud Specialist"},
            new(){Matricola=11345, Nome="Laura",     Cognome="De Laurentis", Email="laura.delaurentis@its.net",  Classe="Backend Developer"},
            new(){Matricola=11350, Nome="Laura",     Cognome="De Giovanni",  Email="laura.degiovanni@its.net",  Classe="Cloud Specialist"},
            new(){Matricola=10121, Nome="Roberto",   Cognome="Vigna",        Email="roberto.vigna@its.net",     Classe="Cloud Specialist"},
            new(){Matricola=13123, Nome="Roberto",   Cognome="Di Pinto",     Email="roberto.dipinto@its.net",   Classe="Backend Developer"},
            new(){Matricola=11256, Nome="Andrea",    Cognome="Scotto",       Email="andrea.scotto@its.net",     Classe="Cloud Specialist"}
        };

        // Menu testuale
        bool esci = false;
        while (!esci)
        {
            Console.WriteLine("\n┌──────────────────────────────────────────┐");
            Console.WriteLine("│          MENÙ LISTA STUDENTI            │");
            Console.WriteLine("├──────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Visualizza elenco studenti           │");
            Console.WriteLine("│ 2. Dettaglio studente (per matricola)   │");
            Console.WriteLine("│ 3. Studenti per cognome                 │");
            Console.WriteLine("│ 4. Studenti per classe                  │");
            Console.WriteLine("│ 0. Esci                                 │");
            Console.WriteLine("└──────────────────────────────────────────┘");
            Console.Write("Scelta: ");

            string? input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    // 1) Visualizzare l'elenco degli studenti
                    Console.WriteLine("--- ELENCO STUDENTI ---");
                    foreach (var s in elenco)
                        Console.WriteLine($"   {s}");
                    break;

                case "2":
                    // 2) Dettaglio per matricola
                    Console.Write("Inserisci matricola: ");
                    if (int.TryParse(Console.ReadLine(), out int mat))
                    {
                        var trovato = elenco.FirstOrDefault(s => s.Matricola == mat);
                        if (trovato is not null)
                            Console.WriteLine($"   {trovato}");
                        else
                            Console.WriteLine("   ❌ Matricola non trovata.");
                    }
                    else
                    {
                        Console.WriteLine("   ❌ Matricola non valida.");
                    }
                    break;

                case "3":
                    // 3) Studenti per cognome
                    Console.Write("Inserisci cognome: ");
                    string? cognome = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(cognome))
                    {
                        var perCognome = elenco.Where(s =>
                            s.Cognome.Contains(cognome, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (perCognome.Count > 0)
                        {
                            Console.WriteLine($"--- Studenti con cognome \"{cognome}\" ({perCognome.Count}) ---");
                            foreach (var s in perCognome)
                                Console.WriteLine($"   {s}");
                        }
                        else
                        {
                            Console.WriteLine("   ❌ Nessuno studente trovato con questo cognome.");
                        }
                    }
                    break;

                case "4":
                    // 4) Studenti per classe
                    Console.Write("Inserisci classe: ");
                    string? classe = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(classe))
                    {
                        var perClasse = elenco.Where(s =>
                            s.Classe.Contains(classe, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (perClasse.Count > 0)
                        {
                            Console.WriteLine($"--- Studenti della classe \"{classe}\" ({perClasse.Count}) ---");
                            foreach (var s in perClasse)
                                Console.WriteLine($"   {s}");
                        }
                        else
                        {
                            Console.WriteLine("   ❌ Nessuno studente trovato in questa classe.");
                        }
                    }
                    break;

                case "0":
                    esci = true;
                    Console.WriteLine("Arrivederci! 👋");
                    break;

                default:
                    Console.WriteLine("   ❌ Scelta non valida. Riprova.");
                    break;
            }
        }
    }
}
