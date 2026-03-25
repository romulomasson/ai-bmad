namespace CrudDemo.Models;

public sealed class ItemUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
