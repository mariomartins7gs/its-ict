---
date: 2026-07-22
topic: "PrestitiBibliotecaMVC Bug Fixes"
status: validated
---

## Problem Statement

The PrestitiBibliotecaMVC application was analyzed. Two controllers (Libri, Studenti) work correctly. The **PrestitiController is completely broken** because it was scaffolded with single-key patterns against a table with a **composite primary key `(IdLibro, Matricola)`**. Five additional medium/low-severity issues were also identified across the codebase.

## Constraints

- No database schema changes — the `Prestito` table's composite key `(IdLibro, Matricola)` is the correct natural key
- No route changes — keep the default convention-based routing
- No new NuGet packages
- Maintain the existing scaffolding patterns (Bind attributes, anti-forgery tokens, concurrency handling)

## Approach

**Query strings for composite key routing.** ASP.NET Core's model binder picks up query string parameters by name automatically, so action methods accept `(int? idLibro, int? matricola)` and views pass them via `asp-route-idLibro` / `asp-route-matricola`. Zero route configuration needed.

Alternative considered: attribute routing with `/Prestiti/Details/5/100`. Rejected because it requires `[Route]` on every action and risks conflicts with convention routing.

## Architecture

No architectural changes. The same three-layer pattern (Controller → DbContext → Views) is preserved. All controllers continue using `PrestitiBibliotecaContext` via constructor injection.

## Components

### Fix #1 (CRITICAL): PrestitiController — Composite Key Support

**Every action method** changes from single `int? id` / `int id` to pair `(int? idLibro, int? matricola)` / `(int idLibro, int matricola)`.

| Method | Change |
|--------|--------|
| `Details(int? idLibro, int? matricola)` | Query uses `m => m.IdLibro == idLibro && m.Matricola == matricola` |
| `Edit(int? idLibro, int? matricola)` GET | `FindAsync(idLibro, matricola)` with both keys |
| `Edit(int idLibro, int matricola, Prestito)` POST | Validate both `idLibro != prestito.IdLibro \|\| matricola != prestito.Matricola` |
| `Delete(int? idLibro, int? matricola)` GET | Query uses both keys |
| `DeleteConfirmed(int idLibro, int matricola)` POST | `FindAsync(idLibro, matricola)`, then remove |
| `PrestitoExists(int idLibro, int matricola)` | Check `e => e.IdLibro == idLibro && e.Matricola == matricola` |

The `Edit` POST's `_context.Update(prestito)` is left unchanged — EF correctly handles detached entities with composite keys because the key values are already set on the object.

The `DeleteConfirmed` POST receives both values from the form's hidden inputs (already present in the view). The model binder matches `IdLibro`/`Matricola` form fields to `idLibro`/`matricola` parameters (case-insensitive).

### Fix #2 (CRITICAL): Prestiti Views — Fix Broken Links

**Index.cshtml:**
- Replace `/* id=item.PrimaryKey */` placeholders with `asp-route-idLibro="@item.IdLibro" asp-route-matricola="@item.Matricola"`
- Change table display from `IdLibroNavigation.Codice` and `MatricolaNavigation.Matricola` (raw IDs) to `IdLibroNavigation.Titolo` (book title) and `MatricolaNavigation.Cognome` (student name)

**Details.cshtml:**
- Fix Edit link: replace `/* id = Model.PrimaryKey */` with `asp-route-idLibro="@Model?.IdLibro" asp-route-matricola="@Model?.Matricola"`
- Show book title and student name instead of raw IDs

**Create.cshtml, Edit.cshtml, Delete.cshtml:** No changes needed. Create/Edit use `<select>` with ViewBag; Delete already has both hidden fields via `asp-for`.

### Fix #3 (MEDIUM): Prestiti Dropdowns — Human-Readable Text

In `PrestitiController.Create()` and `Edit()` GET actions, change `SelectList` third parameter:

- `new SelectList(_context.Libri, "Codice", "Codice")` → `new SelectList(_context.Libri, "Codice", "Titolo")`
- `new SelectList(_context.Studenti, "Matricola", "Matricola")` → `new SelectList(_context.Studenti, "Matricola", "Cognome")`

The value field (FK column) stays the same. Only the display text changes.

### Fix #4 (MEDIUM): Libri/Studenti Details — Include Prestiti

**LibriController.Details():** Add `.Include(l => l.Prestiti).ThenInclude(p => p.MatricolaNavigation)` to the query.

**StudentiController.Details():** Add `.Include(s => s.Prestiti).ThenInclude(p => p.IdLibroNavigation)` to the query.

**Libri/Details.cshtml:** Add a `<h4>Prestiti</h4>` section after the book details, looping through `Model.Prestiti` showing: student name, loan date, return date.

**Studenti/Details.cshtml:** Add a `<h4>Prestiti</h4>` section after the student details, looping through `Model.Prestiti` showing: book title, loan date, return date.

### Fix #5 (MEDIUM): FK Violation — Graceful Error Handling

**LibriController.DeleteConfirmed:** Wrap `SaveChangesAsync` in try/catch for `DbUpdateException`. If the inner exception message contains FK constraint indicators, add a `ModelState` error ("Cannot delete this book — it has associated loans.") and return the Delete view with the model.

**StudentiController.DeleteConfirmed:** Same pattern — catch FK violation and return a user-friendly error instead of crashing.

### Fix #6 (LOW): Remove Hardcoded Connection String

**PrestitiBibliotecaContext.cs:** Remove the `OnConfiguring` override entirely. The parameterless constructor stays (needed by EF scaffolding tooling), but without `OnConfiguring`, direct instantiation produces a clear error instead of silently connecting with wrong credentials. At runtime, DI always provides the configured options via the parameterized constructor.

## Data Flow

```
Browser: /Prestiti/Details?idLibro=5&matricola=100
  → ModelBinder: idLibro=5, matricola=100
    → Details(int? idLibro, int? matricola)
      → EF: FirstOrDefaultAsync(m => m.IdLibro == 5 && m.Matricola == 100)
        → View with Prestito + navigation properties
```

For Delete POST:
```
Form POST: /Prestiti/Delete (body: IdLibro=5&Matricola=100)
  → ModelBinder: idLibro=5, matricola=100
    → DeleteConfirmed(int idLibro, int matricola)
      → EF: FindAsync(5, 100)
        → Remove + SaveChangesAsync
```

## Error Handling

- **Composite key validation:** Both `Details` and `Edit` GET check `if (idLibro == null || matricola == null)` and return `NotFound()`
- **Concurrency:** Existing `DbUpdateConcurrencyException` handling in Edit POST is preserved
- **FK violations:** New try/catch in Libri and Studenti `DeleteConfirmed` catches `DbUpdateException` and returns a user-friendly error message via `ModelState`
- **Not found:** All query methods return `NotFound()` when the record doesn't exist

## Testing Strategy

Manual testing checklist:

1. **Prestiti CRUD:** Create a loan, view it in Details, edit the dates, delete it
2. **Composite key uniqueness:** Create a loan for book 1 + student 100. Create another for book 1 + student 200. Verify both appear in Index and can be individually viewed/edited/deleted
3. **Prestiti Index links:** Click Edit, Details, Delete from the Index page — all should navigate correctly
4. **Dropdown usability:** Prestiti Create page shows book titles and student names (not numbers)
5. **Libri Details:** View a book that has loans — the Prestiti section should appear
6. **Studenti Details:** View a student with loans — the Prestiti section should appear
7. **FK violation:** Try to delete a book that has loans — expect a friendly error message instead of a crash
8. **Concurrency:** Verify Edit still handles concurrent modification correctly

## Open Questions

None — all identified issues have clear fixes.
