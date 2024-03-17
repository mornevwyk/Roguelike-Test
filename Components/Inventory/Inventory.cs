using SadRogue.Primitives.SpatialMaps;

class Inventory{
    public List<Item> items = new();
    public Inventory(){
        items.Add(new Potion());
        items.Add(new Potion());
        items.Add(new Potion());
    }

    public List<Item> GetInventory(){
        return items;
    }
}