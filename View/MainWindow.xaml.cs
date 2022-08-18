﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrossesCircles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image[,] ImageArr = new Image[3, 3];
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
        }

        #region Button Clicked and Initializing image
        private void BtnClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            Image image = FindImage(row, column);
            image.Source = source;
            source = source == circle ? cross : circle;
            MessageBox.Show(CheckForWinner().ToString());
        }
        /// <summary>
        /// Find the location of Image by row and column of the Grid.
        /// </summary>
        /// <param name="row">Row of a button in the Grid</param>
        /// <param name="column">Column of a button in the Grid</param>
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
        //TODO: масштабирование окна
    
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
        private bool CheckForWinner()
        {
            for (int i = 0; i < 2; i++) // Horisontal Lines
            {
                int k = 0;
                for (int j = 0; j < 2; j++)
                {
                    if (ImageArr[i, j].Source != ImageArr[i, j + 1].Source || ImageArr[i, j].Source == null)
                        break;
                    k = j;
                }
                if (k != 1) continue;
                return true;
            }
            for (int i = 0; i < 2; i++) // Vertical Lines
            {
                int k = 0;
                for (int j = 0; j < 2; j++)
                {
                    if (ImageArr[j, i].Source != ImageArr[j + 1, i].Source || ImageArr[j, i].Source == null)
                        break;
                    k = j;
                }
                if (k != 1) continue;
                return true;
            }
            if (ImageArr[1, 1].Source == null)
                return false;
            if (ImageArr[0, 0].Source == ImageArr[1, 1].Source && ImageArr[2, 2].Source == ImageArr[1, 1].Source) // diagonals
                return true;
            if (ImageArr[2, 0].Source == ImageArr[1, 1].Source && ImageArr[0, 2].Source == ImageArr[1, 1].Source)
                return true;    
            return false;
        }

    }
}