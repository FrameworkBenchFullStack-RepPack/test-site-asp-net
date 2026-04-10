using test_site.Data;
using test_site.Pages.Partials.List;

namespace test_site.Pages.List;

public class IndexModel(ApplicationDbContext context) : ListFilterBase
{
    public ListModel ListModel { get; set; } = default!;

    public async Task OnGetAsync()
    {
        ListModel = new ListModel(context)
        {
            sticky = true,
            sort = sort,
            age_from = age_from,
            age_to = age_to,
            category = category,
            page_num = page_num,
            size = size,
        };
        await ListModel.OnGetAsync();
    }
}
