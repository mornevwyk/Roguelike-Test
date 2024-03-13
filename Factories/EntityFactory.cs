using SadConsole.Entities;

class EntityFactory{
    public EntityFactory(){}

    public Enemy GenericEnemy(Map gameMap){
        return new Enemy(
            glyph: new ColoredGlyph(Color.GreenYellow, Color.AnsiBlack,'o'),
            gameMap        
            );
    }
}