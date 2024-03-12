// Author: Jason Morley (Source: http://www.morleydev.co.uk/blog/2010/11/18/generic-bresenhams-line-algorithm-in-visual-basic-net/)
using System;
using System.Diagnostics;

public static class Algorithms
{
    private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

    /// <summary>
    /// The plot function delegate
    /// </summary>
    /// <param name="x">The x co-ord being plotted</param>
    /// <param name="y">The y co-ord being plotted</param>
    /// <returns>True to continue, false to stop the algorithm</returns>
    public delegate bool PlotFunction(int x, int y, ScreenSurface surface, Tile[,] tiles);
    public delegate bool FOVFunction(int x, int y, ScreenSurface surface, Tile[,] tiles);

    /// <summary>
    /// Plot the line from (x0, y0) to (x1, y10
    /// </summary>
    /// <param name="x0">The start x</param>
    /// <param name="y0">The start y</param>
    /// <param name="x1">The end x</param>
    /// <param name="y1">The end y</param>
    /// <param name="plot">The plotting function (if this returns false, the algorithm stops early)</param>
    public static void LineFOV(Point p0, Point p1, ScreenSurface surface, Tile[,] tiles, PlotFunction plot)
    {
        int x0 = p0.X;
        int y0 = p0.Y;
        int x1 = p1.X;
        int y1 = p1.Y;

        bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (steep) { Swap<int>(ref x0, ref y0); Swap<int>(ref x1, ref y1); }
        if (x0 > x1) { Swap<int>(ref x0, ref x1); Swap<int>(ref y0, ref y1); }
        int dX = (x1 - x0), dY = Math.Abs(y1 - y0), err = (dX / 2), ystep = (y0 < y1 ? 1 : -1), y = y0;

        for (int x = x0; x <= x1; ++x)
        {
            if (!(steep ? plot(y, x, surface, tiles) : plot(x, y, surface, tiles))) return;
            err = err - dY;
            if (err < 0) { y += ystep;  err += dX; }
        }
    }

    public static void BasicFOV(Point p0, int distance, ScreenSurface surface, Tile[,] tiles, FOVFunction func){
        int x0 = p0.X;
        int y0 = p0.Y;
        //int x1 = p1.X;
        //int y1 = p1.Y;

        //hroizontals and verticals
        for (int x = x0; x < x0 + distance; x++){
            if(!func(x,y0,surface,tiles)) break;
        }
        for (int x = x0; x > x0 - distance; x--){
            if(!func(x,y0,surface,tiles)) break;
        }
        for (int y = y0; y < y0 + distance; y++){
            if(!func(x0,y,surface,tiles)) break;
        }
        for (int y = y0; y > y0 - distance; y--){
            if(!func(x0,y,surface,tiles)) break;
        }

        for (int i = 0; i < distance; i++){
            int x = x0 + i;
            int y = y0 + i;
            if(!func(x,y,surface,tiles)) break;
            
        }
    }

    public static void FOV360(Point p0, int distance, ScreenSurface surface, Tile[,] tiles, FOVFunction func){
        float x0 = p0.X + 0.5f;
        float y0 = p0.Y + 0.5f;
        
        float angle = 0;
        while(angle <= 360)
        {
            float moveX = MathF.Sin(angle);
            float moveY = MathF.Cos(angle);
            float newX = x0;
            float newY = y0;
            
            float dist = 0;
            while(dist < distance){
                
                newX += moveX;
                newY += moveY;
                float distX = newX - x0;
                float distY = newY - y0;
                dist = MathF.Sqrt(MathF.Pow(distX,2) + MathF.Pow(distY,2));
                if(!func((int)MathF.Floor(newX),(int)MathF.Floor(newY),surface,tiles)) break;
            }
            
            angle += 0.2f;
        }
    }

    
}
