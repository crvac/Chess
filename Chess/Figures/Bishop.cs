using Chess.Figures.Contracts;
using System.Drawing;

namespace Chess.Figures;

internal class Bishop : IMoveFigure
{
    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool BishopValidate(string oldcoord, string newcoord)
    {
        var coordStruct = new Coords();

        var fromCoord = coordStruct.StringCoordParse(oldcoord);
        var toCoord = coordStruct.StringCoordParse(newcoord);

        return (Math.Abs(coordStruct.ParseLetterCoordinate(toCoord) - coordStruct.ParseLetterCoordinate(fromCoord)) == Math.Abs(toCoord.number - fromCoord.number));

    }

    public bool NewCoordMoveValidate(string coord, string newcoord)
    {
        var coordinateActions = new Coordinates();


        if (coordinateActions.ValidateCoordinates(newcoord))
        {
            if (BishopValidate(coord, newcoord))
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
