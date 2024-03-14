using System.Diagnostics.CodeAnalysis;
using SadConsole.Entities;

class MapEntityManager : EntityManager{
    Player? player;
    List<Enemy> enemies;
    List<Entity> misc;
    public MapEntityManager()
        :base(){
            this.enemies = new();
            this.misc = new();
        }

    public List<Enemy> GetEnemyEntities(){
        return this.enemies;
    }

    public List<Enemy> GetEnemyEntitiesSortedByInitiative(){
        return this.enemies;
    }

    public List<Enemy> GetEnemyEntitiesSortedByDistance(){
        return this.enemies;
    }

    public List<Entity> GetMiscEntities(){
        return this.misc;
    }

    public Player getPlayer(){
        if(player == null) throw new ArgumentException("No player entity added to manager");
        return player;
    }

    protected override void OnEntityAdded(Entity entity)
    {
        base.OnEntityAdded(entity);

        if(entity.GetType() == typeof(Enemy)) this.enemies.Add((Enemy)entity);
        if(entity.GetType() == typeof(Player)) this.player = (Player)entity;
    }
}