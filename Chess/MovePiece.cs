using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chess;

internal class MovePiece
{
    /// <summary>
    /// Validates the move for the knight piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool KnightValidate(string oldcoord, string newcoord) // if (|X2-X1|=1 and |Y2-Y1|=2) or (|X2-X1|=2 and |Y2-Y1|=1).
    {
        var fromCoord = new Coords(oldcoord);
        var toCoord = new Coords(newcoord);

        // All possible moves of a knight
        int[] X = { 2, 2, 1, 1, -2, -2, -1, -1 };
        int[] Y = { 1, -1, 2, -2, 1, -1, 2, -2 };

        // Check if the move is valid or not
        for (int i = 0; i < 8; i++)
        {
            if (toCoord.letter == fromCoord.letter + X[i] && toCoord.number == fromCoord.number + Y[i]) return true;
        }

        return false;
    }

    /// <summary>
    /// Validates the move for the rook piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool Rook(string oldcoord, string newcoord)
    {
        var fromCoord = new Coords(oldcoord);
        var toCoord = new Coords(newcoord);

        return toCoord.letter == fromCoord.letter ^ toCoord.number == fromCoord.number;        
    }

    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool Bishop(string oldcoord, string newcoord)
    {
        var fromCoord = new Coords(oldcoord);
        var toCoord = new Coords(newcoord);

        for (int i = 1; i < 8; i++)
        {
            if (Math.Abs(toCoord.letter - fromCoord.letter) == Math.Abs(toCoord.number - fromCoord.number)) return true;
        }

        return false;
    }

    /// <summary>
    /// Validates the move for the Queen piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool Queen(string oldcoord, string newcoord)
    {
        return Bishop(oldcoord, newcoord) || Rook(oldcoord, newcoord);
    }

    /// <summary>
    /// Validates the move for the King piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool King(string oldcoord, string newcoord) //|X2-X1|<=1 and |Y2-Y1|<=1.
    {
        var fromCoord = new Coords(oldcoord);
        var toCoord = new Coords(newcoord);

        return (Math.Abs(toCoord.letter - fromCoord.letter) <=1 && Math.Abs(toCoord.number - fromCoord.number) <= 1);
    }
}
