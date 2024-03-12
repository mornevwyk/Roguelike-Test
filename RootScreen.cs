using SadConsole.Configuration;
using SadConsole.Entities;
using SadConsole.Input;

namespace tes_2.Scenes;

class RootScreen : ScreenObject
{
    private ScreenSurface mainSurface;
    private ScreenSurface box;
    //private ScreenSurface map;
    private MapScreen map;
    private Console bottomConsole;
    private int bottomConsoleHeight = 4;
    private int leftConsoleWidth = 20;

    public RootScreen()
    {
        /* mainSurface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);
        this.Children.Add(mainSurface);

        bottomConsole = new Console(GameSettings.GAME_WIDTH-2 , bottomConsoleHeight);
        bottomConsole.Position = new Point(1, GameSettings.GAME_HEIGHT - bottomConsoleHeight);
        bottomConsole.DrawBox(new Rectangle(0, 0, bottomConsole.Width, bottomConsole.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.AnsiBlack)));
        bottomConsole.Print(2,0,"Title");
        mainSurface.Children.Add(bottomConsole);
        */
        box = new Console(leftConsoleWidth, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        box.Position = new Point(1,1);
        box.DrawBox(new Rectangle(0, 0, box.Width, box.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
        box.Print(2,0,"Title");
        this.Children.Add(box);

        //map = new ScreenSurface(GameSettings.GAME_WIDTH - leftConsoleWidth - 2, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        //map.Position = new Point(leftConsoleWidth + 1, 1);
        //map.Fill(Color.Blue, Color.Blue,2);
        //mainSurface.Children.Add(map);
        /*
        map = new MapScreen(GameSettings.GAME_WIDTH - leftConsoleWidth - 2, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        map.Position = new Point(leftConsoleWidth + 1, 1);
        mainSurface.Children.Add(map);
        map.IsFocused = true; */
        
        map = new MapScreen(GameSettings.GAME_WIDTH - leftConsoleWidth - 2, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        map.Position = new Point(leftConsoleWidth + 1, 1);
        this.Children.Add(map);
        //map.IsFocused = true;

        //IsFocused = true;

        
        //box.Print(1,10,$"mouse position {map.gameMap.mousePos}");
        //this.Children.MoveToTop(box);
    }

    public override bool ProcessMouse(MouseScreenObjectState state){
        
        Point mousePos = state.Mouse.ScreenPosition;
        box.Print(0,4,$"screen pos {mousePos}");
        box.Print(0,6,$"cell position {state.SurfacePixelPosition}");
        return base.ProcessMouse(state);
    }

    public override void Update(TimeSpan delta)
    {
        base.Update(delta);
        box.Print(1,10,$"cell pos: {map.gameMap.mousePos}");
    }
}
