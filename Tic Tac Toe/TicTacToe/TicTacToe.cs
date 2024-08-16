using System;
namespace TicTacToe
{
    class TicTacToe
    {
        private SinglePlayerMode singleMode;
        private TwoPlayerMode twoPlayersMode;

        private int GetModeNumber()
        {
            int choice;
            Console.Write("Choose mode (1 - 3): ");
            while (!Int32.TryParse(Console.ReadLine(), out choice) || !Helper.IsNumberInRange(choice, 1, 3))
            {
                Console.Write($"Choose mode (1 - 3): ");
            }
            return choice;
        }

        public void Play()
        {
            Console.WriteLine("  1-Two Players Mode.\n  2-Single Player Mode.\n  3-exit");

            int mode = GetModeNumber();

            if (mode == 1) StartTwoPlayersGame();
            else if (mode == 2) Console.WriteLine("This mode will developed later.");
            else return;
        }

        private void StartTwoPlayersGame()
        {
            twoPlayersMode = new TwoPlayerMode();
            twoPlayersMode.Start();
            Play();
        }
        private void StartSingleModeGame()
        {
            //Play();
        }
    }
}
