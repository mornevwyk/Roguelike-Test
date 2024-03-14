using SadConsole.Entities;

class BaseAI{
    public Enemy enemy {get; private set;}
    public BaseAI(Enemy enemy){
        this.enemy = enemy;
    }

    public virtual void TakeTurn(){
        Actions.LogEvent?.Invoke("AI not implimented");
    }

    public virtual void TakeTurn(Player player, Map gameMap){
        Actions.LogEvent?.Invoke("AI not implimented");
    }
}

class EnemyAI : BaseAI {
    public EnemyAI(Enemy enemy)
        :base(enemy){}

    public List<Point> ComputePath(Point destination){
        List<Point> path = new();
        Point counter = this.enemy.Position;
        List<Point> adjacentTiles = new(){
            new Point(-1, 0),
            new Point(1, 0),
            new Point(0, 1),
            new Point(0, -1),
        };
        Point bestMove;
        while(true){
            bestMove = adjacentTiles[0];
            int value = this.enemy.gameMap.dijkstraMap[counter.X + bestMove.X, counter.Y + bestMove.Y];

            for(int i = 1; i < adjacentTiles.Count; i++){

                int newValue = this.enemy.gameMap.dijkstraMap[counter.X + adjacentTiles[i].X, counter.Y + adjacentTiles[i].Y];

                if( newValue < value){
                    bestMove = adjacentTiles[i];
                    value = newValue;
                }
                else if(newValue == value){
                    float valueDistance = this.enemy.DistanceFrom(new Point(counter.X + bestMove.X, counter.Y + bestMove.Y));
                    float newValueDistance = this.enemy.DistanceFrom(new Point(counter.X + adjacentTiles[i].X, counter.Y + adjacentTiles[i].Y));
                    if( newValueDistance < valueDistance){
                        bestMove = adjacentTiles[i];
                        value = this.enemy.gameMap.dijkstraMap[counter.X + bestMove.X, counter.Y + bestMove.Y];
                    }
                }
            }

            counter = new Point(counter.X + bestMove.X, counter.Y + bestMove.Y);
            path.Add(bestMove);
            if(counter == destination) break;
        }
        return path;
    }

    public override void TakeTurn(Player player, Map gameMap)
    {
        //int currentTile = gameMap.dijkstraMap[this.enemy.Position.X, this.enemy.Position.Y];

        /* BumpAction[] moves = new BumpAction[4];
        moves[0] = new BumpAction(enemy, 1, 0);
        moves[1] = new BumpAction(enemy, 0, 1);
        moves[2] = new BumpAction(enemy, -1, 0);
        moves[3] = new BumpAction(enemy, 0, -1); */

        /* Dictionary<string, BumpAction> possibleMoves = new(){
            {"left", new BumpAction(enemy, -1, 0)},
            {"right", new BumpAction(enemy, 1, 0)},
            {"up", new BumpAction(enemy, 0, 1)},
            {"down", new BumpAction(enemy, 0, -1)},
        };     

        Dictionary<string, Point> adjacentTiles = new(){
            {"left",new Point(-1, 0)},
            {"right",new Point(1, 0)},
            {"up", new Point(0, 1)},
            {"down", new Point(0, -1)},
        };

        var top = adjacentTiles.OrderBy(pair => gameMap.dijkstraMap[enemy.Position.X + pair.Value.X, enemy.Position.Y + pair.Value.Y])
                                .ThenBy(pair => enemy.DistanceFrom(new Point(enemy.Position.X + pair.Value.X, enemy.Position.Y + pair.Value.Y))).Take(1)
                                .ToDictionary(pair => pair.Key, pair => pair.Value);

        string str = top.Keys.First(); */
        //Actions.LogEvent?.Invoke(str);
        //possibleMoves[str].Perform();
        //Actions.LogEvent?.Invoke(possibleMoves[str].ToString());

        List<Point> path = ComputePath(player.Position);
        EntityAction action;
        if(new Point(this.enemy.Position.X + path[0].X, this.enemy.Position.Y + path[0].Y) == player.Position){
            action = new MeleeAction(this.enemy, path[0].X, path[0].Y);
        }
        else{
            action = new MoveAction(this.enemy, path[0].X, path[0].Y);
        }
        
        action.Perform();
    }

    /* void RenderPath(List<Point> path){
        foreach (Point point in path){
            this.enemy.gameMap.Surface.SetCellAppearance(this.enemy.Position.X + point.X, this.enemy.Position.Y + point.Y, new ColoredGlyph(Color.Blue, Color.Black, 4));
        }
    } */

}