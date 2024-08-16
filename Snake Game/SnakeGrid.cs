using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class SnakeGrid
    {
        private char[,] _grid;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public char this[int row,int col] => _grid[row,col];

        public SnakeGrid(int height,int width)
        {
            Height = height;
            Width = width;            
            _grid = new char[Height,Width];
            fillGrid();
        }

        private void fillGrid()
        {
            for (int i = 0; i < Height; i++) 
            {
                for(int j = 0; j < Width; j++)
                {
                    _grid[i,j] = ' ';
                }
            }
        }

        public void Place(List<KeyValuePair<int, int>> snake,char head,char symbol) 
        {
            bool putHead = false;
            foreach (var cell in snake)
            {
                if (!putHead)
                    Place(cell.Key,cell.Value,head);
                else
                    Place(cell.Key, cell.Value, symbol);
                putHead = true;
            }
        }

        public void Place(int row,int col,char symbol) => _grid[row,col] = symbol;

        public bool Collision(int row,int col) => row == Height || col == Width || row == -1 || col == -1;
        
        public bool IsCellEmpty(int row,int col) => _grid[row,col].Equals(' ');

        public void ResetCell(int row, int col) => _grid[row, col] = ' '; 


    }
}