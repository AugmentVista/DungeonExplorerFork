using System;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    /// <Summary>
    /// Exchanges coins for other items
    /// </Summary>
    internal class Market
    {
        Player player;
        ItemManager itemManager;

        public float[] portfoilo;

        public float apple_Stock;

        public float walmart_Stock;

        public float amazon_Stock;

        public Market(Player player, ItemManager itemManager)
        {
            this.player = player;
            this.itemManager = itemManager;

        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }


        public void PlayerCoinsCheck()
            {
                if(player.playerCoins < 3)
                {
                    Debug.WriteLine("Player has " + player.playerCoins + " coins");
                    //player.playerDamage = player.StartingDamage;
                    //healthSystem.armor = 0;
                }
            }
        }
}