using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossesCircles.Model
{
    static class Animation
    {
        /// <summary>
        /// Функция анимации картинки. Увеличивает в 1.2 раза изображение, а потом возвращает его к исходному размеру.
        /// </summary>
        /// <param name="image">Изображение необходиоме для анимации.</param>
        /// <returns></returns>
        private static async Task Anim(Image image)
        {
            for (double i = 1; i < 1.2; i += 0.005)
            {
                image.RenderTransform = new ScaleTransform(i, i);
                await Task.Delay(10);
            }
            image.RenderTransform = new ScaleTransform(1, 1);
        }

        /// <summary>
        /// По полученным координатам ячейки вызывает функцию анимации
        /// </summary>
        /// <param name="i">Строка победившей ячейки</param>
        /// <param name="j">Колонка победившей ячейки</param>
        /// <param name="ImageArr">Массив изображений</param>
        public static void AnimateWin(int i, int j, Checker.Line line, Image[,] ImageArr)
        {
            switch (line)
            {
                case Checker.Line.Horizontal:
                    {
                        for (int x = 0; x < 3; x++)
                            Anim(ImageArr[i, x]);
                    }
                    break;
                case Checker.Line.Vertical:
                    {
                        for (int x = 0; x < 3; x++)
                            Anim(ImageArr[x, j]);
                    }
                    break;
                default:
                    {
                        if (i == 2 && j == 0)
                        {
                            Anim(ImageArr[0, 2]);
                            Anim(ImageArr[1, 1]);
                            Anim(ImageArr[2, 0]);
                        }

                        if (i == 0 && j == 0)
                        {
                            Anim(ImageArr[0, 0]);
                            Anim(ImageArr[1, 1]);
                            Anim(ImageArr[2, 2]);
                        }
                    }
                break;
            }
        }
    }
}
