using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    class ScoreBoard
    {
        int numberOfPlayers;
        public int[,] playerScores;
        int columnWidth = 5;
        int columnCount = 10;
        int rowHeight = 12;
        string[] columnHeaders = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public ScoreBoard(Player[] playerList)
        {
            numberOfPlayers = playerList.Length;
            playerScores = new int[numberOfPlayers, 21];


        }

        public void DisplayScoreboard()
        {
            //(186: ║), (187: ╗) (188: ╝), (200: ╚), (201: ╔), (205: ═)

            //top row
            Console.Write("╔");
            Console.Write(new string('═', columnCount + columnWidth * 10));
            Console.Write("╗\n");

            //2nd 

            Console.Write("║");
            for (var i = 0; i < 10; i++)
            {            
                Console.Write(new string(' ', columnWidth));
                Console.Write("║");
            }


            //3rd
            Console.Write("\n║");
            Console.Write(new string('═', columnCount + columnWidth * 10));
            Console.Write("║\n");
        }

        public void UpdatePlayerScore(Player player, int numberOfPins, int ballsBowled)
        {
            playerScores[player.GetPlayerID(), ballsBowled] = numberOfPins;
        }

    }
}
