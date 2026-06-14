using System.ComponentModel.DataAnnotations;

namespace HomeBudgetTracker.Models;

public class Expense
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategoria jest wymagana")]
    [StringLength(50)]
    public string Category { get; set; } = "";

    [Required(ErrorMessage = "Kwota jest wymagana")]
    [Range(0.01, 999999)]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Data jest wymagana")]
    public DateTime Date { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }
}