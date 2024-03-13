

class InfoConsole : Console{
    public Console console {get;set;}
    public List<string> messages = new();
    public InfoConsole(int width, int height)
        :base(width, height){
            
            this.DrawBox(new Rectangle(0, 0, width, height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
            
            console = new(width-2, height-2);
            console.Position = new Point(1,1);
            console.Parent = this;

            Actions.LogEvent += AddMessage;
        }

    void AddMessage(string message){
        messages.Add(message);
        
        console.Cursor.Print(message);
        console.Cursor.NewLine();
        //console.Cursor.IsVisible = true;
    }
}