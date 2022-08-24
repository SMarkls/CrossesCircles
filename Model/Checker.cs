using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossesCircles.Model
{
    static class Checker
    {
        public enum Line
        {
            Vertical,
            Horizontal,
            DiagonalOrNull
        };


        /// <summary>
        /// Возвращает кортеж значений: bool - есть ли победная комбинация; ImageSource - картинка победной комбинации; int, int - координаты одной из победивших ячеек
        /// </summary>
        /// <param name="ImageArr">Массив изображений</param>
        /// <returns></returns>
        public static (bool, ImageSource, int, int, Line) CheckForWinner(Image[, ] ImageArr)
        {
            for (int i = 0; i <= 2; i++) // Horisontal Lines
            {   
                int k = 0;
                for (int j = 0; j < 2; j++)
                {
                    if (ImageArr[i, j].Source != ImageArr[i, j + 1].Source || ImageArr[i, j].Source == null)
                        break;
                    k = j;
                }
                if (k != 1) continue;
                return (true, ImageArr[i, k].Source, i, k, Line.Horizontal);
            }
            for (int i = 0; i <= 2; i++) // Vertical Lines
            {
                int k = 0;
                for (int j = 0; j < 2; j++)
                {
                    if (ImageArr[j, i].Source != ImageArr[j + 1, i].Source || ImageArr[j, i].Source == null)
                        break;
                    k = j;
                }
                if (k != 1) continue;
                return (true, ImageArr[k, i].Source, k, i, Line.Vertical);
            }
            if (ImageArr[1, 1].Source == null)
                return (false, null, -1, -1, Line.DiagonalOrNull);
            if (ImageArr[0, 0].Source == ImageArr[1, 1].Source && ImageArr[2, 2].Source == ImageArr[1, 1].Source) // diagonals
                return (true, ImageArr[1, 1].Source, 0, 0, Line.DiagonalOrNull);
            if (ImageArr[2, 0].Source == ImageArr[1, 1].Source && ImageArr[0, 2].Source == ImageArr[1, 1].Source)
                return (true, ImageArr[1, 1].Source, 2, 0, Line.DiagonalOrNull);
            return (false, null, -1, -1, Line.DiagonalOrNull);
        }
    }
}
