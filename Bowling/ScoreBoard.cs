using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    class ScoreBoard
    {
        int numberOfPlayers;
        public int[,] playerScores;
        int[,] runningTotals;
        string[] playerNames;

        int rowHeight = 12;

        public ScoreBoard(Player[] playerList)
        {
            numberOfPlayers = playerList.Length;
            playerNames = playerList.Select(x => x.playerName).ToArray();
            playerScores = new int[numberOfPlayers, 21];
            runningTotals = new int[numberOfPlayers, 10];
        }

        public void DisplayScoreboard()
        {
            //(186: ║), (187: ╗) (188: ╝), (200: ╚), (201: ╔), (205: ═)
            Console.Clear();

            for (var i = 0; i < numberOfPlayers; i++)
            {               
                //top row
                Console.Write('\n');
                Console.Write($"{ playerNames[i] }'s Score Card\n");
                Console.Write(new string('═', 85));
                Console.Write('\n');
                for (var j = 0; j < 21; j++)
                {
                    Console.Write($"{playerScores[i, j]} | ");
                }
                
                Console.Write('\n');
                Console.Write(new string('═', 85));
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        public void UpdatePlayerScore(Player player, int numberOfPins, int ballsBowled)
        {
            playerScores[player.GetPlayerID(), ballsBowled] = numberOfPins;
            DisplayScoreboard();
        }

    }
}
