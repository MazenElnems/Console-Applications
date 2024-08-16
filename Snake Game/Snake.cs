using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Snake
    {
        private List<KeyValuePair<int, int>> _body;

        public List<KeyValuePair<int, int>> Body { get => new List<KeyValuePair<int, int>>(_body); }

        public KeyValuePair<int, int> Head { get => _body[0]; }
        public KeyValuePair<int, int> Tail { get => _body[_body.Count - 1]; }
        public int Length { get => _body.Count; }
        public char Symbol { get; } = 'O';
        public char HeadSymbol { get; } = '0';

        public Snake(int row,int col,char head = '0',char symbol = 'o')
        {
            _body = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(row, col) };
            Symbol = symbol;
            HeadSymbol = head;
        }

        public void Move(int row, int col)
        {
            var cell = new KeyValuePair<int, int>(row, col);
            for (int i = 0; i < _body.Count; i++) 
            {
                var tmp = _body[i];
                _body[i] = cell;
                cell = tmp;
            }
        }

        public void Eat(int row,int col) => _body.Add(new KeyValuePair<int, int>(row, col));
    }
}
