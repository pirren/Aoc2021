namespace Aoc2021.Library
{
    public abstract class DayBase : ISolver
    {
        public virtual string Indata => File.ReadAllText(Path.Combine(UseSample ? "sample" : "indata", UseSample ? Day+"-sample.in" : Day + ".in"));
        public virtual bool UseSample => false;
        public virtual string ProblemName => throw new NotImplementedException();
        public virtual int Day => throw new NotImplementedException();
        public abstract object PartOne(string Indata);
        public abstract object PartTwo(string Indata);
    }
}
