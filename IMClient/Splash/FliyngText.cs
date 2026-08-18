
// Type: IMClient.Splash.FliyngText




using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient.Splash
{
    internal class FliyngText : Control
    {
      private StringFormat _sf;
      private SolidBrush _brush;
      internal bool _active;
      internal string _text1 = "explore";
      internal string _text2 = string.Empty;
      private IContainer components;

      public FliyngText()
      {
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        this.InitializeComponent();
        this._sf = new StringFormat(StringFormat.GenericTypographic);
        this._sf.Alignment = StringAlignment.Near;
        this._sf.LineAlignment = StringAlignment.Center;
        this._brush = new SolidBrush(Color.Empty);
      }

      public override Color ForeColor
      {
        get => this._brush.Color;
        set
        {
          this._brush.Color = value;
          this.Invalidate();
        }
      }

      protected override void OnPaint(PaintEventArgs pe)
      {
        if (!this._active)
          return;
        pe.Graphics.DrawString(this._text1, this.Font, (Brush) this._brush, (RectangleF) this.ClientRectangle, this._sf);
        if (this._text2.Length <= 0)
          return;
        Color color = this._brush.Color;
        this._brush.Color = Color.FromArgb((int) byte.MaxValue - (int) color.A, (int) color.R, (int) color.G, (int) color.B);
        pe.Graphics.DrawString(this._text2, this.Font, (Brush) this._brush, (RectangleF) this.ClientRectangle, this._sf);
        this._brush.Color = color;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
    }
}
