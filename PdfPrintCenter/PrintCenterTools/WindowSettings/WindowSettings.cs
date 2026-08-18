using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings
{
    [DataContract]
    internal class WindowSettings : FreezableObject, ICloneable
    {
        private Point _location;
        private Size _size;
        private FormWindowState _windowState;

        [DataMember]
        public Point Location
        {
            get => this._location;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Location));
                this._location = value;
            }
        }

        [DataMember]
        public Size Size
        {
            get => this._size;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Size));
                this._size = value;
            }
        }

        [DataMember]
        public FormWindowState WindowState
        {
            get => this._windowState;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(WindowState));
                this._windowState = value;
            }
        }

        public object Clone()
        {
            Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowSettings = new Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings();
            Point location = this.Location;
            int x = location.X;
            location = this.Location;
            int y = location.Y;
            windowSettings.Location = new Point(x, y);
            Size size = this.Size;
            int width = size.Width;
            size = this.Size;
            int height = size.Height;
            windowSettings.Size = new Size(width, height);
            windowSettings.WindowState = this.WindowState;
            return (object)windowSettings;
        }
    }
}
