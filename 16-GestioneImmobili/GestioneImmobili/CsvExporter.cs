using System.Text;

namespace GestioneImmobili;

/// <summary>
/// Esporta elenchi di immobili in file CSV,
/// salvati nella cartella File/ del progetto.
/// </summary>
public static class CsvExporter
{
    /// <summary>
    /// Esporta una lista di immobili in un file CSV con nome personalizzato.
    /// </summary>
    /// <param name="immobili">Lista da esportare.</param>
    /// <param name="nomeFile">Nome del file CSV (es. "ElencoBox.csv").</param>
    /// <returns>Percorso completo del file salvato.</returns>
    public static string EsportaCSV(List<Immobile> immobili, string nomeFile)
    {
        // Assicura che il nome finisca con .csv
        if (!nomeFile.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            nomeFile += ".csv";

        // Crea cartella File/ se non esiste
        string cartella = Path.Combine(Directory.GetCurrentDirectory(), "File");
        Directory.CreateDirectory(cartella);

        string percorso = Path.Combine(cartella, nomeFile);

        var sb = new StringBuilder();

        // Intestazione CSV (delimitatore ; per locale IT)
        sb.AppendLine("Codice;Tipologia;Indirizzo;CAP;Città;SuperficieMq;Proposta€;Dettagli");

        foreach (var imm in immobili)
        {
            string dettagli = imm switch
            {
                Box b          => $"{b.NumeroPostiAuto} posti auto",
                Appartamento a when a is not Villa => $"{a.NumeroVani} vani, {a.NumeroBagni} bagni",
                Villa v        => $"{v.NumeroVani} vani, {v.NumeroBagni} bagni, giardino {v.DimensioneGiardinoMq} mq",
                _              => ""
            };

            // Escape delle virgolette nei campi testo
            string indirizzo = imm.Indirizzo.Replace("\"", "\"\"");
            string città = imm.Città.Replace("\"", "\"\"");

            sb.AppendLine($"{imm.Codice};{imm.Tipologia};\"{indirizzo}\";{imm.CAP};\"{città}\";{imm.SuperficieMq};{imm.PropostaEconomica:N2};{dettagli}");
        }

        File.WriteAllText(percorso, sb.ToString(), Encoding.UTF8);

        return percorso;
    }
}
