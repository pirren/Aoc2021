using Aoc2021.Library;

bool developmentMode = false;

IEnumerable<Action> actions = AocCore.Activation<ISolver>.Get().Select(solver => new Action(solver.RunProblems));

if(developmentMode)
{
    actions.Last().Invoke();
}
else
{
    actions.ForEach(action => action.Invoke());
}

