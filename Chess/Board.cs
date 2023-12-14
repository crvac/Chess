using Chess;
using Chess.Figures;
using Chess.Figures.Contracts;
using System.Drawing;

internal class Board
{
    // The board
    private char[,] board = new char[8, 8];

    /// <summary>
    /// Creates a chessboard without the coordinates.
    /// </summary>
    /// <returns>An array of the board</returns>
    public void MakeNewBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i + j + 2) % 2 == 0) board[i, j] = ' '; //■
                else board[i, j] = ' ';
            }
        }
    }

    /// <summary>
    /// Prints out the board with the coordinates.
    /// </summary>
    /// <param name="board">Chess board</param>
    public void PrintBoard()
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
                    Console.Write(coordinates[i][j] + " ");
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write(coordinates[i][0]);
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j + 2) % 2 == 0) Console.BackgroundColor = ConsoleColor.DarkRed; //■
                    else Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                    Console.Write(board[i - 1, j]);
                }
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Places a figure on the specified coordinates.
    /// </summary>
    /// <param name="board">Previous board</param>
    public void PlaceOnBoard()
    {
        var coordinateActions = new Coordinates();

        string coord = coordinateActions.InputCoorinates();
        if (coordinateActions.ValidateCoordinates(coord))
        {
            var coordinates = new Coords();
            coordinates = coordinates.StringCoordParse(coord);

            bool tryagain = true;
            do
            {
                Console.Write("What piece do you want to put on the board? (K = king, Q = Queen, B = Bishop, R = Rook, N = knight)");
                string piece = Console.ReadLine();
                if (FigureNames.TryParse(piece.ToUpper(), out FigureNames pieceUpper))
                {
                    board[coordinates.number, coordinates.numericLetter] = char.ToUpper(char.Parse(piece));
                    PrintBoard();
                    NewCordValidate(piece, coord);
                    PrintBoard();
                    tryagain = false;
                }
                else
                {
                    Console.WriteLine("Invalid figure");
                }
            } while (tryagain);
        }
        else
        {
            Console.WriteLine("Invalid coordinates, try again");
            PlaceOnBoard();
        }

    }

    public void MoveFiguretoNewCoord(string oldcoord, string newcoord)
    {
        var coordAction = new Coords();

        var fromCoords = coordAction.StringCoordParse(oldcoord);
        var toCoords = coordAction.StringCoordParse(newcoord);

        board[toCoords.number, toCoords.numericLetter] = char.ToUpper(board[fromCoords.number, fromCoords.numericLetter]);
        board[fromCoords.number, fromCoords.numericLetter] = ' ';
    }

    public void NewCordValidate(string piece, string coord)
    {
        var coordinateActions = new Coordinates();
        Console.WriteLine("Where do you want to move your piece?");
        var newcoord = coordinateActions.InputCoorinates();
        IMoveFigure figure = null;
        switch (piece.ToUpper())
        {
            case "B":
                figure = new Bishop();
                break;
            case "K":
                figure = new King();
                break;
            case "N":
                figure = new Knight();
                break;
            case "Q":
                figure = new Knight();
                break;
            case "R":
                figure = new Knight();
                break;
            default : 
                break;

        }

        if (MoveFigure(figure, coord, newcoord)) MoveFiguretoNewCoord(coord, newcoord);
        else NewCordValidate(piece.ToUpper(), coord);
    }

    public bool MoveFigure(IMoveFigure figure, string coord, string newcoord)
    {
        return figure.NewCoordMoveValidate(coord, newcoord);
    }
}
