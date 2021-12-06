using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class OceanVentPuzzle
    {
        public List<OceanVentLine> OceanVentLineList { get; set; } = new List<OceanVentLine>();
        public Dictionary<Point,int> IntersectingPointDict { get; set; } = new Dictionary<Point, int>();

        public void LoadOceanVentLines()
        {
            string[] fileLineList = System.IO.File.ReadAllLines(@"C:\DATA\Code\AdventOfCode\AdventOfCode\OceanVentsPuzzle.txt");

            foreach(var line in fileLineList) 
            {
                var regEx = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)", RegexOptions.IgnoreCase); //309,347 -> 309,464
                var match = regEx.Match(line);
                if (match.Success)
                {
                    var x1 = match.Groups["x1"].Value.ToInteger();
                    var y1 = match.Groups["y1"].Value.ToInteger();
                    var x2 = match.Groups["x2"].Value.ToInteger();
                    var y2 = match.Groups["y2"].Value.ToInteger();

                    OceanVentLineList.Add(new OceanVentLine(x1, y1, x2, y2));
                }
            }
        }

        public int SolveByDiagram()
        {
            int maxX = 0;
            int maxY = 0;

            foreach(var line in OceanVentLineList)
            {
                if (line.p1.X > maxX)
                    maxX = line.p1.X;

                if (line.p2.X > maxX)
                    maxX = line.p2.X;

                if (line.p1.Y > maxY)
                    maxY = line.p2.Y;

                if (line.p2.Y > maxY)
                    maxY = line.p2.Y;
            }
            maxX++;
            maxY++;
            var ventDiagram = new int[maxX, maxY];

            foreach(var vent in OceanVentLineList)
            {
                var pointList = vent.GetPoints();

                foreach(var point in pointList)
                {
                    ventDiagram[point.X, point.Y]++;
                }
            }

            int puzzleAnswer = 0;

            for(int i = 0; i < maxX; i++)
            {
                for(int j = 0; j < maxY; j++)
                {
                    if (ventDiagram[i, j] >= 2)
                        puzzleAnswer++;
                }
            }

            return puzzleAnswer;
        }

        public int CheckLineIntersections()
        {
            var puzzleAnswer = 0;

            for(int i = 0; i < OceanVentLineList.Count; i++)
            {
                for(int j = i + 1; j < OceanVentLineList.Count; j++)
                {
                    var line1 = OceanVentLineList[i];
                    var line2 = OceanVentLineList[j];

                    if(line1.IsNotDiagonal && line2.IsNotDiagonal)
                    {
                        var point = IntersectionPoint(line1, line2);

                        if (point != null)
                        {
                            if (!IntersectingPointDict.ContainsKey(point.Value))
                                IntersectingPointDict.Add(point.Value, 1);
                            else
                                IntersectingPointDict[point.Value]++;
                        }
                    }                   
                }
            }

            var intersectionList = IntersectingPointDict.ToList();
            foreach(var intersection in intersectionList)
            {
                if (intersection.Value >= 2)
                    puzzleAnswer++;
            }

            return puzzleAnswer;
        }

        public Point? IntersectionPoint(OceanVentLine v1, OceanVentLine v2)
        {
            Point line1V1 = v1.p1;
            Point line1V2 = v1.p2;
            Point line2V1 = v2.p1;
            Point line2V2 = v2.p2;

            //Line1
            float A1 = line1V2.Y - line1V1.Y;
            float B1 = line1V1.X - line1V2.X;
            float C1 = A1 * line1V1.X + B1 * line1V1.Y;

            //Line2
            float A2 = line2V2.Y - line2V1.Y;
            float B2 = line2V1.X - line2V2.X;
            float C2 = A2 * line2V1.X + B2 * line2V1.Y;

            float delta = A1 * B2 - A2 * B1;

            if (delta == 0)
                return null;

            float x = (B2 * C1 - B1 * C2) / delta;
            float y = (A1 * C2 - A2 * C1) / delta;

            return new Point((int)x, (int)y);
        }
    }

    public class OceanVentLine
    {
        public Point p1 { get; set; }
        public Point p2 { get; set; }

        public bool IsNotDiagonal
        {
            get
            {
                return p1.X == p2.X || p1.Y == p2.Y;
            }
        }

        public OceanVentLine(int x1, int y1, int x2, int y2)
        {
            p1 = new Point(x1, y1);
            p2 = new Point(x2, y2);
        }

        public override string ToString()
        {
            return $"{p1.X},{p1.Y} -> {p2.X},{p2.Y}";
        }

        public int NumPoints
        {
            get
            {
                if (p1.X - p2.X == 0)
                    return Math.Abs(p1.Y - p2.Y);
                else
                    return Math.Abs(p1.X - p2.X);
            }
        }

        public List<Point> GetPoints()
        {
            var pointList = new List<Point>();
            int ydiff = p2.Y - p1.Y, xdiff = p2.X - p1.X;
            double slope = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
            double x, y;

            for (double i = 0; i < NumPoints; i++)
            {
                y = slope == 0 ? 0 : ydiff * (i / NumPoints);
                x = slope == 0 ? xdiff * (i / NumPoints) : y / slope;
                pointList.Add(new Point((int)Math.Round(x) + p1.X, (int)Math.Round(y) + p1.Y));
            }

            pointList.Add(p2);
            return pointList;
        }
    }

}
