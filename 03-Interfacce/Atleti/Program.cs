namespace Atleti;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ESERCIZIO INTERFACE — ATLETI ===\n");

        // === FASE 1: IAtletaUniversale ===
        Console.WriteLine("--- FASE 1: Atleta Universale ---");

        Atleta a1 = new()
        {
            Nome = "Andrea",
            Cognome = "Furlan",
            Pettorina = 12,
            Disciplina = "Atletica"
        };

        Console.WriteLine(a1);
        Console.WriteLine(a1.Corro());
        Console.WriteLine(a1.Salto());
        Console.WriteLine(a1.Dritto());
        Console.WriteLine(a1.Rovescio());
        Console.WriteLine(a1.Rana());
        Console.WriteLine(a1.Dorso());
        Console.WriteLine(a1.Mangio());
        Console.WriteLine(a1.Bevo());

        Console.WriteLine();

        // === FASE 2: Calciatore ===
        Console.WriteLine("--- FASE 2: Calciatore ---");

        Calciatore c1 = new()
        {
            Nome = "Luca",
            Cognome = "Rossi",
            Pettorina = 10,
            Disciplina = "Calcio",
            GoalSegnati = 15,
            PartiteGiocate = 20
        };

        Calciatore c2 = new()
        {
            Nome = "Marco",
            Cognome = "Bianchi",
            Pettorina = 9,
            Disciplina = "Calcio",
            GoalSegnati = 8,
            PartiteGiocate = 18
        };

        Console.WriteLine(c1);
        Console.WriteLine($"Media goal: {c1.MediaGoal():F2}");
        Console.WriteLine(c2);
        Console.WriteLine($"Media goal: {c2.MediaGoal():F2}");

        // Equals
        Console.WriteLine($"\nc1.Equals(c2): {c1.Equals(c2)}");

        // CompareTo
        int confronto = c1.CompareTo(c2);
        Console.WriteLine($"Confronto media goal (c1 vs c2): {confronto} " +
            $"→ {(confronto > 0 ? "c1 ha media maggiore" : confronto < 0 ? "c2 ha media maggiore" : "pari")}");

        // Clone
        try
        {
            Calciatore c1Clone = (Calciatore)c1.Clone();
            c1Clone.Nome = "Clone";
            c1Clone.Cognome = "Di Luca";
            Console.WriteLine($"\nClone creato: {c1Clone}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore clone: {ex.Message}");
        }

        // Test clone bloccato (PartiteGiocate = 0)
        Calciatore c3 = new()
        {
            Nome = "Test",
            Cognome = "Zero",
            Pettorina = 0,
            Disciplina = "Calcio",
            GoalSegnati = 0,
            PartiteGiocate = 0
        };

        try
        {
            c3.Clone();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nClone bloccato (corretto): {ex.Message}");
        }

        Console.WriteLine("\n=== FINE ===");
    }
}
