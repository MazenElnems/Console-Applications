﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Food
    {
        public char Symbol { get; }

        public Food(char symbol = 'F') => Symbol = symbol;
    
    }
}
