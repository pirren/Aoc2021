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
            Console.WriteLine("Part 1: " + solver.PartOne(solver.Indata) + " (" + stopWatch.ElapsedMilliseconds + "ms)");
            stopWatch.Start();
            Console.WriteLine("Part 2: " + solver.PartTwo(solver.Indata) + " (" + stopWatch.ElapsedMilliseconds + "ms)");
            stopWatch.Stop();

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('\u00D7', 40));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\n\n");
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
