using HomeBudgetTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudgetTracker.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Total =
            _context.Expenses.Sum(x => x.Amount);

        ViewBag.Count =
            _context.Expenses.Count();

        decimal maxExpense = 0;

        if (_context.Expenses.Any())
    {
        maxExpense = _context.Expenses
        .ToList()
        .Max(x => x.Amount);
    }

        ViewBag.MaxExpense = maxExpense;

        return View();
    }
}
