using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess;

internal struct FigureStructure
{
    public FigureTeam team;
    public FigureNames name;

    
    public FigureStructure()
    {
        team = FigureTeam.empty;
        name = FigureNames.empty;
    }
    public FigureStructure(FigureNames piece)
    {
        name = piece;

        team = FigureTeam.white; // If no color is specified default color will be set to white
    }
    public FigureStructure(string piece, string color)
    {
        if (FigureNames.TryParse(piece.ToUpper(), out FigureNames _upper))
        {
            name = _upper;
        }
        else name = FigureNames.empty;

        if (FigureTeam.TryParse(color.ToUpper(), out FigureTeam _team))
        {
            team = _team;
        }
        else team = FigureTeam.empty;
    }
}
