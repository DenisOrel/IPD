
// Type: Intermech.Docking.DockContainerDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Docking;

[Serializable]
internal class DockContainerDesigner : ControlDesigner
{
  private DesignerVerbCollection _verbs;
  private DockContainer _dockContainer;
  private Point _c;
  private StandardDocker _docker;
  private SplitLayoutResizer _resizer;

  public DockContainerDesigner()
  {
    this._verbs = (DesignerVerbCollection) null;
    this._dockContainer = (DockContainer) null;
    this._c = Point.Empty;
    this._docker = (StandardDocker) null;
    this._resizer = (SplitLayoutResizer) null;
    this._verbs = new DesignerVerbCollection();
    this._verbs.Add(new DesignerVerb("Add Control", new EventHandler(this.Verb_AddControl)));
  }

  private void DetachResizer()
  {
    this._resizer.Cancel -= new EventHandler(this.OnResizeCancel);
    this._resizer.Commit -= new SplitLayoutResizer.SplitResizeEventHandler(this.OnResizeCommit);
    this._resizer = (SplitLayoutResizer) null;
  }

  protected override void OnMouseDragEnd(bool cancel)
  {
    this._c = Point.Empty;
    this.Control.Capture = false;
    if (this._resizer != null)
    {
      this._resizer.OnCommit();
      this._dockContainer.Capture = false;
    }
    else if (this._docker != null)
    {
      this._docker.OnCommit();
      this._dockContainer.Capture = false;
    }
    else
    {
      if (this.b(this._dockContainer.PointToClient(Cursor.Position)) != null)
        return;
      ControlLayoutSystem layoutSystemAt = this._dockContainer.GetLayoutSystemAt(this._dockContainer.PointToClient(Cursor.Position)) as ControlLayoutSystem;
    }
  }

  public override void Initialize(IComponent A_0)
  {
    base.Initialize(A_0);
    ((ISelectionService) this.GetService(typeof (ISelectionService))).SelectionChanged += new EventHandler(this.Selecton_Changed);
    this._dockContainer = (DockContainer) A_0;
  }

  private void a(Point A_0)
  {
    LayoutSystemBase layoutSystemAt = this._dockContainer.GetLayoutSystemAt(A_0);
    if (!(layoutSystemAt is ControlLayoutSystem) || this._docker != null)
      return;
    DockControl controlAt = ((ControlLayoutSystem) layoutSystemAt).GetControlAt(A_0);
    this._docker = new StandardDocker(this._dockContainer.Manager, this._dockContainer, layoutSystemAt, controlAt, A_0, DockingHints.TranslucentFill, false);
    this._docker.Commit += new StandardDocker.DockingManagerCommittedEventHandler(this.DockManager_Committed);
    this._docker.Cancel += new EventHandler(this.DockManager_Canceled);
    this._dockContainer.Capture = true;
  }

  internal virtual void DockManager_Committed(StandardDocker.DockingSite A_0)
  {
    IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    ControlLayoutSystem sourceLayoutSystem = (ControlLayoutSystem) this._docker.SourceLayoutSystem;
    bool wholeMoving = this._docker.SourceDockControl == null;
    DockControl selectedControl = sourceLayoutSystem.SelectedControl;
    this.DisposeDocker();
    if (A_0 == null || A_0._redockType == StandardDocker.RedockType.None || A_0._redockType == StandardDocker.RedockType.AlreadyActioned)
      return;
    DesignerTransaction transaction = service2.CreateTransaction("Move DockControl");
    service1.OnComponentChanging((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"]);
    if (wholeMoving)
      ((SplitLayoutSystem) sourceLayoutSystem.Parent).LayoutSystems.Remove((LayoutSystemBase) sourceLayoutSystem);
    else
      DockHelper.DetachDockControl(selectedControl);
    service1.OnComponentChanged((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"], (object) null, (object) null);
    if (A_0._dockContainer != null)
    {
      service1.OnComponentChanging((object) A_0._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) A_0._dockContainer)["LayoutSystem"]);
      sourceLayoutSystem.ProcessReDocking(selectedControl, wholeMoving, A_0);
      service1.OnComponentChanged((object) A_0._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) A_0._dockContainer)["LayoutSystem"], (object) null, (object) null);
    }
    transaction.Commit();
  }

  protected override void OnMouseDragBegin(int A_0, int A_1)
  {
    ISelectionService service1 = (ISelectionService) this.GetService(typeof (ISelectionService));
    Point client = this._dockContainer.PointToClient(new Point(A_0, A_1));
    LayoutSystemBase layoutSystemAt = this._dockContainer.GetLayoutSystemAt(client);
    switch (layoutSystemAt)
    {
      case SplitLayoutSystem _:
        SplitLayoutSystem splitLayoutSystem = (SplitLayoutSystem) layoutSystemAt;
        if (splitLayoutSystem.a(client.X, client.Y))
        {
          LayoutSystemBase A_1_1;
          LayoutSystemBase A_2;
          splitLayoutSystem.a(client, out A_1_1, out A_2);
          this._resizer = new SplitLayoutResizer(this._dockContainer, splitLayoutSystem, A_1_1, A_2, client, DockingHints.TranslucentFill);
          this._resizer.Cancel += new EventHandler(this.OnResizeCancel);
          this._resizer.Commit += new SplitLayoutResizer.SplitResizeEventHandler(this.OnResizeCommit);
          this._dockContainer.Capture = true;
          return;
        }
        break;
      case ControlLayoutSystem _:
        ControlLayoutSystem controlLayoutSystem = (ControlLayoutSystem) layoutSystemAt;
        DockControl controlAt = controlLayoutSystem.GetControlAt(client);
        if (controlAt != null && controlAt._layoutSystem.SelectedControl != controlAt)
        {
          IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
          service2.OnComponentChanging((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"]);
          controlAt._layoutSystem.SelectedControl = controlAt;
          service2.OnComponentChanged((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"], (object) null, (object) null);
        }
        if (controlLayoutSystem._titleBarBounds.Contains(client) || controlAt != null)
        {
          if (controlLayoutSystem.SelectedControl != null)
          {
            object[] components = new object[1]
            {
              (object) controlLayoutSystem.SelectedControl
            };
            service1.SetSelectedComponents((ICollection) components, SelectionTypes.MouseDown | SelectionTypes.Click);
          }
          this._c = new Point(A_0, A_1);
          return;
        }
        object[] components1 = new object[1]
        {
          (object) this._dockContainer
        };
        service1.SetSelectedComponents((ICollection) components1, SelectionTypes.MouseDown | SelectionTypes.Click);
        this._dockContainer.Capture = true;
        return;
    }
    object[] components2 = new object[1]
    {
      (object) this._dockContainer
    };
    service1.SetSelectedComponents((ICollection) components2, SelectionTypes.MouseDown | SelectionTypes.Click);
  }

  private void Selecton_Changed(object A_0, EventArgs A_1)
  {
    foreach (LayoutSystemBase layoutSystem in this._dockContainer._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
        ((ControlLayoutSystem) layoutSystem).InvalidateTitleBar();
    }
  }

  private void OnResizeCommit(LayoutSystemBase A_0, LayoutSystemBase A_1, int A_2, int A_3)
  {
    SplitLayoutSystem splitLayoutSystem = this._resizer.d();
    this.DetachResizer();
    IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    service.OnComponentChanging((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"]);
    if (splitLayoutSystem.SplitMode == Orientation.Horizontal)
    {
      A_0._workingSize.Height = (float) A_2;
      A_1._workingSize.Height = (float) A_3;
    }
    else
    {
      A_0._workingSize.Width = (float) A_2;
      A_1._workingSize.Width = (float) A_3;
    }
    service.OnComponentChanged((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"], (object) null, (object) null);
    splitLayoutSystem.Resize();
  }

  private void DisposeDocker()
  {
    this._docker.Commit -= new StandardDocker.DockingManagerCommittedEventHandler(this.DockManager_Committed);
    this._docker.Cancel -= new EventHandler(this.DockManager_Canceled);
    this._docker = (StandardDocker) null;
  }

  protected override void Dispose(bool disposing)
  {
    ((ISelectionService) this.GetService(typeof (ISelectionService))).SelectionChanged -= new EventHandler(this.Selecton_Changed);
    base.Dispose(disposing);
  }

  private DockControl b(Point A_0)
  {
    LayoutSystemBase layoutSystemAt = this._dockContainer.GetLayoutSystemAt(A_0);
    return layoutSystemAt is ControlLayoutSystem ? ((ControlLayoutSystem) layoutSystemAt).GetControlAt(A_0) : (DockControl) null;
  }

  protected override void OnMouseDragMove(int x, int y)
  {
    Point client = this._dockContainer.PointToClient(new Point(x, y));
    if (this._resizer != null)
      this._resizer.Update(client);
    else if (this._docker != null)
    {
      this._docker.Update(Cursor.Position);
      if (this._docker.GetDockingSite() == null || this._docker.GetDockingSite()._redockType == StandardDocker.RedockType.None)
        Cursor.Current = Cursors.No;
      else
        Cursor.Current = Cursors.Default;
    }
    else
    {
      if (!(this._c != Point.Empty))
        return;
      Rectangle rectangle = new Rectangle(this._c, SystemInformation.DragSize);
      rectangle.Offset(-SystemInformation.DragSize.Width / 2, -SystemInformation.DragSize.Height / 2);
      if (rectangle.Contains(x, y))
        return;
      this.a(this._dockContainer.PointToClient(this._c));
      this._c = Point.Empty;
    }
  }

  private void Verb_AddControl(object A_0, EventArgs A_1) => this.AddDockControl();

  protected override void OnMouseEnter()
  {
    Point client = this._dockContainer.PointToClient(Cursor.Position);
    LayoutSystemBase layoutSystemAt = this._dockContainer.GetLayoutSystemAt(client);
    if (layoutSystemAt is SplitLayoutSystem && ((SplitLayoutSystem) layoutSystemAt).a(client.X, client.Y))
    {
      if (((SplitLayoutSystem) layoutSystemAt).SplitMode == Orientation.Horizontal)
        Cursor.Current = Cursors.HSplit;
      else
        Cursor.Current = Cursors.VSplit;
    }
    else
      Cursor.Current = Cursors.Default;
  }

  private void OnResizeCancel(object A_0, EventArgs A_1) => this.DetachResizer();

  internal virtual void DockManager_Canceled(object A_0, EventArgs A_1) => this.DisposeDocker();

  public void AddDockControl()
  {
    IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    ISelectionService service3 = (ISelectionService) this.GetService(typeof (ISelectionService));
    DesignerTransaction transaction = service2.CreateTransaction("Add Dock Control");
    ControlLayoutSystem layoutSystem = new ControlLayoutSystem();
    DockControl component = (DockControl) service2.CreateComponent(typeof (DockControl));
    ((ComponentDesigner) service2.GetDesigner((IComponent) component)).InitializeNewComponent((IDictionary) null);
    service1.OnComponentChanging((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"]);
    this._dockContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem);
    layoutSystem.Controls.Add(component);
    service1.OnComponentChanged((object) this._dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockContainer)["LayoutSystem"], (object) null, (object) null);
    transaction.Commit();
    object[] components = new object[1]
    {
      (object) component
    };
    service3.SetSelectedComponents((ICollection) components);
  }

  public override ICollection AssociatedComponents => (ICollection) this._dockContainer.Controls;

  public override DesignerVerbCollection Verbs => this._verbs;
}
