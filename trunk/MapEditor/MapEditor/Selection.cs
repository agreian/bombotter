using System.Windows.Controls;

namespace MapEditor
{
    public class Selection
    {
        public Border Image
        {
            get;
            private set;
        }

        public int X
        {
            get;
            private set;
        }

        public int Y
        {
            get;
            private set;
        }

        public Selection(Border image, int x, int y)
        {
            this.Image = image;
            this.X = x;
            this.Y = y;
        }
    }
}
