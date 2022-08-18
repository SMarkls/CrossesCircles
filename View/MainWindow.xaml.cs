using System;
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
        BitmapImage circle = new BitmapImage(new Uri("../Resources/Circle.png", UriKind.Relative));
        BitmapImage cross = new BitmapImage(new Uri("../Resources/Cross.png", UriKind.Relative));
        BitmapImage source;
        Grid grid;
        public MainWindow()
        {
            InitializeComponent();
            grid = FindName("MainGrid") as Grid;
            source = cross;
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
        }
        /// <summary>
        /// Find the location of Image by row and column of the Grid.
        /// </summary>
        /// <param name="row">Row of a button in the Grid</param>
        /// <param name="column">Column of a button in the Grid</param>
        /// <returns></returns>
        Image FindImage(int row, int column)
        {
            foreach(var c in grid.Children.OfType<Image>())
            {
                if (Grid.GetRow(c) == row && Grid.GetColumn(c) == column)
                    return c;
            }
            return null;
        }
        #endregion
        //TODO: масштабирование окна
    }
}
