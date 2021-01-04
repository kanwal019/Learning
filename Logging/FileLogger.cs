using Common.Classes;
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

        public static void OnBookAdded(object source, EventArgs args)
        {
            if (args is StringArgs e)
            {
                LogMessage(e.Args);
                return;
            }
            throw new InvalidCastException();
        }
    }
}
