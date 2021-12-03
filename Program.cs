using Aoc2021.Library;

bool developmentMode = false;

IEnumerable<ISolver> solvers = AocCore.Solvers();
IEnumerable<Action> actions = solvers.Select(solver => new Action(solver.ProcessSolutions));

if(developmentMode)
{
    actions.Last().Invoke();
}
else
{
    actions.ToList().ForEach(action => action.Invoke());
}

