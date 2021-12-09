using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class SevenSegmentPuzzle
    {
        public static void Puzzle1()
        {
            var fileLineList = File.ReadLines("SevenSegmentPuzzle.txt");

            var signalList = new List<List<string>>();
            var outputList = new List<List<string>>();

            foreach(var line in fileLineList)
            {
                var result = line.Split("|");

                if(result.Length > 1)
                {
                    signalList.Add(result[0].Split(" ").ToList());
                    outputList.Add(result[1].Split(" ").ToList());
                }
            }

            int numTimes = 0;
            //1 = 2
            //4 = 4
            //7 = 3
            //8 = 7
            foreach (var outputLine in outputList)
            {
                foreach (var output in outputLine)
                {
                    if (output.Length == 2 ||
                        output.Length == 4 ||
                        output.Length == 3 ||
                        output.Length == 7)
                        numTimes++;
                    
                    //Console.Write(output + " ");
                }
                //Console.Write(Environment.NewLine);
            }

            Console.WriteLine($"Answer: {numTimes}");

        }

        public static void Puzzle2()
        {
            var fileLineList = File.ReadLines("SevenSegmentPuzzle.txt");

            var signalList = new List<List<string>>();
            var outputList = new List<List<string>>();

            foreach (var line in fileLineList)
            {
                var result = line.Split("|");

                if (result.Length > 1)
                {
                    signalList.Add(result[0].Split(" ").ToList());
                    outputList.Add(result[1].Split(" ").ToList());
                }
            }

            int answer = 0;
            
            for(int i = 0; i < signalList.Count; i++)
            {
                var segment = new SevenSegment(signalList[i]);
                answer += segment.GetOutputValue(outputList[i]);
            }

            Console.WriteLine($"Answer: {answer}");

        }

        public class SevenSegment
        {
            string[] Segments = new string[7];
            string[] Codes = new string[10];

            string oneCode = string.Empty;
            string threeCode = string.Empty;
            string fourCode = string.Empty;
            string sixCode = string.Empty;
            string sevenCode = string.Empty;
            string eightCode = string.Empty;
            string nineCode = string.Empty;

            List<string> sixCountCodeList = new List<string>();
            List<string> fiveCountCodeList = new List<string>();

            public SevenSegment(List<string> signalList)
            {
                foreach (var signal in signalList)
                {   
                    if (signal.Length == 2)
                        oneCode = signal;
                    else if (signal.Length == 3)
                        sevenCode = signal;
                    else if (signal.Length == 4)
                        fourCode = signal;                    
                    else if (signal.Length == 7)
                        eightCode = signal;
                    else
                    {
                        if (signal.Length == 6) //0,6,9
                            sixCountCodeList.Add(signal);
                        else if (signal.Length == 5) //2,3,5
                            fiveCountCodeList.Add(signal);
                    }
                }

                Segments[0] = string.Concat(sevenCode.Except(oneCode));

                foreach(var signal in sixCountCodeList)
                {                    
                    if (signal.Except(sevenCode).Except(fourCode).Count() == 1)
                    {
                        nineCode = signal;                        
                    }   

                    if(signal.Except(sevenCode).Count() == 4)
                    {
                        sixCode = signal;
                    }
                }

                Segments[4] = string.Concat(eightCode.Except(nineCode));
                Segments[6] = string.Concat(eightCode.Except(sevenCode).Except(fourCode).Except(Segments[4]));

                foreach(var signal in fiveCountCodeList)
                {
                    var segment = string.Concat(signal.Except(sevenCode).Except(Segments[6]));

                    if(segment.Length == 1)
                    {
                        threeCode = signal;
                        Segments[3] = segment;
                        break;
                    }                       
                }

                Segments[1] = string.Concat(eightCode.Except(threeCode).Except(Segments[4]));
                Segments[2] = string.Concat(eightCode.Except(sixCode));
                Segments[5] = string.Concat(oneCode.Except(Segments[2]));
            }

            public int GetOutputValue(List<string> outputList)
            {
                var outputString = string.Empty;
                foreach(var output in outputList)
                {
                    if(!string.IsNullOrEmpty(output))
                        outputString += decodeOutput(output);
                }

                return outputString.ToInteger();
            }

            private string decodeOutput(string output)
            {
                if (output.Length == 2)
                    return "1";
                else if (output.Length == 3)
                    return "7";
                else if (output.Length == 4)
                    return "4";
                else if (output.Length == 7)
                    return "8";
                else
                {
                    if (output.Length == 6)
                    {
                        if (!output.Contains(Segments[3]))
                            return "0";
                        else if(!output.Contains(Segments[2]))
                            return "6";
                        else
                            return "9";
                    }
                    else if (output.Length == 5) //2,3,5
                    {
                        if (!output.Contains(Segments[5]))
                            return "2";
                        else if (!output.Contains(Segments[1]))
                            return "3";
                        else
                            return "5";
                    }                        
                }

                throw new Exception($"Cannot find Number for Output: {output}");
            }
        }
    }
}
