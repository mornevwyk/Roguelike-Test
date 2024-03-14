using SadConsole.Entities;

class EntityFactory{
    public EntityFactory(){}

    public Enemy GenericEnemy(Map gameMap){
        Enemy enemy = new(glyph: new ColoredGlyph(Color.GreenYellow, Color.AnsiBlack, 'o'), gameMap)
        {
            health = 10,
            strength = 2
        };
        return enemy;
    }
}