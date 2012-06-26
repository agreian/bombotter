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
using System.IO;
using Microsoft.Win32;

namespace MapEditor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[,] _map = new byte[13, 11];
        private Image[,] _imgs = new Image[13, 11];

        private List<Selection> _selectedElement = new List<Selection>();
        private Texture[] _bmps = new Texture[3];

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 3; ++i)
            {
                _bmps[i] = new Texture(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Resources\blank.jpg"));
            }
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
                    _imgs[i, j].Source = _bmps[0].Image;
                    //btn.Content = _imgs[i, j];
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(2);
                    border.Child = _imgs[i, j];
                    //gridView.Children.Add(_imgs[i, j]);
                    gridView.Children.Add(border);
                    //gridView.Children.Add(btn);

                    //_imgs[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    border.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    border.MouseEnter += new MouseEventHandler(border_MouseEnter);
                    border.MouseLeave += new MouseEventHandler(border_MouseLeave);
                    //border.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    //border.Mouse
                    //border.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    //btn.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    _imgs[i, j].Stretch = Stretch.Fill;
                    _imgs[i, j].StretchDirection = StretchDirection.Both;

                    //Grid.SetColumn(_imgs[i, j], i);
                    //Grid.SetRow(_imgs[i, j], j);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    //Grid.SetColumn(btn, i);
                    //Grid.SetRow(btn, j);
                }
            }
        }

        void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border temp = (Border)sender;

                if (temp.BorderBrush == Brushes.DarkBlue)
                {
                    temp.BorderBrush = Brushes.Black;
                }
            }
        }

        void border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border temp = (Border)sender;

                if (temp.BorderBrush == Brushes.Black)
                {
                    temp.BorderBrush = Brushes.DarkBlue;
                }
            }
        }

        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border)
            {
                Border temp = (Border)sender;

                if (_selectedElement.Count == 0)
                {
                    int x = Grid.GetColumn((UIElement)sender), y = Grid.GetRow((UIElement)sender);

                    switch (_map[x, y])
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
                }

                if (temp.BorderBrush == Brushes.Red)
                {
                    temp.BorderBrush = Brushes.Black;

                    for (int i = 0; i < _selectedElement.Count; ++i)
                    {
                        if (_selectedElement[i].Image == temp)
                        {
                            _selectedElement.RemoveAt(i);
                            break;
                        }
                    }
                }
                else
                {
                    temp.BorderBrush = Brushes.Red;

                    _selectedElement.Add(new Selection(temp, Grid.GetColumn((UIElement)sender), Grid.GetRow((UIElement)sender)));
                }
            }
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (optBlank.IsChecked.Value)
            {
                for (int i = 0; i < _selectedElement.Count; ++i)
                {
                    _map[_selectedElement[i].X, _selectedElement[i].Y] = 0;
                }
            }
            else if (optBreakable.IsChecked.Value)
            {
                for (int i = 0; i < _selectedElement.Count; ++i)
                {
                    if (TestPosition(_selectedElement[i].X, _selectedElement[i].Y))
                        _map[_selectedElement[i].X, _selectedElement[i].Y] = 1;
                }
            } 
            else if (optUnbreakable.IsChecked.Value)
            {
                for (int i = 0; i < _selectedElement.Count; ++i)
                {
                    if (TestPosition(_selectedElement[i].X, _selectedElement[i].Y))
                        _map[_selectedElement[i].X, _selectedElement[i].Y] = 2;
                }
            }

            for (int i = 0; i < _selectedElement.Count; ++i)
            {
                _imgs[_selectedElement[i].X, _selectedElement[i].Y].Source = _bmps[_map[_selectedElement[i].X, _selectedElement[i].Y]].Image;
                _selectedElement[i].Image.BorderBrush = Brushes.Black;
            }

            _selectedElement.Clear();
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

        void window_TextureSelected(int type, Texture image)
        {
            _bmps[type] = image;

            for (int i = 0; i < 13; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    if (_map[i, j] == type)
                    {
                        _imgs[i, j].Source = image.Image;
                    }
                }
            }

            for (int i = 0; i < _selectedElement.Count; ++i)
            {
                _selectedElement[i].Image.BorderBrush = Brushes.Black;
            }

            _selectedElement.Clear();
        }

        void window_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }

        private bool TestPosition(int x, int y)
        {
            if (
                (x == 0 && y == 0) ||
                (x == 1 && y == 0) ||
                (x == 0 && y == 1) ||
                (x == 12 && y == 0) ||
                (x == 11 && y == 0) ||
                (x == 12 && y == 1) ||
                (x == 0 && y == 10) ||
                (x == 1 && y == 10) ||
                (x == 0 && y == 9) ||
                (x == 12 && y == 10) ||
                (x == 11 && y == 10) ||
                (x == 12 && y == 9)
                )
                return false;
            else
                return true;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".map";
            dlg.Filter = "Fichier map (.map)|*.map";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

                using (StreamReader sr = new StreamReader(filename))
                {
                    for (int j = 0; j < 11; ++j)
                    {
                        string line = sr.ReadLine();
                        string[] chars = line.Split(';');
                        for (int i = 0; i < 13; ++i)
                        {
                            _map[i, j] = byte.Parse(chars[i]);
                        }
                    }

                    for (int i = 0; i < _bmps.Length; ++i)
                    {
                        string line = sr.ReadLine();

                        string file = line.Split('_')[1];
                        string textDir = string.Empty;

                        switch (i)
                        {
                            case 0:
                                textDir = "Sols";
                                break;
                            case 1:
                                textDir = "Destructibles";
                                break;
                            case 2:
                                textDir = "Indestructibles";
                                break;
                            default:
                                break;
                        }

                        _bmps[i] = new Texture(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Textures", textDir, file));
                    }

                    string nameLine = sr.ReadLine();

                    txtName.Text = nameLine.Replace("#Name_", string.Empty);
                }

                for (int i = 0; i < 13; ++i)
                {
                    for (int j = 0; j < 11; ++j)
                    {
                        _imgs[i, j].Source = _bmps[_map[i, j]].Image;
                    }
                }

                for (int i = 0; i < _selectedElement.Count; ++i)
                {
                    _selectedElement[i].Image.BorderBrush = Brushes.Black;
                }

                _selectedElement.Clear();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _bmps.Length; ++i)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(_bmps[i].Path) == "blank" && (new System.IO.DirectoryInfo(_bmps[i].Path)).Parent.ToString() == "Resources")
                {
                    MessageBox.Show("Veuillez sélectionner des textures avant d'enregistrer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if(txtName.Text == "Nom de la carte" || txtName.Text == string.Empty)
            {
                MessageBox.Show("Veuillez indiquer un nom pour la map", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".map";
            dlg.Filter = "Fichier map (.map)|*.map";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    for (int j = 0; j < 11; ++j)
                    {
                        for (int i = 0; i < 13; ++i)
                        {
                            sw.Write(string.Format("{0};", _map[i, j]));
                        }
                        sw.WriteLine();
                    }

                    for (int i = 0; i < _bmps.Length; ++i)
                    {
                        sw.WriteLine(string.Format(@"#{0}_{1}\{2}", i, (new DirectoryInfo(_bmps[i].Path)).Parent, System.IO.Path.GetFileName(_bmps[i].Path)));
                    }

                    sw.WriteLine(string.Format("#Name_{0}", txtName.Text));
                }
            }
        }

        private void TextBox_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox temp = (TextBox)sender;
                if (temp.Text == "Nom de la carte")
                {
                    temp.Text = string.Empty;
                }
            }
        }
    }
}
