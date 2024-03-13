using SadConsole.Entities;

class MapEntity : Entity{
    public Map gameMap;
    public MapEntity(ColoredGlyph glyph, Map gameMap)
        :base(glyph, 0){
            this.IsVisible = false;
            this.gameMap = gameMap;
        }
}

class Enemy: MapEntity{
    public EnemyAI? ai;
    public Enemy(ColoredGlyph glyph, Map gameMap)
        :base(glyph, gameMap){
            this.IsVisible = false;
            this.Name = "generic enemy";
            this.ai = new();
            this.gameMap = gameMap;
        }

    public void Move(int x, int y){
        this.Position = new Point(this.Position.X + x, this.Position.Y + y);
    }

    public float Distance(){
        return MathF.Sqrt(MathF.Pow(this.Position.X - gameMap.player.Position.X,2) + MathF.Pow(this.Position.Y - gameMap.player.Position.Y,2));
    }

    public void Perform(Player player){
        this.ai?.TakeTurn(this, player, gameMap);
    }
        
}