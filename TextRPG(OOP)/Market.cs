using System;

namespace TextRPG_OOP_
{
    /// <Summary>
    /// Exchanges coins for other items
    /// </Summary>
    internal class Market
    {
        Player player;
        ItemManager itemManager;

        public Market(Player player, ItemManager itemManager)
        {
            this.player = player;
            this.itemManager = itemManager;

        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }


        public void UpPlayerStats()
            {
                if(player.playerCoins < 3)
                {
                    Console.WriteLine("coin");
                    //player.playerDamage = player.StartingDamage;
                    //healthSystem.armor = 0;
                }
                if(player.playerCoins >= 3 && player.playerCoins < 6)
                {
                    player.playerDamage = player.StartingDamage+2;
                    //healthSystem.armor = 1;
                }
                if(player.playerCoins >= 6 && player.playerCoins < 9)
                {
                    player.playerDamage = player.StartingDamage+3;
                    //healthSystem.armor = 2;
                }
                if(player.playerCoins >= 9 && player.playerCoins < 15)
                {
                    player.playerDamage = player.StartingDamage+5;
                    //healthSystem.armor = 3;
                }
                if(player.playerCoins >= 15 && player.playerCoins < 25)
                {
                    player.playerDamage = player.StartingDamage+7;
                }
                if(player.playerCoins >= 25)
                {
                    player.playerDamage = player.StartingDamage+15;
                }
            }
        }
}