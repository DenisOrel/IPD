
// Type: Intermech.Controls.PopupDropDown
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;


namespace Intermech.Controls;

public class PopupDropDown : ToolStripDropDown
{
  protected const int WM_GETMINMAXINFO = 36;
  protected const int WM_NCHITTEST = 132;
  protected const int HTTRANSPARENT = -1;
  protected const int HTLEFT = 10;
  protected const int HTRIGHT = 11;
  protected const int HTTOP = 12;
  protected const int HTTOPLEFT = 13;
  protected const int HTTOPRIGHT = 14;
  protected const int HTBOTTOM = 15;
  protected const int HTBOTTOMLEFT = 16 /*0x10*/;
  protected const int HTBOTTOMRIGHT = 17;
  private PopupResizeMode _resizeMode;
  private bool _lockedHostedControlSize;
  private bool _lockedThisSize;
  private bool _refreshSize;

  public PopupDropDown(bool autoSize)
  {
    this.AutoSize = autoSize;
    this.Padding = this.Margin = Padding.Empty;
  }

  protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
  {
    Control hostedControl = this.GetHostedControl();
    if (hostedControl != null)
      hostedControl.SizeChanged -= new EventHandler(this.hostedControl_SizeChanged);
    base.OnClosing(e);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this.GripBounds = Rectangle.Empty;
    if (this.CompareResizeMode(PopupResizeMode.BottomLeft))
    {
      e.Graphics.FillRectangle(SystemBrushes.ButtonFace, 1, this.Height - 16 /*0x10*/, this.Width - 2, 14);
      this.GripBounds = new Rectangle(1, this.Height - 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/);
      GripRenderer.Render(e.Graphics, this.GripBounds.Location, GripAlignMode.BottomLeft);
    }
    else if (this.CompareResizeMode(PopupResizeMode.BottomRight))
    {
      e.Graphics.FillRectangle(SystemBrushes.ButtonFace, 1, this.Height - 16 /*0x10*/, this.Width - 2, 14);
      this.GripBounds = new Rectangle(this.Width - 17, this.Height - 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/);
      GripRenderer.Render(e.Graphics, this.GripBounds.Location, GripAlignMode.BottomRight);
    }
    else if (this.CompareResizeMode(PopupResizeMode.TopLeft))
    {
      e.Graphics.FillRectangle(SystemBrushes.ButtonFace, 1, 1, this.Width - 2, 14);
      this.GripBounds = new Rectangle(1, 0, 16 /*0x10*/, 16 /*0x10*/);
      GripRenderer.Render(e.Graphics, this.GripBounds.Location, GripAlignMode.TopLeft);
    }
    else
    {
      if (!this.CompareResizeMode(PopupResizeMode.TopRight))
        return;
      e.Graphics.FillRectangle(SystemBrushes.ButtonFace, 1, 1, this.Width - 2, 14);
      this.GripBounds = new Rectangle(this.Width - 17, 0, 16 /*0x10*/, 16 /*0x10*/);
      GripRenderer.Render(e.Graphics, this.GripBounds.Location, GripAlignMode.TopRight);
    }
  }

  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    if (this._lockedThisSize)
      return;
    this.RecalculateHostedControlLayout();
  }

  protected void hostedControl_SizeChanged([NotNull] object sender, [NotNull] EventArgs e)
  {
    if (this._lockedHostedControlSize)
      return;
    this.ResizeFromContent(-1);
  }

  public new void Show(int x, int y) => this.Show(x, y, -1, -1);

  public void Show(int x, int y, int width, int height)
  {
    Control hostedControl = this.GetHostedControl();
    if (hostedControl == null)
      return;
    this._lockedHostedControlSize = true;
    this._lockedThisSize = true;
    this.Size = new Size(1, 1);
    base.Show(x, y);
    this._lockedHostedControlSize = false;
    this._lockedThisSize = false;
    this.ResizeFromContent(width);
    if (this._refreshSize)
      this.RecalculateHostedControlLayout();
    if (y > this.Top && y <= this.Bottom)
    {
      this.Top = y - this.Height - (height != -1 ? height : 0);
      PopupResizeMode resizeMode = this.ResizeMode;
      if (this.ResizeMode == PopupResizeMode.BottomLeft)
        this.ResizeMode = PopupResizeMode.TopLeft;
      else if (this.ResizeMode == PopupResizeMode.BottomRight)
        this.ResizeMode = PopupResizeMode.TopRight;
      if (this.ResizeMode != resizeMode)
        this.RecalculateHostedControlLayout();
    }
    hostedControl.SizeChanged += new EventHandler(this.hostedControl_SizeChanged);
  }

  protected void ResizeFromContent(int width)
  {
    if (this._lockedThisSize)
      return;
    this._lockedHostedControlSize = true;
    Rectangle bounds = this.Bounds with
    {
      Size = this.SizeFromContent(width)
    };
    if (!this.CompareResizeMode(PopupResizeMode.None) && width > 0 && bounds.Width - 2 > width && !this.CompareResizeMode(PopupResizeMode.Right))
      bounds.X -= bounds.Width - 2 - width;
    this.Bounds = bounds;
    this._lockedHostedControlSize = false;
  }

  protected void RecalculateHostedControlLayout()
  {
    if (this._lockedHostedControlSize)
      return;
    this._lockedThisSize = true;
    Control hostedControl = this.GetHostedControl();
    if (hostedControl != null)
    {
      Rectangle bounds = hostedControl.Bounds with
      {
        Location = this.CompareResizeMode(PopupResizeMode.TopLeft) || this.CompareResizeMode(PopupResizeMode.TopRight) ? new Point(1, 16 /*0x10*/) : new Point(1, 1),
        Width = this.ClientRectangle.Width - 2,
        Height = this.ClientRectangle.Height - 2
      };
      if (this.IsGripShown)
        bounds.Height -= 16 /*0x10*/;
      if (bounds.Size != hostedControl.Size)
        hostedControl.Size = bounds.Size;
      if (bounds.Location != hostedControl.Location)
        hostedControl.Location = bounds.Location;
    }
    this._lockedThisSize = false;
  }

  [CanBeNull]
  public Control GetHostedControl()
  {
    return this.Items.Count > 0 && this.Items[0] is ToolStripControlHost stripControlHost ? stripControlHost.Control : (Control) null;
  }

  public bool CompareResizeMode(PopupResizeMode resizeMode) => this.ResizeMode == resizeMode;

  protected Size SizeFromContent(int width)
  {
    Size size = Size.Empty;
    this._refreshSize = false;
    Control hostedControl = this.GetHostedControl();
    if (hostedControl != null)
    {
      hostedControl.Location = this.CompareResizeMode(PopupResizeMode.TopLeft) || this.CompareResizeMode(PopupResizeMode.TopRight) ? new Point(1, 16 /*0x10*/) : new Point(1, 1);
      size = this.SizeFromClientSize(hostedControl.Size);
      if (width > 0 && size.Width < width)
      {
        size.Width = width;
        this._refreshSize = true;
      }
    }
    if (this.IsGripShown)
      size.Height += 16 /*0x10*/;
    size.Width += 2;
    size.Height += 2;
    return size;
  }

  protected static int HIWORD(int n) => n >> 16 /*0x10*/ & (int) ushort.MaxValue;

  protected static int HIWORD(IntPtr n) => PopupDropDown.HIWORD((int) (long) n);

  protected static int LOWORD(int n) => n & (int) ushort.MaxValue;

  protected static int LOWORD(IntPtr n) => PopupDropDown.LOWORD((int) (long) n);

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  protected override void WndProc(ref Message m)
  {
    if (this.ProcessGrip(ref m, false))
      return;
    base.WndProc(ref m);
  }

  /// <summary>Processes the resizing messages.</summary>
  /// <param name="m">The message.</param>
  /// <returns>true, if the WndProc method from the base class shouldn't be invoked.</returns>
  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  public bool ProcessGrip(ref Message m) => this.ProcessGrip(ref m, true);

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  private bool ProcessGrip(ref Message m, bool contentControl)
  {
    if (this.ResizeMode != PopupResizeMode.None)
    {
      switch (m.Msg)
      {
        case 36:
          return this.OnGetMinMaxInfo(ref m);
        case 132:
          return this.OnNcHitTest(ref m, contentControl);
      }
    }
    return false;
  }

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  private bool OnGetMinMaxInfo(ref Message m)
  {
    Control hostedControl = this.GetHostedControl();
    if (hostedControl != null)
    {
      PopupDropDown.MINMAXINFO structure = (PopupDropDown.MINMAXINFO) Marshal.PtrToStructure(m.LParam, typeof (PopupDropDown.MINMAXINFO));
      Size size;
      if (hostedControl.MaximumSize.Width != 0)
      {
        ref Size local = ref structure.maxTrackSize;
        size = hostedControl.MaximumSize;
        int width = size.Width;
        local.Width = width;
      }
      size = hostedControl.MaximumSize;
      if (size.Height != 0)
      {
        ref Size local = ref structure.maxTrackSize;
        size = hostedControl.MaximumSize;
        int height = size.Height;
        local.Height = height;
      }
      structure.minTrackSize = new Size(32 /*0x20*/, 32 /*0x20*/);
      size = hostedControl.MinimumSize;
      if (size.Width > structure.minTrackSize.Width)
      {
        ref Size local = ref structure.minTrackSize;
        size = hostedControl.MinimumSize;
        int width = size.Width;
        local.Width = width;
      }
      size = hostedControl.MinimumSize;
      if (size.Height > structure.minTrackSize.Height)
      {
        ref Size local = ref structure.minTrackSize;
        size = hostedControl.MinimumSize;
        int height = size.Height;
        local.Height = height;
      }
      Marshal.StructureToPtr<PopupDropDown.MINMAXINFO>(structure, m.LParam, false);
    }
    return true;
  }

  private bool OnNcHitTest(ref Message m, bool contentControl)
  {
    Point client = this.PointToClient(new Point(PopupDropDown.LOWORD(m.LParam), PopupDropDown.HIWORD(m.LParam)));
    IntPtr num = new IntPtr(-1);
    if (this.GripBounds.Contains(client))
    {
      if (this.CompareResizeMode(PopupResizeMode.BottomLeft))
      {
        m.Result = contentControl ? num : (IntPtr) 16 /*0x10*/;
        return true;
      }
      if (this.CompareResizeMode(PopupResizeMode.BottomRight))
      {
        m.Result = contentControl ? num : (IntPtr) 17;
        return true;
      }
      if (this.CompareResizeMode(PopupResizeMode.TopLeft))
      {
        m.Result = contentControl ? num : (IntPtr) 13;
        return true;
      }
      if (this.CompareResizeMode(PopupResizeMode.TopRight))
      {
        m.Result = contentControl ? num : (IntPtr) 14;
        return true;
      }
    }
    else
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (client.X > clientRectangle.Right - 3 && client.X <= clientRectangle.Right && this.CompareResizeMode(PopupResizeMode.Right))
      {
        m.Result = contentControl ? num : (IntPtr) 11;
        return true;
      }
      if (client.Y > clientRectangle.Bottom - 3 && client.Y <= clientRectangle.Bottom && this.CompareResizeMode(PopupResizeMode.Bottom))
      {
        m.Result = contentControl ? num : (IntPtr) 15;
        return true;
      }
      if (client.X > -1 && client.X < 3 && this.CompareResizeMode(PopupResizeMode.Left))
      {
        m.Result = contentControl ? num : (IntPtr) 10;
        return true;
      }
      if (client.Y > -1 && client.Y < 3 && this.CompareResizeMode(PopupResizeMode.Top))
      {
        m.Result = contentControl ? num : (IntPtr) 12;
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Type of resize mode, grips are automatically drawn at bottom-left and bottom-right corners.
  /// </summary>
  public PopupResizeMode ResizeMode
  {
    get => this._resizeMode;
    set
    {
      if (value == this._resizeMode)
        return;
      this._resizeMode = value;
      this.Invalidate();
    }
  }

  /// <summary>Bounds of active grip box position.</summary>
  protected Rectangle GripBounds { get; set; } = Rectangle.Empty;

  /// <summary>Indicates when a grip box is shown.</summary>
  protected bool IsGripShown
  {
    get
    {
      return this.ResizeMode == PopupResizeMode.TopLeft || this.ResizeMode == PopupResizeMode.TopRight || this.ResizeMode == PopupResizeMode.BottomLeft || this.ResizeMode == PopupResizeMode.BottomRight;
    }
  }

  internal struct MINMAXINFO
  {
    public Point reserved;
    public Size maxSize;
    public Point maxPosition;
    public Size minTrackSize;
    public Size maxTrackSize;
  }
}
