using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    /// <Summary>
    /// Handles Collision and player movement
    /// </Summary>
    internal class CollisionHandler
    {
        public ConsoleKeyInfo playerInput;
        public Map map;
        public ItemManager ItemManager;
        public Market market;

        public CollisionHandler(Map map, ItemManager itemManager, Settings settings, Market market)
       : base(map, itemManager, settings, market) // Call to base constructor
        {
            this.map = map;
            this.ItemManager = itemManager;
            this.market = market;
        }

        public enum Direction
        {
            Up, Right, Down, Left

        }

        public void SetPlayerPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
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

        public void ConsumeItem(Map map)
        {
            itemManager.items[map.itemIndex].isActive = false;
            itemManager.items[map.itemIndex].position.x = 0;
            itemManager.items[map.itemIndex].position.y = 0;
        }

        public void HitCreature(int moveDirection, Map map)
        {
            if(map.CreatureInTarget(moveDirection, position.x) && map.index != 0) // Player should always be 0, need to prevent self harm.
            {
                map.characters[map.index].healthSystem.TakeDamage(playerDamage);
                enemyHitName = map.characters[map.index].name;
                enemyHitHealth = map.characters[map.index].healthSystem.health;
                enemyHitArmor = map.characters[map.index].healthSystem.armor;
                Debug.WriteLine("Player Hit " + enemyHitName);
                return;
            }
        }

         public void CollectItem(int moveDirection, Map map)
        {
            if(map.ItemInTarget(moveDirection, position.x) && itemManager.items[map.itemIndex].isActive)
            {
                if(itemManager.items[map.itemIndex].itemType == "Health Pickup" && healthSystem.health < PlayerMaxHP)
                {
                    ConsumeItem(map);
                    healthSystem.Heal(itemManager.items[map.itemIndex].gainAmount, PlayerMaxHP);
                }
                if(itemManager.items[map.itemIndex].itemType == "Armor Pickup")
                {
                    ConsumeItem(map);
                    healthSystem.armor += itemManager.items[map.itemIndex].gainAmount;
                }
                if(itemManager.items[map.itemIndex].itemType == "Coin")
                {
                    ConsumeItem(map);
                    playerCoins += itemManager.items[map.itemIndex].gainAmount;
                }
            }
        }

        public (int moveX, int moveY) OutOfBoundsCheck(Direction currentDirection, Map map)
        {
            int moveX = position.x;
            int moveY = position.y;
            bool legalMove = false;

            switch (currentDirection)
            {
                case Direction.Up:
                    moveY = position.y - 1;
                    if (moveY >= 0 && moveY < map.activeMap.GetLength(0))
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Right:
                    moveX = position.x + 1;
                    if (moveX >= 0 && moveX < map.activeMap.GetLength(1))
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Down:
                    moveY = position.y + 1;
                    if (moveY >= 0 && moveY < map.activeMap.GetLength(0))
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Left:
                    moveX = position.x - 1;
                    if (moveX >= 0 && moveX < map.activeMap.GetLength(1))
                    {
                        legalMove = true;
                    }
                    break;
            }

            if (legalMove)
            {
                position.x = moveX;
                position.y = moveY;
            }

            return (moveX, moveY); // Return the new moveX and moveY
        }


        public void HandleMovement(ConsoleKey key, Map map, Direction currentDirection)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            var playerInput = Console.ReadKey(true);
            switch (playerInput.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    currentDirection = Direction.Up;
                    var (moveXUp, moveYUp) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveYUp, map);
                    CollectItem(moveYUp, map);
                    CheckGameState(map, moveXUp, moveYUp); 
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    currentDirection = Direction.Right;
                    var (moveXRight, moveYRight) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveXRight, map);
                    CollectItem(moveXRight, map);
                    CheckGameState(map, moveXRight, moveYRight); 
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    currentDirection = Direction.Down;
                    var (moveXDown, moveYDown) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveYDown, map);
                    CollectItem(moveYDown, map);
                    CheckGameState(map, moveXDown, moveYDown); 
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    currentDirection = Direction.Left;
                    var (moveXLeft, moveYLeft) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveXLeft, map);
                    CollectItem(moveXLeft, map);
                    CheckGameState(map, moveXLeft, moveYLeft); 
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }

        private void CheckGameState(Map map, int moveX, int moveY)
        {
            char tile = map.activeMap[moveY, moveX]; // reads new position char

            if (tile == '$')
            {
                // Ends game when touching the "Grail"
                gameWon = true;
                gameIsOver = true;
            }
            else if (tile == '~')
            {
                // Advances to next level
                map.levelNumber += 1;
                map.ChangeLevels();
                SetPlayerPosition(map.playerX, map.playerY);
            }
            else if (tile == '*')
            {
                // Hurts player
                healthSystem.health -= 1;
            }
        }

    }
}