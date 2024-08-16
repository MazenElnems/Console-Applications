using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class IntExtentions
    {
        public static bool IsNumberBetween(this int number, int min, int max) { return number >= min && number <= max; }
    }
}
