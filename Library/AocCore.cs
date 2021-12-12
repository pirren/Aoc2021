using System.Diagnostics;
using System.Reflection;

namespace Aoc2021.Library
{
    public static class AocCore
    {
        public static Dictionary<int, ConsoleColor> BenchmarkColors = new()
        {
            { 20, ConsoleColor.Green },
            { 30, ConsoleColor.DarkGreen },
            { 40, ConsoleColor.Yellow },
            { 55, ConsoleColor.DarkYellow },
            { 60, ConsoleColor.Red },
            { 80, ConsoleColor.DarkRed }
        };

        public static ConsoleColor DefaultColor = ConsoleColor.White;

        public static void PrintTableHeader()
        {
            Console.Write("Year".PadRight(10));
            Console.Write("Day".PadRight(5));
            Console.Write("Part".PadRight(5));
            Console.Write("Name".PadRight(30));
            Console.Write("Result".PadRight(25));
            Console.Write("Duration (ms)\r\n\r\n");
        }

        public static void RunProblems(this ISolver solver)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.Write("2021".PadRight(10));
            Console.Write(solver.Day.ToString().PadRight(5));
            Console.Write("1".PadRight(5));
            Console.Write(solver.Name.PadRight(30));
            Console.Write(solver.PartOne(solver.Indata)?.ToString()?.PadRight(25) ?? "");
            PrintTime(stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
            Console.Write("2021".PadRight(10));
            Console.Write(solver.Day.ToString().PadRight(5));
            Console.Write("2".PadRight(5));
            Console.Write(solver.Name.PadRight(30));
            Console.Write(solver.PartTwo(solver.Indata)?.ToString()?.PadRight(25) ?? "");
            PrintTime(stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }

        private static void PrintTime(long time)
        {
            Console.ForegroundColor = BenchmarkColors[BenchmarkColors.Keys.Aggregate((x, y) => Math.Abs(x - time) < Math.Abs(y - time) ? x : y)];
            Console.Write(time.ToString() + "\r\n");
            Console.ForegroundColor = DefaultColor;
        }

        public static class Activation<T> where T : class
        {
            const string solutionsNamespace = "Aoc2021.Solutions";
            static readonly string[] whiteList = new[] { "Day" };

            public static T[] Get(string[] skip)
                => Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .NotNull()
                    .Where(type => type.IsClass && type.Namespace == solutionsNamespace)
                    .GroupBy(type => type.Namespace)
                    .SelectMany(types => types)
                    .Where(type => whiteList.Any(n => type.Name.Contains(n)) && !skip.Contains(type.Name))
                    .Select(type => (T?)Activator.CreateInstance(type) ?? null)
                    .NotNull()
                    .ToArray();
        }
    }
}
