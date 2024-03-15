using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using SadConsole.Entities;

class EntityAction{
    public EntityAction(){}

    public virtual void Perform(){}
}

class WaitAction : EntityAction{
    public WaitAction(){}
    public override void Perform(){
        return;
    }
}

class ActionWithDirection : EntityAction{
    public Actor entity {get; private set;}
    public int dx {get; private set;}
    public int dy {get; private set;}
    public ActionWithDirection(Actor entity, int dx, int dy)
        :base(){
            this.entity = entity;
            this.dx = dx;
            this.dy = dy;
        }

    public Point Destination(){
        return new Point(this.entity.Position.X + dx, this.entity.Position.Y + dy);
    }

    public Actor? TargetEntity(){
        return this.entity.gameMap.GetEntityAt(Destination());
    }
}

class MoveAction : ActionWithDirection{
    public MoveAction(Actor entity, int dx, int dy)
        :base(entity, dx, dy){}

    public override void Perform(){
        Point newPosition = Destination();
        //Actions.LogEvent?.Invoke($"Move {entity.Position} to {newPosition}");
        if(!entity.gameMap.Surface.Area.Contains(newPosition)){
            Actions.LogEvent?.Invoke("Not on surface");
            return;
        }

        if(!entity.gameMap.tiles[newPosition.X,newPosition.Y].walkable){
            Actions.LogEvent?.Invoke("not walkable");
            return;
        } 

        if(entity.gameMap.blockingEntity(newPosition, entity.gameMap.mapEntityManager.EntitiesVisible)){
                //Enemy Action for this situation
                Actions.LogEvent?.Invoke("Blocked by entity");
                return;
            }

        entity.Position = newPosition;
    }
}

class MeleeAction : ActionWithDirection{
    public MeleeAction(Actor entity, int dx, int dy)
        :base(entity, dx, dy){}

    public override void Perform(){
        Actor? target = TargetEntity();

        if(target == null){
            Actions.LogEvent?.Invoke("nothing to attack");
        }
        else{
            Actions.LogEvent?.Invoke($"{this.entity.Name} attacked {target.Name}");
            int damage = entity.strength;
            target.health -= damage;
            target.OnHealthChanged();
            if(target.health <= 0){
                target.ai = null;
                target.Die();
                Actions.LogEvent?.Invoke($"{target.Name} was slain");
            }
        }
    }
}

class BumpAction : ActionWithDirection{
    public BumpAction(Actor entity, int dx, int dy)
        :base(entity, dx, dy){}

    public override void Perform()
    {
        Actor? target = TargetEntity();
        if (target == null){
            MoveAction action = new MoveAction(entity, dx, dy);
            //Actions.LogEvent?.Invoke(action.ToString());
            action.Perform();
        }
        else{
            MeleeAction action = new MeleeAction(entity, dx, dy);
            //Actions.LogEvent?.Invoke(action.ToString());
            action.Perform();
        }
    }
}

            