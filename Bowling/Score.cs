using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    class Score
    {
        int[] scores;
        public int[] runningTotals { get; private set; }
        public string[] scoreDisplay { get; private set; }

        public Score()
        {
            scores = new int[21];
            scoreDisplay = new string[21];
            runningTotals = new int[10];
        }

        public void UpdateScore(int numberOfPins, int totalBallsBowled, int ballNumber, int frameNumber)
        {
            scores[totalBallsBowled - 1] = numberOfPins;

            if (frameNumber < 10)
            {
                if (ballNumber == 1 && IsStrike(scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled] = "X";
                    //STRIKE

                }
                else if (ballNumber == 2 && isSpare(scores[totalBallsBowled - 2], scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "/";
                    //SPARE

                }
                else
                {
                    scoreDisplay[totalBallsBowled - 1] = scores[totalBallsBowled - 1].ToString();
                    //OPEN FRAME

                }
            }
            else
            {
                if (IsStrike(scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "X";
                }
                else if (ballNumber > 1 && isSpare(scores[totalBallsBowled - 2], scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "/";
                }
                else
                {
                    scoreDisplay[totalBallsBowled - 1] = scores[totalBallsBowled - 1].ToString();
                }
            }
        }

        public void UpdateRunningTotal()
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
