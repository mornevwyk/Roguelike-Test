using SadConsole.Entities;

class Enemy: Entity{
    public Enemy(ColoredGlyph glyph)
        :base(glyph, 0){
            this.IsVisible = false;
        }
        
}