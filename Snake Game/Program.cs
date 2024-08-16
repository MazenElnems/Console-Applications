using System;
using System.Text;
using System.Threading.Channels;
namespace Snake_Game
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();

        }

    }
}
