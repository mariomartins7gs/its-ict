// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - LINQ
// Studente: Mario Martins
// Data: 09/07/2026

string[] nomi = {
    "Pietro", "Mario", "Giulia", "Francesca", "Laura", "Piero",
    "antonio", "Vito", "Antonella", "Giada", "Rossella", "Simone",
    "Saverio", "Rosa", "Michela", "Andrea", "Mattia", "Ilaria",
    "Alex", "Vanessa", "Ciro", "Elia", "Giuditta", "Stefano",
    "Alessandro", "Carlo", "Drusilla", "Dorothea", "Lucilla",
    "Marialuisa", "Marcolino", "Pietrangelo", "Ermenegildo"
};

bool esci = false;
do
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════╗");
    Console.WriteLine("║       LINQ - STRINGHE        ║");
    Console.WriteLine("╚══════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine("1] Elenco completo");
    Console.WriteLine("2] Nomi che iniziano con A");
    Console.WriteLine("3] Nomi in ordine alfabetico crescente");
    Console.WriteLine("4] Nomi con lunghezza 7, ordine decrescente");
    Console.WriteLine("5] Nomi tutti in maiuscolo");
    Console.WriteLine("6] Termina");
    Console.WriteLine();
    Console.Write("Scegli: ");
    string input = Console.ReadLine()?.Trim() ?? "";

    Console.WriteLine();

    switch (input)
    {
        case "1": Query1(nomi); break;
        case "2": Query2(nomi); break;
        case "3": Query3(nomi); break;
        case "4": Query4(nomi); break;
        case "5": Query5(nomi); break;
        case "6": esci = true; Console.WriteLine("Arrivederci!"); break;
        default: Console.WriteLine("Opzione non valida."); break;
    }

    if (!esci)
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
} while (!esci);

// ── Query 1: Elenco completo ──
static void Query1(string[] nomi)
{
    Console.WriteLine("── ELENCO COMPLETO ──");
    var risultati = from n in nomi select n;
    foreach (var n in risultati)
        Console.WriteLine($"  {n}");
    Console.WriteLine($"\nTotale: {risultati.Count()} nomi");
}

// ── Query 2: Nomi che iniziano con A (case-insensitive) ──
static void Query2(string[] nomi)
{
    Console.WriteLine("── NOMI CHE INIZIANO CON A ──");
    var risultati = from n in nomi
                    where n.StartsWith("A", StringComparison.OrdinalIgnoreCase)
                    select n;
    foreach (var n in risultati)
        Console.WriteLine($"  {n}");
    Console.WriteLine($"\nTotale: {risultati.Count()} nomi");
}

// ── Query 3: Ordine alfabetico crescente ──
static void Query3(string[] nomi)
{
    Console.WriteLine("── NOMI IN ORDINE ALFABETICO CRESCENTE ──");
    var risultati = from n in nomi
                    orderby n ascending
                    select n;
    foreach (var n in risultati)
        Console.WriteLine($"  {n}");
    Console.WriteLine($"\nTotale: {risultati.Count()} nomi");
}

// ── Query 4: Lunghezza 7, ordine decrescente ──
static void Query4(string[] nomi)
{
    Console.WriteLine("── NOMI CON LUNGHEZZA 7, ORDINE DECRESCENTE ──");
    var risultati = from n in nomi
                    where n.Length == 7
                    orderby n descending
                    select n;
    foreach (var n in risultati)
        Console.WriteLine($"  {n}");
    Console.WriteLine($"\nTotale: {risultati.Count()} nomi");
}

// ── Query 5: Tutti in maiuscolo ──
static void Query5(string[] nomi)
{
    Console.WriteLine("── NOMI TUTTI IN MAIUSCOLO ──");
    var risultati = from n in nomi
                    select n.ToUpper();
    foreach (var n in risultati)
        Console.WriteLine($"  {n}");
    Console.WriteLine($"\nTotale: {risultati.Count()} nomi");
}
