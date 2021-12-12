using Aoc2021.Library;
using System.Diagnostics;

bool developmentMode = true;
var skip = Array.Empty<string>();

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
    st.Start();
    actions.ForEach(action => action.Invoke());
    Console.WriteLine($"Total time solutions: {st.ElapsedMilliseconds}ms");
    st.Stop();
}

