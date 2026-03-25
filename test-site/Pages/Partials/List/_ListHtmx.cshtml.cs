using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Models;

namespace test_site.Pages.Partials.List;

public class ListHtmxModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ListHtmxModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Person> People { get; set; } = default!;

    [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
    public List<int> FilterCategoryIds { get; set; } = [];

    [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
    public int AgeFrom { get; set; } = 0;

    [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
    public int AgeTo { get; set; } = 100;

    [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
    public int Offset { get; set; } = 0;

    [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 50;

    public async Task OnGetAsync()
    {
        Response.Headers["HX-Push-Url"] = $"/List{Request.QueryString}";

        IQueryable<Person> query = _context.People.Include(p => p.Category);

        if (FilterCategoryIds.Count > 0)
        {
            query = query.Where(person => FilterCategoryIds.Contains(person.CategoryId));
        }

        query = query.Where(person => person.Age >= AgeFrom && person.Age <= AgeTo);

        query = query.OrderBy(person => person.Id);

        query = query.Skip(Offset).Take(PageSize);

        People = await query.ToListAsync();
    }
}
