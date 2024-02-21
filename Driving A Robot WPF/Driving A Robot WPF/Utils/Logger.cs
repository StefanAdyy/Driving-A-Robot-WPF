using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Driving_A_Robot_WPF.Utils
{
    public static class Logger
    {
        private static readonly string logFilePath = @"Logs\ErrorLog.txt";

        public static void LogError(string errorMessage)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\" +logFilePath;

            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                        sw.WriteLine($"{DateTime.Now} - Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log error: {ex.Message}");
            }
        }
    }
}
