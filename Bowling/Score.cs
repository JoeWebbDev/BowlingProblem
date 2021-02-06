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
        public string finalScore { get; private set; }

        private BonusScore bonusScore;

        public Score()
        {
            scores = new int[21];
            scoreDisplay = new string[21];
            runningTotals = new int[10];
            bonusScore = new BonusScore();
            finalScore = String.Empty;
        }

        public void UpdateScore(int numberOfPinsBowled, int totalBallsBowled, int ballNumber, int frameNumber)
        {
            scores[totalBallsBowled - 1] = numberOfPinsBowled;
            bonusScore.IncrementCount();
            bonusScore.UpdateBonusScores(numberOfPinsBowled);

            if (frameNumber < 10)
            {
                if (ballNumber == 1 && IsStrike(scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled] = "X";
                    //STRIKE BONUS
                    bonusScore.AddBonus(frameNumber, numberOfPinsBowled, ScoreType.Strike);
                }
                else if (ballNumber == 2 && isSpare(scores[totalBallsBowled - 2], scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "/";
                    //SPARE BONUS
                    bonusScore.AddBonus(frameNumber, numberOfPinsBowled, ScoreType.Spare);
                }
                else
                {
                    scoreDisplay[totalBallsBowled - 1] = scores[totalBallsBowled - 1].ToString();
                    //this will always trigger if no strike
                    if (ballNumber == 1)
                    {
                        bonusScore.AddBonus(frameNumber, numberOfPinsBowled, ScoreType.OpenFrame);
                    }

                }
                UpdateRunningTotal(frameNumber, totalBallsBowled, ballNumber);
            }
            else
            {
                if (IsStrike(scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "X";
                    if(ballNumber == 1)
                    {
                        bonusScore.AddBonus(frameNumber, numberOfPinsBowled, ScoreType.FinalFrame);
                    }
                }
                else if (ballNumber > 1 && isSpare(scores[totalBallsBowled - 2], scores[totalBallsBowled - 1]))
                {
                    scoreDisplay[totalBallsBowled - 1] = "/";
                }
                else
                {
                    scoreDisplay[totalBallsBowled - 1] = scores[totalBallsBowled - 1].ToString();
                    if (ballNumber == 1)
                    {
                        bonusScore.AddBonus(frameNumber, numberOfPinsBowled, ScoreType.FinalFrame);
                    }
                }
                UpdateRunningTotal(frameNumber, totalBallsBowled, ballNumber);

                if(totalBallsBowled == 21)
                {
                    finalScore = runningTotals[9].ToString();
                }
            }
            
        }

        public void UpdateRunningTotal(int frameNumber, int totalBallsBowled, int ballNumber)
        {
            //check for any complete bonuses. If complete, update running totals accordingly
            bonusScore.ApplyCompleteBonuses(runningTotals);

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
