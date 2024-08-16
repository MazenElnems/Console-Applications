using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    enum GameHardness
    {
        Easy = 100,
        Normal = 50,
        Hard = 10
    }

    internal class Game
    {
        static int width = 60;
        static int height = 25;
        static int maxScore = 0;

        private SnakeGrid _grid;
        private Snake _snake;
        private Food _food;
        private bool _gameOver = false;
        private GameHardness _diffcultyLevel;
        private int _snakeSpeed;

        public Game()
        {
            setData();
        }

        private void setData()
        {
            _grid = new SnakeGrid(height - 2, width - 2);
            _snake = new Snake((height - 2) / 2, (width - 2) / 2);
            _food = new Food();
            _gameOver = false;
        }

        private void welcomScreen()
        {
            Console.WriteLine("\t -----------------------");
            Console.WriteLine("\t| WELCOME TO SNAKE GAME |");
            Console.WriteLine("\t -----------------------");

            Console.WriteLine("\nChoose The Difficulty Level");
            Console.WriteLine("\t1.Easy");
            Console.WriteLine("\t2.Normal");
            Console.WriteLine("\t3.Hard");


            int choice;
            do
            {
                Console.Write("Enter The Choice(1-3): ");
            }
            while (!int.TryParse(Console.ReadLine(),out choice) || choice < 1 || choice >
            3);

            switch (choice)
            {
                case 1:
                    _diffcultyLevel = GameHardness.Easy;
                    break;
                case 2:
                    _diffcultyLevel = GameHardness.Normal; 
                    break;
                case 3:
                    _diffcultyLevel = GameHardness.Hard;
                    break;
            }
            Console.WriteLine("\n  Press Any Key To Play !!");
            Console.ReadKey();
        }

        private void snakeGridScreen()
        {
            Console.Clear();
            Console.WriteLine(new string('#', width));
            for (int i = 0; i < height - 2; i++)
            {
                Console.WriteLine("#" + new string(' ', width - 2) + "#");
            }
            Console.WriteLine(new string('#', width));
        }

        private void displayGrid()
        {
            Console.SetCursorPosition(1, 1);
            for (int i = 0; i < height - 2; i++) 
            {

                for(int j = 0; j < width - 2; j++)
                {
                    if(_grid[i, j] == _food.Symbol) Console.ForegroundColor = ConsoleColor.Gray;
                    else Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(_grid[i,j]);
                }
                if(i + 1 == height - 2) 
                    Console.SetCursorPosition(1,i + 3);
                else
                    Console.SetCursorPosition(1, i + 2);
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private void putFood()
        {
            int row = 0, col = 0;
            do
            {
                row = Random.Shared.Next(0,height - 2);
                col = Random.Shared.Next(0,width - 2);
            }
            while (_grid.Collision(row,col) || !_grid.IsCellEmpty(row,col));
            _grid.Place(row, col, _food.Symbol);
        }

        private void gameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine("\n\n\t GAME OVER");
            Console.WriteLine("\t-------------");
            Console.WriteLine($"\tYOUR SCORE: {_snake.Length - 1}");
            maxScore = Math.Max(maxScore, _snake.Length - 1);
            Console.Write($"\tMAX SCORE: {maxScore}");

            Console.Write("\n\nRestart The Game(Y/N): ");
            if (Console.ReadLine().ToLower() == "y") { setData(); Run(); }
        }

        private void scoreBoard()
        {
            Console.SetCursorPosition(width + 8 , 1);
            Console.Write(new string('-',10));
            Console.SetCursorPosition(width + 8, 2);
            Console.Write('|');
            Console.Write($"SCORE: {_snake.Length - 1}");
            Console.Write('|');
            Console.SetCursorPosition(width + 8, 3);
            Console.WriteLine(new string('-', 10));
        }

        private void play()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            snakeGridScreen();
            putFood();
            scoreBoard();

            var defaultDirection = ConsoleKey.RightArrow;
            do
            {
                _grid.Place(_snake.Body , _snake.HeadSymbol, _snake.Symbol);
                displayGrid();

                var key = defaultDirection;

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                int nextRow = 0, nextCol = 0;
                switch (key) 
                {
                    case ConsoleKey.LeftArrow:
                        if (defaultDirection == ConsoleKey.RightArrow)
                        {
                            key = ConsoleKey.RightArrow;
                            nextCol++;
                        }
                        else
                            nextCol--;
                        break;
                    
                    case ConsoleKey.RightArrow:
                        if (defaultDirection == ConsoleKey.LeftArrow)
                        {
                            key = ConsoleKey.LeftArrow;
                            nextCol--;
                        }
                        else
                            nextCol++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (defaultDirection == ConsoleKey.DownArrow)
                        {
                            key = ConsoleKey.DownArrow;
                            nextRow++;
                        }
                        else
                            nextRow--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (defaultDirection == ConsoleKey.UpArrow)
                        {
                            key = ConsoleKey.UpArrow;
                            nextRow--;
                        }
                        else
                            nextRow++;
                        break;
                }

                defaultDirection = key;

                if (_grid.Collision(_snake.Head.Key + nextRow, _snake.Head.Value + nextCol) ||              // Collision Happen
                    _grid[_snake.Head.Key + nextRow, _snake.Head.Value + nextCol] == _snake.Symbol)         
                    _gameOver = true;

                else if (_grid[_snake.Head.Key + nextRow, _snake.Head.Value + nextCol] == _food.Symbol)       // Snake Eate   
                {
                    if (!_grid.Collision(_snake.Tail.Key, _snake.Tail.Value + 1) && _grid.IsCellEmpty(_snake.Tail.Key, _snake.Tail.Value + 1))
                        _snake.Eat(_snake.Tail.Key, _snake.Tail.Value);
                    else if(!_grid.Collision(_snake.Tail.Key, _snake.Tail.Value - 1) && _grid.IsCellEmpty(_snake.Tail.Key, _snake.Tail.Value - 1))
                        _snake.Eat(_snake.Tail.Key, _snake.Tail.Value - 1);
                    else if(!_grid.Collision(_snake.Tail.Key + 1, _snake.Tail.Value) && _grid.IsCellEmpty(_snake.Tail.Key + 1, _snake.Tail.Value))
                        _snake.Eat(_snake.Tail.Key + 1, _snake.Tail.Value);
                    else
                        _snake.Eat(_snake.Tail.Key - 1, _snake.Tail.Value);
                    putFood();
                    scoreBoard();
                }

                else      // Normal move
                    _grid.ResetCell(_snake.Tail.Key, _snake.Tail.Value);        

                _snake.Move(_snake.Head.Key + nextRow , _snake.Head.Value + nextCol);       // update snake position

                Thread.Sleep((int)_diffcultyLevel);
            }
            while(!_gameOver);
            gameOver();
        }

        public void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            welcomScreen();
            play();
            Console.ResetColor();
        }

    }
}
