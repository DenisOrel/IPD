
// Type: Intermech.Util.ToolTips
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Intermech.Util
{
    internal class ToolTips : IDisposable
    {
      private bool _disposed;
      private bool _visible;
      private Control _control;
      private Timer _timer;
      private Form _parentForm;
      private Point _showPoint;
      private ToolTips.ToolTipsForm _tooltipForm;
      private bool _dropShadow = true;

      public event ToolTips.GetToolTipTextEventHandler GetToolTipText;

      public ToolTips(Control control)
      {
        this._control = control;
        control.MouseMove += new MouseEventHandler(this.Control_MouseMove);
        control.MouseLeave += new EventHandler(this.Control_MouseLeave);
        control.MouseDown += new MouseEventHandler(this.Control_MouseDown);
        control.MouseWheel += new MouseEventHandler(this.Control_MouseWheel);
        control.Disposed += new EventHandler(this.Control_Disposed);
        control.FontChanged += new EventHandler(this.Control_FontChanged);
        this._tooltipForm = new ToolTips.ToolTipsForm(this);
        this._tooltipForm.MouseMove += new MouseEventHandler(this.Tooltip_MouseMove);
        this._timer = new Timer();
        int num = SystemInformation.DoubleClickTime;
        if (num == 0)
          num = 480;
        this._timer.Interval = num;
        this._timer.Tick += new EventHandler(this.Timer_Tick);
      }

      public void Dispose()
      {
        if (this._disposed)
          return;
        this.Hide();
        this._tooltipForm.MouseMove -= new MouseEventHandler(this.Tooltip_MouseMove);
        this._tooltipForm.Dispose();
        this._tooltipForm = (ToolTips.ToolTipsForm) null;
        this._control.MouseMove -= new MouseEventHandler(this.Control_MouseMove);
        this._control.MouseLeave -= new EventHandler(this.Control_MouseLeave);
        this._control.MouseDown -= new MouseEventHandler(this.Control_MouseDown);
        this._control.MouseWheel -= new MouseEventHandler(this.Control_MouseWheel);
        this._control.Disposed -= new EventHandler(this.Control_Disposed);
        this._control.FontChanged -= new EventHandler(this.Control_FontChanged);
        this._control = (Control) null;
        this._timer.Tick -= new EventHandler(this.Timer_Tick);
        this._timer.Dispose();
        this._disposed = true;
      }

      [DllImport("user32.dll")]
      private static extern bool SetWindowPos(
        IntPtr hWnd,
        int hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        int flags);

      private void Tooltip_MouseMove(object sender, MouseEventArgs xfbf34718e704c6bc) => this.Hide();

      private void Control_MouseDown(object sender, MouseEventArgs xfbf34718e704c6bc)
      {
        if (this._visible)
          this.Hide();
        this._timer.Enabled = false;
      }

      public void Show(Point pos, string text)
      {
        this._tooltipForm.Text = text;
        Size size = Size.Ceiling(this._tooltipForm.MeasureString(text));
        size.Height += 4;
        size.Width += 4;
        pos.Y += 19;
        Screen screen = Screen.FromPoint(pos);
        Rectangle bounds;
        if (pos.X < screen.Bounds.Left)
        {
          ref Point local = ref pos;
          bounds = screen.Bounds;
          int left = bounds.Left;
          local.X = left;
        }
        int num1 = pos.X + size.Width;
        bounds = screen.Bounds;
        int right = bounds.Right;
        if (num1 > right)
        {
          ref Point local = ref pos;
          bounds = screen.Bounds;
          int num2 = bounds.Right - size.Width;
          local.X = num2;
          int x = pos.X;
          bounds = screen.Bounds;
          int left = bounds.Left;
          if (x < left)
            return;
        }
        int y1 = pos.Y;
        bounds = screen.Bounds;
        int top1 = bounds.Top;
        if (y1 < top1)
        {
          ref Point local = ref pos;
          bounds = screen.Bounds;
          int top2 = bounds.Top;
          local.Y = top2;
        }
        int num3 = pos.Y + size.Height;
        bounds = screen.Bounds;
        int bottom = bounds.Bottom;
        if (num3 > bottom)
        {
          ref Point local = ref pos;
          bounds = screen.Bounds;
          int num4 = bounds.Bottom - size.Height;
          local.Y = num4;
          int y2 = pos.Y;
          bounds = screen.Bounds;
          int top3 = bounds.Top;
          if (y2 < top3)
            return;
          ++pos.X;
        }
        ToolTips.SetWindowPos(this._tooltipForm.Handle, -1, pos.X, pos.Y, size.Width, size.Height, 80 /*0x50*/);
        VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
        if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(normal))
        {
          using (Graphics graphics = this._tooltipForm.CreateGraphics())
            this._tooltipForm.Region = new VisualStyleRenderer(normal).GetBackgroundRegion((IDeviceContext) graphics, this._tooltipForm.ClientRectangle);
        }
        this._tooltipForm.Invalidate();
        this._visible = true;
        if (this._parentForm != null)
          this._parentForm.Deactivate -= new EventHandler(this.ParentForm_Deactivate);
        this._parentForm = this.GetControlForm(this._control);
        if (this._parentForm == null)
          return;
        this._parentForm.Deactivate += new EventHandler(this.ParentForm_Deactivate);
        this._tooltipForm.Owner = this._parentForm;
      }

      public void Hide()
      {
        this._tooltipForm.Owner = (Form) null;
        this._tooltipForm.Visible = false;
        this._visible = false;
        if (this._parentForm == null)
          return;
        this._parentForm.Deactivate -= new EventHandler(this.ParentForm_Deactivate);
        this._parentForm = (Form) null;
      }

      private void Control_MouseMove(object sender, MouseEventArgs mea)
      {
        if (mea.Button != MouseButtons.None)
          return;
        if (this._visible)
        {
          string text = this.GetToolTipText(new Point(mea.X, mea.Y));
          if (text == null || text.Length == 0)
          {
            this.Hide();
          }
          else
          {
            if (text.Length == 0 || !(text != this._tooltipForm.Text))
              return;
            this.Show(Cursor.Position, text);
          }
        }
        else
        {
          Point point = new Point(mea.X, mea.Y);
          if (!(point != this._showPoint))
            return;
          this._showPoint = point;
          this._timer.Enabled = false;
          this._timer.Enabled = true;
        }
      }

      private void Control_MouseWheel(object sender, MouseEventArgs mea)
      {
        if (this._visible)
          this.Hide();
        this._timer.Enabled = false;
      }

      private Form GetControlForm(Control control)
      {
        while (control.Parent != null)
          control = control.Parent;
        return control as Form;
      }

      private void Control_MouseLeave(object sender, EventArgs e)
      {
        if (this._visible)
          this.Hide();
        this._timer.Enabled = false;
      }

      private void Control_Disposed(object sender, EventArgs e) => this.Dispose();

      private void Timer_Tick(object sender, EventArgs e)
      {
        this._timer.Enabled = false;
        Point client = this._control.PointToClient(Cursor.Position);
        if (!this._control.ClientRectangle.Contains(client))
          return;
        string text = this.GetToolTipText(client).Trim();
        switch (text)
        {
          case null:
            break;
          case "":
            break;
          default:
            Form controlForm = this.GetControlForm(this._control);
            Form activeForm = Form.ActiveForm;
            if (controlForm == null || activeForm == null || activeForm != controlForm && activeForm != controlForm.Owner || !this._control.Visible)
              break;
            this.Show(Cursor.Position, text);
            break;
        }
      }

      private static bool IsWinXP()
      {
        bool flag = false;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
          flag = Environment.OSVersion.Version >= new Version(5, 1, 0, 0);
        return flag;
      }

      public static void Paint(
        Graphics g,
        Rectangle rect,
        string text,
        Font font,
        TextFormatFlags tff)
      {
        VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
        if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(normal))
        {
          VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(normal);
          visualStyleRenderer.DrawBackground((IDeviceContext) g, rect);
          Rectangle textExtent = visualStyleRenderer.GetTextExtent((IDeviceContext) g, rect, text, tff);
          textExtent.X = rect.X + rect.Width / 2 - textExtent.Width / 2;
          textExtent.Y = rect.Y + rect.Height / 2 - textExtent.Height / 2;
          visualStyleRenderer.DrawText((IDeviceContext) g, textExtent, text, false, tff);
        }
        else
        {
          g.FillRectangle(SystemBrushes.Info, rect);
          Pen pen = SystemInformation.HighContrast ? SystemPens.InfoText : SystemPens.Control;
          g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
          g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
          g.DrawLine(SystemPens.InfoText, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
          g.DrawLine(SystemPens.InfoText, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom);
          rect.Inflate(-2, -2);
          TextRenderer.DrawText((IDeviceContext) g, text, font, rect, SystemColors.InfoText, tff);
        }
      }

      private void Control_FontChanged(object sender, EventArgs e)
      {
        this._tooltipForm.Font = this._control.Font;
      }

      private void ParentForm_Deactivate(object sender, EventArgs e) => this.Hide();

      public bool HidePrefix
      {
        get => this._tooltipForm.HidePrefix;
        set => this._tooltipForm.HidePrefix = value;
      }

      public bool DropShadow
      {
        get => this._dropShadow;
        set => this._dropShadow = value;
      }

      internal delegate string GetToolTipTextEventHandler(Point location);

      private class ToolTipsForm : Form
      {
        private const int x2b7f5d3ca7ec1edf = -2147483648 /*0x80000000*/;
        private const int x3e8b9d6faeff6586 = 32 /*0x20*/;
        private const int x836e53e090609b16 = 4132;
        private ToolTips _tooltips;
        private TextFormatFlags _textFormatFlags;
        private const int xd708511d2241a4fb = 131072 /*0x020000*/;

        public ToolTipsForm(ToolTips tooltips)
        {
          this._tooltips = tooltips;
          this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
          this.Font = tooltips._control.Font;
          this._textFormatFlags = TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter;
          this.ShowInTaskbar = false;
          this.FormBorderStyle = FormBorderStyle.None;
          this.ControlBox = false;
          this.StartPosition = FormStartPosition.Manual;
        }

        protected override void Dispose(bool disposing) => base.Dispose(disposing);

        protected override void OnPaint(PaintEventArgs e)
        {
          ToolTips.Paint(e.Graphics, this.ClientRectangle, this.Text, this.Font, this._textFormatFlags);
        }

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(
          int nAction,
          int nParam,
          ref int i,
          int nUpdate);

        public SizeF MeasureString(string text)
        {
          using (Graphics graphics = this.CreateGraphics())
          {
            VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(normal))
            {
              VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(normal);
              Rectangle textExtent = visualStyleRenderer.GetTextExtent((IDeviceContext) graphics, text, TextFormatFlags.Default);
              return (SizeF) visualStyleRenderer.GetBackgroundExtent((IDeviceContext) graphics, textExtent).Size;
            }
            SizeF sizeF = (SizeF) TextRenderer.MeasureText((IDeviceContext) graphics, text, this.Font, new Size(SystemInformation.PrimaryMonitorSize.Width, int.MaxValue), this._textFormatFlags);
            sizeF.Width -= 2f;
            sizeF.Height += 2f;
            return sizeF;
          }
        }

        protected override CreateParams CreateParams
        {
          get
          {
            CreateParams createParams = base.CreateParams;
            if (this._tooltips != null && this._tooltips.DropShadow && ToolTips.ToolTipsForm.CanDropShadow)
              createParams.ClassStyle |= 131072 /*0x020000*/;
            return createParams;
          }
        }

        private static bool CanDropShadow
        {
          get
          {
            int i = 0;
            if (!ToolTips.IsWinXP())
              return false;
            ToolTips.ToolTipsForm.SystemParametersInfo(4132, 0, ref i, 0);
            return Convert.ToBoolean(i);
          }
        }

        public bool HidePrefix
        {
          get => (this._textFormatFlags & TextFormatFlags.HidePrefix) != TextFormatFlags.HidePrefix;
          set
          {
            if (value)
            {
              this._textFormatFlags |= TextFormatFlags.HidePrefix;
              this._textFormatFlags &= ~TextFormatFlags.NoPrefix;
            }
            else
            {
              this._textFormatFlags &= ~TextFormatFlags.HidePrefix;
              this._textFormatFlags |= TextFormatFlags.NoPrefix;
            }
          }
        }
      }
    }
}
