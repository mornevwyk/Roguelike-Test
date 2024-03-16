using SadConsole.Entities;

class Actor : Entity{
    public Map gameMap;
    public int health;
    public int strength;
    public EnemyAI? ai;
    public bool active = false;
    public Actor(ColoredGlyph glyph, Map gameMap, int zIndex)
        :base(glyph, zIndex){
            this.IsVisible = false;
            this.gameMap = gameMap;
        }

    public Actor(SingleCell singleCell, Map gameMap, int zIndex)
        :base(singleCell, zIndex){
            this.gameMap = gameMap;
        }

    public void Die(){
        Corpse corpse = new();
        corpse.Position = this.Position;
        this.gameMap.staticEntityManager.Add(corpse);
        this.gameMap.mapEntityManager.Remove(this);
        
    }

    public void SetActive(bool active){
        this.active = active;
    }

    public virtual void OnHealthChanged(){return;}
}

class Enemy: Actor{
    public Enemy(ColoredGlyph glyph, Map gameMap)
        :base(glyph, gameMap, 0){
            this.IsVisible = false;
            this.Name = "generic enemy";
            this.ai = new(this);
            this.gameMap = gameMap;
        }

    public void Move(int x, int y){
        this.Position = new Point(this.Position.X + x, this.Position.Y + y);
    }

    /// <summary>
    /// Gets the distance to the player from a specified position.
    /// </summary>
    /// <param name="point">The specified position from which to calculate the distance.</param>
    /// <returns></returns>
    public float DistanceFrom(Point point){
        return MathF.Sqrt(MathF.Pow(point.X - gameMap.player.Position.X,2) + MathF.Pow(point.Y - gameMap.player.Position.Y,2));
    }

    /// <summary>
    /// Gets the distnce from the enemy's positin to the player.
    /// </summary>
    /// <returns></returns>
    public float Distance(){
        return MathF.Sqrt(MathF.Pow(this.Position.X - gameMap.player.Position.X,2) + MathF.Pow(this.Position.Y - gameMap.player.Position.Y,2));
    }

    /* public void Die(){
        Corpse corpse = new();
        corpse.Position = this.Position;
        this.gameMap.staticEntityManager.Add(corpse);
    } */

    public void Perform(Player player){
        this.ai?.TakeTurn(player, gameMap);
    }
        
}