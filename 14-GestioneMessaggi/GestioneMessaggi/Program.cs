using GestioneMessaggi;

// ─── Seed: archivio pre-caricato con messaggi di esempio ──
var archivio = new ArchivioMessaggi();

archivio.Inserisci(new Messaggio(
    "Mario Rossi", "Luca Bianchi",
    "Aggiornamento progetto",
    "Il progetto procede secondo i tempi previsti. Domani consegna.",
    new DateTime(2026, 7, 1, 14, 30, 0),
    Priorita.Alta));

archivio.Inserisci(new Messaggio(
    "Giulia Verdi", "Mario Rossi",
    "Richiesta ferie",
    "Vorrei prenotare una settimana di ferie ad agosto.",
    new DateTime(2026, 7, 3, 9, 15, 0),
    Priorita.Normale));

archivio.Inserisci(new Messaggio(
    "Luca Bianchi", "Giulia Verdi",
    "Buon compleanno!",
    "Tanti auguri di buon compleanno! 🎂",
    new DateTime(2026, 7, 5, 8, 0, 0),
    Priorita.Bassa));

archivio.Inserisci(new Messaggio(
    "Sistema Notifiche", "Tutti gli utenti",
    "Manutenzione programmata",
    "Il sistema sarà offline il 10/07 dalle 02:00 alle 04:00.",
    new DateTime(2026, 7, 6, 18, 0, 0),
    Priorita.Alta));

archivio.Inserisci(new Messaggio(
    "Mario Rossi", "Sistema Notifiche",
    "Conferma ricezione",
    "Ricevuta comunicazione manutenzione. Grazie.",
    new DateTime(2026, 7, 6, 19, 45, 0),
    Priorita.Normale));

// ─── Menu testuale ──────────────────────────────────────
bool esci = false;

while (!esci)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║       GESTIONE MESSAGGI              ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║  1. Inserisci nuovo messaggio        ║");
    Console.WriteLine("║  2. Cerca per mittente               ║");
    Console.WriteLine("║  3. Cerca per destinatario           ║");
    Console.WriteLine("║  4. Conta messaggi dopo data         ║");
    Console.WriteLine("║  5. Visualizza tutti i messaggi      ║");
    Console.WriteLine("║  0. Esci                             ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("Scelta: ");

    string? scelta = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (scelta)
    {
        // ─── 1. Inserisci ────────────────────────────
        case "1":
            InserisciMessaggio(archivio);
            break;

        // ─── 2. Cerca per mittente ───────────────────
        case "2":
            CercaMessaggi("mittente", archivio.CercaPerMittente);
            break;

        // ─── 3. Cerca per destinatario ───────────────
        case "3":
            CercaMessaggi("destinatario", archivio.CercaPerDestinatario);
            break;

        // ─── 4. Conta dopo data ─────────────────────
        case "4":
            ContaDopoData(archivio);
            break;

        // ─── 5. Visualizza tutti ─────────────────────
        case "5":
            VisualizzaTutti(archivio);
            break;

        // ─── 0. Esci ─────────────────────────────────
        case "0":
            esci = true;
            Console.WriteLine("Arrivederci! 👋");
            break;

        // ─── Default ─────────────────────────────────
        default:
            Console.WriteLine("❌ Scelta non valida. Riprova.");
            break;
    }

    if (!esci)
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
}

// ═══════════════════════════════════════════════════════════
//  METODI DI SUPPORTO
// ═══════════════════════════════════════════════════════════

/// <summary>
/// GUIDA ALL'INSERIMENTO di un nuovo messaggio.
/// </summary>
static void InserisciMessaggio(ArchivioMessaggi archivio)
{
    Console.WriteLine("━━━ INSERISCI NUOVO MESSAGGIO ━━━\n");

    Console.Write("Mittente:      ");
    string mittente = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Destinatario:  ");
    string destinatario = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Oggetto:       ");
    string oggetto = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Testo:         ");
    string testo = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Priorità (0=Bassa, 1=Normale, 2=Alta): ");
    int prioritaInput = int.TryParse(Console.ReadLine(), out int p) ? p : 1;
    Priorita priorita = prioritaInput switch
    {
        0 => Priorita.Bassa,
        2 => Priorita.Alta,
        _ => Priorita.Normale
    };

    var messaggio = new Messaggio(mittente, destinatario, oggetto, testo, DateTime.Now, priorita);
    archivio.Inserisci(messaggio);

    Console.WriteLine($"\n✅ Messaggio inserito con successo! (Totale: {archivio.TotaleMessaggi})");
}

/// <summary>
/// GUIDA ALLA RICERCA — riutilizzabile per mittente e destinatario.
/// </summary>
static void CercaMessaggi(string campo, Func<string, List<Messaggio>> ricerca)
{
    Console.Write($"Cerca per {campo}: ");
    string? valore = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(valore))
    {
        Console.WriteLine("❌ Inserisci un valore valido.");
        return;
    }

    var risultati = ricerca(valore);

    Console.WriteLine(risultati.Count == 0
        ? $"❌ Nessun messaggio trovato per {campo} \"{valore}\"."
        : $"━━━ {risultati.Count} messagg{(risultati.Count == 1 ? "io" : "i")} trovato{(risultati.Count == 1 ? "" : "i")} ━━━");

    foreach (var msg in risultati)
        Console.WriteLine($"\n{msg}\n");
}

/// <summary>
/// GUIDA AL CONTEGGIO dopo una certa data.
/// </summary>
static void ContaDopoData(ArchivioMessaggi archivio)
{
    Console.Write("Inserisci data (gg/MM/yyyy): ");
    string? input = Console.ReadLine()?.Trim();

    if (!DateOnly.TryParseExact(input, "dd/MM/yyyy", out DateOnly data))
    {
        Console.WriteLine("❌ Formato data non valido. Usa gg/MM/yyyy (es. 01/07/2026).");
        return;
    }

    int count = archivio.ContaDopoData(data);
    Console.WriteLine(count == 0
        ? $"📭 Nessun messaggio dopo il {data:dd/MM/yyyy}."
        : $"📬 {count} messagg{(count == 1 ? "io" : "i")} dopo il {data:dd/MM/yyyy}.");
}

/// <summary>
/// GUIDA ALLA VISUALIZZAZIONE di tutti i messaggi.
/// </summary>
static void VisualizzaTutti(ArchivioMessaggi archivio)
{
    var tutti = archivio.ElencaTutti();

    if (tutti.Count == 0)
    {
        Console.WriteLine("📭 Nessun messaggio presente nell'archivio.");
        return;
    }

    Console.WriteLine($"━━━ TUTTI I MESSAGGI ({tutti.Count}) ━━━\n");
    foreach (var msg in tutti)
    {
        Console.WriteLine(msg.ToSummary());
    }
    Console.WriteLine($"\n─── {tutti.Count} messagg{(tutti.Count == 1 ? "io" : "i")} in totale ───");
}
