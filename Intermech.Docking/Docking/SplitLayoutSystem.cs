
// Type: Intermech.Docking.SplitLayoutSystem
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

[TypeConverter("Intermech.Docking.SplitLayoutSystemConverter")]
public class SplitLayoutSystem : LayoutSystemBase
{
  private SplitLayoutSystem.LayoutSystemBaseCollection _layoutSystems;
  private Orientation _orientation;
  private ArrayList _splitBoundsList;
  private SplitLayoutResizer _resizer;

  internal event EventHandler LayoutSystemsChanged;

  public SplitLayoutSystem()
  {
    this._orientation = Orientation.Horizontal;
    this._resizer = (SplitLayoutResizer) null;
    this._layoutSystems = new SplitLayoutSystem.LayoutSystemBaseCollection(this);
    this._splitBoundsList = new ArrayList();
  }

  public SplitLayoutSystem(int desiredWidth, int desiredHeight)
    : this()
  {
    this._workingSize = new SizeF((float) desiredWidth, (float) desiredHeight);
  }

  public SplitLayoutSystem(
    SizeF desiredSize,
    Orientation splitMode,
    LayoutSystemBase[] layoutSystems)
    : this()
  {
    this._workingSize = desiredSize;
    this._orientation = splitMode;
    this._layoutSystems.AddRange(layoutSystems);
  }

  public SplitLayoutSystem(
    int desiredWidth,
    int desiredHeight,
    Orientation splitMode,
    LayoutSystemBase[] layoutSystems)
    : this(desiredWidth, desiredHeight)
  {
    this._orientation = splitMode;
    this._layoutSystems.AddRange(layoutSystems);
  }

  private void DetachResizer()
  {
    this._resizer.Cancel -= new EventHandler(this.OnResizerCancel);
    this._resizer.Commit -= new SplitLayoutResizer.SplitResizeEventHandler(this.OnResizerCommit);
    this._resizer = (SplitLayoutResizer) null;
  }

  private LayoutSystemBase[] a(out int count)
  {
    count = 0;
    LayoutSystemBase[] layoutSystemBaseArray = new LayoutSystemBase[this.LayoutSystems.Count];
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        if (!((ControlLayoutSystem) layoutSystem).Collapsed || this.IsInContainer && !this.DockContainer.CanShowCollapsed)
          layoutSystemBaseArray[count++] = layoutSystem;
      }
      else if (layoutSystem is SplitLayoutSystem && ((SplitLayoutSystem) layoutSystem).d())
        layoutSystemBaseArray[count++] = layoutSystem;
    }
    return layoutSystemBaseArray;
  }

  internal void a(ControlLayoutSystem A_0)
  {
    int count;
    LayoutSystemBase[] layoutSystemBaseArray = this.a(out count);
    float num1 = 0.0f;
    for (int index = 0; index < count; ++index)
    {
      LayoutSystemBase layoutSystemBase = layoutSystemBaseArray[index];
      if (layoutSystemBase != A_0)
      {
        if (this._orientation == Orientation.Horizontal)
          num1 += layoutSystemBase._workingSize.Height;
        else
          num1 += layoutSystemBase._workingSize.Width;
      }
    }
    float num2 = this._orientation == Orientation.Horizontal ? A_0._workingSize.Height : A_0._workingSize.Width;
    float num3 = this._orientation == Orientation.Horizontal ? (float) (this.Bounds.Height - (count - 1) * 4) : (float) (this.Bounds.Width - (count - 1) * 4);
    if ((double) num2 > (double) num3 * 0.75)
      num2 = num3 * 0.75f;
    for (int index = 0; index < count; ++index)
    {
      if (layoutSystemBaseArray[index] != A_0)
      {
        if (this._orientation == Orientation.Horizontal)
          layoutSystemBaseArray[index]._workingSize.Height -= num2 * (layoutSystemBaseArray[index]._workingSize.Height / num1);
        else
          layoutSystemBaseArray[index]._workingSize.Width -= num2 * (layoutSystemBaseArray[index]._workingSize.Width / num1);
      }
    }
  }

  internal bool a(int A_0, int A_1)
  {
    foreach (Rectangle splitBounds in this._splitBoundsList)
    {
      if (splitBounds.Contains(A_0, A_1))
        return true;
    }
    return false;
  }

  private void OnResizerCancel(object A_0, EventArgs A_1) => this.DetachResizer();

  private void a(SplitLayoutSystem A_0, ArrayList A_1)
  {
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) A_0._layoutSystems)
    {
      if (layoutSystem is SplitLayoutSystem)
        this.a((SplitLayoutSystem) layoutSystem, A_1);
      else if (layoutSystem is ControlLayoutSystem)
      {
        foreach (DockControl control in (CollectionBase) ((ControlLayoutSystem) layoutSystem).Controls)
          A_1.Add((object) control);
      }
    }
  }

  internal void a(Point A_0, out LayoutSystemBase A_1, out LayoutSystemBase A_2)
  {
    int index = 0;
    Rectangle bounds;
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
    {
      if (this.SplitMode == Orientation.Horizontal)
      {
        int y1 = A_0.Y;
        bounds = layoutSystem.Bounds;
        int bottom = bounds.Bottom;
        if (y1 >= bottom)
        {
          int y2 = A_0.Y;
          bounds = layoutSystem.Bounds;
          int num = bounds.Bottom + 4;
          if (y2 <= num)
          {
            index = this.LayoutSystems.IndexOf(layoutSystem);
            break;
          }
        }
      }
      else
      {
        int x1 = A_0.X;
        bounds = layoutSystem.Bounds;
        int right = bounds.Right;
        if (x1 >= right)
        {
          int x2 = A_0.X;
          bounds = layoutSystem.Bounds;
          int num = bounds.Right + 4;
          if (x2 <= num)
          {
            index = this.LayoutSystems.IndexOf(layoutSystem);
            break;
          }
        }
      }
    }
    A_1 = this.LayoutSystems[index];
    A_2 = this.LayoutSystems[index + 1];
  }

  private void OnResizerCommit(LayoutSystemBase A_0, LayoutSystemBase A_1, int A_2, int A_3)
  {
    this.DetachResizer();
    if (this.SplitMode == Orientation.Horizontal)
    {
      A_0._workingSize.Height = (float) A_2;
      A_1._workingSize.Height = (float) A_3;
    }
    else
    {
      A_0._workingSize.Width = (float) A_2;
      A_1._workingSize.Width = (float) A_3;
    }
    this.Resize();
  }

  internal void OnLayoutSystemsChanged()
  {
    if (this.DockContainer != null)
      this.DockContainer.LayoutSystemsChanged();
    if (this.LayoutSystemsChanged == null)
      return;
    this.LayoutSystemsChanged((object) this, EventArgs.Empty);
  }

  internal void Resize()
  {
    if (this.DockContainer != null)
      this.DockContainer.PerformResize((LayoutSystemBase) this, this.Bounds);
    if (this.DockContainer == null)
      return;
    this.DockContainer.Invalidate(this.Bounds);
  }

  internal bool d()
  {
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        if (!((ControlLayoutSystem) layoutSystem).Collapsed || this.IsInContainer && !this.DockContainer.CanShowCollapsed)
          return true;
      }
      else if (((SplitLayoutSystem) layoutSystem).d())
        return true;
    }
    return false;
  }

  public override void Dispose()
  {
    LayoutSystemBase[] array = new LayoutSystemBase[this.LayoutSystems.Count];
    this.LayoutSystems.CopyTo(array, 0);
    this.LayoutSystems.Clear();
    foreach (LayoutSystemBase layoutSystemBase in array)
      layoutSystemBase.Dispose();
    base.Dispose();
  }

  internal override bool IsDockLocationValid(DockLocation location)
  {
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
    {
      if (!layoutSystem.IsDockLocationValid(location))
        return false;
    }
    return true;
  }

  protected internal override void Layout(
    RendererBase renderer,
    Graphics graphics,
    Rectangle bounds,
    bool floating)
  {
    base.Layout(renderer, graphics, bounds, floating);
    int count;
    LayoutSystemBase[] layoutSystemBaseArray = this.a(out count);
    if (count == 0)
      return;
    if (count > 1)
      floating = false;
    for (int index = 0; index < count; ++index)
    {
      if (this._orientation == Orientation.Horizontal)
      {
        if ((double) layoutSystemBaseArray[index]._workingSize.Height <= 0.0)
          layoutSystemBaseArray[index]._workingSize.Height = 400f;
        layoutSystemBaseArray[index]._workingSize.Width = (float) bounds.Width;
      }
      if (this._orientation == Orientation.Vertical)
      {
        if ((double) layoutSystemBaseArray[index]._workingSize.Width <= 0.0)
          layoutSystemBaseArray[index]._workingSize.Width = 250f;
        layoutSystemBaseArray[index]._workingSize.Height = (float) bounds.Height;
      }
    }
    int num1 = this._orientation == Orientation.Horizontal ? bounds.Height - (count - 1) * 4 : bounds.Width - (count - 1) * 4;
    float num2 = 0.0f;
    for (int index = 0; index < count; ++index)
      num2 += this._orientation == Orientation.Horizontal ? layoutSystemBaseArray[index]._workingSize.Height : layoutSystemBaseArray[index]._workingSize.Width;
    this._splitBoundsList.Clear();
    if (num1 <= 0)
      return;
    if ((double) num1 != (double) num2)
    {
      float num3 = (float) num1 - num2;
      for (int index = 0; index < count; ++index)
      {
        if (this._orientation == Orientation.Horizontal)
          layoutSystemBaseArray[index]._workingSize.Height += num3 * (layoutSystemBaseArray[index]._workingSize.Height / num2);
        else
          layoutSystemBaseArray[index]._workingSize.Width += num3 * (layoutSystemBaseArray[index]._workingSize.Width / num2);
      }
      float num4 = 0.0f;
      for (int index = 0; index < count; ++index)
        num4 += this._orientation == Orientation.Horizontal ? layoutSystemBaseArray[index]._workingSize.Height : layoutSystemBaseArray[index]._workingSize.Width;
      float num5 = (float) num1 - num4;
      if (this._orientation == Orientation.Horizontal)
        layoutSystemBaseArray[0]._workingSize.Height += num5;
      else
        layoutSystemBaseArray[0]._workingSize.Width += num5;
    }
    int num6 = this._orientation == Orientation.Horizontal ? bounds.Y : bounds.X;
    for (int index = 0; index < count; ++index)
    {
      if (this._orientation == Orientation.Horizontal)
      {
        int height = Convert.ToInt32(layoutSystemBaseArray[index]._workingSize.Height);
        if (index == count - 1)
          height = bounds.Bottom - num6;
        layoutSystemBaseArray[index].Layout(renderer, graphics, new Rectangle(bounds.X, num6, bounds.Width, height), floating);
        num6 += height + 4;
      }
      else
      {
        int width = Convert.ToInt32(layoutSystemBaseArray[index]._workingSize.Width);
        if (index == count - 1)
          width = bounds.Right - num6;
        layoutSystemBaseArray[index].Layout(renderer, graphics, new Rectangle(num6, bounds.Y, width, bounds.Height), floating);
        num6 += width + 4;
      }
    }
    for (int index = 0; index < count - 1; ++index)
    {
      bounds = layoutSystemBaseArray[index].Bounds;
      if (this._orientation == Orientation.Horizontal)
      {
        bounds.Offset(0, bounds.Height);
        bounds.Height = 4;
      }
      else
      {
        bounds.Offset(bounds.Width, 0);
        bounds.Width = 4;
      }
      this._splitBoundsList.Add((object) bounds);
    }
  }

  public void MoveToLayoutSystem(ControlLayoutSystem layoutSystem)
  {
    this.MoveToLayoutSystem(layoutSystem, 0);
  }

  public void MoveToLayoutSystem(ControlLayoutSystem layoutSystem, int index)
  {
    DockControl dockControl = (DockControl) null;
    if (this.LayoutSystems.Count == 1 && this.LayoutSystems[0] is ControlLayoutSystem)
      dockControl = ((ControlLayoutSystem) this.LayoutSystems[0]).SelectedControl;
    ArrayList A_1 = new ArrayList();
    this.a(this, A_1);
    foreach (DockControl control in A_1)
      control._layoutSystem.Controls.Remove(control);
    foreach (DockControl control in A_1)
      layoutSystem.Controls.Insert(index, control);
    if (dockControl == null)
      return;
    layoutSystem.SelectedControl = dockControl;
  }

  internal override void OnDockingManagerCommitted(StandardDocker.DockingSite target)
  {
    base.OnDockingManagerCommitted(target);
    if (target == null || target._redockType == StandardDocker.RedockType.None || target._redockType == StandardDocker.RedockType.AlreadyActioned)
      return;
    FloatingDockContainer dockContainer = (FloatingDockContainer) this.DockContainer;
    DockManager manager = this.DockContainer.Manager;
    if (target._redockType == StandardDocker.RedockType.Float)
    {
      dockContainer.SetWindowPos(target._bounds, true, true);
    }
    else
    {
      this._workingSize = (SizeF) this.Bounds.Size;
      dockContainer.LayoutSystem = new SplitLayoutSystem();
      manager.DisposeFloatingContainer(dockContainer);
      if (target._redockType == StandardDocker.RedockType.CreateNewContainer)
      {
        if (target._dockContainer.Empty)
          target._dockContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) this);
        else
          target._dockContainer.AddLayoutSystem((LayoutSystemBase) this);
      }
      else if (target._redockType == StandardDocker.RedockType.JoinExistingSystem)
      {
        this.MoveToLayoutSystem(target._layoutSystem, target._childIndex);
      }
      else
      {
        if (target._redockType != StandardDocker.RedockType.SplitExistingSystem)
          return;
        if (this.LayoutSystems.Count == 1 && this.LayoutSystems[0] is ControlLayoutSystem)
        {
          ControlLayoutSystem layoutSystem = (ControlLayoutSystem) this.LayoutSystems[0];
          this.LayoutSystems.Remove((LayoutSystemBase) layoutSystem);
          target._layoutSystem.SplitForLayoutSystem((LayoutSystemBase) layoutSystem, target._dockSide);
        }
        else
          target._layoutSystem.SplitForLayoutSystem((LayoutSystemBase) this, target._dockSide);
      }
    }
  }

  protected internal override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    foreach (Rectangle splitBounds in this._splitBoundsList)
    {
      if (splitBounds.Contains(e.X, e.Y))
      {
        LayoutSystemBase A_1;
        LayoutSystemBase A_2;
        this.a(new Point(e.X, e.Y), out A_1, out A_2);
        if (this._resizer != null)
          this._resizer.Dispose();
        DockingHints dockingHints = this.DockContainer.Manager != null ? this.DockContainer.Manager.DockingHints : this.DockContainer.DockingHints;
        this._resizer = new SplitLayoutResizer(this.DockContainer, this, A_1, A_2, new Point(e.X, e.Y), dockingHints);
        this._resizer.Cancel += new EventHandler(this.OnResizerCancel);
        this._resizer.Commit += new SplitLayoutResizer.SplitResizeEventHandler(this.OnResizerCommit);
        break;
      }
    }
  }

  protected internal override void OnMouseMove(MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left)
    {
      if (this._docker != null)
      {
        this._docker.Update(Cursor.Position);
        return;
      }
      if (this._resizer != null)
      {
        this._resizer.Update(new Point(e.X, e.Y));
        return;
      }
    }
    if (this.a(e.X, e.Y))
    {
      if (this._orientation == Orientation.Horizontal)
        this.DockContainer.Cursor = Cursors.HSplit;
      else
        this.DockContainer.Cursor = Cursors.VSplit;
    }
    else
      this.DockContainer.Cursor = Cursors.Default;
    base.OnMouseMove(e);
  }

  protected internal override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    if (this._docker != null)
    {
      this._docker.OnCommit();
    }
    else
    {
      if (this._resizer == null)
        return;
      this._resizer.OnCommit();
    }
  }

  public bool Optimize()
  {
    if (this.LayoutSystems.Count == 1 && this.LayoutSystems[0] is SplitLayoutSystem)
    {
      SplitLayoutSystem layoutSystem1 = (SplitLayoutSystem) this.LayoutSystems[0];
      if (layoutSystem1.LayoutSystems.Count != 1 || !(layoutSystem1.LayoutSystems[0] is SplitLayoutSystem) || ((SplitLayoutSystem) layoutSystem1.LayoutSystems[0]).SplitMode != this.SplitMode)
        return false;
      SplitLayoutSystem layoutSystem2 = (SplitLayoutSystem) layoutSystem1.LayoutSystems[0];
      LayoutSystemBase[] layoutSystemBaseArray = new LayoutSystemBase[layoutSystem2.LayoutSystems.Count];
      layoutSystem2.LayoutSystems.CopyTo(layoutSystemBaseArray, 0);
      layoutSystem2.LayoutSystems._rangeAdding = true;
      layoutSystem2.LayoutSystems.Clear();
      this.LayoutSystems._rangeAdding = true;
      this.LayoutSystems.Clear();
      layoutSystem1.Dispose();
      this.LayoutSystems.AddRange(layoutSystemBaseArray);
      this.LayoutSystems._rangeAdding = false;
      return true;
    }
    foreach (LayoutSystemBase layoutSystem3 in (CollectionBase) this.LayoutSystems)
    {
      if (layoutSystem3 is SplitLayoutSystem)
      {
        SplitLayoutSystem layoutSystem4 = (SplitLayoutSystem) layoutSystem3;
        if (layoutSystem4.SplitMode == this.SplitMode)
        {
          LayoutSystemBase[] array = new LayoutSystemBase[layoutSystem4.LayoutSystems.Count];
          layoutSystem4.LayoutSystems.CopyTo(array, 0);
          layoutSystem4.LayoutSystems._rangeAdding = true;
          layoutSystem4.LayoutSystems.Clear();
          int index1 = this.LayoutSystems.IndexOf((LayoutSystemBase) layoutSystem4);
          this.LayoutSystems._rangeAdding = true;
          this.LayoutSystems.Remove((LayoutSystemBase) layoutSystem4);
          layoutSystem4.Dispose();
          for (int index2 = array.Length - 1; index2 >= 0; --index2)
            this.LayoutSystems.Insert(index1, array[index2]);
          this.LayoutSystems._rangeAdding = false;
          return true;
        }
        if (layoutSystem4.Optimize())
          return true;
      }
    }
    return false;
  }

  internal override void Paint(RendererBase renderer, Graphics graphics, Font font)
  {
    foreach (Rectangle splitBounds in this._splitBoundsList)
      renderer.DrawSplitter(graphics, splitBounds, this._orientation);
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
    {
      if (!(layoutSystem is ControlLayoutSystem) || !((ControlLayoutSystem) layoutSystem).Collapsed || !this.DockContainer.CanShowCollapsed)
      {
        Region clip = graphics.Clip;
        graphics.SetClip(layoutSystem.Bounds);
        layoutSystem.Paint(renderer, graphics, font);
        graphics.Clip = clip;
      }
    }
  }

  internal override void SetDockContainer(DockContainer dockContainer)
  {
    base.SetDockContainer(dockContainer);
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
      layoutSystem.SetDockContainer(dockContainer);
  }

  internal override bool ContainsPersistableDockControls
  {
    get
    {
      foreach (LayoutSystemBase layoutSystem in (CollectionBase) this.LayoutSystems)
      {
        if (layoutSystem.ContainsPersistableDockControls)
          return true;
      }
      return false;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public SplitLayoutSystem.LayoutSystemBaseCollection LayoutSystems => this._layoutSystems;

  [Description("Indicates whether this layout is split horizontally or vertically.")]
  [Category("Layout")]
  [DefaultValue(typeof (Orientation), "Horizontal")]
  public Orientation SplitMode
  {
    get => this._orientation;
    set
    {
      this._orientation = value;
      this.Resize();
    }
  }

  public class LayoutSystemBaseCollection : CollectionBase
  {
    private SplitLayoutSystem _parent;
    internal bool _rangeAdding;

    internal LayoutSystemBaseCollection(SplitLayoutSystem parent)
    {
      this._parent = (SplitLayoutSystem) null;
      this._rangeAdding = false;
      this._parent = parent;
    }

    private void OnChanged() => this._parent.OnLayoutSystemsChanged();

    public int Add(LayoutSystemBase layoutSystem)
    {
      int count = this.Count;
      this.Insert(count, layoutSystem);
      return count;
    }

    public void AddRange(LayoutSystemBase[] layoutSystems)
    {
      this._rangeAdding = true;
      foreach (LayoutSystemBase layoutSystem in layoutSystems)
        this.Add(layoutSystem);
      this._rangeAdding = false;
      this.OnChanged();
    }

    public bool Contains(LayoutSystemBase layoutSystem)
    {
      return this.List.Contains((object) layoutSystem);
    }

    public void CopyTo(LayoutSystemBase[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(LayoutSystemBase layoutSystem) => this.List.IndexOf((object) layoutSystem);

    public void Insert(int index, LayoutSystemBase layoutSystem)
    {
      if (layoutSystem._parent != null)
        throw new ArgumentException("Layout system already has a parent. You must first remove it from its parent.");
      this.List.Insert(index, (object) layoutSystem);
    }

    protected override void OnClear()
    {
      base.OnClear();
      foreach (LayoutSystemBase layoutSystemBase in (CollectionBase) this)
      {
        layoutSystemBase._parent = (LayoutSystemBase) null;
        layoutSystemBase.SetDockContainer((DockContainer) null);
      }
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      if (this._rangeAdding)
        return;
      this.OnChanged();
    }

    protected override void OnInsertComplete(int index, object value)
    {
      base.OnInsertComplete(index, value);
      LayoutSystemBase layoutSystemBase = (LayoutSystemBase) value;
      layoutSystemBase._parent = (LayoutSystemBase) this._parent;
      layoutSystemBase.SetDockContainer(this._parent.DockContainer);
      if (this._parent._parent == null && this._parent.DockContainer != null)
      {
        int num = (double) layoutSystemBase._workingSize.Width < (double) layoutSystemBase._workingSize.Height ? (int) layoutSystemBase._workingSize.Width : (int) layoutSystemBase._workingSize.Height;
        this._parent.DockContainer.UpdateContentSize(new Size(num, num));
      }
      if (this._rangeAdding)
        return;
      this.OnChanged();
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      base.OnRemoveComplete(index, value);
      ((LayoutSystemBase) value)._parent = (LayoutSystemBase) null;
      ((LayoutSystemBase) value).SetDockContainer((DockContainer) null);
      if (this._rangeAdding)
        return;
      if (this.Count <= 1 && this._parent._parent != null)
      {
        SplitLayoutSystem parent = (SplitLayoutSystem) this._parent._parent;
        if (this.Count == 1)
        {
          LayoutSystemBase layoutSystem = this[0];
          this._rangeAdding = true;
          this.Remove(layoutSystem);
          this._rangeAdding = false;
          parent.LayoutSystems._rangeAdding = true;
          int index1 = parent.LayoutSystems.IndexOf((LayoutSystemBase) this._parent);
          parent.LayoutSystems.Remove((LayoutSystemBase) this._parent);
          parent.LayoutSystems.Insert(index1, layoutSystem);
          parent.LayoutSystems._rangeAdding = false;
          parent.OnLayoutSystemsChanged();
        }
        else
        {
          if (this.Count != 0)
            return;
          parent.LayoutSystems.Remove((LayoutSystemBase) this._parent);
        }
      }
      else
        this.OnChanged();
    }

    public void Remove(LayoutSystemBase layoutSystem) => this.List.Remove((object) layoutSystem);

    public LayoutSystemBase this[int index] => (LayoutSystemBase) this.List[index];

    internal int PersistableCount
    {
      get
      {
        int persistableCount = 0;
        foreach (LayoutSystemBase layoutSystemBase in (CollectionBase) this)
        {
          if (layoutSystemBase.ContainsPersistableDockControls)
            ++persistableCount;
        }
        return persistableCount;
      }
    }
  }
}
