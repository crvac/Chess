    using Chess.Figures.Contracts;

namespace Chess.Figures;

internal class Queen : IMoveFigure
{
    /// <summary>
    /// Validates the move for the Queen piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool QueenValidate(string oldcoord, string newcoord)
    {
        var bishop = new Bishop();
        var rook = new Rook();
        return bishop.BishopValidate(oldcoord, newcoord) || rook.RookValidate(oldcoord, newcoord);
    }

    public bool NewCoordMoveValidate(string coord, string newcoord)
    {
        var coordinateActions = new Coordinates();


        if (coordinateActions.ValidateCoordinates(newcoord))
        {
            if (QueenValidate(coord, newcoord))
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
