using SadConsole.Entities;
class StaticEntityManager : EntityManager{
    List<Entity> staticEntities = new();
    public StaticEntityManager()
        :base(){}

    protected override void OnEntityAdded(Entity entity){
        staticEntities.Add(entity);
    }
    
    public List<Entity> GetEntities(){
        return staticEntities;
    }
}
