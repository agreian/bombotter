using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[,] _map = new byte[13, 11];
        private Label[,] _imgs = new Label[13, 11];
        private int _currentX = 0, _currentY = 0;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 13; ++i)
            {
                gridView.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 11; ++i)
            {
                gridView.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 13; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    _map[i, j] = 0;
                    _imgs[i, j] = new Label();
                    gridView.Children.Add(_imgs[i, j]);
                    _imgs[i, j].Content = _map[i, j].ToString();
                    switch (_map[i,j])
                    {
                        case 0:
                            optBlank.IsChecked = true;
                            break;
                        case 1:
                            optBreakable.IsChecked = true;
                            break;
                        case 2:
                            optUnbreakable.IsChecked = true;
                            break;
                        default:
                            break;
                    }

                    _imgs[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);

                    Grid.SetColumn(_imgs[i, j], i);
                    Grid.SetRow(_imgs[i, j], j);
                }
            }
        }

        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement)
            {
                _currentX = Grid.GetColumn((UIElement)sender);
                _currentY = Grid.GetRow((UIElement)sender);

                //MessageBox.Show(string.Format("x : {0}, y : {1}", x, y));
            }
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if(optBlank.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 0;
            }
            else if(optBreakable.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 1;
            }
            else if(optUnbreakable.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 2;
            }

            _imgs[_currentX, _currentY].Content = _map[_currentX, _currentY];
        }

        private void btnSelectTexture_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
