using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace MapEditor
{
    public class Texture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            private set
            {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }
        private BitmapImage _image;

        public string Label
        {
            get
            {
                return _label;
            }
            private set
            {
                _label = value;
                NotifyPropertyChanged("Label");
            }
        }
        private string _label;

        public string Path
        {
            get;
            private set;
        }

        public Texture(string path)
        {
            this.Path = path;
            this.Label = string.Format("{0} {1}", (new DirectoryInfo(path)).Parent.ToString(), System.IO.Path.GetFileNameWithoutExtension(path));
            this.Image = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }
    }
}
