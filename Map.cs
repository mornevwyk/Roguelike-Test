using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;
using SadRogue.Primitives.SpatialMaps;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SadRogue.Primitives.SerializedTypes;

class Map : ScreenSurface{

    //ScreenSurface mapScreen;
    EntityManager em = new EntityManager();
    public EnemyEntityManager enemyEntityManager = new();
    public Player player {get; private set;}
    int height;
    int width;
    public Tile[,] tiles;
    public int[,] dijkstraMap;
    HashSet<Point> visibleTiles = new();
    HashSet<Point> seenTiles = new();
    Random rand = new Random();
    MapFactory mapFactory = new MapFactory();
    EntityFactory entityFactory = new EntityFactory();
    public Point mousePos;

    public Map(int width, int height)
        : base(width, height)
    {
        this.height = height;
        this.width = width;

        tiles = new Tile[width,height];
        dijkstraMap = new int[width,height];
        FillTiles();
        
        /* player = new Entity(new Entity.SingleCell(Color.White, Color.AnsiBlack, '@'), 100)
        {
            Position = new Point(1, 1),
        }; */
        player = new Player();
        player.Position = new Point(1,1);
        
        GenerateMap();    

        em.Add(player);
        UpdateFOV();
        RenderTiles();

        SadComponents.Add(enemyEntityManager);
        SadComponents.Add(em);

        //IsFocused = true;
        //UseMouse = true;

    }

    void FillTiles(){
        for(int i = 0; i < tiles.GetLength(0); i++){
            for ( int j = 0 ; j < tiles.GetLength(1); j ++){
                tiles[i,j] = mapFactory.WallTile(i,j);
            }
        }
        this.Fill(TileTypes.shroud);
    }

    public bool blockingEntity(Point newPosition, IReadOnlyList<Entity> entities){
        for(int i = 0; i<entities.Count; i++){
            if(entities[i].Position == newPosition){
                return true;
            }
        }
        return false;
    }

    RectangularRoom GenerateRoom(){
        int maxRoomWidth = 20;
        int maxRoomHeight = 20;
        int minRoomWidth = 5;
        int minRoomHeight = 5;

        int roomWidth = (int)rand.NextInt64(minRoomWidth,maxRoomWidth);
        int roomHeight = (int)rand.NextInt64(minRoomHeight,maxRoomHeight);

        int roomX = (int)rand.NextInt64(0, this.Width - roomWidth);
        int roomY = (int)rand.NextInt64(0, this.Height - roomHeight);

        RectangularRoom room = new RectangularRoom(roomX, roomY, roomWidth, roomHeight);
        return room;
        /* foreach( Point point in room.InnerArea()){
            this.SetGlyph(point.X, point.Y, 0, Color.AnsiCyan);
        } */ 
    }

    void PlaceEntities(RectangularRoom room){

        Point spawnPoint = room.InnerArea()[rand.Next(room.InnerArea().Count)];
        Enemy enemy = entityFactory.GenericEnemy(this);
        enemyEntityManager.Add(enemy);
        enemy.Position = spawnPoint;
     }

     void GenerateMap(){
        int maxRooms = 20;
        int minRooms = 10;
        int roomCount = 0;
        List<RectangularRoom> rooms = new List<RectangularRoom>();

        RectangularRoom firstRoom = GenerateRoom();
        rooms.Add(firstRoom);
        roomCount ++;
        player.Position = firstRoom.Center;
        
        while(roomCount < minRooms){
            bool intersects = false;
            RectangularRoom newRoom = GenerateRoom();

            for(int j = 0; j < rooms.Count; j++){
                if(newRoom.Intersects(rooms[j])){
                    intersects = true;
                }
            }

            if(!intersects){
                rooms.Add(newRoom);
                PlaceEntities(newRoom);
                roomCount ++;
            }
        }

        for(int i = roomCount; i <= maxRooms; i++){
            bool intersects = false;
            RectangularRoom newRoom = GenerateRoom();

            for(int j = 0; j < rooms.Count; j++){
                if(newRoom.Intersects(rooms[j])){
                    intersects = true;
                }
            }

            if(!intersects){
                rooms.Add(newRoom);
            }
        }

        foreach( RectangularRoom r in rooms){
            foreach( Point point in r.InnerArea()){
            //this.SetGlyph(point.X, point.Y, 0, Color.AnsiCyan);
            tiles[point.X,point.Y] = mapFactory.FloorTile(point.X,point.Y);
            //visibleTiles.Add(tiles[point.X,point.Y]);
            }
        }

        for(int i = 1; i < rooms.Count; i++){
            foreach( Point point in rooms[i].Tunnel(rooms[i-1])){
            //this.SetGlyph(point.X, point.Y, 0, Color.AnsiCyan);
            tiles[point.X,point.Y] = mapFactory.FloorTile(point.X,point.Y);
            //visibleTiles.Add(tiles[point.X,point.Y]);
            }
        }
     }

    public void UpdateFOV(){
        Point center = this.player.Position;
        visibleTiles.Clear();
        //Algorithms.LineFOV(center, new Point(center.X + 6, center.Y), this , tiles, (x, y, z, w)=>Method(x, y, z, w));
        //Algorithms.BasicFOV(center, 6, this , tiles, (x, y, z, w)=>Method(x, y, z, w));
        Algorithms.FOV360(center, 8, this , tiles, (x, y, z, w) => FOVMethod(x, y, z, w));
    }

    public bool FOVMethod(int x, int y, ScreenSurface surface, Tile[,] tiles){
        if (!surface.Surface.Area.Contains(new Point(x,y))){
            return false;
        } 
        if(!tiles[x,y].transparent){
            visibleTiles.Add(new Point(x,y));
            seenTiles.Add(new Point(x,y));
            return false;
        }
        visibleTiles.Add(new Point(x,y));
        seenTiles.Add(new Point(x,y));
        return true;
    }

    public void RenderTiles(){
        foreach(Point point in seenTiles){
            this.Surface.SetCellAppearance(point.X, point.Y, this.tiles[point.X,point.Y].glyphDark);
        }
        foreach(Point point in visibleTiles){
            this.Surface.SetCellAppearance(point.X, point.Y, this.tiles[point.X,point.Y].glyphLight);
        }
        //ComputeDijkstra();
        for(int i = 0; i < dijkstraMap.GetLength(0); i++){
            for ( int j = 0 ; j < dijkstraMap.GetLength(1); j ++){
                
                if(dijkstraMap[i,j] != int.MaxValue) this.Surface.SetCellAppearance(i, j, new ColoredGlyph(Color.Blue, Color.Black, 48 + dijkstraMap[i,j]));
            }
        }
    }

    public void HandleEntities(){
        foreach(Enemy enemy in enemyEntityManager.Entities){
            if(visibleTiles.Contains(enemy.Position)){
                enemy.IsVisible = true;
                enemy.Perform(player);
            }
            else{
                enemy.IsVisible = false;
            }
        }
        //Handle Item entities
    }

    public string GetEntityAt(Point point){
        string entityName = "";
        foreach(Enemy enemy in enemyEntityManager){
            if(enemy.IsVisible && enemy.Position == point){
                entityName = enemy.Name;
            }
        }

        return entityName;
    }

    public void ComputeDijkstra(){

        for(int i = 0; i < dijkstraMap.GetLength(0); i++){
            for ( int j = 0 ; j < dijkstraMap.GetLength(1); j ++){
                
                dijkstraMap[i,j] = int.MaxValue;
            }
        }

        List<Point> queue = new(){player.Position};
        dijkstraMap[queue[0].X,queue[0].Y] = 0;

        
        while(queue.Count > 0){
            Point visited = queue[0];
            List<Point> newPoints = new(){
                new Point(visited.X + 1, visited.Y),
                new Point(visited.X, visited.Y + 1),
                new Point(visited.X - 1, visited.Y),
                new Point(visited.X, visited.Y - 1)
                };
            foreach(Point point in newPoints){
                if(point.X < 0) continue;
                if(point.Y < 0) continue;
                if(point.X >= dijkstraMap.GetLength(0)) continue;
                if(point.Y >= dijkstraMap.GetLength(1)) continue;
                if(dijkstraMap[point.X,point.Y] < int.MaxValue) continue;
                if(!tiles[point.X,point.Y].walkable) continue;

                dijkstraMap[point.X, point.Y] = dijkstraMap[visited.X,visited.Y] + 1;
                queue.Add(point);
            }

            queue.RemoveAt(0);
            

        }
    }

    public override bool ProcessMouse(MouseScreenObjectState state){
        //base.ProcessMouse(state);
        if(state.Mouse.IsOnScreen){
            mousePos = state.CellPosition;
            return false;
        }
        else return base.ProcessMouse(state);
    }
}