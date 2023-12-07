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
    public static bool Knight(string oldcoord, string newcoord)
    {
        int letter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(oldcoord[0]).ToString());
        int number = int.Parse(oldcoord[1].ToString()) - 1;

        int newletter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(newcoord[0]).ToString());
        int newnumber = int.Parse(newcoord[1].ToString()) - 1;

        // All possible moves of a knight
        int[] X = { 2, 2, 1, 1, -2, -2, -1, -1 };
        int[] Y = { 1, -1, 2, -2, 1, -1, 2, -2 };

        // Check if the move is valid or not
        for (int i = 0; i < 8; i++)
        {
            if (newletter == letter + X[i] && newnumber == number + Y[i]) return true;
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
        int letter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(oldcoord[0]).ToString());
        int number = int.Parse(oldcoord[1].ToString()) - 1;

        int newletter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(newcoord[0]).ToString());
        int newnumber = int.Parse(newcoord[1].ToString()) - 1;

        return newletter == letter ^ newnumber == number;        
    }

    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public static bool Bishop(string oldcoord, string newcoord)
    {
        int letter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(oldcoord[0]).ToString());
        int number = int.Parse(oldcoord[1].ToString()) - 1;

        int newletter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(newcoord[0]).ToString());
        int newnumber = int.Parse(newcoord[1].ToString()) - 1;

        for (int i = 1; i < 8; i++)
        {
            if (Math.Abs(newletter - letter) == Math.Abs(newnumber - number)) return true;
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
        int letter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(oldcoord[0]).ToString());
        int number = int.Parse(oldcoord[1].ToString()) - 1;

        int newletter = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(newcoord[0]).ToString());
        int newnumber = int.Parse(newcoord[1].ToString()) - 1;

        return (Math.Abs(newletter - letter) <=1 && Math.Abs(newnumber - number) <= 1);
    }
}
