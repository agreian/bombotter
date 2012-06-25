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
        private Image[,] _imgs = new Image[13, 11];
        private int _currentX = 0, _currentY = 0;
        private BitmapImage[] _bmps = new BitmapImage[3];

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
                    //Button btn = new Button();
                    //btn.Background = Brushes.White;
                    //btn.Style = Style.
                    _imgs[i, j] = new Image();
                    //btn.Content = _imgs[i, j];
                    gridView.Children.Add(_imgs[i, j]);
                    //gridView.Children.Add(btn);

                    _imgs[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    //btn.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    _imgs[i, j].Stretch = Stretch.Fill;
                    _imgs[i, j].StretchDirection = StretchDirection.Both;

                    Grid.SetColumn(_imgs[i, j], i);
                    Grid.SetRow(_imgs[i, j], j);
                    //Grid.SetColumn(btn, i);
                    //Grid.SetRow(btn, j);
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
            if (optBlank.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 0;
            }
            else if (optBreakable.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 1;
            }
            else if (optUnbreakable.IsChecked.Value)
            {
                _map[_currentX, _currentY] = 2;
            }
            
            _imgs[_currentX, _currentY].Source = _bmps[_map[_currentX, _currentY]];
        }

        private void btnSelectTexture_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            SelectTexture window;
            if (optBlank.IsChecked.Value)
            {
                window = new SelectTexture(0);
            }
            else if (optBreakable.IsChecked.Value)
            {
                window = new SelectTexture(1);
            }
            else
            {
                window = new SelectTexture(2);
            }

            window.Show();
            window.Closed += new EventHandler(window_Closed);
            window.TextureSelected += new SelectTexture.TextureSelectedEventHandler(window_TextureSelected);
        }

        void window_TextureSelected(int type, BitmapImage image)
        {
            _bmps[type] = image;

            for (int i = 0; i < 13; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    if (_map[i, j] == type)
                    {
                        _imgs[i, j].Source = image;
                    }
                }
            }
        }

        void window_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
