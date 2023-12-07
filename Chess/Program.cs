using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Chess;

class Program
{
    static void Main()
    {
        //Console.WriteLine("♙♘♗♖♕♔♟♞♝♜♛♚");
        PrintwCoordinates(MakeANewBoard());
        PlaceOnBoard(MakeANewBoard());
    }

    /// <summary>
    /// Creates a chessboard without the coordinates.
    /// </summary>
    /// <returns>An array of the board</returns>
    static char[,] MakeANewBoard()
    {
        char[,] board = new char[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i + j + 2) % 2 == 0) board[i, j] = '■'; //■
                else board[i, j] = '_';
                //Console.Write(board[i, j]);
            }
            //Console.WriteLine();
        }

        return board;
    }

    /// <summary>
    /// Prints out the board with the coordinates.
    /// </summary>
    /// <param name="board">Chess board</param>
    static void PrintwCoordinates(char[,] board)
    {
        string[][] coordinates = new string[9][];

        coordinates[0] = new string[9];
        for (int i = 0; i < 9; i++)
        {
            if (i < 8) coordinates[0][i + 1] = ((Cord)i).ToString();
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
    /// Takes the input coordinates.
    /// </summary>
    /// <returns>coordinates</returns>
    static string InputCoorinates()
    {
        Console.Write("Enter coordinates to put the piece on (A -> H, 1 -> 8) (example: a1, b8, h3): ");
        string coord = Console.ReadLine();
        return coord;
    }

    /// <summary>
    /// Valdiates the coordinates.
    /// </summary>
    /// <param name="coord">Coordinate input (Letter + Number)</param>
    /// <returns>true or false</returns>
    static bool ValidateCoordinates(string coord)
    {
        if (coord.Length == 2 && (char.ToUpper(coord[0]) <= 'H' && char.ToUpper(coord[0]) >= 'A')
                && int.TryParse(coord[1].ToString(), out int coord2) && coord2 >= 1 && coord2 <= 8)
        {
            return true;
        }
        else
        {
            Console.WriteLine("Invalid input");
            return false;
        }
    }

    /// <summary>
    /// Places a figure on the specified coordinates.
    /// </summary>
    /// <param name="coord1">Letter coordinate</param>
    /// <param name="coord2">Number coordinate (need to substract 1 as array starts from 0)</param>
    /// <param name="board">Previous board</param>
    /// <returns></returns>
    static char[,] PlaceOnBoard(char[,] board)
    {
        bool @continue = true;

        do
        {
            string coord = InputCoorinates();
            if (ValidateCoordinates(coord))
            {
                int coord1 = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(coord[0]).ToString());
                int coord2 = int.Parse(coord[1].ToString()) - 1;
                bool @continue2 = true;
                do
                {
                    Console.Write("What piece do you want to put on the board? (K = king, Q = Queen, B = Bishop, R = Rook, N = knight)");
                    string piece = Console.ReadLine();
                    string newcoord;
                    switch (piece.ToUpper())
                    {
                        case "N":
                            board[coord2, coord1] = 'N';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = InputCoorinates();
                           if (ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Knight(coord ,newcoord));
                                @continue = false;
                            }
                           @continue2 = false;
                           break;
                        case "R":
                            board[coord2, coord1] = 'R';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = InputCoorinates();
                            if (ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Rook(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "B":
                            board[coord2, coord1] = 'B';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = InputCoorinates();
                            if (ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Bishop(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "Q":
                            board[coord2, coord1] = 'Q';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = InputCoorinates();
                            if (ValidateCoordinates(newcoord))
                            {
                                Console.WriteLine(MovePiece.Queen(coord, newcoord));
                                @continue = false;
                            }
                            @continue2 = false;
                            break;
                        case "K":
                            board[coord2, coord1] = 'K';
                            PrintwCoordinates(board);
                            Console.WriteLine("Lets see if you know your move!");
                            newcoord = InputCoorinates();
                            if (ValidateCoordinates(newcoord))
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
