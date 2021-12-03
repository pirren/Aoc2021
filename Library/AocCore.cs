using System.Reflection;

namespace Aoc2021.Library
{
    public static class AocCore
    {
        public static void ProcessSolutions(this ISolver solver)
        {
            Console.WriteLine("Problem name: " + solver.ProblemName);
            Console.WriteLine($"Day: " + solver.Day + Environment.NewLine);

            foreach (var solution in solver.Solve())
            {
                Console.WriteLine("Solution: " + solution);
            }
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

                //.ToDictionary(key => Attribute.GetCustomAttribute(key.GetType(), typeof(Problem)) as Problem ?? new Problem(0, ""), value => value)
                //.Where(kvp => kvp.Key.Day != 0) // We keep DayNull around so we can do this hack. Any null attributed problem will default to a new instance of the DayNull problem which will be skipped
                //.ToDictionary(k => k.Key, v => v.Value);
    }
}
