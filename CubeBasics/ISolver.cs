using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    interface ISolver
    {
        void Solve();
        void DescribePlan();
        int TotalStepsWithoutParity { get; }
    }
}
