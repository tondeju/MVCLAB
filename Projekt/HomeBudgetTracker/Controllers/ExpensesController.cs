using HomeBudgetTracker.Data;
using HomeBudgetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetTracker.Controllers;

public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
    string search,
    DateTime? fromDate,
    DateTime? toDate)
{
    var expenses = _context.Expenses.AsQueryable();

    if (!string.IsNullOrEmpty(search))
    {
        expenses = expenses.Where(x =>
            x.Category.Contains(search));
    }

    if (fromDate.HasValue)
    {
        expenses = expenses.Where(x =>
            x.Date >= fromDate.Value);
    }

    if (toDate.HasValue)
    {
        expenses = expenses.Where(x =>
            x.Date <= toDate.Value);
    }

    return View(await expenses.ToListAsync());
}

    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
        return NotFound();

    var expense = await _context.Expenses.FindAsync(id);

    if (expense == null)
        return NotFound();

    return View(expense);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(
    int id,
    Expense expense)
{
    if (id != expense.Id)
        return NotFound();

    if (ModelState.IsValid)
    {
        _context.Update(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    return View(expense);
}
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
        return NotFound();

    var expense = await _context.Expenses
        .FirstOrDefaultAsync(m => m.Id == id);

    if (expense == null)
        return NotFound();

    return View(expense);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var expense = await _context.Expenses.FindAsync(id);

    if (expense != null)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Expense expense)
    {
        if (ModelState.IsValid)
        {
            _context.Add(expense);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(expense);
    }
}