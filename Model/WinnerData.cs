using System.Windows;

namespace CrossesCircles.Model;

public class WinnerData
{
    public char WinnerShape { get; init; }
    public LineType WinnerLineType { get; init; }
    public Point TopLeftSideCoordinate { get; init; }
}

public enum LineType
{
    Vertical,
    Horizontal,
    Diagonal
}