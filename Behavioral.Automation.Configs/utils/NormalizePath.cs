using System.IO;

namespace Behavioral.Automation.Configs.utils
{

    public static class NormalizePath
    {
        public static string NormalizePathAccordingOs(this string fullPath)
        {
            fullPath = fullPath.Replace("/", Path.DirectorySeparatorChar.ToString());
            fullPath = fullPath.Replace(@"\", Path.DirectorySeparatorChar.ToString());
            return fullPath;
        }
    }
}