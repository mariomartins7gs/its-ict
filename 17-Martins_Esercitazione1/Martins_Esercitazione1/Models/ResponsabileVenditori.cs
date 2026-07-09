// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System.Collections.Generic;

namespace Martins_Esercitazione1.Models
{
    public class ResponsabileVenditori : Venditore
    {
        public List<Venditore> VenditoriSeguiti { get; set; } = new();

        public override double Tredicesima()
        {
            double bonus = 0;
            foreach (Venditore v in VenditoriSeguiti)
                bonus += 0.15 * v.TariffaGiornaliera;

            return (Stipendio * 2) + bonus;
        }

        public override string ToString()
        {
            string info = $"{base.ToString()}\n  Ruolo: Responsabile Venditori";
            info += $"\n  Venditori seguiti: {VenditoriSeguiti.Count}";
            foreach (Venditore v in VenditoriSeguiti)
                info += $"\n    - {v.Nome} {v.Cognome} ({v.Settore})";
            return info;
        }
    }
}
