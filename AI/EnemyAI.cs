class BaseAI{
    public BaseAI(){}

    public virtual void TakeTurn(){
        Actions.LogEvent?.Invoke("AI not implimented");
    }

    public virtual void TakeTurn(Enemy enemy, Player player, Map gameMap){
        Actions.LogEvent?.Invoke("AI not implimented");
    }
}

class EnemyAI : BaseAI {
    public EnemyAI()
        :base(){}

    public override void TakeTurn(Enemy enemy, Player player, Map gameMap)
    {
        int currentTile = gameMap.dijkstraMap[enemy.Position.X, enemy.Position.Y];

        /* BumpAction[] moves = new BumpAction[4];
        moves[0] = new BumpAction(enemy, 1, 0);
        moves[1] = new BumpAction(enemy, 0, 1);
        moves[2] = new BumpAction(enemy, -1, 0);
        moves[3] = new BumpAction(enemy, 0, -1); */

        Dictionary<string, BumpAction> possibleMoves = new(){
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

        string str = top.Keys.First();
        //Actions.LogEvent?.Invoke(str);
        possibleMoves[str].Perform();
        //Actions.LogEvent?.Invoke(possibleMoves[str].ToString());
    }

}