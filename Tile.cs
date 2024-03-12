public class Tile{
    public int x {get; private set;}
    public int y {get; private set;}
    public bool walkable;
    public bool transparent;
    public ColoredGlyph glyphLight;
    public ColoredGlyph glyphDark;

    public Tile(int x, int y, bool walkable, bool transparent, ColoredGlyph glyphLight, ColoredGlyph glyphDark){
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        this.transparent = transparent;
        this.glyphLight = glyphLight;
        this.glyphDark = glyphDark;
    }
}