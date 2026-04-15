using Microsoft.AspNetCore.Mvc;
using test_site.Data;
using test_site.Pages.Partials.List;

namespace test_site.Pages;

[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
public class IndexModel(ApplicationDbContext context) : ListFilterBase
{

    public ListModel ListModel { get; set; } = default!;

    public async Task OnGetAsync()
    {
        ListModel = new ListModel(context)
        {
            sticky = false,
            sort = sort,
            age_from = age_from,
            age_to = age_to,
            category = category,
            page_num = page_num,
            size = (Request.Query.ContainsKey("size") &&
                    int.TryParse(Request.Query["size"], out int s) &&
                    s > 0 &&
                    s <= 1000) ? size : 8,
        };
        await ListModel.OnGetAsync();
    }
}
