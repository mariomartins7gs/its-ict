// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System.Collections.Generic;

namespace Martins_Esercitazione1.Models
{
    public class CapoOfficina : Meccanico
    {
        public List<Ordine> OrdiniGestiti { get; set; } = new();

        public override double Tredicesima()
        {
            double importoOrdini = 0;
            foreach (Ordine o in OrdiniGestiti)
                importoOrdini += o.Totale();

            return (Stipendio * 2) + (0.05 * importoOrdini);
        }

        public override string ToString()
        {
            string info = $"{base.ToString()}\n  Ruolo: Capo Officina";
            info += $"\n  Ordini gestiti: {OrdiniGestiti.Count}";
            foreach (Ordine o in OrdiniGestiti)
                info += $"\n    - {o.Codice} ({o.Data:dd/MM/yyyy}) - Totale: {o.Totale():F2} euro";
            return info;
        }
    }
}
