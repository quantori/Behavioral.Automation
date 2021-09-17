using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]

//change if you need to run tests in parallel
[assembly: LevelOfParallelism(1)]