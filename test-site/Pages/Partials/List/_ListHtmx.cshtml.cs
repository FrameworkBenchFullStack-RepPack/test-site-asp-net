using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Models;

namespace test_site.Pages.Partials.List;

[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
public class ListHtmxModel(ApplicationDbContext context) : ListFilterBase
{
    public IList<Person> People { get; set; } = default!;

    public async Task OnGetAsync()
    {
        IQueryable<Person> query = context.People.Include(p => p.Category);
        if (category.Count > 0)
        {
            query = query.Where(person => category.Contains(person.CategoryId));
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
