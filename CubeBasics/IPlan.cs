﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public interface IContainsMoves
    {
        IEnumerable<Turn> GetTurns();
        void Describe();
    }
}
