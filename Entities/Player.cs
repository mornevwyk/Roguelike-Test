using System.Security.Cryptography;
using SadConsole.Entities;

class Player : Actor{
    int maxHealth;
    public Player(Map gameMap)
        :base(new SingleCell(Color.Blue, Color.AnsiWhite, '@'), gameMap, 100){
            this.Name = "You";
            this.maxHealth = 30;
            this.health = 30;
            this.strength = 3;
        }

    public override void OnHealthChanged()
    {
        Actions.healthChanged?.Invoke(this.health, this.maxHealth);
        if(health <= 0){
            Actions.gameOver?.Invoke();
        }
    }

}
