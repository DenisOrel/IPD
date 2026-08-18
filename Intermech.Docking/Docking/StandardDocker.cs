
// Type: Intermech.Docking.StandardDocker
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class StandardDocker : BaseDocker
{
  private const int _a = 30;
  private DockManager _dockManager;
  private DockContainer _sourceDockContainer;
  private LayoutSystemBase _sourceLayoutSystem;
  private DockControl _sourceDockControl;
  private Size _f;
  private bool _canFloat;
  private Point _h;
  private StandardDocker.DockingSite _dockingSite;
  private Cursor _splittingCursor;
  private Cursor _splittingCursorNo;
  private int _l;
  private ControlLayoutSystem[] _m;

  public StandardDocker(
    DockManager dockManager,
    DockContainer dockContainer,
    LayoutSystemBase layoutSystem,
    DockControl dockControl,
    Point A_4,
    DockingHints dockingHints,
    bool canFloat)
    : base((Control) dockContainer, dockingHints, true, dockContainer.WorkingRenderer.TabStripMetrics.Height)
  {
    this._f = Size.Empty;
    this._h = Point.Empty;
    this._dockingSite = (StandardDocker.DockingSite) null;
    this._l = 0;
    this._m = (ControlLayoutSystem[]) null;
    this._dockManager = dockManager;
    this._sourceDockContainer = dockContainer;
    this._sourceLayoutSystem = layoutSystem;
    this._sourceDockControl = dockControl;
    this._canFloat = canFloat;
    if (dockContainer is DocumentContainer)
    {
      this._canFloat = false;
      using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Resources.splitting.cur"))
        this._splittingCursor = new Cursor(manifestResourceStream);
      using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Resources.splittingno.cur"))
        this._splittingCursorNo = new Cursor(manifestResourceStream);
    }
    Rectangle bounds1;
    if (layoutSystem is SplitLayoutSystem)
      this._f = ((FloatingDockContainer) dockContainer).GetSize();
    else if (dockControl != null)
      this._f = dockControl.FloatingSize;
    else if (layoutSystem is ControlLayoutSystem && ((ControlLayoutSystem) layoutSystem).SelectedControl != null)
    {
      this._f = ((ControlLayoutSystem) layoutSystem).SelectedControl.FloatingSize;
    }
    else
    {
      bounds1 = layoutSystem.Bounds;
      this._f = bounds1.Size;
    }
    Rectangle bounds2 = layoutSystem.Bounds;
    int num = bounds2.Width;
    if (num < 1)
      num = 1;
    A_4.X -= bounds2.Left;
    A_4.X = Convert.ToInt32((float) A_4.X / (float) num * (float) this._f.Width);
    this._h = dockControl == null ? new Point(A_4.X, A_4.Y - bounds2.Top) : new Point(A_4.X, this._f.Height - (bounds2.Bottom - A_4.Y));
    if (this._h.Y < 0)
      this._h.Y = 0;
    if (this._h.Y > this._f.Height)
      this._h.Y = this._f.Height;
    this._m = this.FillSystemsArray();
    foreach (Screen allScreen in Screen.AllScreens)
    {
      bounds1 = allScreen.Bounds;
      if (bounds1.Y < this._l)
      {
        bounds1 = allScreen.Bounds;
        this._l = bounds1.Y;
      }
    }
    this._sourceDockContainer.OnDockingStarted(EventArgs.Empty);
  }

  public override void Update(Point pos)
  {
    StandardDocker.DockingSite dockingSite = (StandardDocker.DockingSite) null;
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
      dockingSite = this.GetDockingSiteAt(pos);
    if (dockingSite == null || dockingSite._redockType == StandardDocker.RedockType.Undefined && this._dockManager != null && this._canFloat)
      dockingSite = this._dockManager == null || !this._canFloat ? new StandardDocker.DockingSite(StandardDocker.RedockType.None) : new StandardDocker.DockingSite(StandardDocker.RedockType.Float);
    if (dockingSite._redockType == StandardDocker.RedockType.Undefined)
      dockingSite._redockType = StandardDocker.RedockType.None;
    this.CheckSite(dockingSite);
    if (dockingSite._redockType == StandardDocker.RedockType.Float)
    {
      dockingSite._bounds = new Rectangle(this.e(), this._f);
      if (dockingSite._bounds.Y < this._l)
        dockingSite._bounds.Y = this._l;
    }
    if (dockingSite._layoutSystem == this._sourceLayoutSystem && this._sourceDockControl != null && dockingSite._dockSide == DockSide.None)
    {
      this.Hide();
      ControlLayoutSystem sourceLayoutSystem = (ControlLayoutSystem) this._sourceLayoutSystem;
      if (dockingSite._childIndex != sourceLayoutSystem.Controls.IndexOf(this._sourceDockControl) && dockingSite._childIndex != sourceLayoutSystem.Controls.IndexOf(this._sourceDockControl) + 1)
        sourceLayoutSystem.Controls.SetChildIndex(this._sourceDockControl, dockingSite._childIndex);
      dockingSite._redockType = StandardDocker.RedockType.AlreadyActioned;
    }
    else if (dockingSite._redockType == StandardDocker.RedockType.None)
      this.Hide();
    else
      this.Redraw(dockingSite._bounds, dockingSite._redockType == StandardDocker.RedockType.JoinExistingSystem);
    if (this._sourceDockContainer is DocumentContainer)
      Cursor.Current = dockingSite._redockType != StandardDocker.RedockType.AlreadyActioned ? (dockingSite._redockType != StandardDocker.RedockType.None ? this._splittingCursor : this._splittingCursorNo) : Cursors.Default;
    this._dockingSite = dockingSite;
  }

  private void CheckSite(StandardDocker.DockingSite dockingSite)
  {
  }

  private Rectangle a(DockContainer dockContainer)
  {
    Rectangle rectangle = new Rectangle(dockContainer.PointToScreen(new Point(0, 0)), dockContainer.Size);
    if (dockContainer.Empty)
    {
      switch (dockContainer.Dock)
      {
        case DockStyle.Top:
          rectangle.Height += 30;
          return rectangle;
        case DockStyle.Bottom:
          rectangle.Offset(0, -30);
          rectangle.Height += 30;
          return rectangle;
        case DockStyle.Left:
          rectangle.Width += 30;
          return rectangle;
        case DockStyle.Right:
          rectangle.Offset(-30, 0);
          rectangle.Width += 30;
          return rectangle;
        default:
          return rectangle;
      }
    }
    else if (!dockContainer.Collapsed && dockContainer.Sizable)
    {
      switch (dockContainer.Dock)
      {
        case DockStyle.Top:
          rectangle.Y = rectangle.Bottom - 4;
          rectangle.Height = 4;
          return rectangle;
        case DockStyle.Bottom:
          rectangle.Height = 4;
          return rectangle;
        case DockStyle.Left:
          rectangle.X = rectangle.Right - 4;
          rectangle.Width = 4;
          return rectangle;
        case DockStyle.Right:
          rectangle.Width = 4;
          return rectangle;
        default:
          return Rectangle.Empty;
      }
    }
    else
    {
      if (!dockContainer.Collapsed)
        rectangle = Rectangle.Empty;
      return rectangle;
    }
  }

  private void a(DockContainer dockContainer, ArrayList list)
  {
    if (dockContainer.Collapsed || !dockContainer.Enabled || !dockContainer.Visible)
      return;
    this.GetContainerControlSystems(dockContainer, dockContainer.LayoutSystem, list);
  }

  private StandardDocker.DockingSite a(DockContainer A_0, ControlLayoutSystem A_1)
  {
    return new StandardDocker.DockingSite(StandardDocker.RedockType.SplitExistingSystem)
    {
      _dockContainer = A_0,
      _layoutSystem = A_1
    };
  }

  protected Rectangle a(DockContainer A_0, LayoutSystemBase A_1)
  {
    int num1 = (double) A_1._workingSize.Width < (double) A_1._workingSize.Height ? (int) A_1._workingSize.Width : (int) A_1._workingSize.Height;
    int num2 = !A_0.Empty ? A_0.ContentSize + num1 : (A_0.ContentSize <= 0 ? num1 : A_0.ContentSize);
    if (A_0.Collapsed && A_0.ContentSize > 0)
      num1 = A_0.ContentSize;
    Rectangle rectangle = new Rectangle(A_0.PointToScreen(new Point(0, 0)), A_0.Size);
    if (A_0.Empty)
    {
      switch (A_0.Dock)
      {
        case DockStyle.Top:
          rectangle.Height += num2;
          return rectangle;
        case DockStyle.Bottom:
          rectangle.Offset(0, -num2);
          rectangle.Height += num2;
          return rectangle;
        case DockStyle.Left:
          rectangle.Width += num2;
          return rectangle;
        case DockStyle.Right:
          rectangle.Offset(-num2, 0);
          rectangle.Width += num2;
          return rectangle;
        default:
          return rectangle;
      }
    }
    else
    {
      switch (A_0.Dock)
      {
        case DockStyle.Top:
          rectangle.Y = rectangle.Bottom;
          rectangle.Height = num1;
          return rectangle;
        case DockStyle.Bottom:
          rectangle.Y -= num1;
          rectangle.Height = num1;
          return rectangle;
        case DockStyle.Left:
          rectangle.X = rectangle.Right;
          rectangle.Width = num1;
          return rectangle;
        case DockStyle.Right:
          rectangle.X -= num1;
          rectangle.Width = num1;
          return rectangle;
        default:
          return rectangle;
      }
    }
  }

  private StandardDocker.DockingSite GetDockingSite(
    DockContainer container,
    ControlLayoutSystem layoutSystem,
    Point A_2)
  {
    StandardDocker.DockingSite dockingSite1 = (StandardDocker.DockingSite) null;
    Point client = container.PointToClient(A_2);
    Rectangle documentBounds = layoutSystem._documentBounds;
    Rectangle rectangle = new Rectangle(documentBounds.Left, documentBounds.Top, documentBounds.Width, 30);
    if (rectangle.Contains(client))
    {
      StandardDocker.DockingSite dockingSite2 = this.a(container, layoutSystem);
      if (client.X < documentBounds.Left + 30)
      {
        this.SetBoundsAndSide(container, layoutSystem, dockingSite2, documentBounds, client);
        return dockingSite2;
      }
      if (client.X > documentBounds.Right - 30)
      {
        this.b(container, layoutSystem, dockingSite2, documentBounds, client);
        return dockingSite2;
      }
      this.SetBoundsAndSide(container, layoutSystem, dockingSite2, DockSide.Top);
      return dockingSite2;
    }
    rectangle = new Rectangle(documentBounds.Left, documentBounds.Top, 30, documentBounds.Height);
    if (rectangle.Contains(client))
    {
      StandardDocker.DockingSite dockingSite3 = this.a(container, layoutSystem);
      if (client.Y < documentBounds.Top + 30)
      {
        this.SetBoundsAndSide(container, layoutSystem, dockingSite3, documentBounds, client);
        return dockingSite3;
      }
      if (client.Y > documentBounds.Bottom - 30)
      {
        this.c1(container, layoutSystem, dockingSite3, documentBounds, client);
        return dockingSite3;
      }
      this.SetBoundsAndSide(container, layoutSystem, dockingSite3, DockSide.Left);
      return dockingSite3;
    }
    rectangle = new Rectangle(documentBounds.Right - 30, documentBounds.Top, 30, documentBounds.Height);
    if (rectangle.Contains(client))
    {
      StandardDocker.DockingSite dockingSite4 = this.a(container, layoutSystem);
      if (client.Y < documentBounds.Top + 30)
      {
        this.b(container, layoutSystem, dockingSite4, documentBounds, client);
        return dockingSite4;
      }
      if (client.Y > documentBounds.Bottom - 30)
      {
        this.d(container, layoutSystem, dockingSite4, documentBounds, client);
        return dockingSite4;
      }
      this.SetBoundsAndSide(container, layoutSystem, dockingSite4, DockSide.Right);
      return dockingSite4;
    }
    rectangle = new Rectangle(documentBounds.Left, documentBounds.Bottom - 30, documentBounds.Width, 30);
    if (rectangle.Contains(client))
    {
      dockingSite1 = this.a(container, layoutSystem);
      if (client.X < documentBounds.Left + 30)
      {
        this.c1(container, layoutSystem, dockingSite1, documentBounds, client);
        return dockingSite1;
      }
      if (client.X > documentBounds.Right - 30)
      {
        this.d(container, layoutSystem, dockingSite1, documentBounds, client);
        return dockingSite1;
      }
      this.SetBoundsAndSide(container, layoutSystem, dockingSite1, DockSide.Bottom);
    }
    return dockingSite1;
  }

  internal Rectangle GetDockingBounds(
    DockContainer container,
    ControlLayoutSystem layoutSystem,
    DockSide dockSide)
  {
    Rectangle dockingBounds = new Rectangle(container.PointToScreen(layoutSystem.Bounds.Location), layoutSystem.Bounds.Size);
    switch (dockSide)
    {
      case DockSide.Top:
        dockingBounds.Height /= 2;
        return dockingBounds;
      case DockSide.Bottom:
        dockingBounds.Offset(0, dockingBounds.Height / 2);
        dockingBounds.Height /= 2;
        return dockingBounds;
      case DockSide.Left:
        dockingBounds.Width /= 2;
        return dockingBounds;
      case DockSide.Right:
        dockingBounds.Offset(dockingBounds.Width / 2, 0);
        dockingBounds.Width /= 2;
        return dockingBounds;
      default:
        return dockingBounds;
    }
  }

  private void GetContainerControlSystems(
    DockContainer dockContainer,
    SplitLayoutSystem splitSystem,
    ArrayList list)
  {
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) splitSystem.LayoutSystems)
    {
      if (layoutSystem is SplitLayoutSystem)
        this.GetContainerControlSystems(dockContainer, (SplitLayoutSystem) layoutSystem, list);
      else if (layoutSystem is ControlLayoutSystem && (this._sourceDockControl == null || layoutSystem != this._sourceLayoutSystem || this._sourceDockControl._layoutSystem.Controls.Count != 1) && !((ControlLayoutSystem) layoutSystem).Collapsed)
        list.Add((object) layoutSystem);
    }
  }

  protected StandardDocker.DockingSite GetDockingSite(
    DockContainer dockContainer,
    ControlLayoutSystem layoutSystem,
    Point pos,
    bool A_3)
  {
    StandardDocker.DockingSite dockingSite = new StandardDocker.DockingSite(StandardDocker.RedockType.Undefined);
    Point client = dockContainer.PointToClient(pos);
    if (this._sourceDockControl == null && layoutSystem == this._sourceLayoutSystem)
      return layoutSystem.JoinCatchmentBounds.Contains(client) ? new StandardDocker.DockingSite(StandardDocker.RedockType.None) : new StandardDocker.DockingSite(StandardDocker.RedockType.Undefined);
    if (layoutSystem.JoinCatchmentBounds.Contains(client) || layoutSystem._tabStripBounds.Contains(client))
    {
      dockingSite = new StandardDocker.DockingSite(StandardDocker.RedockType.JoinExistingSystem);
      dockingSite._dockContainer = dockContainer;
      dockingSite._layoutSystem = layoutSystem;
      dockingSite._dockSide = DockSide.None;
      dockingSite._bounds = new Rectangle(dockContainer.PointToScreen(layoutSystem.Bounds.Location), layoutSystem.Bounds.Size);
      dockingSite._childIndex = !layoutSystem._tabStripBounds.Contains(client) ? layoutSystem.Controls.Count : layoutSystem.GetChildIndex(client);
    }
    if (dockingSite._redockType == StandardDocker.RedockType.Undefined & A_3)
      dockingSite = this.GetDockingSite(dockContainer, layoutSystem, pos);
    return dockingSite;
  }

  private void SetBoundsAndSide(
    DockContainer container,
    ControlLayoutSystem layoutSystem,
    StandardDocker.DockingSite dockSite,
    DockSide dockSide)
  {
    dockSite._bounds = this.GetDockingBounds(container, layoutSystem, dockSide);
    dockSite._dockSide = dockSide;
  }

  private void SetBoundsAndSide(
    DockContainer container,
    ControlLayoutSystem layoutSystem,
    StandardDocker.DockingSite dockSite,
    Rectangle bounds,
    Point pos)
  {
    pos.X -= bounds.Left;
    pos.Y -= bounds.Top;
    bounds = new Rectangle(0, 0, 30, 30);
    if (pos.Y > bounds.Top + (int) ((double) bounds.Height * ((double) pos.X / (double) bounds.Width)))
      this.SetBoundsAndSide(container, layoutSystem, dockSite, DockSide.Left);
    else
      this.SetBoundsAndSide(container, layoutSystem, dockSite, DockSide.Top);
  }

  public override void Dispose()
  {
    this._sourceDockContainer.OnDockingFinished(EventArgs.Empty);
    if (this._splittingCursor != (Cursor) null)
      this._splittingCursor.Dispose();
    if (this._splittingCursorNo != (Cursor) null)
      this._splittingCursorNo.Dispose();
    base.Dispose();
  }

  protected virtual StandardDocker.DockingSite GetDockingSiteAt(Point pos)
  {
    Rectangle rectangle;
    if (this._dockManager != null)
    {
      foreach (DockContainer dockContainer in this._dockManager._dockContainers)
      {
        if (dockContainer.IsFloating && ((FloatingDockContainer) dockContainer).GetForm().Visible && dockContainer.HasSingleControlLayoutSystem && dockContainer.LayoutSystem != this._sourceLayoutSystem)
        {
          rectangle = ((FloatingDockContainer) dockContainer).GetBounds();
          if (rectangle.Contains(pos))
          {
            rectangle = new Rectangle(dockContainer.PointToScreen(dockContainer.LayoutSystem.LayoutSystems[0].Bounds.Location), dockContainer.LayoutSystem.LayoutSystems[0].Bounds.Size);
            if (!rectangle.Contains(pos))
              return new StandardDocker.DockingSite(StandardDocker.RedockType.JoinExistingSystem)
              {
                _dockContainer = dockContainer,
                _layoutSystem = (ControlLayoutSystem) dockContainer.LayoutSystem.LayoutSystems[0],
                _bounds = ((FloatingDockContainer) dockContainer).GetBounds()
              };
          }
        }
      }
    }
    foreach (ControlLayoutSystem layoutSystem in this._m)
    {
      ref Rectangle local = ref rectangle;
      DockContainer dockContainer = layoutSystem.DockContainer;
      Rectangle bounds = layoutSystem.Bounds;
      Point location = bounds.Location;
      Point screen = dockContainer.PointToScreen(location);
      bounds = layoutSystem.Bounds;
      Size size = bounds.Size;
      local = new Rectangle(screen, size);
      if (rectangle.Contains(pos))
      {
        StandardDocker.DockingSite dockingSite = this.GetDockingSite(layoutSystem.DockContainer, layoutSystem, pos, true);
        if (dockingSite != null)
          return dockingSite;
      }
    }
    if (this.DockManager != null)
    {
      foreach (DockContainer dockContainer in this._dockManager._dockContainers)
      {
        if (!dockContainer.IsFloating && dockContainer.Enabled && dockContainer.Visible && (dockContainer.Empty || this._sourceDockControl != null || dockContainer != this._sourceDockContainer || !dockContainer.HasSingleControlLayoutSystem))
        {
          rectangle = this.a(dockContainer);
          if (rectangle.Contains(pos))
            return new StandardDocker.DockingSite(StandardDocker.RedockType.CreateNewContainer)
            {
              _dockContainer = dockContainer,
              _bounds = this.a(dockContainer, this._sourceLayoutSystem)
            };
        }
      }
    }
    return (StandardDocker.DockingSite) null;
  }

  protected bool CanDockTo(DockLocation dockLocation)
  {
    return this._sourceDockControl != null ? this._sourceDockControl.IsDockLocationValid(dockLocation) : this._sourceLayoutSystem.IsDockLocationValid(dockLocation);
  }

  private void b(
    DockContainer A_0,
    ControlLayoutSystem A_1,
    StandardDocker.DockingSite A_2,
    Rectangle A_3,
    Point A_4)
  {
    A_3.X = A_3.Right - 30;
    A_4.X -= A_3.Left;
    A_4.Y -= A_3.Top;
    A_3 = new Rectangle(0, 0, 30, 30);
    if (A_4.Y > A_3.Top + (int) ((double) A_3.Height * ((double) (A_3.Right - A_4.X) / (double) A_3.Width)))
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Right);
    else
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Top);
  }

  public override void OnCancel()
  {
    base.OnCancel();
    if (this.Cancel == null)
      return;
    this.Cancel((object) this, EventArgs.Empty);
  }

  public override void OnCommit()
  {
    base.OnCommit();
    if (this.Commit == null)
      return;
    this.Commit(this._dockingSite);
  }

  private void c1(
    DockContainer A_0,
    ControlLayoutSystem A_1,
    StandardDocker.DockingSite A_2,
    Rectangle A_3,
    Point A_4)
  {
    A_3.Y = A_3.Bottom - 30;
    A_4.X -= A_3.Left;
    A_4.Y -= A_3.Top;
    A_3 = new Rectangle(0, 0, 30, 30);
    if (A_4.Y > A_3.Bottom - (int) ((double) A_3.Height * ((double) A_4.X / (double) A_3.Width)))
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Bottom);
    else
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Left);
  }

  private ControlLayoutSystem[] FillSystemsArray()
  {
    ArrayList list = new ArrayList();
    if (this._dockManager == null)
    {
      this.a(this.SourceDockContainer, list);
    }
    else
    {
      foreach (DockContainer dockContainer in this._dockManager._dockContainers)
      {
        if (dockContainer.IsFloating && ((FloatingDockContainer) dockContainer).GetForm().Visible && (!(this._sourceLayoutSystem is SplitLayoutSystem) || this._sourceLayoutSystem.DockContainer != dockContainer))
          this.a(dockContainer, list);
      }
      foreach (DockContainer dockContainer in this._dockManager._dockContainers)
      {
        if (!dockContainer.IsFloating && !dockContainer.Empty)
          this.a(dockContainer, list);
      }
    }
    ControlLayoutSystem[] controlLayoutSystemArray = new ControlLayoutSystem[list.Count];
    list.CopyTo((Array) controlLayoutSystemArray, 0);
    return controlLayoutSystemArray;
  }

  private void d(
    DockContainer A_0,
    ControlLayoutSystem A_1,
    StandardDocker.DockingSite A_2,
    Rectangle A_3,
    Point A_4)
  {
    A_3.X = A_3.Right - 30;
    A_3.Y = A_3.Bottom - 30;
    A_4.X -= A_3.Left;
    A_4.Y -= A_3.Top;
    A_3 = new Rectangle(0, 0, 30, 30);
    if (A_4.Y > A_3.Top + (int) ((double) A_3.Height * ((double) A_4.X / (double) A_3.Width)))
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Bottom);
    else
      this.SetBoundsAndSide(A_0, A_1, A_2, DockSide.Right);
  }

  private Point e()
  {
    Point position = Cursor.Position;
    int x = position.X - this._h.X;
    position = Cursor.Position;
    int y = position.Y - this._h.Y;
    return new Point(x, y);
  }

  public LayoutSystemBase SourceLayoutSystem => this._sourceLayoutSystem;

  protected ControlLayoutSystem[] GetLayoutSystems() => this._m;

  public DockControl SourceDockControl => this._sourceDockControl;

  public DockContainer SourceDockContainer => this._sourceDockContainer;

  public StandardDocker.DockingSite GetDockingSite() => this._dockingSite;

  public DockManager DockManager => this._dockManager;

  public event StandardDocker.DockingManagerCommittedEventHandler Commit;

  public event EventHandler Cancel;

  public delegate void DockingManagerCommittedEventHandler(StandardDocker.DockingSite A_0);

  public class DockingSite
  {
    public StandardDocker.RedockType _redockType;
    public DockContainer _dockContainer;
    public ControlLayoutSystem _layoutSystem;
    public DockSide _dockSide;
    public Rectangle _bounds;
    public int _childIndex;

    public DockingSite(StandardDocker.RedockType redockSite)
    {
      this._dockContainer = (DockContainer) null;
      this._layoutSystem = (ControlLayoutSystem) null;
      this._dockSide = DockSide.None;
      this._bounds = Rectangle.Empty;
      this._childIndex = 0;
      this._redockType = redockSite;
    }
  }

  public enum RedockType
  {
    Undefined,
    None,
    Float,
    SplitExistingSystem,
    JoinExistingSystem,
    CreateNewContainer,
    AlreadyActioned,
  }
}
