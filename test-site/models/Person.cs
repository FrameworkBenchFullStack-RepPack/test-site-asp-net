namespace test_site.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short Age { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
