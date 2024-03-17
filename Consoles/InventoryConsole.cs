using SadConsole.UI;

class InventoryConsole : Console{
    public Console console {get;set;}
    private int health;
    private int strength;
    private int defense;
    public InventoryConsole(int width, int height)
        :base(width, height){
            
            this.DrawBox(new Rectangle(0, 0, width, height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
            this.Print(1,0,"Inventory");
            console = new(width-2, height-2);
            console.Position = new Point(1,1);
            console.Parent = this;
            console.Surface.DefaultBackground = Color.AnsiBlack;
            //console.Fill(Color.AnsiBlack, Color.AnsiBlack, 0);
            
        }
    public void RedrawBorder(){
        this.DrawBox(new Rectangle(0, 0, this.Width, this.Height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
            this.Print(1,0,"Inventory");
    }
}