﻿using Driving_A_Robot_WPF.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line = sr.ReadLine();

                        while (line != null)
                        {
                            lines.Add(line);
                            line = sr.ReadLine();
                        }
                    }
                }
                else
                {
                    throw new FileOperationException.FileNotFoundException(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new FileOperationException.FileReadingException(ex.Message);
            }

            return lines;
        }
    }
}
