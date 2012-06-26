using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace MapEditor
{
    /// <summary>
    /// Logique d'interaction pour SelectTexture.xaml
    /// </summary>
    public partial class SelectTexture : Window
    {
        public ObservableCollection<Texture> Textures
        {
            get;
            private set;
        }

        public delegate void TextureSelectedEventHandler(int type, Texture image);
        public event TextureSelectedEventHandler TextureSelected;

        private int _type = -1;

        private void NotifyTextureSelected(Texture image)
        {
            if (TextureSelected != null)
                TextureSelected(_type, image);
        }

        public SelectTexture(byte type)
        {
            InitializeComponent();

            listTextures.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(listTextures_MouseDoubleClick);

            _type = type;
            string currentDir = Directory.GetCurrentDirectory();
            string textureDir = Path.Combine(currentDir, "Textures");
            string dirName = string.Empty;
            switch (type)
            {
                case 0:
                    dirName = "Sols";
                    this.Title = "Textures du sol";
                    break;
                case 1:
                    dirName = "Destructibles";
                    this.Title = "Textures des blocs destructibles";
                    break;
                case 2:
                    dirName = "Indestructibles";
                    this.Title = "Textures des blocs indestructibles";
                    break;
                default:
                    break;
            }
            string currentTextureDir = Path.Combine(textureDir, dirName);
            Textures = new ObservableCollection<Texture>();
            string[] dirs = Directory.GetDirectories(currentTextureDir);
            for (int i = 0; i < dirs.Length; ++i)
            {
                string[] files = Directory.GetFiles(dirs[i]);
                for (int j = 0; j < files.Length; ++j)
                {
                    Textures.Add(new Texture(files[j]));
                }
            }
        }

        void listTextures_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listTextures.SelectedItem != null && listTextures.SelectedItem is Texture)
            {
                NotifyTextureSelected((Texture)listTextures.SelectedItem);
            }

            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (listTextures.SelectedItem != null && listTextures.SelectedItem is Texture)
            {
                NotifyTextureSelected((Texture)listTextures.SelectedItem);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
