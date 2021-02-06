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
        string[,] playerScoresDisplay;
        int[,] runningTotals;
        string[] playerNames;

        public ScoreBoard(Player[] playerList)
        {
            numberOfPlayers = playerList.Length;
            playerNames = playerList.Select(x => x.playerName).ToArray();
            playerScores = new int[numberOfPlayers, 21];
            playerScoresDisplay = new string[numberOfPlayers, 21];
            runningTotals = new int[numberOfPlayers, 10];
            
        }

        public void DisplayScoreboard()
        {
            //(186: ║), (187: ╗) (188: ╝), (200: ╚), (201: ╔), (205: ═)
            Console.Clear();

            Console.Write('\n');

            //Construct header
            var padding = 1;
            var nameColumnWidth = playerNames.Select(x => x.Length).Max() + (padding * 2);
            var nameColumnHeader = "Name";
            nameColumnHeader = nameColumnHeader.PadLeft(nameColumnWidth / 2 + nameColumnHeader.Length / 2).PadRight(nameColumnWidth);
            var header = $"|{nameColumnHeader}|   1   |   2   |   3   |   4   |   5   |   6   |   7   |   8   |   9   |    10     | Total |\n";

            //Draw header
            Console.Write(new string('═', header.Length) + "\n");
            Console.Write(header);
            Console.Write(new string('═', header.Length) + "\n");

            for (var i = 0; i < numberOfPlayers; i++)
            {              
                Console.Write($"|{ new string(' ', padding) + playerNames[i].PadLeft(nameColumnWidth - 2) + new string(' ', padding) }| ");
                for (var j = 0; j < 21; j++)
                {       
                    //draw player score
                    if(playerScoresDisplay[i, j] == null)
                    {
                        Console.Write($" ");
                    }
                    else
                    {
                        Console.Write($"{playerScoresDisplay[i, j]}");
                    }
                    Console.Write(new string(' ', padding) + "|" + new string(' ', padding));

                    //Display total
                                 
                }
                Console.Write('\n');
                Console.Write($"|{ new string(' ', nameColumnWidth) }| ");
                for (var k = 0; k < 10; k++)
                {
                    //draw running totals
                    if (k < 9)
                    {                      
                        Console.Write($"{runningTotals[i, k].ToString().PadLeft(5)}");
                        Console.Write(new string(' ', padding) + "|" + new string(' ', padding));
                    }
                    else
                    {
                        Console.Write($"{runningTotals[i, k].ToString().PadLeft(9)}");
                        Console.Write(new string(' ', padding) + "|" + new string(' ', padding));
                    }                  
                }

                Console.Write('\n');
                Console.Write(new string('═', header.Length) + "\n");
            }
            Console.Write('\n');
        }

        public void UpdatePlayerScore(Player player, int numberOfPins, int totalBallsBowled,int ballNumber, int frameNumber)
        {
            playerScores[player.GetPlayerID(), totalBallsBowled - 1] = numberOfPins;

            if(frameNumber < 10)
            {
                if (ballNumber == 1 && IsStrike(playerScores[player.GetPlayerID(), totalBallsBowled - 1]))
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled] = "X";
                    //STRIKE

                }
                else if (ballNumber == 2 && isSpare(playerScores[player.GetPlayerID(), totalBallsBowled - 2], playerScores[player.GetPlayerID(), totalBallsBowled - 1]))
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled - 1] = "/";
                    //SPARE

                }
                else
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled - 1] = playerScores[player.GetPlayerID(), totalBallsBowled - 1].ToString();
                    //OPEN FRAME

                }
            }
            else
            {
                if (IsStrike(playerScores[player.GetPlayerID(), totalBallsBowled - 1]))
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled -1] = "X";
                }
                else if (ballNumber > 1 && isSpare(playerScores[player.GetPlayerID(), totalBallsBowled - 2], playerScores[player.GetPlayerID(), totalBallsBowled - 1]))
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled - 1] = "/";
                }
                else
                {
                    playerScoresDisplay[player.GetPlayerID(), totalBallsBowled - 1] = playerScores[player.GetPlayerID(), totalBallsBowled - 1].ToString();
                }
            }
                  
            //UpdateRunningTotal(player, frameNumber);
            DisplayScoreboard();
        }

        private void UpdateRunningTotal(Player player, int totalBallsBowled, int ballNumber, int frameNumber)
        {
            
            
        }

        private bool IsStrike(int firstBowl)
        {
            if (firstBowl == 10)
                return true;
            else
                return false;
        }
        
        private bool isSpare(int firstBowl, int secondBowl)
        {
            if (secondBowl > 0 && firstBowl + secondBowl == 10)
                return true;
            else
                return false;
        }
    }
}
