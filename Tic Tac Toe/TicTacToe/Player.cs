using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player
    {
        public char Symbol { get; private set; }
        public ConsoleColor Color { get; private set; }

        public Player(char symbol,ConsoleColor color = ConsoleColor.Red)
        {
            Symbol = symbol;
            Color = color;
        }

        public void MakeMove(Board board,int row, int column)
        {
            board.UpdateBoard(row,column,Symbol);
        }
    }
}
