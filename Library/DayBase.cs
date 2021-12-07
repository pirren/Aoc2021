namespace Aoc2021.Library
{
    public abstract class DayBase : ISolver
    {
        private const string indataFolder = "Indata";
        private const string sampleFolder = "Sample";

        protected string Folder => UseSample ? sampleFolder : indataFolder;
        protected string Filename => UseSample ? $"{Day}-sample.in" : $"{Day}.in";

        public virtual int Order => Day;
        public virtual string Indata => File.ReadAllText(Path.Combine(Folder, Filename));
        public virtual bool UseSample => false;
        public virtual string ProblemName => throw new NotImplementedException();
        public virtual int Day => throw new NotImplementedException();

        public abstract object PartOne(string Indata);
        public abstract object PartTwo(string Indata);
    }
}
