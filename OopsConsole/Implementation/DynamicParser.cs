using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace OopsConsole.Implementation
{
    public class DynamicParser
    {
        public static void DisplayResult()
        {
            string fileName = Path.Combine(Path.GetTempPath(), "Dynamic.txt");
            string contents = File.ReadAllText(fileName);
            dynamic result = Parse(contents);

            Console.WriteLine(result.UserName);
            Console.WriteLine(result.Password);
            Console.WriteLine(result.TimeToLive);
            Console.WriteLine(result.IsEnabled);
        }

        private static dynamic Parse(string configuration)
        {
            dynamic output = new ExpandoObject(); 
            var result = (IDictionary<string, dynamic>)output;

            if (configuration != null && (configuration != String.Empty && configuration.Length > 0))
            {
                var lines = configuration.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Trim().Length > 0)
                    {
                        var temp = line.Split(": ");
                        string key = temp[0].Trim();
                        string value = temp[1].Trim();
                        int intValue;
                        bool boolValue;

                        value = value.Remove(value.Length - 1, 1);

                        if (key.Length > 0)
                        {
                            if (result.ContainsKey(key))
                            {
                                throw new Exception();
                            }

                            if((!Char.IsDigit(key[0])) && key.All(c => Char.IsLetterOrDigit(c)))
                            {
                                if (int.TryParse(value, out intValue))
                                {
                                    result.Add(key, intValue);
                                    continue;
                                }

                                if (Boolean.TryParse(value, out boolValue))
                                {
                                    result.Add(key, boolValue);
                                    continue;
                                }
                                result.Add(key, value);
                                continue;
                            }
                            throw new Exception();
                        }
                        throw new Exception();
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
            return output;
        }
    }
}
