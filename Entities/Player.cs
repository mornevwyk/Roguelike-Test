using System.Security.Cryptography;
using SadConsole.Entities;

class Player : Actor{
    public Player(Map gameMap)
        :base(new SingleCell(Color.Blue, Color.AnsiWhite, '@'), gameMap, 100){
            this.Name = "You";
            this.health = 30;
            this.strength = 3;
        }

}
