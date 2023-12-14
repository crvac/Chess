using Chess;

internal class Coordinates
{

    /// <summary>
    /// Takes the input coordinates.
    /// </summary>
    /// <returns>coordinates</returns>
    public string InputCoorinates()
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
    public bool ValidateCoordinates(string coord)
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
}
