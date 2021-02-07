using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    class ScoreBoard
    {
        public ScoreBoard()
        {
            
        }

        public void DisplayScoreboard(Player[] playerList)
        {
            //(186: ║), (187: ╗) (188: ╝), (200: ╚), (201: ╔), (205: ═)
            Console.Clear();
            var sb = new StringBuilder();
            sb.AppendLine();

            //Construct header
            var padding = 1;
            var nameColumnWidth = playerList.Select(x => x.name.Length).Max() + (padding * 2);
            var nameColumnHeader = "Name";
            nameColumnHeader = nameColumnHeader.PadLeft(nameColumnWidth / 2 + nameColumnHeader.Length / 2).PadRight(nameColumnWidth);
            var header = $"|{nameColumnHeader}|   1   |   2   |   3   |   4   |   5   |   6   |   7   |   8   |   9   |    10     | Total |";

            //Draw header
            sb.AppendLine(new string('═', header.Length));
            sb.AppendLine(header);
            sb.AppendLine(new string('═', header.Length));

            foreach(var player in playerList)
            {              
                sb.Append($"|{ new string(' ', padding) + player.name.PadLeft(nameColumnWidth - 2) + new string(' ', padding) }| ");
                for (var i = 0; i < 21; i++)
                {       
                    //draw player score
                    if(player.score.scoreDisplay[i] == null)
                    {
                        sb.Append($" ");
                    }
                    else
                    {
                        sb.Append($"{player.score.scoreDisplay[i]}");
                    }
                    sb.Append(new string(' ', padding) + "|" + new string(' ', padding));               
                }
                //Display total
                sb.Append(player.score.finalScore);
                sb.AppendLine();
                sb.Append($"|{ new string(' ', nameColumnWidth) }| ");
                for (var i = 0; i < 10; i++)
                {
                    //draw running totals
                    if (i < 9)
                    {                      
                        sb.Append($"{player.score.runningTotals[i].ToString().PadLeft(5)}");
                        sb.Append(new string(' ', padding) + "|" + new string(' ', padding));
                    }
                    else
                    {
                        sb.Append($"{player.score.runningTotals[i].ToString().PadLeft(9)}");
                        sb.Append(new string(' ', padding) + "|" + new string(' ', padding));
                    }                  
                }

                sb.AppendLine();
                sb.AppendLine(new string('═', header.Length));
            }
            sb.AppendLine();
            Console.Write(sb.ToString());
        }

    }
}
