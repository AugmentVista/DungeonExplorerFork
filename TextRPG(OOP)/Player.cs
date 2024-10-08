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
        public bool gameIsOver; // MIGRATE RESPONSABILITY
        public bool gameWon; // MIGRATE RESPONSABILITY
        public string enemyHitName; // MIGRATE RESPONSABILITY
        public int enemyHitHealth; // MIGRATE RESPONSABILITY
        public int enemyHitArmor; // MIGRATE RESPONSABILITY
        public int StartingDamage;
        public Map gameMap;
        public char avatar;
        public ItemManager itemShop;
        public Market market;
        public CollisionHandler collisionHandler;
        public Player(Map map, ItemManager itemManager, Settings settings, Market market)
        {
            avatar = (char)2; //Sets player to smiley face.
            healthSystem.IsAlive = true; // initializes player as alive.
            gameIsOver = false; // MIGRATE RESPONSABILITY
            gameWon = false; // MIGRATE RESPONSABILITY
            playerCoins = settings.playerStartingCoins; //starts player with 0 coins.
            StartingDamage = settings.PlayerBaseDamage; //Sets player starting damage
            playerDamage = StartingDamage;
            PlayerMaxHP = settings.playerMaxHP; //Sets stating health
            healthSystem.SetHealth(PlayerMaxHP); // hands starting value to health system
            market?.SetPlayer(this); // Grabs reference from Market
            name = "Koal"; // Testing for passing string.
            enemyHitName = ""; // clears enemy hit for starting
            gameMap = map; // hands map to player
            itemShop = itemManager; // hands item manager to player
            this.market = market;

            // Pass 'this' instead of creating a new instance
            collisionHandler = new CollisionHandler(map, itemManager, settings, market, this);
        }
        public void Begin()
        {
            SetMaxPlayerPosition(gameMap);
        }
        
        public void SetMaxPlayerPosition(Map map)
        {
            int mapX;
            int mapY;
            mapX = map.activeMap.GetLength(1);
            mapY = map.activeMap.GetLength(0);
            position.maxX = mapX - 1;
            position.maxY = mapY - 1;
        }

        
        public void SetPlayerPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
        }

        public void Update()
        {
            ConsoleKeyInfo playerInput = Console.ReadKey(true);
            collisionHandler.HandleMovement(playerInput, gameMap);
            market.PlayerCoinsCheck();
        }
        public void Attack(int moveDirection, Map map)
        {
           if(map.CreatureInTarget(moveDirection, position.x) && map.index != 0) // Player should always be 0, need to prevent self harm.
            {
                map.characters[map.index].healthSystem.TakeDamage(playerDamage);
                enemyHitName = map.characters[map.index].name;
                enemyHitHealth = map.characters[map.index].healthSystem.health;
                enemyHitArmor = map.characters[map.index].healthSystem.armor;
                return;
            } 
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
