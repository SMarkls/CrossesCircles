using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CrossesCircles.Components;
using CrossesCircles.Model;
using CrossesCircles.Utils;

namespace CrossesCircles.View;

public partial class MainWindow
{
    private byte currentState;
    private readonly Grid grid;
    private readonly PlayField playField;
    private Image?[,] images = new Image?[3, 3];
    private FieldElementFactory factory = new();

    public MainWindow()
    {
        InitializeComponent();
        grid = FindName("MainGrid") as Grid ?? throw new InvalidOperationException("Grid is null");
        playField = new PlayField();
        currentState = 0;
        InitGrid();
    }

    private void ResetBtnClicked(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playField.Field[i, j] = '.';
            }
        }

        foreach (var image in images)
        {
            grid.Children.Remove(image);     
        }

        ClearImageArray();
        InitGrid();
        currentState = 0;
        StatusTextBlock.Text = "Игра началась!";
    }

    public void ClearImageArray()
    {
        int width = images.GetLength(0);
        int height = images.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                images[i, j] = null;
            }
        }
    }

    private async void BtnClicked(object sender, RoutedEventArgs e)
    {
        if (sender is not OneClickableButton button) 
            throw new ArgumentNullException(nameof(button));
        if (button.IsClicked)
            return;
        button.IsClicked = true;

        await Task.Delay(250); // Duration of animtion.
        button.Visibility = Visibility.Collapsed;

        int column = Grid.GetColumn(button) - 1;
        int row = Grid.GetRow(button) - 1;

        playField.Field[row, column] = currentState % 2 == 0 ? 'x' : 'o';
        UpdateGrid(column, row);
        var resultOfStep = Checker.CheckForWinner(playField.Field);
        if (resultOfStep != null)
        {
            var winner = resultOfStep.WinnerShape == 'x' ? "Крестик" : "Нолик";
            StatusTextBlock.Text = $"Игра окончена.\nПобедил: {winner}";
            Animation.AnimateWin((int)resultOfStep.TopLeftSideCoordinate.X, (int)resultOfStep.TopLeftSideCoordinate.Y, resultOfStep.WinnerLineType, images);
        }

        currentState++;
    }

    private void UpdateGrid(int column, int row)
    {
        var symbol = playField.Field[row, column];
        var element = factory.GetElement(symbol);

        switch (element)
        {
            case Image image:
                images[row, column] = image;
                break;
            case Button btn:
                btn.Click += BtnClicked;
                break;
        }

        grid.Children.Add(element);
        Grid.SetColumn(element, column + 1);
        Grid.SetRow(element, row + 1);
    }

    private void InitGrid()
    {
        for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
        {
            UpdateGrid(j, i);
        }
    }
}