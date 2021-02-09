using System;
using System.Collections.Generic;
using System.Text;

namespace App.Framework
{
    static public class ConsoleLogger
    {
        static private object _loggerMutex = new object();

        public enum WarningLevel
        {
            INFO,
            WARNING,
            ERROR,
        }

        static private void log(WarningLevel warningLevel, string message)
        {
            var logEntry = new 
            {
                Date = DateTime.Now,
                Type = Enum.GetName(typeof(WarningLevel), warningLevel),
                Message = message,
            };

            Console.WriteLine($"{logEntry}");
        }

        static public void Error(string message)
        {
            lock (_loggerMutex)
            {
                var currentBackgroundColor = Console.BackgroundColor;
                var currentForegroundColor= Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                log(warningLevel: WarningLevel.ERROR, message: message);
                Console.BackgroundColor = currentBackgroundColor;
                Console.ForegroundColor = currentForegroundColor;
            }
        }

        static public void Info(string message)
        {
            lock (_loggerMutex)
            {
                var currentBackgroundColor = Console.BackgroundColor;
                var currentForegroundColor = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Green;
                log(warningLevel: WarningLevel.INFO, message: message);
                Console.BackgroundColor = currentBackgroundColor;
                Console.ForegroundColor = currentForegroundColor;
            }
        }

        static public void Warning(string message)
        {
            lock (_loggerMutex)
            {
                var currentBackgroundColor = Console.BackgroundColor;
                var currentForegroundColor = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                log(warningLevel: WarningLevel.WARNING, message: message);
                Console.BackgroundColor = currentBackgroundColor;
                Console.ForegroundColor = currentForegroundColor;
            }
        }
    }
}
