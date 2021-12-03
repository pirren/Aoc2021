using System.Diagnostics;
using System.Reflection;

namespace Aoc2021.Library
{
    public static class AocCore
    {
        public static void ProcessSolutions(this ISolver solver)
        {
            var stopWatch = new Stopwatch();
            Console.WriteLine("Problem name: " + solver.ProblemName);
            Console.WriteLine($"Day: " + solver.Day + Environment.NewLine);

            stopWatch.Start();
            Console.WriteLine("Part 1: " + solver.PartOne(solver.Data) + " (" + stopWatch.ElapsedMilliseconds + "ms)");
            stopWatch.Start();
            Console.WriteLine("Part 1: " + solver.PartTwo(solver.Data) + " (" + stopWatch.ElapsedMilliseconds + "ms)");
            stopWatch.Stop();

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string('\u00D7', 35));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\n\n");
        }

        public static List<ISolver> Solvers()
            => Assembly.GetExecutingAssembly()
                .GetTypes()
                .NotNull()
                .Where(t => t.IsClass)
                .GroupBy(t => t.Namespace)
                .SelectMany(s => s)
                .Where(x => x.Name.Contains("Day") && x.Name != "DayBase")
                .Select(s => (ISolver?)Activator.CreateInstance(s) ?? null)
                .NotNull()
                .ToList();
    }
}
