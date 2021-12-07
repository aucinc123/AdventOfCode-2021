using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class CrabSubmarinePuzzle
    {
        public static void Puzzle1Answer()
        {
            var PositionList = File.ReadLines("CrabSubmarinePuzzle.txt")
                                .SelectMany(x => x.Split(','))
                                .Select(int.Parse)
                                .ToList();

            var currentLeastFuel = 0;
            var currentPosition = 0;

            for (int i = 0; i < PositionList.Count; i++)
            {
                var fuelUsed = PositionList
                                .Select(p => Math.Abs(p - i))
                                .Sum();

                if (currentLeastFuel == 0)
                {
                    currentLeastFuel = fuelUsed;
                }                    
                else if(currentLeastFuel > fuelUsed)
                {
                    currentLeastFuel = fuelUsed;
                    currentPosition = i;
                }
            }

            Console.WriteLine($"Puzzle 1 Least Fuel: {currentLeastFuel}");
            Console.WriteLine($"Position: {currentPosition}");            
        }

        public static void Puzzle2Answer()
        {
            var PositionList = File.ReadLines("CrabSubmarinePuzzle.txt")
                                .SelectMany(x => x.Split(','))
                                .Select(int.Parse)
                                .ToList();

            var currentLeastFuel = 0;
            var currentPosition = 0;

            for (int i = 0; i < PositionList.Count; i++)
            {
                var fuelUsed = PositionList
                                .Select(p => (Math.Abs(p - i)) * (Math.Abs(p - i) + 1) / 2)
                                .Sum();

                if (currentLeastFuel == 0)
                {
                    currentLeastFuel = fuelUsed;
                }
                else if (currentLeastFuel > fuelUsed)
                {
                    currentLeastFuel = fuelUsed;
                    currentPosition = i;
                }
            }

            Console.WriteLine($"Puzzle 2 Least Fuel: {currentLeastFuel}");
            Console.WriteLine($"Position: {currentPosition}");
        }
    }
}
