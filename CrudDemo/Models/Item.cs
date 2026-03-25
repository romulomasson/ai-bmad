namespace CrudDemo.Models;

public sealed class Item
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }

    public Item(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
