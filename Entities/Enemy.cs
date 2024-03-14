using SadConsole.Entities;

class Actor : Entity{
    public Map gameMap;
    public int health;
    public int strength;
    public EnemyAI? ai;
    public Actor(ColoredGlyph glyph, Map gameMap, int zIndex)
        :base(glyph, zIndex){
            this.IsVisible = false;
            this.gameMap = gameMap;
        }

    public Actor(SingleCell singleCell, Map gameMap, int zIndex)
        :base(singleCell, zIndex){
            this.gameMap = gameMap;
        }
}

class Enemy: Actor{
    public Enemy(ColoredGlyph glyph, Map gameMap)
        :base(glyph, gameMap, 0){
            this.IsVisible = false;
            this.Name = "generic enemy";
            this.ai = new();
            this.gameMap = gameMap;
        }

    public void Move(int x, int y){
        this.Position = new Point(this.Position.X + x, this.Position.Y + y);
    }

    public float DistanceFrom(Point point){
        return MathF.Sqrt(MathF.Pow(point.X - gameMap.player.Position.X,2) + MathF.Pow(point.Y - gameMap.player.Position.Y,2));
    }

    public float Distance(){
        return MathF.Sqrt(MathF.Pow(this.Position.X - gameMap.player.Position.X,2) + MathF.Pow(this.Position.Y - gameMap.player.Position.Y,2));
    }

    public void Perform(Player player){
        this.ai?.TakeTurn(this, player, gameMap);
    }
        
}