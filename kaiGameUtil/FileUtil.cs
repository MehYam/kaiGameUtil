using System;
using System.Collections.Generic;
using System.IO;

namespace kaiGameUtil
{
    public static class FileUtil
    {
        public static string ReadFileIntoString(string fileName)
        {
            return File.ReadAllText(fileName);
        }
        private static readonly string[] lineSplitSpec = new string[] { "\r\n", "\n" };
        public static string[] SplitStringIntoLines(string str)
        {
            return str.Split(lineSplitSpec, System.StringSplitOptions.None);
        }
        public static string[] SplitAndTrim(string str, char delimiter)
        {
            var strs = str.Split(new char[] { delimiter });
            var trimmed = Array.ConvertAll<string, string>(strs, s => s.Trim());
            return trimmed;
        }
    }
}
