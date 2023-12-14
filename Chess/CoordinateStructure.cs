namespace Chess;

/// <summary>
/// Takes in the string of the coordinate input and gives integer values for number and letter.
/// </summary>
public struct Coords
{
    public char letter;
    public int numericLetter;
    public int number;

    public Coords StringCoordParse(string coordinate)
    {
        var numericcoord = new Coords();

        numericcoord.numericLetter = (int)Letters.Parse(typeof(Letters), char.ToUpper(coordinate[0]).ToString());
        numericcoord.letter = char.ToUpper(coordinate[0]);
        numericcoord.number = int.Parse(coordinate[1].ToString()) - 1;

        return numericcoord;
    }

}
