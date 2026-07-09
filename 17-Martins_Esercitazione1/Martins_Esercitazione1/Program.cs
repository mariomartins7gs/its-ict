// Corso: ITS-ICT Piemonte
// Modulo: Programmazione C# - Ereditarieta e Polimorfismo
// Studente: Mario Martins
// Data: 09/07/2026

using System;
using System.Collections.Generic;
using System.Linq;
using Martins_Esercitazione1.Models;

namespace Martins_Esercitazione1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ── Seed Data ──
            SeedData seed = new();
            ResponsabileVenditori respVenditori = seed.Responsabile;
            CapoOfficina capo = seed.Capo;
            List<Venditore> venditori = seed.Venditori;
            List<Meccanico> meccanici = seed.Meccanici;
            List<Prodotto> prodotti = seed.Prodotti;
            List<Ordine> ordini = seed.Ordini;

            // ── Menu ──
            bool esci = false;
            do
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║       OFFICINA AUTO & MOTO          ║");
                Console.WriteLine("║         Gestionale v1.0             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1] Visualizza dati Responsabile Venditori");
                Console.WriteLine("2] Visualizza dati Capo Officina");
                Console.WriteLine("3] Stampa ordine su file (tramite codice)");
                Console.WriteLine("4] Report IVA ordini ultimo trimestre (Capo Officina)");
                Console.WriteLine("5] Totale stipendi da pagare (Tredicesime)");
                Console.WriteLine("6] Termina programma");
                Console.WriteLine();
                Console.Write("Scegli un'opzione: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        Opzione1(respVenditori);
                        break;
                    case "2":
                        Opzione2(capo);
                        break;
                    case "3":
                        Opzione3(ordini);
                        break;
                    case "4":
                        Opzione4(capo);
                        break;
                    case "5":
                        Opzione5(respVenditori, capo, venditori, meccanici);
                        break;
                    case "6":
                        esci = true;
                        Console.WriteLine("Arrivederci!");
                        break;
                    default:
                        Console.WriteLine("Opzione non valida. Premi un tasto per riprovare.");
                        Console.ReadKey();
                        break;
                }
            }
            while (!esci);
        }

        // ── Opzione 1 ──
        static void Opzione1(ResponsabileVenditori rv)
        {
            Console.WriteLine("── RESPONSABILE VENDITORI ──");
            Console.WriteLine(rv);
            Console.WriteLine($"\nTredicesima: {rv.Tredicesima():F2} euro");
            Console.WriteLine("\nPremi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        // ── Opzione 2 ──
        static void Opzione2(CapoOfficina co)
        {
            Console.WriteLine("── CAPO OFFICINA ──");
            Console.WriteLine(co);
            Console.WriteLine($"\nTredicesima: {co.Tredicesima():F2} euro");
            Console.WriteLine("\nPremi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        // ── Opzione 3 ──
        static void Opzione3(List<Ordine> ordini)
        {
            Console.Write("Inserisci il codice ordine da stampare: ");
            string codice = Console.ReadLine()?.Trim().ToUpper() ?? "";

            Ordine trovato = ordini.Find(o => o.Codice == codice);
            if (trovato == null)
            {
                Console.WriteLine($"Nessun ordine trovato con codice '{codice}'.");
            }
            else
            {
                trovato.Stampa();
                Console.WriteLine($"Ordine {codice} stampato su file ({codice}.txt).");
            }

            Console.WriteLine("\nPremi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        // ── Opzione 4 ──
        static void Opzione4(CapoOfficina co)
        {
            DateTime soglia = DateTime.Now.AddMonths(-3);
            var ordiniTrimestre = co.OrdiniGestiti
                .Where(o => o.Data >= soglia)
                .ToList();

            if (ordiniTrimestre.Count == 0)
            {
                Console.WriteLine("Nessun ordine gestito dal Capo Officina nell'ultimo trimestre.");
            }
            else
            {
                double totIvaEsclusa = ordiniTrimestre.Sum(o => o.Totale());
                double iva = totIvaEsclusa * 0.22;
                double totIvaInclusa = totIvaEsclusa * 1.22;

                Console.WriteLine("── REPORT IVA ORDINI (Ultimo Trimestre) ──\n");
                Console.WriteLine($"Periodo: dal {soglia:dd/MM/yyyy} al {DateTime.Now:dd/MM/yyyy}");
                Console.WriteLine($"Ordini considerati: {ordiniTrimestre.Count}");
                Console.WriteLine();
                Console.WriteLine($"{"Ordine",-12} {"Data",-12} {"Totale",-12}");
                Console.WriteLine(new string('-', 36));
                foreach (Ordine o in ordiniTrimestre)
                    Console.WriteLine($"{o.Codice,-12} {o.Data,-12:dd/MM/yyyy} {o.Totale(),10:F2} euro");
                Console.WriteLine(new string('-', 36));
                Console.WriteLine($"{"TOTALE IVA ESCLUSA:",-30} {totIvaEsclusa,10:F2} euro");
                Console.WriteLine($"{"IVA (22%):",-30} {iva,10:F2} euro");
                Console.WriteLine($"{"TOTALE IVA INCLUSA:",-30} {totIvaInclusa,10:F2} euro");
            }

            Console.WriteLine("\nPremi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        // ── Opzione 5 ──
        static void Opzione5(ResponsabileVenditori rv, CapoOfficina co, List<Venditore> venditori, List<Meccanico> meccanici)
        {
            Console.WriteLine("── TOTALE STIPENDI (TREDICESIME) ──\n");

            double totale = 0;

            Console.WriteLine("Venditori:");
            foreach (Venditore v in venditori)
            {
                double t = v.Tredicesima();
                Console.WriteLine($"  {v.Nome,-10} {v.Cognome,-12} {t,10:F2} euro");
                totale += t;
            }

            Console.WriteLine("\nMeccanici:");
            foreach (Meccanico m in meccanici)
            {
                double t = m.Tredicesima();
                Console.WriteLine($"  {m.Nome,-10} {m.Cognome,-12} {t,10:F2} euro");
                totale += t;
            }

            double tResp = rv.Tredicesima();
            Console.WriteLine($"\nResponsabile Venditori: {rv.Nome} {rv.Cognome} - {tResp,10:F2} euro");
            totale += tResp;

            double tCapo = co.Tredicesima();
            Console.WriteLine($"Capo Officina:          {co.Nome} {co.Cognome} - {tCapo,10:F2} euro");
            totale += tCapo;

            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"{"TOTALE COMPLESSIVO:",-28} {totale,10:F2} euro");

            Console.WriteLine("\nPremi un tasto per tornare al menu...");
            Console.ReadKey();
        }
    }

    // ── Seed Data ──
    internal class SeedData
    {
        public ResponsabileVenditori Responsabile { get; private set; }
        public CapoOfficina Capo { get; private set; }
        public List<Venditore> Venditori { get; private set; }
        public List<Meccanico> Meccanici { get; private set; }
        public List<Prodotto> Prodotti { get; private set; }
        public List<Ordine> Ordini { get; private set; }

        public SeedData()
        {
            // ---- Persone ----
            Responsabile = new ResponsabileVenditori
            {
                Nome = "Marco",
                Cognome = "Ferrari",
                Stipendio = 3500.0,
                Settore = SettoreVendita.Auto
            };

            Capo = new CapoOfficina
            {
                Nome = "Luca",
                Cognome = "Bianchi",
                Stipendio = 3800.0,
                Tipologia = TipologiaMeccanico.Meccanica
            };

            Venditori = new List<Venditore>
            {
                new Venditore { Nome = "Anna", Cognome = "Rossi", Stipendio = 2200.0, Settore = SettoreVendita.Auto },
                new Venditore { Nome = "Paolo", Cognome = "Verdi", Stipendio = 2100.0, Settore = SettoreVendita.Moto },
                new Venditore { Nome = "Sara", Cognome = "Gallo", Stipendio = 2300.0, Settore = SettoreVendita.Auto },
                new Venditore { Nome = "Davide", Cognome = "Neri", Stipendio = 2050.0, Settore = SettoreVendita.Moto }
            };

            Meccanici = new List<Meccanico>
            {
                new Meccanico { Nome = "Giuseppe", Cognome = "Russo", Stipendio = 2400.0, Tipologia = TipologiaMeccanico.Meccanica },
                new Meccanico { Nome = "Francesca", Cognome = "Esposito", Stipendio = 2350.0, Tipologia = TipologiaMeccanico.Carrozzeria }
            };

            Responsabile.VenditoriSeguiti.AddRange(Venditori);

            // ---- Prodotti ----
            Prodotti = new List<Prodotto>
            {
                new Prodotto { Codice = "P001", Denominazione = "Olio Motore 5W40",     Descrizione = "Olio sintetico 5W40 4L",       Prezzo = 45.50,  Giacenza = 30 },
                new Prodotto { Codice = "P002", Denominazione = "Freno a disco anteriore", Descrizione = "Disco freno anteriore 280mm", Prezzo = 85.00,  Giacenza = 15 },
                new Prodotto { Codice = "P003", Denominazione = "Batteria 12V 60Ah",     Descrizione = "Batteria auto 12V 60Ah",       Prezzo = 120.00, Giacenza = 10 },
                new Prodotto { Codice = "P004", Denominazione = "Filtro Olio",            Descrizione = "Filtro olio motore universale", Prezzo = 12.50,  Giacenza = 50 },
                new Prodotto { Codice = "P005", Denominazione = "Candela Accensione",     Descrizione = "Candela iridio lunga durata",   Prezzo = 8.90,   Giacenza = 80 },
                new Prodotto { Codice = "P006", Denominazione = "Catena Trasmissione",    Descrizione = "Catena moto 520",              Prezzo = 65.00,  Giacenza = 12 },
                new Prodotto { Codice = "P007", Denominazione = "Pastiglie Freno",        Descrizione = "Pastiglie freno ceramiche",     Prezzo = 35.00,  Giacenza = 25 },
                new Prodotto { Codice = "P008", Denominazione = "Ammortizzatore Post.",   Descrizione = "Ammortizzatore posteriore moto", Prezzo = 150.00, Giacenza = 8 },
                new Prodotto { Codice = "P009", Denominazione = "Liquido Raffreddamento", Descrizione = "Antigelo 5L pronta uso",        Prezzo = 18.00,  Giacenza = 40 },
                new Prodotto { Codice = "P010", Denominazione = "Pneumatico 205/55R16",   Descrizione = "Pneumatico estivo 4 stagioni",  Prezzo = 95.00,  Giacenza = 20 }
            };

            // ---- Ordini ----
            Ordini = new List<Ordine>
            {
                CreaOrdine(Venditori[0], new DateTime(2026, 4, 15),
                    (Prodotti[0], 2), (Prodotti[3], 3), (Prodotti[4], 4)),
                CreaOrdine(Venditori[1], new DateTime(2026, 5, 10),
                    (Prodotti[5], 1), (Prodotti[7], 2), (Prodotti[9], 4)),
                CreaOrdine(Venditori[2], new DateTime(2026, 6, 5),
                    (Prodotti[1], 2), (Prodotti[6], 2), (Prodotti[2], 1)),
                CreaOrdine(Venditori[3], new DateTime(2026, 6, 20),
                    (Prodotti[5], 2), (Prodotti[4], 8), (Prodotti[8], 3)),
                CreaOrdine(Venditori[0], new DateTime(2026, 7, 2),
                    (Prodotti[0], 3), (Prodotti[3], 5), (Prodotti[9], 2), (Prodotti[4], 6)),
                CreaOrdine(Venditori[2], new DateTime(2026, 7, 8),
                    (Prodotti[6], 4), (Prodotti[1], 1), (Prodotti[2], 2))
            };

            Capo.OrdiniGestiti.AddRange(Ordini);
        }

        private static Ordine CreaOrdine(Venditore venditore, DateTime data, params (Prodotto prodotto, int quantita)[] items)
        {
            Ordine o = new()
            {
                Data = data,
                Venditore = venditore
            };
            foreach (var (prodotto, quantita) in items)
                o.ElencoProdotti[prodotto] = quantita;
            return o;
        }
    }
}
