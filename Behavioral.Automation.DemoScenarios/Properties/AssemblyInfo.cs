using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]

//edit if more threads are needed
[assembly: LevelOfParallelism(1)]