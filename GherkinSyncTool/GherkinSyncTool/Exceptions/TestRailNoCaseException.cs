using System;

namespace GherkinSyncTool.Exceptions
{
    /// <summary>
    /// Thrown when TestRail does not have a test case with provided Id
    /// </summary>
    public class TestRailNoCaseException : Exception
    {
        public TestRailNoCaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}