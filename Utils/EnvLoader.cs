using System;
using System.IO;

namespace Utils
{
    public static class EnvLoader
    {
        public static void LoadEnv(string path)
        {
            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    // Skip empty lines and # comments
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    // Split line into key and value
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        // Trim whitespace and set environment variable
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        Environment.SetEnvironmentVariable(key, value);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: .env file not found at {path}");
            }
        }
    }
}
