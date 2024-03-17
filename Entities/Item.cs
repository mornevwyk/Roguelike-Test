using SadConsole.Entities;

class Item : Entity{
    public Item(ColoredGlyph glyph, int zIndex)
        :base(glyph, zIndex){
            this.IsVisible = false;
        }
}

class Potion : Item{
    public Potion()
        :base(
            glyph: new ColoredGlyph(Color.Purple, Color.AnsiBlack, '!'), 
            zIndex: 1
            ){
                this.Name = "Potion";
            }
}