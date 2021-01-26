using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bowling
{
    class Game
    {
        private int numPlayers = 0;
        private Player[] playerList;
        private int playerTurn;
        private int totalFrames = 10;
        private static int ballDeliveryCount = 1;
        private int pinsLeft = 10;
        private int maxPlayers = 10;
        private ScoreBoard scoreBoard;

        public void Run()
        {
            CreatePlayers();
            CreateScoreBoard();
            Play();

        }

        private void Play()
        {

            for(var frameNumber= 1; frameNumber <= totalFrames; frameNumber++)
            {
                //frames 1 - 9
                if (ballDeliveryCount < 19) 
                {
                   
                    foreach (var player in playerList)
                    {
                        scoreBoard.DisplayScoreboard();
                        Bowl(frameNumber, player);

                        if (pinsLeft > 0)
                        {
                            Bowl(frameNumber, player);

                            //reset for next player
                            ballDeliveryCount -= 2;
                            pinsLeft = 10;
                        }
                        else{
                            //reset for next player
                            ballDeliveryCount -= 1;
                            pinsLeft = 10;
                        }
                        
                    }
                    //Set for start of next frame
                    ballDeliveryCount += 2;
                    
                }
                else //frame 10
                {                   
                    foreach (var player in playerList)
                    {
                        //1st ball
                        scoreBoard.DisplayScoreboard();
                        Bowl(frameNumber, player);

                        //2nd ball, player can try for spare
                        if (pinsLeft > 0)
                        {
                            Bowl(frameNumber, player);

                            if (pinsLeft == 0) //3rd ball, earned for getting spare
                            {
                                //Reset pins
                                pinsLeft = 10;

                                Bowl(frameNumber, player);

                                //reset for next player
                                ballDeliveryCount -= 3;
                                pinsLeft = 10;
                            }
                            else //no extra ball earned, end turn
                            {
                                //reset for next player
                                ballDeliveryCount -= 2;
                                pinsLeft = 10;
                            }
                        }
                        else //2 balls earned for getting a strike, 2nd ball
                        {
                            //reset pins
                            pinsLeft = 10;

                            Bowl(frameNumber, player);

                            if (pinsLeft == 0) //Strike, 3rd ball
                            {
                                //Reset pins
                                pinsLeft = 10;

                                Bowl(frameNumber, player);

                                //Reset for next player
                                ballDeliveryCount -= 3;
                                pinsLeft = 10;
                            }
                            else //3rd ball, player can try for spare
                            {
                                Bowl(frameNumber, player);

                                //Reset for next player
                                ballDeliveryCount -= 3;
                                pinsLeft = 10;
                            }
                        }
                        
                    }
                }
            }
           
        }

        private void Bowl(int frameNumber, Player player)
        {
            var bowledPinCount = GetPlayerPinsBowled(frameNumber, player);
            scoreBoard.UpdatePlayerScore(player, bowledPinCount, ballDeliveryCount - 1);
            ballDeliveryCount += 1;
            pinsLeft -= bowledPinCount;
        }

        private int GetPlayerPinsBowled(int frameNumber, Player player)
        {
            int bowledPinCount;
            do
            {
                Console.WriteLine($"{player.playerName}, please enter your score for the { GetBallNumber(ballDeliveryCount) } bowl of frame { frameNumber }(Pins left: { pinsLeft })");
                int.TryParse(Console.ReadLine(), out bowledPinCount);
                if (bowledPinCount < 0 || bowledPinCount > pinsLeft)
                {
                    //Console.Clear();
                    Console.WriteLine("You entered an invalid number.");
                    //Console.WriteLine($"{player.playerName}, please enter your score for the { GetBallNumber(ballDeliveryCount) } bowl of frame { frameNumber }(Pins left: { pinsLeft })");

                }
            } while (bowledPinCount < 0 || bowledPinCount > pinsLeft);

            return bowledPinCount;
        }

        private object GetBallNumber(int ballDeliveryCount)
        {
            if (ballDeliveryCount == 21)
            {
                return "3rd";
            }
            else if (ballDeliveryCount % 2 != 0) 
            {
                return "1st";
            } 
            else 
            {
                return "2nd";
            }
        }

        private void CreateScoreBoard()
        {
            scoreBoard = new ScoreBoard(playerList);
        }

        public void CreatePlayers()
        {
            Console.Write("Please enter how many players will be playing in this game (between 1 and 10): ");
            do
            {
                int.TryParse(Console.ReadLine(), out numPlayers);

                if (numPlayers < 1 || numPlayers > maxPlayers)
                {
                    Console.Clear();
                    Console.WriteLine("You entered an invalid number.");
                    Console.Write($"Please enter how many players will be playing in this game (between 1 and { maxPlayers}): ");
                }
            } while (numPlayers < 1 || numPlayers > maxPlayers) ;

                Console.WriteLine($"Creating { numPlayers } players...");

            var playerNames = new string[numPlayers];

            for (int i = 1; i <= numPlayers; i++)
            {
                Console.Write($"Please enter the name of player { i }: ");
                string playerName = Console.ReadLine();
                playerNames[i - 1] = playerName;
            }

            playerList = playerNames.Select(x => new Player(x)).ToArray();

            foreach (var player in playerList)
            {
                Console.WriteLine($"Player { player.GetPlayerID() + 1}: { player.playerName } has been created");
            }
            Thread.Sleep(2000);
            Console.Clear();
        }
    }     
}

