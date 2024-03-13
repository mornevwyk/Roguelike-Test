using System.Security.Cryptography.X509Certificates;
using SadConsole.Entities;

class MoveAction{
    MapEntity entity;
    int x;
    int y;
    public MoveAction(MapEntity entity, int x, int y){
        this.entity = entity;
        this.x = x;
        this.y = y;
    }

    public void Perform(){
        Point newPosition = new Point(entity.Position.X + x, entity.Position.Y + y);
        
        if(!entity.gameMap.Surface.Area.Contains(newPosition)) return;

        if(!entity.gameMap.tiles[newPosition.X,newPosition.Y].walkable) return;

        if(entity.gameMap.blockingEntity(newPosition, entity.gameMap.enemyEntityManager.EntitiesVisible)){
                entity.gameMap.player.MeleeAttack(newPosition);
                entity.gameMap.HandleEntities();
                return;
            }

        entity.Position = newPosition;
    }
}

            