using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    static class Helper
    {

        public static bool IsNumberInRange(int value, int min , int max) => value >= min && value <= max;

        public static string GetStringFromConsole(string inputMessage)
        {
            string str;
            do { Console.Write($"Enter {inputMessage}: "); str = Console.ReadLine(); }
            while (String.IsNullOrEmpty(str));
            return str;
        }

        public static int GetIntegerInput(int min, int max)
        {
            int choice;
            do { Console.Write($"\n\tEnter Choice ({min}-{max}): "); }
            while (!(int.TryParse(Console.ReadLine(), out choice) && IsNumberInRange(choice, min, max)));
            return choice;
        }

    }
}
