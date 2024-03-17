using System;
static class Actions{
    public static Action<string>? LogEvent;
    public static Action<int,int>? healthChanged;

    public static Action? gameOver;
    public static Action? toggleInventory;
}