using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoggerService
{
    public class FileLogger
    {
        public static void LogMessage(string message)
        {
            try
            {
                string temp = Path.Combine(Path.GetTempPath(), "BookLogs.txt");
                using (StreamWriter sw = File.AppendText(temp))
                {
                    sw.WriteLine(message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
