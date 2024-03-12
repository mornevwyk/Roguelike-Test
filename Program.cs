

using SadConsole.Components;
using SadConsole.Configuration;
using SadConsole.Entities;
using tes_2.Scenes;

Settings.WindowTitle = "My SadConsole Game";

Builder configuration = new Builder()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .SetStartingScreen<RootScreen>()
    //.SetSplashScreen<SadConsole.SplashScreens.Ansi1>()
    .ConfigureFonts((fConfig, game) => 
    {
        fConfig.UseCustomFont("Fonts\\Cheepicus12.font");
    })
    ;


Game.Create(configuration);

Game.Instance.Run();

Game.Instance.Dispose();




