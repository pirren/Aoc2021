﻿namespace Aoc2021.Library
{
    public interface ISolver
    {
        int Order { get; }
        string ProblemName { get; }
        int Day { get; }
        string Indata { get; }
        object PartOne(string Indata);
        object PartTwo(string Indata);
    }
}
