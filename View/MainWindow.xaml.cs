using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using CrossesCircles.Model;


namespace CrossesCircles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image[,] ImageArr = new Image[3, 3];
        Button[,] ButtonArr = new Button[3, 3];
        BitmapImage circle = new BitmapImage(new Uri("../Resources/Circle.png", UriKind.Relative));
        BitmapImage cross = new BitmapImage(new Uri("../Resources/Cross.png", UriKind.Relative));
        BitmapImage source;
        Grid grid;

        public MainWindow()
        {
            InitializeComponent();
            grid = FindName("MainGrid") as Grid;
            source = cross;
            InitImagesArray();
            InitButtonsArray();
        }

        private void ResetBtnClicked(object sender, RoutedEventArgs e)
        {
            foreach(var b in ButtonArr)
            {
                b.Visibility = Visibility.Visible;
                b.Click -= BtnClicked;
                b.Click += BtnClicked;
                b.RenderTransform = new ScaleTransform(1, 1);
            }
            foreach (var c in ImageArr)
                c.Source = null;
            StatusTextBlock.Text = "Игра началась!";
            source = cross;
        }


        #region Button Clicked and Initializing image
        private async void BtnClicked(object sender, RoutedEventArgs e)
        {
            await Task.Delay(250); // Duration of animtion.
            var button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            Image image = FindImage(row, column);
            image.Source = source;
            source = source == circle ? cross : circle;
            var resultOfStep = Checker.CheckForWinner(ImageArr);
            if (resultOfStep.Item1)
            {
                foreach (var b in ButtonArr)
                    b.Click -= BtnClicked;
                var winner = resultOfStep.Item2 == cross ? "Крестик" : "Нолик";
                StatusTextBlock.Text = $"Игра окончена.\nПобедил: {winner}";
                Animation.AnimateWin(resultOfStep.Item3, resultOfStep.Item4, resultOfStep.Item5, ImageArr);

            }
        } 

        /// <summary>
        /// Находит изображение в сетке по строке и колонке кнопки..
        /// </summary>
        /// <param name="row">Строка кнопки в сетке</param>
        /// <param name="column">Колонка кнопки в сетке</param>
        /// <returns></returns>
        Image FindImage(int row, int column)
        {
            foreach(var c in ImageArr)
            {
                if (Grid.GetRow(c) == row && Grid.GetColumn(c) == column)
                    return c;
            }
            return null;
        }
        #endregion
        #region Initializing Arrays
        private void InitImagesArray()
        {
            int i = 0;
            int j = 0;
            foreach (var c in grid.Children.OfType<Image>().OrderBy(x => Grid.GetRow(x)).ThenBy(x => Grid.GetColumn(x)))
            {
                ImageArr[i, j] = c;
                j++;
                if (j == 3)
                {
                    j = 0;
                    i++;
                }

            }
        }

        private void InitButtonsArray()
        {
            int i = 0;
            int j = 0;
            foreach (var b in grid.Children.OfType<Button>().Where(x => Grid.GetColumn(x) != 5).OrderBy(x => Grid.GetRow(x)).ThenBy(x => Grid.GetColumn(x)))
            {
                ButtonArr[i, j] = b;
                j++;
                if (j == 3)
                {
                    j = 0;
                    i++;
                }
            }

        }
        #endregion
    }
}
        //TODO: масштабирование окна