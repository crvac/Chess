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
            //Prints an empty Board
            boardActions.PrintwCoordinates(boardActions.MakeNewBoard());
            //Prints the board with your figure on it
            boardActions.PlaceOnBoard(boardActions.MakeNewBoard());
        } while (true);
    }






    

    
}
