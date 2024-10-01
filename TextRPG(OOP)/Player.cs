using System;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Is the player, Handles all player interactions.
    /// </summary>
    internal class Player : Character
    {
        public int playerDamage;
        public int playerCoins;
        public int PlayerMaxHP;
        public bool gameIsOver;
        public bool gameWon;
        public string enemyHitName;
        public int enemyHitHealth;
        public int enemyHitArmor;
        public int StartingDamage;
        public Map gameMap;
        public char avatar;
        public ItemManager itemManager;
        public Market market;
        public CollisionHandler collisionHandler;
        public Player(Map map, ItemManager IM, Settings settings, Market market)
        {
            avatar = (char)2; //Sets player to smiley face.
            healthSystem.IsAlive = true; // initilizes player as alive.
            gameIsOver = false;
            gameWon = false;
            playerCoins = settings.playerStartingCoins; //starts player with 0 coins.
            StartingDamage = settings.PlayerBaseDamage; //Sets player starting damage
            playerDamage = StartingDamage; 
            PlayerMaxHP = settings.playerMaxHP; //Sets stating health
            healthSystem.SetHealth(PlayerMaxHP);//hands starting value to health system
            market?.SetPlayer(this);// Grabs reference from Market
            name = "Koal"; // Testing for passing string.
            enemyHitName = ""; //clears enemy hit for starting
            gameMap = map; //hands map to player
            itemManager = IM; //hands item manager to player
            this.market = market;
            collisionHandler = new CollisionHandler(map, IM, settings, market);
        }
        public void Start()
        {
            collisionHandler.SetMaxPlayerPosition(gameMap);
        }
        public void Update()
        {
            ConsoleKeyInfo playerInput = Console.ReadKey(true);
            collisionHandler.HandleMovement(playerInput.Key, gameMap, CollisionHandler.Direction.Up); // Pass the current direction as needed
            market.PlayerCoinsCheck();
        }

        public void Draw()
        {
            // used to draw the player
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(position.x,position.y);
            Console.Write(avatar);
            gameMap.SetColorDefault();
        }
    }
}
