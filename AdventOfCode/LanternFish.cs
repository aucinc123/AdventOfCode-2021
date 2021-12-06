using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class LanternFishPuzzle
    {
        public long GetLanternFishPuzzleOneAnswer()
        {
            var fishList = new List<long>
            {
                //3,4,3,1,2
                2,5,3,4,4,5,3,2,3,3,2,2,4,2,5,4,1,1,4,4,5,1,2,1,5,2,1,5,1,1,1,2,4,3,3,1,4,2,3,4,5,1,2,5,1,2,2,5,2,4,4,1,4,5,4,2,1,5,5,3,2,1,3,2,1,4,2,5,5,5,2,3,3,5,1,1,5,3,4,2,1,4,4,5,4,5,3,1,4,5,1,5,3,5,4,4,4,1,4,2,2,2,5,4,3,1,4,4,3,4,2,1,1,5,3,3,2,5,3,1,2,2,4,1,4,1,5,1,1,2,5,2,2,5,2,4,4,3,4,1,3,3,5,4,5,4,5,5,5,5,5,4,4,5,3,4,3,3,1,1,5,2,4,5,5,1,5,2,4,5,4,2,4,4,4,2,2,2,2,2,3,5,3,1,1,2,1,1,5,1,4,3,4,2,5,3,4,4,3,5,5,5,4,1,3,4,4,2,2,1,4,1,2,1,2,1,5,5,3,4,1,3,2,1,4,5,1,5,5,1,2,3,4,2,1,4,1,4,2,3,3,2,4,1,4,1,4,4,1,5,3,1,5,2,1,1,2,3,3,2,4,1,2,1,5,1,1,2,1,2,1,2,4,5,3,5,5,1,3,4,1,1,3,3,2,2,4,3,1,1,2,4,1,1,1,5,4,2,4,3
            };

            long[] ages = new long[9];
            foreach (long value in fishList) ages[value]++;

            for (int i = 0; i < 256; i++)
            {
                long last = ages[0];
                for (int j = 0; j < 8; j++) ages[j] = ages[j + 1];
                ages[6] += last;
                ages[8] = last;
            }

            Console.WriteLine($"{ages.Sum()}");


            return fishList.Count;
        }

        public class LanternFish
        {
            public int TimerValue { get; set; }
            public bool IsNew { get; set; }

            public LanternFish(int timerValue)
            {
                this.TimerValue = timerValue;
            }
        }
    }
}
