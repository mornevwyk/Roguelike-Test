
class RectangularRoom{

    int x {get;}
    int y {get;}
    int x2 {get;}
    int y2 {get;}

    public RectangularRoom(int x, int y, int width, int height){
        this.x = x;
        this.y = y;
        this.x2 = x + width;
        this.y2 = y + height;
    }

    public Point Center{
        get {
            return new Point((x + x2)/2, (y+y2)/2);
        }
    }

    public List<Point> InnerArea(){
        List<Point> inner = new List<Point>();
        for(int i = x+1; i < x2; i++){
            for(int j = y+1; j<y2; j++){
                inner.Add(new Point(i,j));
            }
        }
        return inner;
    }

    /// <summary>
    /// Return true if intersetcs woth another room.
    /// </summary>
    public bool Intersects(RectangularRoom room){
        
        return this.x <= room.x2 && this.x2 >= room.x && this.y <= room.y2 && this.y2 >= room.y;
        
    }

    public List<Point> Tunnel(RectangularRoom room){
        Random rand = new Random();
        List<Point> tunnel = new List<Point>();

        Point start = this.InnerArea()[rand.Next(this.InnerArea().Count)];
        Point end = room.InnerArea()[rand.Next(room.InnerArea().Count)];
        
        Point corner;
        if( rand.Next(10) < 5){
            corner = new Point(start.X, end.Y);
        }
        else{
            corner = new Point(end.X, start.Y);
        }
        
        for(int i = start.X; i <= corner.X; i++){
            tunnel.Add(new Point(i, corner.Y));
        }
        for(int i = corner.X; i <= end.X; i++){
            tunnel.Add(new Point(i, corner.Y));
        }
        for(int i = start.Y; i <= corner.Y; i++){
            tunnel.Add(new Point(corner.X, i));
        }
        for(int i = corner.Y; i <= end.Y; i++){
            tunnel.Add(new Point(corner.X, i));
        }

        //reverse CLEAN UP LATER WITH MORE EFFICIENT METHOD
        for(int i = corner.X; i <= start.X; i++){
            tunnel.Add(new Point(i, corner.Y));
        }
        for(int i = end.X; i <= corner.X; i++){
            tunnel.Add(new Point(i, corner.Y));
        }
        for(int i = corner.Y; i <= start.Y; i++){
            tunnel.Add(new Point(corner.X, i));
        }
        for(int i = end.Y; i <= corner.Y; i++){
            tunnel.Add(new Point(corner.X, i));
        }

        return tunnel;
    }
}