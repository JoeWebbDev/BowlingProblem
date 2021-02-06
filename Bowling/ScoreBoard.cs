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

            Console.Write('\n');

            //Construct header
            var padding = 1;
            var nameColumnWidth = playerList.Select(x => x.name.Length).Max() + (padding * 2);
            var nameColumnHeader = "Name";
            nameColumnHeader = nameColumnHeader.PadLeft(nameColumnWidth / 2 + nameColumnHeader.Length / 2).PadRight(nameColumnWidth);
            var header = $"|{nameColumnHeader}|   1   |   2   |   3   |   4   |   5   |   6   |   7   |   8   |   9   |    10     | Total |\n";

            //Draw header
            Console.Write(new string('═', header.Length) + "\n");
            Console.Write(header);
            Console.Write(new string('═', header.Length) + "\n");

            foreach(var player in playerList)
            {              
                Console.Write($"|{ new string(' ', padding) + player.name.PadLeft(nameColumnWidth - 2) + new string(' ', padding) }| ");
                for (var j = 0; j < 21; j++)
                {       
                    //draw player score
                    if(player.score.scoreDisplay[j] == null)
                    {
                        Console.Write($" ");
                    }
                    else
                    {
                        Console.Write($"{player.score.scoreDisplay[j]}");
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
                        Console.Write($"{player.score.runningTotals[k].ToString().PadLeft(5)}");
                        Console.Write(new string(' ', padding) + "|" + new string(' ', padding));
                    }
                    else
                    {
                        Console.Write($"{player.score.runningTotals[k].ToString().PadLeft(9)}");
                        Console.Write(new string(' ', padding) + "|" + new string(' ', padding));
                    }                  
                }

                Console.Write('\n');
                Console.Write(new string('═', header.Length) + "\n");
            }
            Console.Write('\n');
        }

    }
}
