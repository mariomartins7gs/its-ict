// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System;

namespace Martins_Esercitazione1.Models
{
    public enum TipologiaMeccanico { Carrozzeria, Meccanica }

    public class Meccanico : Persona
    {
        public TipologiaMeccanico Tipologia { get; set; }

        public override double Tredicesima()
        {
            return Stipendio * 1.93;
        }

        public override bool Equals(object obj)
        {
            if (obj is Meccanico other)
                return Nome == other.Nome
                    && Cognome == other.Cognome
                    && Tipologia == other.Tipologia;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome, Cognome, Tipologia);
        }

        public override string ToString()
        {
            return $"{base.ToString()} - Tipologia: {Tipologia}";
        }
    }
}
