using GestioneImmobili;

// ─── Seed: archivio pre-caricato ──────────────────────
var archivio = new ArchivioImmobili();

archivio.Aggiungi(new Box("B-001", "Via Roma 10", "20121", "Milano", 18, 25000m, 1));
archivio.Aggiungi(new Box("B-002", "Via Torino 5", "10123", "Torino", 22, 30000m, 2));

archivio.Aggiungi(new Appartamento("A-001", "Via Dante 15", "20123", "Milano", 80, 180000m, 4, 2));
archivio.Aggiungi(new Appartamento("A-002", "Corso Italia 8", "50123", "Firenze", 65, 145000m, 3, 1));

archivio.Aggiungi(new Villa("V-001", "Via Collina 22", "20145", "Milano", 180, 550000m, 6, 3, 400));
archivio.Aggiungi(new Villa("V-002", "Strada Panoramica 1", "37010", "Verona", 220, 690000m, 7, 4, 800));

// ─── Menu testuale ────────────────────────────────────
bool esci = false;

while (!esci)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║       GESTIONE IMMOBILI              ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║  1. Numero immobili                  ║");
    Console.WriteLine("║  2. Elenco completo                  ║");
    Console.WriteLine("║  3. Elenco Appartamenti              ║");
    Console.WriteLine("║  4. Elenco Ville                     ║");
    Console.WriteLine("║  5. Elenco Box                       ║");
    Console.WriteLine("║  6. Cerca per città                  ║");
    Console.WriteLine("║  7. Scheda dettaglio (codice)        ║");
    Console.WriteLine("║  8. Esporta CSV                      ║");
    Console.WriteLine("║  0. Termina                          ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("Scelta: ");

    string? scelta = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (scelta)
    {
        case "1":  NumeroImmobili(archivio);            break;
        case "2":  ElencaTutti(archivio);               break;
        case "3":  ElencaAppartamenti(archivio);        break;
        case "4":  ElencaVille(archivio);               break;
        case "5":  ElencaBox(archivio);                 break;
        case "6":  CercaPerCittà(archivio);             break;
        case "7":  SchedaDettaglio(archivio);           break;
        case "8":  EsportaCSV(archivio);                break;
        case "0":
            esci = true;
            Console.WriteLine("Arrivederci! 👋");
            break;
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
//  METODI
// ═══════════════════════════════════════════════════════════

/// <summary>1) Numero totale immobili.</summary>
static void NumeroImmobili(ArchivioImmobili archivio)
{
    Console.WriteLine($"📊 Numero totale immobili: {archivio.ConteggioTotale}");
    Console.WriteLine($"{' ',4}• {archivio.ElencaBox().Count} Box");
    Console.WriteLine($"{' ',4}• {archivio.ElencaAppartamenti().Count} Appartamenti");
    Console.WriteLine($"{' ',4}• {archivio.ElencaVille().Count} Ville");
}

/// <summary>2) Elenco completo immobili.</summary>
static void ElencaTutti(ArchivioImmobili archivio)
{
    var tutti = archivio.ElencaTutti();
    if (tutti.Count == 0) { Console.WriteLine("📭 Nessun immobile."); return; }

    Console.WriteLine($"━━━ TUTTI GLI IMMOBILI ({tutti.Count}) ━━━\n");
    foreach (var imm in tutti)
        Console.WriteLine($"   {imm}\n");
}

/// <summary>3) Elenco appartamenti (escluse ville).</summary>
static void ElencaAppartamenti(ArchivioImmobili archivio)
{
    var appartamenti = archivio.ElencaAppartamenti();
    if (appartamenti.Count == 0) { Console.WriteLine("📭 Nessun appartamento."); return; }

    Console.WriteLine($"━━━ APPARTAMENTI ({appartamenti.Count}) ━━━\n");
    foreach (var a in appartamenti)
        Console.WriteLine($"   {a}\n");
}

/// <summary>4) Elenco ville.</summary>
static void ElencaVille(ArchivioImmobili archivio)
{
    var ville = archivio.ElencaVille();
    if (ville.Count == 0) { Console.WriteLine("📭 Nessuna villa."); return; }

    Console.WriteLine($"━━━ VILLE ({ville.Count}) ━━━\n");
    foreach (var v in ville)
        Console.WriteLine($"   {v}\n");
}

/// <summary>5) Elenco box.</summary>
static void ElencaBox(ArchivioImmobili archivio)
{
    var box = archivio.ElencaBox();
    if (box.Count == 0) { Console.WriteLine("📭 Nessun box."); return; }

    Console.WriteLine($"━━━ BOX ({box.Count}) ━━━\n");
    foreach (var b in box)
        Console.WriteLine($"   {b}\n");
}

/// <summary>6) Cerca per città.</summary>
static void CercaPerCittà(ArchivioImmobili archivio)
{
    Console.Write("Inserisci città: ");
    string? città = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(città))
    {
        Console.WriteLine("❌ Inserisci un nome di città valido.");
        return;
    }

    var risultati = archivio.CercaPerCittà(città);

    if (risultati.Count == 0)
    {
        Console.WriteLine($"❌ Nessun immobile a {città}.");
        return;
    }

    Console.WriteLine($"━━━ IMMOBILI A {città.ToUpper()} ({risultati.Count}) ━━━\n");
    foreach (var imm in risultati)
        Console.WriteLine($"   {imm}\n");
}

/// <summary>7) Scheda dettaglio per codice.</summary>
static void SchedaDettaglio(ArchivioImmobili archivio)
{
    Console.Write("Inserisci codice immobile: ");
    string? codice = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(codice))
    {
        Console.WriteLine("❌ Inserisci un codice valido.");
        return;
    }

    var immobile = archivio.CercaPerCodice(codice);

    if (immobile is null)
    {
        Console.WriteLine($"❌ Nessun immobile con codice \"{codice}\".");
        return;
    }

    Console.WriteLine($"━━━ SCHEDA DETTAGLIO — {immobile.Codice} ━━━\n");
    Console.WriteLine($"  Tipologia:       {immobile.Tipologia}");
    Console.WriteLine($"  Indirizzo:       {immobile.Indirizzo}");
    Console.WriteLine($"  CAP:             {immobile.CAP}");
    Console.WriteLine($"  Città:           {immobile.Città}");
    Console.WriteLine($"  Superficie:      {immobile.SuperficieMq} mq");
    Console.WriteLine($"  Proposta €:      {immobile.PropostaEconomica:N2}");
    Console.WriteLine($"  Dettagli specifici:");

    switch (immobile)
    {
        case Box b:
            Console.WriteLine($"     Posti auto:    {b.NumeroPostiAuto}");
            break;
        case Villa v:
            Console.WriteLine($"     Vani:          {v.NumeroVani}");
            Console.WriteLine($"     Bagni:         {v.NumeroBagni}");
            Console.WriteLine($"     Giardino:      {v.DimensioneGiardinoMq} mq");
            break;
        case Appartamento a:
            Console.WriteLine($"     Vani:          {a.NumeroVani}");
            Console.WriteLine($"     Bagni:         {a.NumeroBagni}");
            break;
    }
}

/// <summary>8) Esporta CSV.</summary>
static void EsportaCSV(ArchivioImmobili archivio)
{
    Console.WriteLine("━━━ ESPORTA CSV ━━━\n");
    Console.WriteLine("Scegli tipo di elenco da esportare:");
    Console.WriteLine("  1. Box");
    Console.WriteLine("  2. Appartamenti");
    Console.WriteLine("  3. Ville");
    Console.WriteLine("  4. Tutti gli immobili");
    Console.Write("Scelta: ");

    string? tipo = Console.ReadLine()?.Trim();

    List<Immobile> daEsportare = tipo switch
    {
        "1" => archivio.ElencaBox().Cast<Immobile>().ToList(),
        "2" => archivio.ElencaAppartamenti().Cast<Immobile>().ToList(),
        "3" => archivio.ElencaVille().Cast<Immobile>().ToList(),
        "4" => archivio.ElencaTutti(),
        _   => new List<Immobile>()
    };

    if (daEsportare.Count == 0)
    {
        Console.WriteLine("❌ Nessun immobile da esportare o scelta non valida.");
        return;
    }

    string nomeDefault = tipo switch
    {
        "1" => "ElencoBox.csv",
        "2" => "ElencoAppartamenti.csv",
        "3" => "ElencoVille.csv",
        "4" => "ElencoImmobili.csv",
        _   => "Elenco.csv"
    };

    Console.Write($"\nNome file (default: {nomeDefault}): ");
    string? nomeFile = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(nomeFile)) nomeFile = nomeDefault;

    string percorso = CsvExporter.EsportaCSV(daEsportare, nomeFile);

    Console.WriteLine($"\n✅ Esportato {daEsportare.Count} immob{(daEsportare.Count == 1 ? "ile" : "ili")} in:");
    Console.WriteLine($"   📁 {percorso}");
}
