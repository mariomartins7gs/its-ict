// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Gestione Esami
// Studente: Mario Martins
// Data: 09/07/2026

namespace GestioneProveITS
{
    internal class Esame
    {
        public string Studente { get; set; } = string.Empty;
        public int ProvaTeorica { get; set; }
        public int ProvaPratica { get; set; }
        public int Colloquio { get; set; }

        public int VotoFinale() => ProvaTeorica + ProvaPratica + Colloquio;

        public override string ToString()
        {
            return $"{Studente,-20} Teorica: {ProvaTeorica,2}  Pratica: {ProvaPratica,2}  Colloquio: {Colloquio,2}  TOT: {VotoFinale(),2}";
        }
    }
}
