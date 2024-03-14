using SadConsole.Entities;

class Corpse : Entity{
    public Corpse()
        :base(
            new ColoredGlyph(Color.Red, Color.Transparent, '%'), 
            0
        ){}
}