// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

namespace Martins_Esercitazione1.Models
{
    public abstract class Persona
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public double Stipendio { get; set; }

        public abstract double Tredicesima();

        public override string ToString()
        {
            return $"{Nome} {Cognome} - Stipendio: {Stipendio:F2} euro";
        }
    }
}
