using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Models;

namespace test_site.Pages.Partials.List;

public class ListHtmxModel(ApplicationDbContext context) : ListFilterBase
{
    public IList<Person> People { get; set; } = default!;

    public async Task OnGetAsync()
    {
        IQueryable<Person> query = context.People.Include(p => p.Category);
        List<int> validCategoryIds = await context.Categories.Select(c => c.Id).ToListAsync();
        List<int> filteredCategory = category.Where(validCategoryIds.Contains).ToList();
        if (filteredCategory.Count > 0)
        {
            query = query.Where(person => filteredCategory.Contains(person.CategoryId));
        }

        query = query.Where(person => person.Age >= age_from && person.Age <= age_to);

        query = sort switch
        {
            "age" => query.OrderBy(p => p.Age).ThenBy(p => p.Name),
            "category" => query.OrderBy(p => p.Category.Name).ThenBy(p => p.Name),
            _ => query.OrderBy(p => p.Name),
        };

        int pageSize = size;
        int skip = (Math.Max(1, page_num) - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);

        People = await query.ToListAsync();
    }
}
