using System;
namespace TicTacToe
{
    internal class Board
    {
        private char[,] board;

        public Board()
        {
            this.ResetBoard();
        }

        public void UpdateBoard(int row,int column,char symbol)
        {
            board[row,column] = symbol;
        }

        public override string ToString()
        {
            char RightAlign = '\t';
            string Separator = "--- + --- + ---";
            string Row1 = String.Format("{1,2}{0,3}{2,3}{0,3}{3,2}", '|', board[0, 0], board[0, 1], board[0,2]);
            string Row2 = String.Format("{1,2}{0,3}{2,3}{0,3}{3,2}", '|', board[1, 0], board[1, 1], board[1, 2]);
            string Row3 = String.Format("{1,2}{0,3}{2,3}{0,3}{3,2}", '|', board[2, 0], board[2, 1], board[2, 2]);

            return String.Format("{0}{2}\n{0}{1}\n{0}{3}\n{0}{1}\n{0}{4}", RightAlign, Separator, Row1, Row2, Row3);
        }

        public void ResetBoard()
        {
            board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        }

        public bool IsBoardFilled()
        {
            for (int i = 0; i < 3; i++)
            {
                for(int j=0;j< 3; j++)
                {
                    if (board[i,j] == ' ') return false;
                }
            }
            return true;
        }

        private bool SameSymbol(char c1,char c2,char c3)
        {
            return c1 == c2 &&  c2 == c3 && c1 != ' ';
        }

        public bool IsCellEmpty(int row , int col)
        {
            return board[row, col] == ' ';
        }

        public bool IsLineFilled(out char winner)
        {
            if (SameSymbol(board[1, 1], board[0, 2], board[2, 0])) { winner = board[1, 1]; return true; }
            else if (SameSymbol(board[1, 1], board[0, 0], board[2, 2])) { winner = board[1, 1]; return true; }
            else if (SameSymbol(board[1, 1], board[0, 1], board[2, 1])) { winner = board[1, 1]; return true; }
            else if (SameSymbol(board[1, 1], board[1, 0], board[1, 2])) { winner = board[1, 1]; return true; }
            else if (SameSymbol(board[0, 0], board[0, 1], board[0, 2])) { winner = board[0, 0]; return true; }
            else if (SameSymbol(board[0, 2], board[1, 2], board[2, 2])) { winner = board[2, 2]; return true; }
            else if (SameSymbol(board[0, 0], board[1, 0], board[2, 0])) { winner = board[0, 0]; return true; }
            else if (SameSymbol(board[2, 0], board[2, 1], board[2, 2])) { winner = board[2, 2]; return true; }
            winner = '\0';
            return false;
        }
        
    }
}
