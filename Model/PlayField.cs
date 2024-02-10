namespace CrossesCircles.Model;

public class PlayField
{
    private char[,] field = {
        { '.', '.', '.' },
        { '.', '.', '.' },
        { '.', '.', '.' } 
    };

    public char[,] Field => field;
}