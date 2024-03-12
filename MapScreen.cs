using SadConsole.Input;
using SadConsole;
using tes_2.Scenes;

class MapScreen : Console{
    private int height;
    private int width;
    public Map gameMap {get; private set;}
    
    public Point mousePos;
    public MapScreen(int width, int height)
        :base(width, height){
            this.height = height;
            this.width = width;
            
            this.gameMap = new Map(this.width, this.height);
            gameMap.Parent = this;

            IsFocused = true;
            //UseMouse = true;

            //FocusOnMouseClick = true;
            //this.MouseButtonClicked += HandleMouseClick;
            /* console = new(30,10);
            console.Position = new Point(0,0);
            console.Parent = this; */
        }

    public override bool ProcessKeyboard(SadConsole.Input.Keyboard info){

        bool keyHit = false;
        Point newPosition = (0, 0);
        // Process UP/DOWN movements
        if (info.IsKeyPressed(Keys.Up))
        {
            newPosition = gameMap.player.Position + (0, -1);
            keyHit = true;
        }
        else if (info.IsKeyPressed(Keys.Down))
        {
            newPosition = gameMap.player.Position + (0, 1);
            keyHit = true;
        }

        // Process LEFT/RIGHT movements
        if (info.IsKeyPressed(Keys.Left))
        {
            newPosition = gameMap.player.Position + (-1, 0);
            keyHit = true;
        }
        else if (info.IsKeyPressed(Keys.Right))
        {
            newPosition = gameMap.player.Position + (1, 0);
            keyHit = true;
        }

        if (keyHit)
        {
            if(!Surface.Area.Contains(newPosition)) return false;

            if(!gameMap.tiles[newPosition.X,newPosition.Y].walkable) return false;

            if(gameMap.blockingEntity(newPosition, gameMap.enemyEntityManager.EntitiesVisible)) return false;

            gameMap.player.Position = newPosition;
            gameMap.UpdateFOV();
            gameMap.RenderTiles();
            gameMap.HandleEntities();
            return true;
        }

        if(info.IsKeyPressed(Keys.Escape)){
            Game.Instance.MonoGameInstance.Exit();
        }

        return false;
    }

    /* private void HandleMouseClick(object? sender, MouseScreenObjectState e)
    {
        console.Print(0,3,"hello");
    } */
    
    public override bool ProcessMouse(MouseScreenObjectState state){
        //base.ProcessMouse(state);
        if(state.Mouse.IsOnScreen){
            mousePos = state.CellPosition;
            return false;
        }
        else return base.ProcessMouse(state);
    }
    
}