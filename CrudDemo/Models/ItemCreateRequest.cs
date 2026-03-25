namespace CrudDemo.Models;

public sealed class ItemCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
