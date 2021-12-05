using System.Diagnostics;
using System.Reflection;

namespace Aoc2021.Library
{
    public static class AocCore
    {
        public static void RunProblems(this ISolver solver)
        {
            var stopWatch = new Stopwatch();
            Console.WriteLine("Problem name: " + solver.ProblemName);
            Console.WriteLine($"Day: " + solver.Day + Environment.NewLine);

            stopWatch.Start();
            Console.Write("Part 1: ");
            PrintResult(solver.PartOne(solver.Indata));
            Console.Write(" (" + stopWatch.ElapsedMilliseconds + "ms)\r\n");
            stopWatch.Restart();
            Console.Write("Part 2: ");
            PrintResult(solver.PartTwo(solver.Indata));
            Console.Write(" (" + stopWatch.ElapsedMilliseconds + "ms)\r\n");
            stopWatch.Stop();

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(new string('\u00D7', 40));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\n\n");
        }

        private static void PrintResult(object result)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(result);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source) action(item);
        }

        public static class Activation<T> where T : class
        {
            static string[] BlackList = new[] { "DayBase" };
            static string[] WhiteList = new[] { "Day" };

            public static List<T> Get()
                => Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .NotNull()
                    .Where(t => t.IsClass)
                    .GroupBy(t => t.Namespace)
                    .SelectMany(s => s)
                    .Where(x => WhiteList.Any(n => x.Name.Contains(n)) && !BlackList.Any(n => n == x.Name))
                    .Select(s => (T?)Activator.CreateInstance(s) ?? null)
                    .NotNull()
                    .ToList();
        }
    }
}
