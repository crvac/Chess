using Chess.Figures.Contracts;

namespace Chess.Figures;

internal class King : IMoveFigure
{
    /// <summary>
    /// Validates the move for the King piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool KingValidate(string oldcoord, string newcoord) //|X2-X1|<=1 and |Y2-Y1|<=1.
    {
        var coordStruct = new Coords();

        var fromCoord = coordStruct.StringCoordParse(oldcoord);
        var toCoord = coordStruct.StringCoordParse(newcoord);

        return (Math.Abs(toCoord.letter - fromCoord.letter) <= 1 && Math.Abs(toCoord.number - fromCoord.number) <= 1);
    }

    public bool NewCoordMoveValidate(string coord, string newcoord)
    {
        var coordinateActions = new Coordinates();


        if (coordinateActions.ValidateCoordinates(newcoord))
        {
            if (KingValidate(coord, newcoord))
            {
                return true;
            }
            else
            {
                Console.WriteLine("The figure cant go there");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Invalid Coordinates");
            return false;
        }
    }
}
