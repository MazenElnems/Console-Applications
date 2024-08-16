using System;
namespace TicTacToe
{
    static class Helper
    {
        public static bool IsNumberInRange(int number, int min, int max)
        {
            return number >= min && number <= max;
        }
    }
}
