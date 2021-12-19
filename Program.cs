using Aoc2021.Library;
using System.Diagnostics;

bool developmentMode = false;
var skip = new[] { "" };

Console.ForegroundColor = AocCore.DefaultColor;

IEnumerable<Action> actions = AocCore.Activation<ISolver>.Get(skip)
    .OrderBy(solver => solver.Order)
    .Select(solver => new Action(solver.RunProblems));

if (developmentMode)
{
    actions.Last().Invoke();
}
else
{
    Stopwatch st = new();
    AocCore.PrintTableHeader();

    st.Start();
    actions.ForEach(action => action.Invoke());
    st.Stop();

    Console.WriteLine();

    Console.WriteLine($"Total time solutions: {st.ElapsedMilliseconds}ms");
}

