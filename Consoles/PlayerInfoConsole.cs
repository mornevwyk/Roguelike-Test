

class PlayerInfoConsole : Console{
    public Console console {get;set;}
    private int health;
    private int strength;
    private int defense;
    public PlayerInfoConsole(int width, int height)
        :base(width, height){
            
            this.DrawBox(new Rectangle(0, 0, width, height), ShapeParameters.CreateStyledBox(ICellSurface.ConnectedLineThin, new ColoredGlyph(Color.Gray, Color.Black)));
            
            console = new(width-2, height-2);
            console.Position = new Point(1,1);
            console.Parent = this;

            UpdateHealth(30,30);

            Actions.healthChanged += UpdateHealth;
        }

    void UpdateHealth(int currentHealth, int maxHealth){
        console.Clear();

        ColoredString healthString;
        if(currentHealth < maxHealth / 3){
            healthString = new ColoredString($"{currentHealth}", Color.Red, Color.Transparent);
        }
        else if(currentHealth < 2 * maxHealth / 3){
            healthString = new ColoredString($"{currentHealth}", Color.Orange, Color.Transparent);
        }
        else{
            healthString = new ColoredString($"{currentHealth}", Color.Green, Color.Transparent);
        }

        int row = 0;
        console.Print(0, row++, "Health :" + healthString);
        console.Print(0, row++, "Str: 3");
        console.Print(0, row++, "Def: 4");
        
    }
}