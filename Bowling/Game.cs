using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bowling
{
    public class Game
    {
        private Player[] playerList;
        private const int TotalFrames = 10;
        private const int MaxPlayers = 10;
        private int _ballDeliveryCount = 1;
        private int _pinsLeft = 10;
        private ScoreBoard scoreBoard;

        public void Run()
        {
            CreatePlayers();
            CreateScoreBoard();
            Play();
        }

        private void Play()
        {

            for (var frameNumber = 1; frameNumber <= TotalFrames; frameNumber++)
            {
                //frames 1 - 9
                if (_ballDeliveryCount < 18)
                {
                    foreach (var player in playerList)
                    {
                        SetupFrame(frameNumber);
                        scoreBoard.DisplayScoreboard(playerList);
                        Bowl(frameNumber, player);

                        if (_pinsLeft > 0)
                        {
                            Bowl(frameNumber, player);
                        }
                    }
                }
                else //frame 10
                {
                    foreach (var player in playerList)
                    {
                        SetupFrame(frameNumber);
                        //1st ball
                        scoreBoard.DisplayScoreboard(playerList);
                        Bowl(frameNumber, player);

                        //2nd ball, player can try for spare
                        if (_pinsLeft > 0)
                        {
                            Bowl(frameNumber, player);

                            if (_pinsLeft == 0) //3rd ball, earned for getting spare
                            {
                                ResetPins();
                                Bowl(frameNumber, player);
                            }
                        }
                        else //2 balls earned for getting a strike, 2nd ball
                        {
                            ResetPins();
                            Bowl(frameNumber, player);
                            if (_pinsLeft == 0) //Strike, 3rd ball
                            {
                                ResetPins();
                                Bowl(frameNumber, player);
                            }
                            else //3rd ball, player can try for spare
                            {
                                Bowl(frameNumber, player);
                            }
                        }

                    }
                }
            }

            do
            {
                Console.Clear();
                scoreBoard.DisplayScoreboard(playerList);
                Console.WriteLine();
                Console.WriteLine("Enter Q to quit the application");
            } while (Console.ReadKey().Key != ConsoleKey.Q);

        }
        private void SetupFrame(int frameNumber)
        {
            //Resets ball delivery count according to frame number.
            _ballDeliveryCount = frameNumber * 2 - 1;
            ResetPins();
        }

        private void ResetPins()
        {
            _pinsLeft = 10;
        }

        private void Bowl(int frameNumber, Player player)
        {
            var bowledPinCount = GetPlayerPinsBowled(frameNumber, player);
            player.score.UpdateScore(bowledPinCount, _ballDeliveryCount, GetBallNumber(_ballDeliveryCount), frameNumber);
            scoreBoard.DisplayScoreboard(playerList);
            _ballDeliveryCount += 1;
            _pinsLeft -= bowledPinCount;
        }

        private int GetPlayerPinsBowled(int frameNumber, Player player)
        {
            int bowledPinCount;
            do
            {
                Console.WriteLine($"{player.name}, please enter your score for the { GetOrdinalBallNumber(_ballDeliveryCount) } bowl of frame { frameNumber }(Pins left: { _pinsLeft })");
                int.TryParse(Console.ReadLine(), out bowledPinCount);
                if (bowledPinCount < 0 || bowledPinCount > _pinsLeft)
                {
                    Console.WriteLine("You entered an invalid number.");
                }
            } while (bowledPinCount < 0 || bowledPinCount > _pinsLeft);

            return bowledPinCount;
        }

        private string GetOrdinalBallNumber(int ballDeliveryCount)
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

        private int GetBallNumber(int ballDeliveryCount)
        {
            if (ballDeliveryCount == 21)
            {
                return 3;
            }
            else if (ballDeliveryCount % 2 != 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private void CreateScoreBoard()
        {
            scoreBoard = new ScoreBoard();
        }

        public void CreatePlayers()
        {

            string[] playerNames = GetPlayerNames(GetPlayerCount);
            playerList = ConstructPlayers(playerNames);

            Thread.Sleep(2000);
            Console.Clear();
        }

        private Player[] ConstructPlayers(string[] playerNames)
        {
            var playerList = playerNames.Select(x => new Player(x)).ToArray();

            foreach (var player in playerList)
            {
                Console.WriteLine($"Player { player.GetPlayerID() + 1}: { player.name } has been created");
            }

            return playerList;
        }

        private string[] GetPlayerNames(Func<int> GetPlayerCount)
        {
            int playerCount = GetPlayerCount();
            Console.WriteLine($"Creating { playerCount } players...");

            var playerNames = new string[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"Please enter the name of player { i + 1 }: ");
                string playerName = Console.ReadLine();
                playerNames[i] = playerName;
            }

            return playerNames;
        }

        private int GetPlayerCount()
        {
            Console.Write("Please enter how many players will be playing in this game (between 1 and 10): ");

            while (true)
            {
                int.TryParse(Console.ReadLine(), out int playerCount);
                if (playerCount < 1 || playerCount > MaxPlayers)
                {
                    Console.Clear();
                    Console.WriteLine("You entered an invalid number.");
                    Console.Write($"Please enter how many players will be playing in this game (between 1 and { MaxPlayers }): ");
                }
                else
                {
                    return playerCount;
                }
            }
        }
    }     
}

