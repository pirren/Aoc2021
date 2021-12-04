using Aoc2021.Library;

bool developmentMode = false;

IEnumerable<ISolver> solvers = AocCore.Activation<ISolver>.Get();
IEnumerable<Action> actions = solvers.Select(solver => new Action(solver.RunProblems));

if(developmentMode)
{
    actions.Last().Invoke();
}
else
{
    actions.ForEach(action => action.Invoke());
}

