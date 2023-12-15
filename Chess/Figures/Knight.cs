using Chess.Figures.Contracts;

namespace Chess.Figures;

internal class Knight : IMoveFigure
{
    /// <summary>
    /// Validates the move for the knight piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool KnightValidate(string oldcoord, string newcoord) // if (|X2-X1|=1 and |Y2-Y1|=2) or (|X2-X1|=2 and |Y2-Y1|=1).
    {
        var coordStruct = new Coords();

        var fromCoord = coordStruct.StringCoordParse(oldcoord);
        var toCoord = coordStruct.StringCoordParse(newcoord);

        //var fromCoord = new Coords()
        //var toCoord = new Coords(newcoord);

        // All possible moves of a knight
        int[] X = { 2, 2, 1, 1, -2, -2, -1, -1 };
        int[] Y = { 1, -1, 2, -2, 1, -1, 2, -2 };

        // Check if the move is valid or not
        for (int i = 0; i < 8; i++)
        {
            if (coordStruct.ParseLetterCoordinate(toCoord) == coordStruct.ParseLetterCoordinate(fromCoord) + X[i] && toCoord.number == fromCoord.number + Y[i]) return true;
        }

        return false;
    }

    public bool NewCoordMoveValidate(string coord, string newcoord)
    {
        var coordinateActions = new Coordinates();


        if (coordinateActions.ValidateCoordinates(newcoord))
        {
            if (KnightValidate(coord, newcoord))
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
