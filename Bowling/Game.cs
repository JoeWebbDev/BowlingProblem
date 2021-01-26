using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bowling
{
    class Game
    {
        private int numPlayers = 0;
        private Player[] playerList;
        private int playerTurn;
        private int totalFrames = 10;
        private int ballDeliveryCount = 1;
        private ScoreBoard scoreBoard;
        
        //one frame 2 ball deliveries
        //calculate current score for frame, if bonus is earned enable bonus
        //play next frame, if bonus is earned enable bonus
        //apply bonus points to previous frame

        //frame 1 & 2 follow these rules
        //if you get a strike, the points from your next 2 balls get added as a bonus for that fram
        //if you get a spare, the poins from your next 1 ball get added as a bonus for that frame

        //final frame
        //you get bonus balls if you strike/spare up to a max of 3 balls
        //if you strike, you get 2 more balls
        //if you spare, you get a 3rd ball

        public Game()
        {

        }

        public void Run()
        {
            CreatePlayers();
            CreateScoreBoard();
            scoreBoard.DisplayScoreboard();
            Play();

        }

        private void Play()
        {
            for(var i= 1; i <= totalFrames; i++)
            {
                if(ballDeliveryCount < 19)
                {
                    foreach (var player in playerList)
                    {
                        Console.WriteLine($"{player.playerName}, please enter your score for the { GetBallNumber(ballDeliveryCount) } bowl of frame { i }");
                        int.TryParse(Console.ReadLine(), out int bowledPinCount);
                        scoreBoard.UpdatePlayerScore(player, bowledPinCount, ballDeliveryCount - 1);
                        ballDeliveryCount += 1;
                        Console.WriteLine($"{player.playerName}, please enter your score for the { GetBallNumber(ballDeliveryCount) } bowl of frame { i }");
                        int.TryParse(Console.ReadLine(), out bowledPinCount);
                        scoreBoard.UpdatePlayerScore(player, bowledPinCount, ballDeliveryCount - 1);
                        ballDeliveryCount -= 1;
                    }
                    ballDeliveryCount += 2;
                }
                else
                {
                    Console.WriteLine("final frame");
                }
            }
           
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
            int numPlayers = 0;
            while (numPlayers < 1 || numPlayers > 10)
            {
                int.TryParse(Console.ReadLine(), out numPlayers);

                if (numPlayers < 1 || numPlayers > 10)
                {
                    Console.Clear();
                    Console.WriteLine("You entered an invalid number.");
                    Console.Write("Please enter how many players will be playing in this game (between 1 and 10): ");
                }
            }

            Console.WriteLine($"You chose { numPlayers } players!");

            var playerNames = new string[numPlayers];

            for (int i = 1; i <= numPlayers; i++)
            {

                Console.Write($"Please enter the name of player { i }: ");
                string playerName = Console.ReadLine();
                playerNames[i - 1] = playerName;
                Console.Clear();
            }

            playerList = playerNames.Select(x => new Player(x)).ToArray();

            foreach (var player in playerList)
            {
                Console.WriteLine($"Player { player.GetPlayerID()}: { player.playerName } has been created");
            }
        }
    }     
}

