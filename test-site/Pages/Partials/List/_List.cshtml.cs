using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Models;
using System.Linq;

namespace test_site.Pages.Partials.List;

public class ListModel(ApplicationDbContext context) : ListFilterBase
{

    public bool sticky { get; set; }

    public List<Category> Categories { get; set; } = [];
    public List<Category> SortedCategories { get; set; } = [];
    public IList<Person> People { get; set; } = [];

    public async Task OnGetAsync()
    {
        Categories = await context.Categories.ToListAsync();
        SortedCategories = Categories.OrderBy(c => c.Name).ToList();

        if (category.Count == 0)
            category = [.. Categories.Select(c => c.Id)];

        IQueryable<Person> query = context.People.Include(p => p.Category);
        List<int> validCategoryIds = [.. Categories.Select(c => c.Id)];
        List<int> filteredCategory = [.. category.Where(validCategoryIds.Contains)];
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
        int skip = (Math.Max(1, page_num) - 1) * size;
        query = query.Skip(skip).Take(size);
        People = await query.ToListAsync();
    }
}
