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

        MoveAction[] moves = new MoveAction[4];
        moves[0] = new MoveAction(enemy, 1, 0);
        moves[1] = new MoveAction(enemy, 0, 1);
        moves[2] = new MoveAction(enemy, -1, 0);
        moves[3] = new MoveAction(enemy, 0, -1);

        Dictionary<string, MoveAction> possibleMoves = new(){
            {"left", new MoveAction(enemy, -1, 0)},
            {"right", new MoveAction(enemy, 1, 0)},
            {"up", new MoveAction(enemy, 0, 1)},
            {"down", new MoveAction(enemy, 0, -1)},
        };     

        Dictionary<string, int> adjacentTiles = new(){
            {"left",gameMap.dijkstraMap[enemy.Position.X - 1, enemy.Position.Y]},
            {"right",gameMap.dijkstraMap[enemy.Position.X + 1, enemy.Position.Y]},
            {"up",gameMap.dijkstraMap[enemy.Position.X, enemy.Position.Y + 1]},
            {"down",gameMap.dijkstraMap[enemy.Position.X, enemy.Position.Y - 1]},
        };

        var top = adjacentTiles.OrderBy(pair => pair.Value)
                                .ThenBy(pair => pair.Value).Take(1)
                                .ToDictionary(pair => pair.Key, pair => pair.Value);

        string str = top.Keys.First();
        Actions.LogEvent?.Invoke(str);
        possibleMoves[str].Perform();
    }
}