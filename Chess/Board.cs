using Chess;
using Chess.Figures;
using Chess.Figures.Contracts;
using System.Drawing;

internal class Board
{
    // The board
    private char[,] board = new char[8, 8];
    private FigureStructure[,] testBoard = new FigureStructure[8, 8];


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
                //if ((i + j + 2) % 2 == 0) board[i, j] = ' '; //■
                //else board[i, j] = ' ';
                  testBoard[i, j].name = FigureNames.empty;
                  testBoard[i, j].team = FigureTeam.empty;
            }
        }
        testBoard[0, 0] = new FigureStructure("R", "W");
        testBoard[0, 1] = new FigureStructure("N", "W");
        testBoard[0, 2] = new FigureStructure("B", "W");
        // mnacacy;

        testBoard[7, 0] = new FigureStructure("R", "B");
        // mnacacy

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
                    if (testBoard[i - 1, j].name == FigureNames.empty)
                    {
                        Console.Write(" ");
                    }
                    else if ((int)testBoard[i - 1, j].team == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(testBoard[i - 1, j].name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(testBoard[i - 1, j].name);
                    }

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
                if (FigureNames.TryParse(piece.ToUpper(), out FigureNames figurename))
                {
                    FigureStructure figure = new FigureStructure(figurename);
                    testBoard[coordinates.number, coordinates.ParseLetterCoordinate(coordinates)] = figure;
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

        testBoard[toCoords.number, coordAction.ParseLetterCoordinate(toCoords)] = testBoard[fromCoords.number, coordAction.ParseLetterCoordinate(fromCoords)];

        testBoard[fromCoords.number, coordAction.ParseLetterCoordinate(fromCoords)] = new FigureStructure();
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
                figure = new Queen();
                break;
            case "R":
                figure = new Rook();
                break;
            default:
                break;

        }

        if (MoveFigure(figure, coord, newcoord) && TakeValidate(coord, newcoord))
        {
            MoveFiguretoNewCoord(coord, newcoord);
        }
        else NewCordValidate(piece.ToUpper(), coord);
    }

    public bool MoveFigure(IMoveFigure figure, string coord, string newcoord)
    {
        return figure.NewCoordMoveValidate(coord, newcoord);
    }

    /// <summary>
    /// Checks if there's a figure on the new coordinate, the nchecks if it can be taken
    /// </summary>
    /// <param name="coord">The coordinate the piece is on</param>
    /// <param name="newcoord">The coordinate the piece needs to move to</param>
    /// <returns>True or Flase</returns>
    public bool TakeValidate(string coord, string newcoord)
    {
        var coordAction = new Coords();

        var fromCoords = coordAction.StringCoordParse(coord);
        var toCoords = coordAction.StringCoordParse(newcoord);

        if (testBoard[toCoords.number, coordAction.ParseLetterCoordinate(toCoords)].team == FigureTeam.empty)
            return true;
        else
            return (testBoard[toCoords.number, coordAction.ParseLetterCoordinate(toCoords)].team != testBoard[fromCoords.number, coordAction.ParseLetterCoordinate(fromCoords)].team);
    }
}
