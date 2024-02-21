using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Utils
{
    public static class FileUtils
    {
        public static List<string> ReadLinesFromFile(string filePath)
        {
            List<string> lines = new List<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    lines.AddRange(File.ReadAllLines(filePath));
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading from the file: {ex.Message}");
            }

            return lines;
        }
    }
}
