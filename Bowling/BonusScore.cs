using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    class BonusScore
    {
        List<int[]> bonusList;

        public BonusScore()
        {
            bonusList = new List<int[]>();
        }

        public void AddBonus(int frameNumber, int score, ScoreType scoreType)
        {
            if (scoreType == ScoreType.Spare)
            {
                score = bonusList.First(x => x[2] == (int)ScoreType.OpenFrame)[1];
                RemoveBonus(ScoreType.OpenFrame);
            }
            var bonus = new int[] { frameNumber, score, (int)scoreType, 1 };
            bonusList.Add(bonus);            
        }

        public void UpdateBonusScores(int score)
        {
            foreach(var bonus in bonusList)
            {
                bonus[1] += score;
            }

        }

        internal void IncrementCount()
        {
            foreach(var bonus in bonusList)
            {
                bonus[3] += 1;
            }
        }
        internal void ApplyCompleteBonuses(int[] runningTotals)
        {
            foreach(var bonus in bonusList)
            {
                //apply strikes && spares first
                if (IsBonusComplete(bonus))
                {
                    if(bonus[0] > 1)
                    {
                        runningTotals[bonus[0] - 1] = runningTotals[bonus[0] - 2] + bonus[1];
                    }
                    else
                    {
                        runningTotals[bonus[0] - 1] = bonus[1];
                    }
                }
                

            }
            RemoveCompleteBonuses();
        }

        internal void RemoveBonus(ScoreType scoreType)
        {
            bonusList.RemoveAll(x => x[2] == (int)scoreType);
        }

        private void RemoveCompleteBonuses()
        {
            bonusList.RemoveAll(x => IsBonusComplete(x));
        }

        private bool IsBonusComplete(int[] bonus)
        {
                if(bonus[2] == (int)ScoreType.Strike && bonus[3] == 3)
                {
                    return true;
                }
                else if(bonus[2] == (int)ScoreType.Spare && bonus[3] == 2)
                {
                    return true;
                }
                else if(bonus[2] == (int)ScoreType.OpenFrame && bonus[3] == 2)
                {
                    return true;
                }
                else if (bonus[2] == (int)ScoreType.FinalFrame && bonus[3] == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        internal bool Exists(int frameNumber)
        {
            return bonusList.Any(x => x[0] == frameNumber);
        }
    }
}
