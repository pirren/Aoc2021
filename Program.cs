using Aoc2021.Library;

bool developmentMode = false;
var skip = new[] { "" }; // skip list of slow solvers

IEnumerable<Action> actions = AocCore.Activation<ISolver>.Get(skip)
    .OrderBy(solver => solver.Order)
    .Select(solver => new Action(solver.RunProblems));

if (developmentMode)
{
    actions.Last().Invoke();
}
else
{
    actions.ForEach(action => action.Invoke());
}

