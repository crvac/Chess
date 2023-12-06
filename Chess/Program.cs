using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Chess;

class Program
{
    static void Main()
    {
        Console.WriteLine("♙♘♗♖♕♔♟♞♝♜♛♚");
        PrintwCoordinates(MakeANewBoard());
        ValidateCoordinates(MakeANewBoard());
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
    /// Gets coordinates from user.
    /// </summary>
    /// <param name="board">Previous board</param>
    static void ValidateCoordinates(char[,] board)
    {
        bool @continue = true;

        do
        {
            Console.Write("Enter coordinates to put the knight on (A -> H, 1 -> 8) (example: a1, b8, h3): ");
            string coord = Console.ReadLine();
            if (coord.Length == 2 && (char.ToUpper(coord[0]) <= 'H' && char.ToUpper(coord[0]) >= 'A'))
            {
                if (int.TryParse(coord[1].ToString(), out int coord2) && coord2 >= 1 && coord2 <= 8)
                {
                    int coord1 = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(coord[0]).ToString());
                    @continue = true;

                    PlaceOnBoard(coord1, coord2, board);
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        } while (@continue);
    }

    /// <summary>
    /// Places a figure on the specified coordinates.
    /// </summary>
    /// <param name="coord1">Letter coordinate</param>
    /// <param name="coord2">Number coordinate (need to substract 1 as array starts from 0)</param>
    /// <param name="board">Previous board</param>
    /// <returns></returns>
    static char[,] PlaceOnBoard(int coord1, int coord2, char[,] board)
    {
        board[coord2 - 1, coord1] = 'K';
        coord2--;
        PrintwCoordinates(board);
        CheckAvalableMoves(coord1, coord2, board);
        return board;
    }

    /// <summary>
    /// Shows avalable moves for the knight (as of now).
    /// </summary>
    /// <param name="coord1">Letter coordinate</param>
    /// <param name="coord2">Number coordinate</param>
    /// <param name="board">The board with the figures on it</param>
    static void CheckAvalableMoves(int coord1, int coord2, char[,] board)
    {
        int[,] avmoves = new int[8,2];
        int i = 0;
        Console.Write($"Avalable moves for the knight on {(Cord)coord1}{coord2+1} are: ");

        // +2 +-1
        if ((coord2 + 2 <= 7 && coord1 + 1 <= 7) && board[coord2 + 2, coord1 + 1] != 'K')
        {
            Console.Write($"{(Cord)coord1 + 1}{(coord2+1) + 2} ");
            avmoves[i,0] = coord1 + 1;
            avmoves[i,1] = coord2 + 2;
            i++;
        }
        if ((coord2 + 2 <= 7 && coord1 - 1 >= 0 ) && board[coord2 + 2, coord1 - 1] != 'K')
        {
            Console.Write($"{(Cord)coord1 - 1}{(coord2 + 1) + 2} ");
            avmoves[i, 0] = coord1 - 1;
            avmoves[i, 1] = coord2 + 2;
            i++;
        }
        // -2 +-1
        if ((coord2 - 2 >= 0 && coord1 + 1 <= 7) && board[coord2 - 2, coord1 + 1] != 'K')
        {
            Console.Write($"{(Cord)coord1 + 1}{(coord2 + 1) - 2} ");
            avmoves[i, 0] = coord1 + 1;
            avmoves[i, 1] = coord2 - 2;
            i++;
        }
        if ((coord2 - 2 >= 0 && coord1 - 1 >= 0) && board[coord2 - 2, coord1 - 1] != 'K')
        {
            Console.Write($"{(Cord)coord1 - 1}{(coord2 + 1) - 2} ");
            avmoves[i, 0] = coord1 - 1;
            avmoves[i, 1] = coord2 - 2;
            i++;
        }
        // +1 +-2
        if ((coord2 + 1 <= 7 && coord1 + 2 <= 7) && board[coord2 + 1, coord1 + 2] != 'K')
        {
            Console.Write($"{(Cord)coord1 + 2}{(coord2 + 1) + 1} ");
            avmoves[i, 0] = coord1 + 2;
            avmoves[i, 1] = coord2 + 1;
            i++;
        }
        if ((coord2 + 1 <= 7 && coord1 - 2 >= 0) && board[coord2 + 1, coord1 - 2] != 'K')
        {
            Console.Write($"{(Cord)coord1 - 2}{(coord2 + 1) + 1} ");
            avmoves[i, 0] = coord1 - 2;
            avmoves[i, 1] = coord2 + 1;
            i++;
        }
        // -1 +-2
        if ((coord2 - 1 >= 0 && coord1 + 2 <= 7) && board[coord2 - 1, coord1 + 2] != 'K')
        {
            Console.Write($"{(Cord)coord1 + 2}{(coord2 + 1) - 1} ");
            avmoves[i, 0] = coord1 + 2;
            avmoves[i, 1] = coord2 - 1;
            i++;
        }
        if ((coord2 - 1 >= 0 && coord1 - 2 >= 0) && board[coord2 - 1, coord1 - 2] != 'K')
        {
            Console.Write($"{(Cord)coord1 - 2}{(coord2 + 1) - 1} ");
            avmoves[i, 0] = coord1 - 2;
            avmoves[i, 1] = coord2 - 1;
        }
        
        Console.WriteLine();
        int maxnumberofmoves = i;
        Console.Write("Do you want to move the piece? (Y/N)");
        string yesno = Console.ReadLine();
        if (yesno.ToUpper() == "Y") MovePiece(coord1, coord2, avmoves, board, maxnumberofmoves);
        else ValidateCoordinates(board);
    }

    /// <summary>
    /// Moves the piece you put on the board.
    /// </summary>
    /// <param name="coord1">the letter coordinate the pice is on</param>
    /// <param name="coord2">the number coordinate the pice is on</param>
    /// <param name="avmoves">Avalable moves</param>
    /// <param name="board">Your borad</param>
    static void MovePiece(int coord1, int coord2, int[,] avmoves, char[,] board, int max)
    { 
        if ((coord1 + coord2 + 2) % 2 == 0) board[coord2, coord1] = '■'; //■
        else board[coord2, coord1] = '_';

        bool @continue = true;
        do
        {
            Console.WriteLine("Enter coordinates from the list: ");
            string newcoord = Console.ReadLine();
            if (newcoord.Length == 2 && (char.ToUpper(newcoord[0]) <= 'H' && char.ToUpper(newcoord[0]) >= 'A'))
            {
                if (int.TryParse(newcoord[1].ToString(), out int newcoord2) && newcoord2 >= 1 && newcoord2 <= 8)
                {
                    int newcoord1 = (int)Cord.Parse(typeof(Chess.Cord), char.ToUpper(newcoord[0]).ToString());
                    for (int i = 0; i < max; i ++)
                    {
                        if (newcoord1 == avmoves[i, 0] && newcoord2 - 1 == avmoves[i, 1])
                        {
                            PlaceOnBoard(newcoord1, newcoord2, board);
                            @continue = false;
                        }
                    }
                    Console.WriteLine("Can't move the piece there.");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    @continue = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
                @continue = true;
            }
        } while (@continue);
    }
}