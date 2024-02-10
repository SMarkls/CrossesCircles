using System.Windows;
using CrossesCircles.Model;

namespace CrossesCircles.Utils;

static class Checker
{
    /// <summary>
    /// Возвращает кортеж значений: bool - есть ли победная комбинация; ImageSource - картинка победной комбинации; int, int - координаты одной из победивших ячеек
    /// </summary>
    /// <param name="field">Игровое поле</param>
    /// <returns></returns>
    public static WinnerData? CheckForWinner(char[,] field)
    {
        for (int i = 0; i < 3; i++)
        {
            if (field[i, 0] != '.' && field[i, 0] == field[i, 1] && field[i, 0] == field[i, 2])
            {
                return new WinnerData
                {
                    TopLeftSideCoordinate = new Point(0, i),
                    WinnerShape = field[i, 0],
                    WinnerLineType = LineType.Horizontal
                };
            }
        }

        for (int j = 0; j < 3; j++)
        {
            if (field[0, j] != '.' && field[0, j] == field[1, j] && field[0, j] == field[2, j])
            {
                return new WinnerData
                {
                    TopLeftSideCoordinate = new Point(j, 0),
                    WinnerShape = field[0, j],
                    WinnerLineType = LineType.Vertical
                };
            }
        }

        if (field[0, 0] != '.' && field[0, 0] == field[1, 1] && field[0, 0] == field[2, 2])
        {
            return new WinnerData
            {
                TopLeftSideCoordinate = new Point(0, 0),
                WinnerShape = field[0, 0],
                WinnerLineType = LineType.Diagonal
            };
        }

        if (field[0, 2] != '.' && field[0, 2] == field[1, 1] && field[0, 2] == field[2, 0])
        {
            return new WinnerData
            {
                TopLeftSideCoordinate = new Point(0, 2),
                WinnerShape = field[2, 0],
                WinnerLineType = LineType.Horizontal
            };
        }

        return null;
    }
}