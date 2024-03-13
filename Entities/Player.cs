using System.Security.Cryptography;
using SadConsole.Entities;

class Player : Entity{
    int health = 20;
    int strength = 4;
    public Player()
        :base(new Entity.SingleCell(Color.White, Color.AnsiBlack, '@'), 100){}

    public void MeleeAttack(Point position){
        Actions.LogEvent?.Invoke($"You attacked for {RandomNumberGenerator.GetInt32(10)} damage!");
    }
}
