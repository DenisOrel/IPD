
// Type: Intermech.Docking.DockManagerDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

[Serializable]
internal class DockManagerDesigner : ComponentDesigner
{
  private DockManager _dockManager;
  private DesignerVerbCollection _verbs;

  public DockManagerDesigner()
  {
    this._dockManager = (DockManager) null;
    this._verbs = new DesignerVerbCollection();
    this._verbs.Add(new DesignerVerb("Add Control", new EventHandler(this.AddControlVerb_Click)));
  }

  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    if (!(service1.RootComponent is Control rootComponent))
      return;
    service2.OnComponentChanging((object) rootComponent, (MemberDescriptor) null);
    this.CreateContainer(service1, rootComponent, DockStyle.Left, "leftDock");
    this.CreateContainer(service1, rootComponent, DockStyle.Right, "rightDock");
    this.CreateContainer(service1, rootComponent, DockStyle.Bottom, "bottomDock");
    this.CreateContainer(service1, rootComponent, DockStyle.Top, "topDock");
    service2.OnComponentChanged((object) rootComponent, (MemberDescriptor) null, (object) null, (object) null);
    this.SendToBack(service1, "leftBarDock");
    this.SendToBack(service1, "rightBarDock");
    this.SendToBack(service1, "bottomBarDock");
    this.SendToBack(service1, "topBarDock");
    this.AddControlVerb_Click((object) null, (EventArgs) null);
  }

  protected override void Dispose(bool disposing)
  {
    ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving -= new ComponentEventHandler(this.Component_Removing);
    base.Dispose(disposing);
  }

  private void SendToBack(IDesignerHost host, string name)
  {
    IComponent component = host.Container.Components[name];
    if (!(component is Control))
      return;
    ((Control) component).SendToBack();
  }

  private void Component_Removing(object sender, ComponentEventArgs cea)
  {
    if (cea.Component != this._dockManager)
      return;
    IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    DockContainer[] dockContainerArray = new DockContainer[this._dockManager._dockContainers.Count];
    this._dockManager._dockContainers.CopyTo((Array) dockContainerArray);
    foreach (DockContainer dockContainer in dockContainerArray)
    {
      Control parent = dockContainer.Parent;
      if (parent != null)
      {
        service.OnComponentChanging((object) parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) parent)["Controls"]);
        dockContainer.Dispose();
        service.OnComponentChanged((object) parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) parent)["Controls"], (object) null, (object) null);
      }
    }
  }

  private void AddControlVerb_Click(object A_0, EventArgs A_1)
  {
    IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    DockContainer dockContainer = this._dockManager.GetDockContainer(DockStyle.Right);
    if (dockContainer == null)
      return;
    ((DockContainerDesigner) service.GetDesigner((IComponent) dockContainer)).AddDockControl();
  }

  private DockContainer CreateContainer(IDesignerHost A_0, Control A_1, DockStyle A_2, string A_3)
  {
    DockContainer container = new DockContainer();
    container.Manager = this._dockManager;
    container.Size = new Size(0, 0);
    container.Dock = A_2;
    A_0.Container.Add((IComponent) container, A_3);
    A_1.Controls.Add((Control) container);
    container.SendToBack();
    return container;
  }

  public override void Initialize(IComponent component)
  {
    base.Initialize(component);
    ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving += new ComponentEventHandler(this.Component_Removing);
    this._dockManager = (DockManager) component;
  }

  public override ICollection AssociatedComponents
  {
    get => (ICollection) this._dockManager._dockContainers;
  }

  public override DesignerVerbCollection Verbs => this._verbs;
}
