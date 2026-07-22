---
date: 2026-07-22
topic: "PrestitiBibliotecaMVC Bug Fixes"
design: thoughts/shared/designs/2026-07-22-prestiti-biblioteca-fixes-design.md
project_root: ADO.NET/PrestitiBibliotecaMVC/PrestitiBibliotecaMVC/PrestitiBibliotecaMVC
---

## Task Dependencies

```
T1 (context) → T2 (controller) → T3 (views) ↘
T4 (libri details) ─────────────────────────→ T8 (verify)
T5 (libri fk) ──────────────────────────────↗
T6 (studenti details) ──────────────────────↗
T7 (studenti fk) ───────────────────────────↗
```

Batch 1: T1 (no deps)
Batch 2: T2 (depends on T1)
Batch 3: T3, T4, T5, T6, T7 (T3 depends on T2; T4-T7 are independent)
Batch 4: T8 (verification, depends on all)

---

## T1: Remove hardcoded connection string from PrestitiBibliotecaContext

**File:** Models/PrestitiBibliotecaContext.cs  
**Independent:** Yes  
**Tests:** Build must succeed  

**Action:** Remove the `OnConfiguring` override method entirely. Keep the parameterless constructor and the parameterized constructor unchanged.

---

## T2: Rewrite PrestitiController for composite key

**File:** Controllers/PrestitiController.cs  
**Depends on:** T1  
**Tests:** Build must succeed  

**Changes:**
1. All action method signatures accept `(int? idLibro, int? matricola)` or `(int idLibro, int matricola)` instead of single `int? id` / `int id`
2. `Details`: query uses `m => m.IdLibro == idLibro && m.Matricola == matricola`, null-check both params
3. `Edit` GET: `FindAsync(idLibro, matricola)`, null-check both params
4. `Edit` POST: validate `idLibro != prestito.IdLibro || matricola != prestito.Matricola`
5. `Delete` GET: query uses both keys, null-check both
6. `DeleteConfirmed` POST: `FindAsync(idLibro, matricola)`
7. `PrestitoExists`: check both `e.IdLibro == idLibro && e.Matricola == matricola`
8. `Create` GET and `Edit` GET: change SelectList text field from `"Codice"` to `"Titolo"` and `"Matricola"` to `"Cognome"` (Fix #3)
9. Keep all existing `[Bind]`, `[ValidateAntiForgeryToken]`, concurrency handling, includes

---

## T3: Fix Prestiti views for composite key

**Files:** Views/Prestiti/Index.cshtml, Views/Prestiti/Details.cshtml  
**Depends on:** T2  
**Tests:** Build must succeed; links must have correct route values  

**Index.cshtml changes:**
1. Replace `/* id=item.PrimaryKey */` in all three ActionLink calls with `asp-route-idLibro="@item.IdLibro" asp-route-matricola="@item.Matricola"`
2. Change `item.IdLibroNavigation.Codice` to `item.IdLibroNavigation.Titolo`
3. Change `item.MatricolaNavigation.Matricola` to `item.MatricolaNavigation.Cognome`

**Details.cshtml changes:**
1. Replace `/* id = Model.PrimaryKey */` with `asp-route-idLibro="@Model?.IdLibro" asp-route-matricola="@Model?.Matricola"`
2. Change `IdLibroNavigation.Codice` to `IdLibroNavigation.Titolo`
3. Change `MatricolaNavigation.Matricola` to `MatricolaNavigation.Cognome`

---

## T4: Libri Details — include Prestiti

**Files:** Controllers/LibriController.cs, Views/Libri/Details.cshtml  
**Independent:** Yes  
**Tests:** Build must succeed  

**Controller change (Details action):**
- Change `.FirstOrDefaultAsync(m => m.Codice == id)` to include: `.Include(l => l.Prestiti).ThenInclude(p => p.MatricolaNavigation).FirstOrDefaultAsync(m => m.Codice == id)`

**View change:**
- After the existing `<dl>` for book details, add a new section:
  ```
  <h4>Prestiti</h4>
  ```
  If `Model.Prestiti` has items, render a table with columns: Studente (Nome Cognome), Data Prestito, Data Restituzione. If empty, show "Nessun prestito."

---

## T5: Libri DeleteConfirmed — handle FK violation

**File:** Controllers/LibriController.cs  
**Independent:** Yes (only depends on existing code, not T2-T4)  
**Tests:** Build must succeed  

**Change (DeleteConfirmed POST action):**
- Wrap `await _context.SaveChangesAsync()` in `try { ... } catch (DbUpdateException) { ... }`
- In the catch, re-query the libro (it's already detached) and return View(libro) with a ModelState error: "Impossibile eliminare questo libro perché ha dei prestiti associati."

---

## T6: Studenti Details — include Prestiti

**Files:** Controllers/StudentiController.cs, Views/Studenti/Details.cshtml  
**Independent:** Yes  
**Tests:** Build must succeed  

**Controller change (Details action):**
- Change `.FirstOrDefaultAsync(m => m.Matricola == id)` to include: `.Include(s => s.Prestiti).ThenInclude(p => p.IdLibroNavigation).FirstOrDefaultAsync(m => m.Matricola == id)`

**View change:**
- After the existing `<dl>` for student details, add a new section:
  ```
  <h4>Prestiti</h4>
  ```
  If `Model.Prestiti` has items, render a table with columns: Libro (Titolo), Data Prestito, Data Restituzione. If empty, show "Nessun prestito."

---

## T7: Studenti DeleteConfirmed — handle FK violation

**File:** Controllers/StudentiController.cs  
**Independent:** Yes  
**Tests:** Build must succeed  

**Change (DeleteConfirmed POST action):**
- Wrap `await _context.SaveChangesAsync()` in `try { ... } catch (DbUpdateException) { ... }`
- In the catch, re-query the studente and return View(studente) with a ModelState error: "Impossibile eliminare questo studente perché ha dei prestiti associati."

---

## T8: Final verification

**Depends on:** All T1-T7  
**Action:** Run `dotnet build` and verify zero errors. Check all modified files compile correctly.
