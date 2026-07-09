// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

namespace Martins_Esercitazione1.Models
{
    public class Prodotto
    {
        public string Codice { get; set; }
        public string Denominazione { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int Giacenza { get; set; }

        public override string ToString()
        {
            return $"[{Codice}] {Denominazione} - {Prezzo:F2} euro (disp. {Giacenza})";
        }
    }
}
