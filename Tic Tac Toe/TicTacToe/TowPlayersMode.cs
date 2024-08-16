using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class TwoPlayerMode
    {
        Player player1;
        Player player2;
        Board board;
        int roundCounter = 0;

        public TwoPlayerMode()
        {
            this.reStart();
        }

        private Player nextPlayer()
        {
            return (roundCounter % 2 == 0 ? player1: player2);
        }

        private void reStart()
        {
            player1 = new Player('X');
            player2 = new Player('O');
            board = new Board();
            roundCounter = 0;
        }

        public void Start()
        {
            char winner;
            while (!board.IsLineFilled(out winner) && !board.IsBoardFilled()) 
            {
                StartRound();
            }

            Console.Clear();
            Console.WriteLine(board);

            if (winner == player1.Symbol) Console.WriteLine($"{player1.Symbol} WINS!");
            else if(winner == player2.Symbol) Console.WriteLine($"{player2.Symbol} WINS!");
            else Console.WriteLine("TIE!");

            Console.Write("Play new game? (Y/N): ");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                reStart();
                Start();
            }
            else
            {
                Console.Clear();
                return;
            }
        }

        private int GetValidCoordinate(string coordinateName)
        {
            int coordinate;
            Console.Write($"\nEnter {coordinateName} (1 - 3): ");
            while (!Int32.TryParse(Console.ReadLine(), out coordinate) || !Helper.IsNumberInRange(coordinate, 1, 3))
            {
                Console.Write($"\nEnter {coordinateName} (1 - 3): ");
            }
            return coordinate;
        }

        private void StartRound()
        {
            Console.Clear();
            Console.WriteLine(board);
            
            Player playerMove = nextPlayer();
            Console.WriteLine($"{playerMove.Symbol}'s move.");

            
            int row = GetValidCoordinate("row");
            int col = GetValidCoordinate("col");

            while (!board.IsCellEmpty(row-1, col-1)) 
            {
                Console.WriteLine("Please enter row and column of empty cell!");
                row = GetValidCoordinate("row");
                col = GetValidCoordinate("col");
            }

            playerMove.MakeMove(board, row-1, col-1);
            roundCounter++;
        }

    }
}
