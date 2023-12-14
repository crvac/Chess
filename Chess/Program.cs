using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Chess;

class Program
{
    static void Main()
    {
        //Console.WriteLine("♙♘♗♖♕♔♟♞♝♜♛♚");

        RunChessSession();
    }

    /// <summary>
    /// Runs the application
    /// </summary>
    static void RunChessSession()
    {
        var boardActions = new Board();
        do
        {
            boardActions.MakeNewBoard();
            //Prints an empty Board
            boardActions.PrintBoard();
            //Prints the board with your figure on it
            boardActions.PlaceOnBoard();
            Console.ReadLine();
        } while (true);
    }
    // library unenanq, figure objectnery sarqel, guyn (Team) sarqel, move validation etc.
}
