using SadConsole.Entities;

class EnemyEntityManager : EntityManager{

    public EnemyEntityManager()
        :base(){}

    public List<Point> GetPositions(){
        List<Point> positions = new();
        foreach( Enemy enemy in this){
            positions.Add(enemy.Position);
        }
        return positions;
    }
}