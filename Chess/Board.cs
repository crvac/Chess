using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess;

internal class Board
{
    /// <summary>
    /// Creates a chessboard without the coordinates.
    /// </summary>
    /// <returns>An array of the board</returns>
    public char[,] MakeNewBoard()
    {
        char[,] board = new char[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i + j + 2) % 2 == 0) board[i, j] = '_'; //■
                else board[i, j] = '■';
            }
        }

        return board;
    }

    /// <summary>
    /// Prints out the board with the coordinates.
    /// </summary>
    /// <param name="board">Chess board</param>
    public void PrintwCoordinates(char[,] board)
    {
        string[][] coordinates = new string[9][];

        coordinates[0] = new string[9];
        for (int i = 0; i < 9; i++)
        {
            if (i < 8) coordinates[0][i + 1] = ((Letters)i).ToString();
            if (i > 0)
            {
                coordinates[i] = new string[1];
                coordinates[i][0] = Convert.ToString(i);
            }
            else coordinates[0][0] = " ";
        }

        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(coordinates[i][j]);
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write(coordinates[i][0]);
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(board[i - 1, j]);
                }
            }
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Places a figure on the specified coordinates.
    /// </summary>
    /// <param name="coord1">Letter coordinate</param>
    /// <param name="coord2">Number coordinate (need to substract 1 as array starts from 0)</param>
    /// <param name="board">Previous board</param>
    /// <returns></returns>
    public char[,] PlaceOnBoard(char[,] board)
    {
        var moveFigure = new MovePiece();
        var coordinateActions = new Coordinates();
        bool @continue = true;

        do
        {
            string coord = coordinateActions.InputCoorinates();
            if (coordinateActions.ValidateCoordinates(coord))
            {
                var coordinates = new Coords(coord);
                bool @continue2 = true;
                do
                {
                    Console.Write("What piece do you want to put on the board? (K = king, Q = Queen, B = Bishop, R = Rook, N = knight)");
                    string piece = Console.ReadLine();
                    string newcoord;
                    switch (piece.ToUpper())
                    {
                        case "N":
                            board[coordinates.number, coordinates.letter] = 'N';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = coordinateActions.InputCoorinates();
                            if (coordinateActions.ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(moveFigure.KnightValidate(coord, newcoord)); //uxxel
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "R":
                            board[coordinates.number, coordinates.letter] = 'R';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = coordinateActions.InputCoorinates();
                            if (coordinateActions.ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Rook(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "B":
                            board[coordinates.number, coordinates.letter] = 'B';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = coordinateActions.InputCoorinates();
                            if (coordinateActions.ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Bishop(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "Q":
                            board[coordinates.number, coordinates.letter] = 'Q';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = coordinateActions.InputCoorinates();
                            if (coordinateActions.ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Queen(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "K":
                            board[coordinates.number, coordinates.letter] = 'K';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets move the piece now!");
                            newcoord = coordinateActions.InputCoorinates();
                            if (coordinateActions.ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.King(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Piece name");
                            break;
                    }
                } while (@continue2);
            }
            else
            {
                Console.WriteLine("Invalid coordinates, try again");
                @continue = true;
            }
        } while (@continue);

        return board;
    }
}
