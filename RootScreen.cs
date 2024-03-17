
using SadConsole.Input;
using SadRogue.Primitives.SpatialMaps;

namespace tes_2.Scenes;

class RootScreen : ScreenObject
{
    private InfoConsole infoBox;
    private PlayerInfoConsole playerconsole;
    InventoryConsole inventoryConsole;
    //private ScreenSurface map;
    private MapScreen map;
    private int bottomConsoleHeight = 8;
    private int leftConsoleWidth = 20;
    bool showInventory = false;

    public RootScreen()
    {

        Actions.gameOver += GameOver;
        Actions.toggleInventory += ToggleInventory;
        /* box = new Console(leftConsoleWidth, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        box.Position = new Point(1,1);
        box.DrawBox(new Rectangle(0, 0, box.Width, box.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
        box.Print(2,0,"Title");
        this.Children.Add(box); */
        infoBox = new(leftConsoleWidth, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        infoBox.Position = new Point(1,1);
        this.Children.Add(infoBox);

        playerconsole = new(leftConsoleWidth, GameSettings.GAME_HEIGHT - infoBox.Height - 1);
        playerconsole.Position = new Point(1, infoBox.Height + 1);
        this.Children.Add(playerconsole);
        
        map = new MapScreen(GameSettings.GAME_WIDTH - leftConsoleWidth - 2, GameSettings.GAME_HEIGHT - bottomConsoleHeight);
        map.Position = new Point(leftConsoleWidth + 1, 1);
        this.Children.Add(map);

        inventoryConsole = new(map.Width/2,10);
        inventoryConsole.Position = new Point(50,10);
        this.Children.Add(inventoryConsole);
        this.Children.MoveToBottom(inventoryConsole);
    }

    public void ToggleInventory(){

        if(map.gameMap.player.Position.X > map.Width/2){
            inventoryConsole.Position = map.Position;
        }
        else{
            inventoryConsole.Position = new Point(map.Position.X + map.Width/2, map.Position.Y);
        }

        if(showInventory == false){
            showInventory = true;
            this.Children.MoveToTop(inventoryConsole);
        }
        else if(showInventory == true){
            showInventory = false;
            this.Children.MoveToBottom(inventoryConsole);
        }

        int row = 0;
        List<Item> items = map.gameMap.player.inventory.GetInventory();
        inventoryConsole.Resize(inventoryConsole.Width, items.Count + 2, false);
        inventoryConsole.RedrawBorder();
        inventoryConsole.console.Resize(inventoryConsole.console.Width, items.Count, false);
        int letter = (int)'a';
        for(int i = 0; i < items.Count; i++){
            char itemOrder = (char)(letter + i);
            string name = items[i].Name;    
            
            inventoryConsole.console.Print(1, row++, $"{itemOrder}) {name}");
        }
        
    }

    public void GameOver(){
        Console gameOverScreen = new SadConsole.Console(12,3);
        gameOverScreen.Position = new Point((GameSettings.GAME_WIDTH / 2) - gameOverScreen.Width/2, (GameSettings.GAME_HEIGHT / 2) - gameOverScreen.Height/2);
        gameOverScreen.Parent = this;
        this.Children.MoveToTop(gameOverScreen);
        gameOverScreen.DrawBox(new Rectangle(0, 0, gameOverScreen.Width, gameOverScreen.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
        gameOverScreen.Print( 1,1,"Game Over!", Color.AnsiWhite, Color.AnsiBlack);
        gameOverScreen.IsFocused = true;
    }

    public override bool ProcessMouse(MouseScreenObjectState state){
        
        Point mousePos = state.Mouse.ScreenPosition;
        //box.Print(0,4,$"screen pos {mousePos}");
        //box.Print(0,6,$"cell position {state.SurfacePixelPosition}");
        return base.ProcessMouse(state);
    }

    public override void Update(TimeSpan delta)
    {
        //infoBox.console.Clear();
        //infoBox.console.Print(0,infoBox.console.Height-3,$"{map.gameMap.mousePos}");
        //infoBox.console.Print(0,infoBox.console.Height-2,$"{map.gameMap.GetEntityAt(map.gameMap.mousePos)}");
        
        base.Update(delta);
    }
}
