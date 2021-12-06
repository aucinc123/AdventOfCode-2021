using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class BingoCard
    {
        public int BingoCardId { get; set; }
        public BingoNumber[,] CardNumbers { get; set; } = new BingoNumber[5,5];

        public void MarkCard(int number)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (this.CardNumbers[i, j].Number == number)
                        this.CardNumbers[i, j].IsMarked = true;
                }                
            }
        }

        public bool CheckWinner()
        {
            var winner = true;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    winner &= this.CardNumbers[i, j].IsMarked;
                }

                if (winner)
                    return true;
                else
                    winner = true;
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    winner &= this.CardNumbers[j, i].IsMarked;
                }

                if (winner)
                    return true;
                else
                    winner = true;
            }

            return false;
        }

        public int SumUnMarked()
        {
            var sum = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!this.CardNumbers[i, j].IsMarked)
                        sum += this.CardNumbers[i, j].Number;
                }
            }

            return sum;
        }
    }

    public class BingoNumber
    {
        public int Number { get; set; }
        public bool IsMarked { get; set; }

        public BingoNumber(int number)
        {
            this.Number = number;
        }
    }

}
