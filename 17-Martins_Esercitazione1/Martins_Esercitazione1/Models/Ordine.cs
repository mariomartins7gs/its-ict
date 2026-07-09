// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Martins_Esercitazione1.Models
{
    public sealed class Ordine
    {
        private static readonly Random _rng = new();
        private static readonly HashSet<string> _codiciUsati = new();

        public string Codice { get; private set; }
        public DateTime Data { get; set; }
        public Dictionary<Prodotto, int> ElencoProdotti { get; set; } = new();
        public Venditore Venditore { get; set; }

        public Ordine()
        {
            Codice = GeneraCodice();
        }

        private static string GeneraCodice()
        {
            const string lettere = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string alfanum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string codice;
            do
            {
                char[] chars = new char[8];
                chars[0] = lettere[_rng.Next(lettere.Length)];
                for (int i = 1; i < 8; i++)
                    chars[i] = alfanum[_rng.Next(alfanum.Length)];
                codice = new string(chars);
            }
            while (_codiciUsati.Contains(codice));

            _codiciUsati.Add(codice);
            return codice;
        }

        public int NumeroPezzi()
        {
            return ElencoProdotti.Values.Sum();
        }

        public double Totale()
        {
            double totale = 0;
            foreach (var kvp in ElencoProdotti)
                totale += kvp.Key.Prezzo * kvp.Value;
            return totale;
        }

        public void Stampa()
        {
            string dirPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..", "file"
            );
            Directory.CreateDirectory(dirPath);
            string filePath = Path.Combine(dirPath, $"{Codice}.txt");

            using StreamWriter sw = new(filePath, false, Encoding.UTF8);
            sw.WriteLine($"Ordine: {Codice}");
            sw.WriteLine($"Data: {Data:dd/MM/yyyy}");
            sw.WriteLine($"Venditore: {Venditore?.Nome} {Venditore?.Cognome}");
            sw.WriteLine(new string('-', 65));
            sw.WriteLine($"{"Codice",-8} {"Prodotto",-22} {"Qta",-5} {"Prezzo",-10} {"Subtotale",-12}");
            sw.WriteLine(new string('-', 65));

            foreach (var kvp in ElencoProdotti)
            {
                double subtotale = kvp.Key.Prezzo * kvp.Value;
                sw.WriteLine($"{kvp.Key.Codice,-8} {kvp.Key.Denominazione,-22} {kvp.Value,-5} {kvp.Key.Prezzo,8:F2} euro  {subtotale,8:F2} euro");
            }

            sw.WriteLine(new string('-', 65));
            sw.WriteLine($"{"TOTALE ORDINE:",-40} {Totale(),8:F2} euro");
        }

        public override string ToString()
        {
            return $"[{Codice}] {Data:dd/MM/yyyy} - {NumeroPezzi()} pezzi - {Totale():F2} euro (Venditore: {Venditore?.Nome} {Venditore?.Cognome})";
        }
    }
}
