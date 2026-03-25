using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Models;

namespace test_site.Pages.Partials.List;

public class ListModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ListModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool Sticky { get; set; }

    public List<Category> Categories { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public List<int> FilterCategoryIds { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public int AgeFrom { get; set; } = 0;

    [BindProperty(SupportsGet = true)]
    public int AgeTo { get; set; } = 100;

    [BindProperty(SupportsGet = true)]
    public int Offset { get; set; } = 0;

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 50;

    public async Task OnGetAsync()
    {
        Categories = await _context.Categories.ToListAsync();

        if (FilterCategoryIds.Count == 0)
            FilterCategoryIds = Categories.Select(c => c.Id).ToList();
    }
}