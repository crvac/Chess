using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures.Contracts
{
    internal interface IMoveFigure
    {
        public bool NewCoordMoveValidate(string coord, string newcoord);
    }
}
