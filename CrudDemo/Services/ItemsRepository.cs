using CrudDemo.Models;

namespace CrudDemo.Services;

public sealed class ItemsRepository
{
    private readonly Dictionary<int, Item> _items = new();
    private readonly object _gate = new();
    private int _nextId = 1;

    public Item Create(string name, string description)
    {
        lock (_gate)
        {
            var id = _nextId++;
            var item = new Item(id, name, description);
            _items[id] = item;
            return item;
        }
    }

    public List<Item> GetAll()
    {
        lock (_gate)
        {
            return _items.Values.ToList();
        }
    }

    public Item? GetById(int id)
    {
        lock (_gate)
        {
            return _items.TryGetValue(id, out var item) ? item : null;
        }
    }

    public Item? Update(int id, string name, string description)
    {
        lock (_gate)
        {
            if (!_items.TryGetValue(id, out var existing))
                return null;

            var updated = new Item(id, name, description);
            _items[id] = updated;
            return updated;
        }
    }

    public bool Delete(int id)
    {
        lock (_gate)
        {
            return _items.Remove(id);
        }
    }
}
