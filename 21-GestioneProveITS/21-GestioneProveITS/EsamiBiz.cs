// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Gestione Esami
// Studente: Mario Martins
// Data: 09/07/2026

namespace GestioneProveITS
{
    internal class EsamiBiz
    {
        public List<Esame> Esami { get; set; }

        public EsamiBiz(List<Esame> esami)
        {
            if (esami == null)
                throw new Exception("Elenco esami non presente");

            if (esami.Count == 0)
                throw new Exception("Elenco esami vuoto");

            Esami = esami;
        }

        public string StampaElenco() => string.Join("\n", Esami);

        public double MediaEsami() => Esami.Average(x => x.VotoFinale());

        public List<Esame> Graduatoria() => Esami.OrderByDescending(x => x.VotoFinale()).ToList();
    }
}
