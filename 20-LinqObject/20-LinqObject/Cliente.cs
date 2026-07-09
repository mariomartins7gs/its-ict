// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - LINQ e Oggetti
// Studente: Mario Martins
// Data: 09/07/2026

namespace LinqObject
{
    internal class Cliente
    {
        public string CodiceFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascita { get; set; }

        public Cliente(string codiceFiscale, string cognome, string nome, DateTime dataNascita)
        {
            CodiceFiscale = codiceFiscale;
            Cognome = cognome;
            Nome = nome;
            DataNascita = dataNascita;
        }

        public string Nominativo() => $"{Cognome} {Nome}";

        public override string ToString()
            => $"[{CodiceFiscale}] {Nominativo()} - {DataNascita:dd/MM/yyyy}";
    }
}
