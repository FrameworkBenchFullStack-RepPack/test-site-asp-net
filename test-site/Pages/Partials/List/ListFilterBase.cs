using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace test_site.Pages.Partials.List;

public abstract class ListFilterBase : PageModel
{
    [BindProperty(SupportsGet = true)]
    public List<int> category { get; set; } = [];

    private int _age_from = 0;

    [BindProperty(SupportsGet = true)]
    public int age_from
    {
        get => _age_from;
        set => _age_from = value >= 0 && value <= 100 ? value : 0;
    }

    private int _age_to = 100;

    [BindProperty(SupportsGet = true)]
    public int age_to
    {
        get => _age_to;
        set => _age_to = value >= 0 && value <= 100 ? value : 100;
    }

    [BindProperty(SupportsGet = true)]
    public string sort { get; set; } = "name";

    private int _page_num = 1;

    [BindProperty(SupportsGet = true)]
    public int page_num
    {
        get => _page_num;
        set => _page_num = value >= 1 && value <= 2000000 ? value : 1;
    }

    private int _size = 100;

    [BindProperty(SupportsGet = true)]
    public int size
    {
        get => _size;
        set => _size = value > 0 && value <= 1000 ? value : 100;
    }
}
