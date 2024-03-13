using System.Linq;

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

        int[] adjacent = new int[4];
        adjacent[0] = gameMap.dijkstraMap[enemy.Position.X + 1, enemy.Position.Y];
        adjacent[1] = gameMap.dijkstraMap[enemy.Position.X, enemy.Position.Y + 1];
        adjacent[2] = gameMap.dijkstraMap[enemy.Position.X - 1, enemy.Position.Y];
        adjacent[3] = gameMap.dijkstraMap[enemy.Position.X, enemy.Position.Y - 1];

        int min = int.MaxValue;
        int index = 0;
        for(int i = 0; i < adjacent.Length; i ++){
            if (adjacent[i] <= min){
                min = adjacent[i];
                index = i;
            }
        }
        
        moves[index].Perform();        
    }
}