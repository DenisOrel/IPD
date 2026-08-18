
// Type: Intermech.Docking.WhidbeyDocker
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using Intermech.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class WhidbeyDocker : StandardDocker
{
  private ControlLayoutSystem _activeControlLayoutSystem;
  private WhidbeyDocker.WhidbeyDockMarker _newSystemMarker;
  private bool _markAmimating;
  private Rectangle _bounds;
  private WhidbeyDocker.WhidbeyDockMarker _leftMarker;
  private WhidbeyDocker.WhidbeyDockMarker _rightMarker;
  private WhidbeyDocker.WhidbeyDockMarker _topMarker;
  private WhidbeyDocker.WhidbeyDockMarker _bottomMarker;

  public WhidbeyDocker(
    DockManager dockManager,
    DockContainer dockContainer,
    LayoutSystemBase A_2,
    DockControl dockControl,
    Point pos,
    DockingHints hints,
    bool canFloat)
    : base(dockManager, dockContainer, A_2, dockControl, pos, hints, canFloat)
  {
    this._activeControlLayoutSystem = (ControlLayoutSystem) null;
    this._newSystemMarker = (WhidbeyDocker.WhidbeyDockMarker) null;
    this._markAmimating = false;
    this._bounds = Rectangle.Empty;
    if (this.DockManager == null || this.DockManager.OwnerForm == null)
      return;
    this.CalcBounds();
    this.CreateMarkers();
  }

  private void DisposeMarker(ref WhidbeyDocker.WhidbeyDockMarker marker)
  {
    if (marker == null)
      return;
    marker.Visible = false;
    marker.Dispose();
    marker = (WhidbeyDocker.WhidbeyDockMarker) null;
  }

  private ControlLayoutSystem GetSystemAndSiteAt(Point pos, out StandardDocker.DockingSite site)
  {
    site = (StandardDocker.DockingSite) null;
    foreach (ControlLayoutSystem layoutSystem in this.GetLayoutSystems())
    {
      if (new Rectangle(layoutSystem.DockContainer.PointToScreen(layoutSystem.Bounds.Location), layoutSystem.Bounds.Size).Contains(pos))
      {
        site = this.GetDockingSite(layoutSystem.DockContainer, layoutSystem, pos, false);
        return site._redockType == StandardDocker.RedockType.Undefined ? layoutSystem : (ControlLayoutSystem) null;
      }
    }
    return (ControlLayoutSystem) null;
  }

  public static Rectangle GetDockingBounds(DockManager dockManager, Control control, bool A_1)
  {
    Rectangle dockingBounds = control.ClientRectangle;
    DockContainer dockContainer1 = dockManager.GetDockContainer(DockStyle.Left, control);
    Rectangle bounds;
    if (dockContainer1 != null)
    {
      ref Rectangle local = ref dockingBounds;
      bounds = dockContainer1.Bounds;
      int right1 = bounds.Right;
      int y = dockingBounds.Y;
      int width1 = dockingBounds.Width;
      bounds = dockContainer1.Bounds;
      int right2 = bounds.Right;
      int width2 = width1 - right2;
      int height = dockingBounds.Height;
      local = new Rectangle(right1, y, width2, height);
    }
    DockContainer dockContainer2 = dockManager.GetDockContainer(DockStyle.Right, control);
    if (dockContainer2 != null)
    {
      ref Rectangle local = ref dockingBounds;
      int width = local.Width;
      int right = dockingBounds.Right;
      bounds = dockContainer2.Bounds;
      int left = bounds.Left;
      int num = right - left;
      local.Width = width - num;
    }
    DockContainer dockContainer3 = dockManager.GetDockContainer(DockStyle.Top, control);
    if (dockContainer3 != null)
    {
      ref Rectangle local = ref dockingBounds;
      int x = dockingBounds.X;
      bounds = dockContainer3.Bounds;
      int bottom1 = bounds.Bottom;
      int width = dockingBounds.Width;
      int height1 = dockingBounds.Height;
      bounds = dockContainer3.Bounds;
      int bottom2 = bounds.Bottom;
      int height2 = height1 - bottom2;
      local = new Rectangle(x, bottom1, width, height2);
    }
    DockContainer dockContainer4 = dockManager.GetDockContainer(DockStyle.Bottom, control);
    if (dockContainer4 != null)
    {
      ref Rectangle local = ref dockingBounds;
      int height = local.Height;
      int bottom = dockingBounds.Bottom;
      bounds = dockContainer4.Bounds;
      int top = bounds.Top;
      int num = bottom - top;
      local.Height = height - num;
    }
    dockingBounds = new Rectangle(control.PointToScreen(dockingBounds.Location), dockingBounds.Size);
    if (dockingBounds.Width < 100 & A_1)
      dockingBounds.Inflate((100 - dockingBounds.Width) / 2, 0);
    if (dockingBounds.Height < 100 & A_1)
      dockingBounds.Inflate(0, (100 - dockingBounds.Height) / 2);
    return dockingBounds;
  }

  public override void Dispose()
  {
    this.DisposeMarker(ref this._newSystemMarker);
    this.DisposeMarker(ref this._leftMarker);
    this.DisposeMarker(ref this._topMarker);
    this.DisposeMarker(ref this._rightMarker);
    this.DisposeMarker(ref this._bottomMarker);
    base.Dispose();
  }

  protected override StandardDocker.DockingSite GetDockingSiteAt(Point pos)
  {
    StandardDocker.DockingSite site = (StandardDocker.DockingSite) null;
    if (!this._markAmimating)
    {
      ControlLayoutSystem controlLayoutSystem = this.GetSystemAndSiteAt(pos, out site);
      if (controlLayoutSystem == this.SourceLayoutSystem && this.SourceDockControl == null)
        controlLayoutSystem = (ControlLayoutSystem) null;
      if (controlLayoutSystem != this._activeControlLayoutSystem)
      {
        if (this._newSystemMarker != null)
        {
          this._newSystemMarker.HideMark();
          if (this._newSystemMarker != null)
          {
            this._newSystemMarker.Dispose();
            this._newSystemMarker = (WhidbeyDocker.WhidbeyDockMarker) null;
          }
        }
        this._activeControlLayoutSystem = controlLayoutSystem;
        if (this._activeControlLayoutSystem != null)
        {
          this._newSystemMarker = new WhidbeyDocker.WhidbeyDockMarker(this, this._activeControlLayoutSystem);
          this._newSystemMarker.ShowMark();
        }
      }
    }
    if (site != null && site._redockType == StandardDocker.RedockType.Undefined)
      site = (StandardDocker.DockingSite) null;
    Rectangle bounds;
    if (this._newSystemMarker != null)
    {
      bounds = this._newSystemMarker.GetBounds();
      if (bounds.Contains(pos) && site == null)
        site = this._newSystemMarker.GetDockingSiteAt(pos);
    }
    if (this._topMarker != null)
    {
      bounds = this._topMarker.GetBounds();
      if (bounds.Contains(pos) && this.CanDockTo(DockLocation.Top) && site == null)
        site = this._topMarker.GetDockingSiteAt(pos);
    }
    if (this._bottomMarker != null)
    {
      bounds = this._bottomMarker.GetBounds();
      if (bounds.Contains(pos) && this.CanDockTo(DockLocation.Bottom) && site == null)
        site = this._bottomMarker.GetDockingSiteAt(pos);
    }
    if (this._leftMarker != null)
    {
      bounds = this._leftMarker.GetBounds();
      if (bounds.Contains(pos) && this.CanDockTo(DockLocation.Left) && site == null)
        site = this._leftMarker.GetDockingSiteAt(pos);
    }
    if (this._rightMarker != null)
    {
      bounds = this._rightMarker.GetBounds();
      if (bounds.Contains(pos) && this.CanDockTo(DockLocation.Right) && site == null)
        site = this._rightMarker.GetDockingSiteAt(pos);
    }
    return site;
  }

  private void CalcBounds()
  {
    DockManager dockManager = this.DockManager;
    if (this.SourceDockContainer.IsFloating)
    {
      foreach (DockContainer dockContainer in dockManager._dockContainers)
      {
        if (!dockContainer.IsFloating)
        {
          this._bounds = WhidbeyDocker.GetDockingBounds(dockManager, dockContainer.Parent, true);
          break;
        }
      }
      this._bounds = WhidbeyDocker.GetDockingBounds(dockManager, (Control) dockManager.OwnerForm, true);
    }
    else
      this._bounds = WhidbeyDocker.GetDockingBounds(dockManager, this.SourceDockContainer.Parent, true);
  }

  private void CreateMarkers()
  {
    Control control = (Control) null;
    if (!this.SourceDockContainer.IsFloating)
      control = this.SourceDockContainer.Parent;
    DockContainer dockContainer1 = this.DockManager.GetDockContainer(DockStyle.Top, control);
    if (dockContainer1 != null && this.CanDockTo(DockLocation.Top))
      this._topMarker = new WhidbeyDocker.WhidbeyDockMarker(this, this._bounds, DockStyle.Top, dockContainer1);
    DockContainer dockContainer2 = this.DockManager.GetDockContainer(DockStyle.Left, control);
    if (dockContainer2 != null && this.CanDockTo(DockLocation.Left))
      this._leftMarker = new WhidbeyDocker.WhidbeyDockMarker(this, this._bounds, DockStyle.Left, dockContainer2);
    DockContainer dockContainer3 = this.DockManager.GetDockContainer(DockStyle.Bottom, control);
    if (dockContainer3 != null && this.CanDockTo(DockLocation.Bottom))
      this._bottomMarker = new WhidbeyDocker.WhidbeyDockMarker(this, this._bounds, DockStyle.Bottom, dockContainer3);
    DockContainer dockContainer4 = this.DockManager.GetDockContainer(DockStyle.Right, control);
    if (dockContainer4 != null && this.CanDockTo(DockLocation.Right))
      this._rightMarker = new WhidbeyDocker.WhidbeyDockMarker(this, this._bounds, DockStyle.Right, dockContainer4);
    if (this._topMarker != null)
      this._topMarker.ShowTopmost();
    if (this._leftMarker != null)
      this._leftMarker.ShowTopmost();
    if (this._bottomMarker != null)
      this._bottomMarker.ShowTopmost();
    if (this._rightMarker == null)
      return;
    this._rightMarker.ShowTopmost();
  }

  private class WhidbeyDockMarker : WhidbeyDockerForm
  {
    private WhidbeyDocker _docker;
    private ControlLayoutSystem _layoutSystem;
    private Rectangle _bounds;
    private DockSide _side;
    private bool _highlight;
    private Bitmap _bitmap;
    private DockContainer _dockContainer;
    private DockStyle _dockStyle;

    private WhidbeyDockMarker()
    {
      this._docker = (WhidbeyDocker) null;
      this._layoutSystem = (ControlLayoutSystem) null;
      this._bounds = Rectangle.Empty;
      this._side = DockSide.None;
      this._highlight = false;
      this._bitmap = (Bitmap) null;
      this._dockContainer = (DockContainer) null;
      this._dockStyle = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.None;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.BackColor = Color.FromArgb(245, 238, 238);
      this._bitmap = new Bitmap(93, 93, PixelFormat.Format32bppArgb);
    }

    public WhidbeyDockMarker(WhidbeyDocker docker, ControlLayoutSystem layoutSystem)
      : this()
    {
      this._docker = docker;
      this._layoutSystem = layoutSystem;
      this._dockContainer = layoutSystem.DockContainer;
      this._bounds = new Rectangle(layoutSystem.DockContainer.PointToScreen(layoutSystem.Bounds.Location), layoutSystem.Bounds.Size);
      this._bounds = new Rectangle(this._bounds.X + this._bounds.Width / 2 - 46, this._bounds.Y + this._bounds.Height / 2 - 46, 93, 93);
      this.ConstructMarkerImage();
    }

    public WhidbeyDockMarker(
      WhidbeyDocker docker,
      Rectangle bounds,
      DockStyle dockStyle,
      DockContainer dockContainer)
      : this()
    {
      this._docker = docker;
      this._dockStyle = dockStyle;
      this._dockContainer = dockContainer;
      switch (dockStyle)
      {
        case DockStyle.Top:
          this._bounds = new Rectangle(bounds.X + bounds.Width / 2 - 46, bounds.Y + 15, 93, 93);
          break;
        case DockStyle.Bottom:
          this._bounds = new Rectangle(bounds.X + bounds.Width / 2 - 46, bounds.Bottom - 93 - 15, 93, 93);
          break;
        case DockStyle.Left:
          this._bounds = new Rectangle(bounds.X + 15, bounds.Y + bounds.Height / 2 - 46, 93, 93);
          break;
        case DockStyle.Right:
          this._bounds = new Rectangle(bounds.Right - 93 - 15, bounds.Y + bounds.Height / 2 - 46, 93, 93);
          break;
      }
      this.ConstructMarkerImage();
    }

    private GraphicsPath GetLeftPath()
    {
      GraphicsPath leftPath = new GraphicsPath();
      Point[] points = new Point[10]
      {
        new Point(29, 57),
        new Point(19, 67),
        new Point(0, 48 /*0x30*/),
        new Point(0, 44),
        new Point(19, 25),
        new Point(29, 35),
        new Point(28, 36),
        new Point(25, 42),
        new Point(25, 50),
        new Point(28, 56)
      };
      leftPath.AddPolygon(points);
      return leftPath;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._bitmap.Dispose();
      base.Dispose(disposing);
    }

    public StandardDocker.DockingSite GetDockingSiteAt(Point pos)
    {
      Point client = this.PointToClient(pos);
      StandardDocker.DockingSite dockingSiteAt = new StandardDocker.DockingSite(StandardDocker.RedockType.SplitExistingSystem);
      dockingSiteAt._layoutSystem = this._layoutSystem;
      dockingSiteAt._dockContainer = this._dockContainer;
      if (this.IsPointInPath(this.GetTopPath(), client) && (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Top))
        dockingSiteAt._dockSide = DockSide.Top;
      else if (this.IsPointInPath(this.GetRightPath(), client) && (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Right))
        dockingSiteAt._dockSide = DockSide.Right;
      else if (this.IsPointInPath(this.GetBottomPath(), client) && (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Bottom))
        dockingSiteAt._dockSide = DockSide.Bottom;
      else if (this.IsPointInPath(this.GetLeftPath(), client) && (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Left))
        dockingSiteAt._dockSide = DockSide.Left;
      else if (this.IsPointInPath(this.GetCenterPath(), client) && this._dockStyle == DockStyle.None)
      {
        dockingSiteAt._redockType = StandardDocker.RedockType.JoinExistingSystem;
        dockingSiteAt._dockSide = DockSide.None;
      }
      else
        dockingSiteAt._redockType = StandardDocker.RedockType.Undefined;
      if (this._dockStyle != DockStyle.None)
      {
        if (dockingSiteAt._redockType != StandardDocker.RedockType.Undefined)
        {
          dockingSiteAt._redockType = StandardDocker.RedockType.CreateNewContainer;
          dockingSiteAt._bounds = this._docker.a(this._dockContainer, this._docker.SourceLayoutSystem);
        }
      }
      else
        dockingSiteAt._bounds = this._docker.GetDockingBounds(this._layoutSystem.DockContainer, this._layoutSystem, dockingSiteAt._dockSide);
      bool flag = dockingSiteAt._redockType != 0;
      DockSide dockSide = dockingSiteAt._redockType == StandardDocker.RedockType.Undefined ? this._side : dockingSiteAt._dockSide;
      if (flag != this._highlight || dockSide != this._side)
      {
        this._highlight = flag;
        this._side = dockSide;
        this.ConstructMarkerImage();
      }
      return dockingSiteAt;
    }

    private void AnimateMark(double A_0, double A_1)
    {
      this._docker._markAmimating = true;
      int tickCount = Environment.TickCount;
      while (true)
      {
        int num1 = Environment.TickCount - tickCount;
        if (num1 <= 200)
        {
          double num2 = (double) num1 / 200.0;
          double alpha = A_0 + (A_1 - A_0) * num2;
          if (!this.IsDisposed)
          {
            this.Update(this._bitmap, (byte) alpha);
            Application.DoEvents();
          }
          else
            goto label_4;
        }
        else
          break;
      }
      this._docker._markAmimating = false;
      if (this.IsDisposed)
        return;
      this.Update(this._bitmap, (byte) A_1);
      return;
label_4:;
    }

    private bool IsPointInPath(GraphicsPath path, Point pos)
    {
      Region region = new Region(new Rectangle(0, 0, 93, 93));
      region.Exclude(new Rectangle(0, 0, 93, 93));
      region.Union(path);
      return region.IsVisible(pos);
    }

    private void ConstructMarkerImage()
    {
      using (Graphics A_0 = Graphics.FromImage((Image) this._bitmap))
      {
        A_0.Clear(Color.Transparent);
        Color cornflowerBlue = Color.CornflowerBlue;
        Color color = RendererBase.InterpolateColors(cornflowerBlue, Color.Transparent, 0.2f);
        if (this._layoutSystem != null)
        {
          Point[] points = new Point[4]
          {
            new Point(46, 9),
            new Point(83, 46),
            new Point(46, 83),
            new Point(9, 46)
          };
          using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            A_0.FillPolygon((Brush) solidBrush, points);
          A_0.DrawPolygon(SystemPens.ControlDark, points);
        }
        if (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Top)
          this.ConstructTopMarker(A_0, !this._highlight || this._side != DockSide.Top ? color : cornflowerBlue);
        if (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Right)
          this.ConstructRightMarker(A_0, !this._highlight || this._side != DockSide.Right ? color : cornflowerBlue);
        if (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Bottom)
          this.ConstructBottomMarker(A_0, !this._highlight || this._side != DockSide.Bottom ? color : cornflowerBlue);
        if (this._dockStyle == DockStyle.None || this._dockStyle == DockStyle.Left)
          this.ConstructLeftMarker(A_0, !this._highlight || this._side != DockSide.Left ? color : cornflowerBlue);
        if (this._dockStyle == DockStyle.None)
          this.ConstructCenterMarker(A_0, !this._highlight || this._side != DockSide.None ? color : cornflowerBlue);
      }
      this.Update(this._bitmap, byte.MaxValue);
    }

    private void ConstructTopMarker(Graphics A_0, Color A_1)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.GetTopPath()))
      {
        pathGradientBrush.CenterColor = A_1;
        Color color1 = RendererBase.InterpolateColors(A_1, Color.White, 0.2f);
        Color color2 = RendererBase.InterpolateColors(A_1, Color.Black, 0.2f);
        pathGradientBrush.SurroundColors = new Color[5]
        {
          color1,
          color1,
          A_1,
          color2,
          color2
        };
        A_0.FillPath((Brush) pathGradientBrush, this.GetTopPath());
      }
      using (Pen pen = new Pen(Color.FromArgb(225, SystemColors.ControlLightLight)))
      {
        A_0.DrawRectangle(pen, 39, 9, 14, 12);
        A_0.DrawLine(pen, 39, 13, 53, 13);
        A_0.DrawLine(pen, 46, 15, 46, 19);
        A_0.DrawLine(pen, 44, 17, 48 /*0x30*/, 17);
        A_0.DrawLine(pen, 45, 16 /*0x10*/, 47, 16 /*0x10*/);
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(130, SystemColors.ControlLightLight)))
        A_0.FillRectangle((Brush) solidBrush, 40, 10, 13, 3);
    }

    private void ConstructRightMarker(Graphics A_0, Color A_1)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.GetRightPath()))
      {
        pathGradientBrush.CenterColor = A_1;
        Color color1 = RendererBase.InterpolateColors(A_1, Color.White, 0.2f);
        Color color2 = RendererBase.InterpolateColors(A_1, Color.Black, 0.2f);
        pathGradientBrush.SurroundColors = new Color[9]
        {
          color1,
          A_1,
          color2,
          color2,
          color1,
          color1,
          color1,
          color1,
          color1
        };
        A_0.FillPath((Brush) pathGradientBrush, this.GetRightPath());
      }
      using (Pen pen = new Pen(Color.FromArgb(225, SystemColors.ControlLightLight)))
      {
        A_0.DrawRectangle(pen, 71, 39, 12, 14);
        A_0.DrawLine(pen, 79, 39, 79, 53);
        A_0.DrawLine(pen, 73, 46, 77, 46);
        A_0.DrawLine(pen, 75, 44, 75, 48 /*0x30*/);
        A_0.DrawLine(pen, 76, 45, 76, 47);
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(130, SystemColors.ControlLightLight)))
        A_0.FillRectangle((Brush) solidBrush, 80 /*0x50*/, 40, 3, 13);
    }

    private void ConstructBottomMarker(Graphics A_0, Color A_1)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.GetBottomPath()))
      {
        pathGradientBrush.CenterColor = A_1;
        Color color1 = RendererBase.InterpolateColors(A_1, Color.White, 0.2f);
        Color color2 = RendererBase.InterpolateColors(A_1, Color.Black, 0.2f);
        pathGradientBrush.SurroundColors = new Color[5]
        {
          color2,
          color2,
          A_1,
          color1,
          color1
        };
        A_0.FillPath((Brush) pathGradientBrush, this.GetBottomPath());
      }
      using (Pen pen = new Pen(Color.FromArgb(225, SystemColors.ControlLightLight)))
      {
        A_0.DrawRectangle(pen, 39, 71, 14, 12);
        A_0.DrawLine(pen, 39, 79, 53, 79);
        A_0.DrawLine(pen, 46, 73, 46, 77);
        A_0.DrawLine(pen, 44, 75, 48 /*0x30*/, 75);
        A_0.DrawLine(pen, 45, 76, 47, 76);
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(130, SystemColors.ControlLightLight)))
        A_0.FillRectangle((Brush) solidBrush, 40, 80 /*0x50*/, 13, 3);
    }

    private void ConstructLeftMarker(Graphics A_0, Color A_1)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.GetLeftPath()))
      {
        pathGradientBrush.CenterColor = A_1;
        Color color1 = RendererBase.InterpolateColors(A_1, Color.White, 0.2f);
        Color color2 = RendererBase.InterpolateColors(A_1, Color.Black, 0.2f);
        pathGradientBrush.SurroundColors = new Color[5]
        {
          color2,
          color2,
          A_1,
          color1,
          color1
        };
        A_0.FillPath((Brush) pathGradientBrush, this.GetLeftPath());
      }
      using (Pen pen = new Pen(Color.FromArgb(225, SystemColors.ControlLightLight)))
      {
        A_0.DrawRectangle(pen, 9, 39, 12, 14);
        A_0.DrawLine(pen, 13, 39, 13, 53);
        A_0.DrawLine(pen, 15, 46, 19, 46);
        A_0.DrawLine(pen, 16 /*0x10*/, 45, 16 /*0x10*/, 47);
        A_0.DrawLine(pen, 17, 44, 17, 48 /*0x30*/);
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(130, SystemColors.ControlLightLight)))
        A_0.FillRectangle((Brush) solidBrush, 10, 40, 3, 13);
    }

    private void ConstructCenterMarker(Graphics A_0, Color A_1)
    {
      Color color = RendererBase.InterpolateColors(A_1, Color.White, 0.2f);
      RendererBase.InterpolateColors(A_1, Color.Black, 0.2f);
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.GetCenterPath()))
      {
        pathGradientBrush.CenterColor = A_1;
        pathGradientBrush.SurroundColors = new Color[1]
        {
          color
        };
        A_0.FillPath((Brush) pathGradientBrush, this.GetCenterPath());
      }
      SmoothingMode smoothingMode = A_0.SmoothingMode;
      A_0.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(RendererBase.InterpolateColors(A_1, Color.Black, 0.1f)))
        A_0.DrawPath(pen, this.GetCenterPath());
      A_0.SmoothingMode = smoothingMode;
      using (Pen pen = new Pen(Color.FromArgb(225, SystemColors.ControlLightLight)))
      {
        A_0.DrawLine(pen, 39, 53, 39, 39);
        A_0.DrawLine(pen, 39, 39, 54, 39);
        A_0.DrawLine(pen, 54, 39, 54, 53);
        A_0.DrawLine(pen, 40, 54, 46, 54);
        A_0.DrawLine(pen, 48 /*0x30*/, 54, 53, 54);
        A_0.DrawLine(pen, 47, 53, 47, 51);
        A_0.DrawLine(pen, 47, 51, 54, 51);
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(130, SystemColors.ControlLightLight)))
      {
        A_0.FillRectangle((Brush) solidBrush, 40, 40, 14, 11);
        A_0.FillRectangle((Brush) solidBrush, 40, 51, 7, 3);
      }
    }

    private GraphicsPath GetBottomPath()
    {
      GraphicsPath bottomPath = new GraphicsPath();
      Point[] points = new Point[10]
      {
        new Point(67, 73),
        new Point(48 /*0x30*/, 92),
        new Point(44, 92),
        new Point(25, 73),
        new Point(35, 63 /*0x3F*/),
        new Point(36, 64 /*0x40*/),
        new Point(42, 67),
        new Point(50, 67),
        new Point(56, 64 /*0x40*/),
        new Point(57, 63 /*0x3F*/)
      };
      bottomPath.AddPolygon(points);
      return bottomPath;
    }

    private GraphicsPath GetRightPath()
    {
      GraphicsPath rightPath = new GraphicsPath();
      Point[] points = new Point[10]
      {
        new Point(73, 25),
        new Point(92, 44),
        new Point(92, 48 /*0x30*/),
        new Point(73, 67),
        new Point(63 /*0x3F*/, 57),
        new Point(64 /*0x40*/, 56),
        new Point(67, 50),
        new Point(67, 42),
        new Point(64 /*0x40*/, 36),
        new Point(63 /*0x3F*/, 35)
      };
      rightPath.AddPolygon(points);
      return rightPath;
    }

    private GraphicsPath GetTopPath()
    {
      GraphicsPath topPath = new GraphicsPath();
      Point[] points = new Point[10]
      {
        new Point(25, 19),
        new Point(44, 0),
        new Point(48 /*0x30*/, 0),
        new Point(67, 19),
        new Point(57, 29),
        new Point(56, 28),
        new Point(50, 25),
        new Point(42, 25),
        new Point(36, 28),
        new Point(35, 29)
      };
      topPath.AddPolygon(points);
      return topPath;
    }

    private GraphicsPath GetCenterPath()
    {
      GraphicsPath centerPath = new GraphicsPath();
      centerPath.AddEllipse(29, 29, 33, 33);
      return centerPath;
    }

    public void HideMark()
    {
      this.AnimateMark((double) byte.MaxValue, 0.0);
      this.Hide();
    }

    public void ShowMark()
    {
      this.Update(this._bitmap, (byte) 0);
      this.ShowTopmost();
      this.AnimateMark(0.0, (double) byte.MaxValue);
    }

    public void ShowTopmost()
    {
      Win32.SetWindowPos(this.Handle, -1, this._bounds.X, this._bounds.Y, this._bounds.Width, this._bounds.Height, 80 /*0x50*/);
    }

    public Rectangle GetBounds() => this._bounds;
  }
}
