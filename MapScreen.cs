using SadConsole.Input;
using SadConsole;
using tes_2.Scenes;

class MapScreen : Console{
    private int height;
    private int width;
    public Map gameMap {get; private set;}
    public GameInput gameInput;
    public GameInputHandler inputHandler = new();
    public Point mousePos;
    public MapScreen(int width, int height)
        :base(width, height){
            this.height = height;
            this.width = width;
            
            this.gameMap = new Map(this.width, this.height);
            gameMap.Parent = this;

            IsFocused = true;

            gameInput = new(this.gameMap);
        }

    public override bool ProcessKeyboard(SadConsole.Input.Keyboard info){
        return gameInput.GetInput(info, gameInput.gameInputFunction);        
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


class GameInput{
    Map gameMap;
    GameInputHandler inputHandler = new();
    public GameInputFunction gameInputFunction;
    public GameInput(Map gameMap){
        this.gameMap = gameMap;
        gameInputFunction = BasicInput;
    }

    public delegate bool GameInputFunction(SadConsole.Input.Keyboard info);

    public bool GetInput(SadConsole.Input.Keyboard info, GameInputFunction func){
        return func(info);
    }

    public bool BasicInput(SadConsole.Input.Keyboard info){
        bool keyHit = false;
        //Point newPosition = (0, 0);
        Point dXdY;
        
        //Movement buttons
        if (info.IsKeyPressed(Keys.Up))
        {
            dXdY = new Point(0, -1);
            keyHit = true;
            inputHandler = new BaseGamePlay(this.gameMap, dXdY);
        }
        else if (info.IsKeyPressed(Keys.Down))
        {
            dXdY = new Point(0, 1);
            keyHit = true;
            inputHandler = new BaseGamePlay(this.gameMap, dXdY);
        }
        else if (info.IsKeyPressed(Keys.Left))
        {
            dXdY = new Point(-1, 0);
            keyHit = true;
            inputHandler = new BaseGamePlay(this.gameMap, dXdY);
        }
        else if (info.IsKeyPressed(Keys.Right))
        {
            dXdY = new Point(1, 0);
            keyHit = true;
            inputHandler = new BaseGamePlay(this.gameMap, dXdY);
        }
        //button to skip a turn
        else if(info.IsKeyPressed(Keys.Delete)){
            keyHit = true;
            inputHandler = new WaitTurn(this.gameMap);
        }
        else if(info.IsKeyPressed(Keys.I)){
            Actions.toggleInventory?.Invoke();
            this.gameInputFunction = InventoryMenuInput;
            return true;
        }

        if (keyHit)
        {
            inputHandler.HandleInput();
        }

        if(info.IsKeyPressed(Keys.Escape)){
            Game.Instance.MonoGameInstance.Exit();
        }

        return false;
    }

    public bool InventoryMenuInput(SadConsole.Input.Keyboard info){
        
        if(info.IsKeyPressed(Keys.Escape)){
            Actions.toggleInventory?.Invoke();
            this.gameInputFunction = BasicInput;
            return true;
        }

        int inventoryCount = this.gameMap.player.inventory.GetInventory().Count;
        foreach( AsciiKey key in info.KeysDown){
            int value = (int)key.Key - 65;
            if(value >= 0 && value < inventoryCount){
                Actions.LogEvent?.Invoke($"used {this.gameMap.player.inventory.GetInventory()[value].Name}");
                //activate item
                Actions.toggleInventory?.Invoke();
                this.gameInputFunction = BasicInput;
                return true;
            }
        }
        
        return false;
    }

}

class GameInputHandler{
    public GameInputHandler(){}

    public virtual bool HandleInput(){return false;}
}

class BaseGamePlay : GameInputHandler{
    Map gameMap;
    Point dXdY;
    public BaseGamePlay(Map gameMap, Point dXdY)
        :base(){
            this.gameMap = gameMap;
            this.dXdY = dXdY;
        }
    
    public override bool HandleInput(){
        Point newPosition = gameMap.player.Position + dXdY;
        if(!gameMap.Surface.Area.Contains(newPosition)) return false;

        if(!gameMap.tiles[newPosition.X,newPosition.Y].walkable) return false;

        BumpAction action = new BumpAction(gameMap.player, dXdY.X, dXdY.Y);
        try{
            action.Perform();
            //gameMap.player.Position = newPosition;
            gameMap.UpdateFOV();
            gameMap.ComputeDijkstra();
            gameMap.HandleEntities();
            gameMap.RenderTiles();
            gameMap.ComputeDijkstra();
            return true;
        }
        catch (Exception exc)
        {
            System.Diagnostics.Debug.Print(exc.Message);  // get at entire error message w/ stacktrace
            System.Diagnostics.Debug.Print(exc.StackTrace);  // or just the stacktrace
        }
        return false;
    }
}

class WaitTurn : GameInputHandler{
    Map gameMap;
    public WaitTurn(Map gameMap)
        :base(){
            this.gameMap = gameMap;
        }
    
    public override bool HandleInput(){
        try{
            gameMap.UpdateFOV();
            gameMap.ComputeDijkstra();
            gameMap.HandleEntities();
            gameMap.RenderTiles();
            gameMap.ComputeDijkstra();
            return true;
        }
        catch (Exception exc)
        {
            System.Diagnostics.Debug.Print(exc.Message);  // get at entire error message w/ stacktrace
            System.Diagnostics.Debug.Print(exc.StackTrace);  // or just the stacktrace
        }
        return false;
    }
}
