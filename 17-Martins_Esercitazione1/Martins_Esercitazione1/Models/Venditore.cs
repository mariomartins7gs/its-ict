// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System;

namespace Martins_Esercitazione1.Models
{
    public enum SettoreVendita { Auto, Moto }

    public class Venditore : Persona
    {
        public SettoreVendita Settore { get; set; }

        public double TariffaGiornaliera => Stipendio / 30.0;

        public override double Tredicesima()
        {
            return Stipendio * 1.91;
        }

        public Venditore Clone()
        {
            return new Venditore
            {
                Nome = this.Nome,
                Cognome = this.Cognome,
                Stipendio = this.Stipendio,
                Settore = this.Settore
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()} - Settore: {Settore} - Tariffa giornaliera: {TariffaGiornaliera:F2} euro";
        }
    }
}
