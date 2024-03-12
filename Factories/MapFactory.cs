using System.Xml.Serialization;

class MapFactory{

    public MapFactory(){}


    public Tile WallTile(int x, int y){
        return new Tile(
            x: x,
            y: y,
            walkable: false, 
            transparent: false,
            glyphLight : new ColoredGlyph(Color.Gray,Color.AnsiBlack,178),
            glyphDark : new ColoredGlyph(Color.AnsiBlackBright,Color.AnsiBlack,178)
        );
    }

    public Tile FloorTile(int x, int y){
        return new Tile(
            x: x,
            y: y,
            walkable: true, 
            transparent: true,
            glyphLight : new ColoredGlyph(Color.AnsiWhite,Color.AnsiWhite,0),
            glyphDark : new ColoredGlyph(Color.AnsiBlackBright,Color.AnsiBlack,61)
        );
    }
}