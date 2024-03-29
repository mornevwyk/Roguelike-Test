﻿using SadConsole.Configuration;
using SadConsole.Entities;
using SadConsole.Input;

namespace tes_2.Scenes;

class RootScreen : ScreenObject
{
    private InfoConsole infoBox;
    //private ScreenSurface map;
    private MapScreen map;
    private int bottomConsoleHeight = 4;
    private int leftConsoleWidth = 20;

    public RootScreen()
    {
        /* box = new Console(leftConsoleWidth, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        box.Position = new Point(1,1);
        box.DrawBox(new Rectangle(0, 0, box.Width, box.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
        box.Print(2,0,"Title");
        this.Children.Add(box); */
        infoBox = new(leftConsoleWidth, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        infoBox.Position = new Point(1,1);
        this.Children.Add(infoBox);
        
        map = new MapScreen(GameSettings.GAME_WIDTH - leftConsoleWidth - 2, GameSettings.GAME_HEIGHT - bottomConsoleHeight - 1);
        map.Position = new Point(leftConsoleWidth + 1, 1);
        this.Children.Add(map);
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
