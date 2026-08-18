
// Type: Intermech.Bars.ContainerBarResizer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class ContainerBarResizer
    {
      private ContainerBar _a;
      private Point _b;

      public ContainerBarResizer(ContainerBar A_0, Point A_1)
      {
        this._a = A_0;
        this._b = A_1;
      }

      private void a(Point A_0)
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        switch (this._a.Parent.Dock)
        {
          case DockStyle.Top:
            num1 = this._a.MinimumSize.Height + (A_0.Y - this._b.Y);
            num2 = this._a.MinimumFloatingSize.Height;
            num3 = this._a.MaximumFloatingSize.Height;
            this._b = A_0;
            break;
          case DockStyle.Bottom:
            num1 = this._a.MinimumSize.Height + (this._b.Y - A_0.Y);
            num2 = this._a.MinimumFloatingSize.Height;
            num3 = this._a.MaximumFloatingSize.Height;
            break;
          case DockStyle.Left:
            num1 = this._a.MinimumSize.Width + (A_0.X - this._b.X);
            num2 = this._a.MinimumFloatingSize.Width;
            num3 = this._a.MaximumFloatingSize.Width;
            this._b = A_0;
            break;
          case DockStyle.Right:
            num1 = this._a.MinimumSize.Width + (this._b.X - A_0.X);
            num2 = this._a.MinimumFloatingSize.Width;
            num3 = this._a.MaximumFloatingSize.Width;
            break;
        }
        if (num1 < num2 && num2 > 0)
          num1 = num2;
        if (num1 > num3 && num3 > 0)
          num1 = num3;
        if (this._a.Parent.Dock == DockStyle.Left || this._a.Parent.Dock == DockStyle.Right)
          this._a.MinimumSize = new Size(num1, this._a.MinimumSize.Height);
        else
          this._a.MinimumSize = new Size(this._a.MinimumSize.Width, num1);
        this._a.Refresh();
      }

      public void b(Point A_0) => this.a(A_0);
    }
}
