using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace OopsConsole.Implementation;

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
        var output = new ExpandoObject(); 
        var result = (IDictionary<string, dynamic>)output;

        if (!string.IsNullOrEmpty(configuration) && configuration.Length > 0)
        {
            var lines = configuration.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Trim().Length > 0)
                {
                    var temp = line.Split(": ");
                    string key = temp[0].Trim();
                    string value = temp[1].Trim();

                    value = value[..^1];

                    if (key.Length > 0)
                    {
                        if (result.ContainsKey(key))
                        {
                            throw new Exception();
                        }

                        if ((!char.IsDigit(key[0])) && key.All(c => char.IsLetterOrDigit(c)))
                        {
                            if (int.TryParse(value, out int intValue))
                            {
                                result.Add(key, intValue);
                                continue;
                            }

                            if (bool.TryParse(value, out bool boolValue))
                            {
                                result.Add(key, boolValue);
                                continue;
                            }
                            result.Add(key, value);
                            continue;
                        }
                        throw new InvalidDataException("Key seems to have invalid characters.");
                    }
                    throw new InvalidDataException("Key length is invalid.");
                }
            }
        }
        else
        {
            throw new InvalidOperationException("File contents are empty.");
        }
        return output;
    }
}
