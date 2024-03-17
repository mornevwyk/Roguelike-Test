using SadConsole.Entities;

class BaseAI{
    public Enemy enemy {get; private set;}
    public bool active;
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
        int maxInterations = 10;
        int iterations = 0;
        while(true && iterations < maxInterations){
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
            iterations++;
        }
        return path;
    }

    public override void TakeTurn(Player player, Map gameMap)
    {
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
}