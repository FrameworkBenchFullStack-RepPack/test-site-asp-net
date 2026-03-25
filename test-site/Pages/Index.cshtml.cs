using Microsoft.AspNetCore.Mvc.RazorPages;
using test_site.Data;
using test_site.Pages.Partials.List;

namespace test_site.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public ListModel ListModel { get; set; } = default!;

    public async Task OnGetAsync()
    {
        ListModel = new ListModel(_context) { Sticky = false, PageSize = 10 };
        await ListModel.OnGetAsync();
    }
}
