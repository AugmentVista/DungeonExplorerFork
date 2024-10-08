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
        public ItemManager itemManager;
        public Market market;
        private Player player;

        private int positionX;
        private int positionY;

        public CollisionHandler(Map map, ItemManager itemManager, Settings settings, Market market, Player player)
        {
            this.map = map;
            this.itemManager = itemManager;
            this.market = market;
            this.player = player; 
            positionY = player.position.y;
            positionX = player.position.x;
        }

        public enum Direction
        {
            Up, Right, Down, Left
        }

        public (int moveX, int moveY) OutOfBoundsCheck(Direction currentDirection, Map map)
        {
            int moveX = positionX;
            int moveY = positionY;
            bool legalMove = false;

            switch (currentDirection)
            {
                case Direction.Up:
                    moveY = positionY - 1;
                    if (moveY >= 0 && moveY < map.activeMap.GetLength(0) && map.CanMove)
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Right:
                    moveX = positionX + 1;
                    if (moveX >= 0 && moveX < map.activeMap.GetLength(1) && map.CanMove)
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Down:
                    moveY = positionY + 1;
                    if (moveY >= 0 && moveY < map.activeMap.GetLength(0) && map.CanMove)
                    {
                        legalMove = true;
                    }
                    break;

                case Direction.Left:
                    moveX = positionX - 1;
                    if (moveX >= 0 && moveX < map.activeMap.GetLength(1) && map.CanMove)
                    {
                        legalMove = true;
                    }
                    break;
            }

            if (legalMove)
            {
                positionX = moveX;
                positionY = moveY;
                player.SetPlayerPosition(positionX, positionY);
            }

            return (moveX, moveY); // Return the new moveX and moveY
        }

        public void HandleMovement(ConsoleKeyInfo key, Map map)
        {
            Direction currentDirection;

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
                    CheckGameState(map, moveXUp, moveYUp); 
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    currentDirection = Direction.Right;
                    var (moveXRight, moveYRight) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveXRight, map);
                    CheckGameState(map, moveXRight, moveYRight); 
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    currentDirection = Direction.Down;
                    var (moveXDown, moveYDown) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveYDown, map);
                    CheckGameState(map, moveXDown, moveYDown); 
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    currentDirection = Direction.Left;
                    var (moveXLeft, moveYLeft) = OutOfBoundsCheck(currentDirection, map);
                    HitCreature(moveXLeft, map);
                    CheckGameState(map, moveXLeft, moveYLeft); 
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
            itemManager.CheckItemPositions(player);
        }
        public void HitCreature(int moveDirection, Map map)
        {
            player.Attack(moveDirection, map);
        }

        private void CheckGameState(Map map, int moveX, int moveY)
        {
        //    Debug.WriteLine(map.activeMap[moveX, moveY]);
        //    char tile = map.activeMap[moveY, moveX]; // reads new position char

        //    if (tile == '$')
        //    {
        //        // Ends game when touching the "Grail"
        //        //gameWon = true;
        //        //gameIsOver = true;
        //    }
        //    else if (tile == '~')
        //    {
        //        // Advances to next level
        //        map.levelNumber += 1;
        //        map.ChangeLevels();
        //        player.SetPlayerPosition(map.playerX, map.playerY);
        //    }
        //    else if (tile == '*')
        //    {
        //        // Hurts player
        //        player.healthSystem.health -= 1;
        //    }
        }
    }
}