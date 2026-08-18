
// Type: Intermech.Docking.Designers.TabControlDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design;


namespace Intermech.Docking.Designers;

[Serializable]
internal class TabControlDesigner : ParentControlDesigner
{
  private DesignerVerbCollection _verbs;
  private TabControl _tabControl;

  private void AddNewTab(object sender, EventArgs e)
  {
    IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    DesignerTransaction transaction = service1.CreateTransaction("Add Tab");
    try
    {
      Tab component = (Tab) service1.CreateComponent(typeof (Tab));
      ((ComponentDesigner) service1.GetDesigner((IComponent) component)).InitializeNewComponent((IDictionary) null);
      service2.OnComponentChanging((object) this._tabControl, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._tabControl)["Controls"]);
      this._tabControl.Tabs.Add(component);
      service2.OnComponentChanged((object) this._tabControl, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._tabControl)["Controls"], (object) null, (object) null);
    }
    finally
    {
      transaction.Commit();
    }
  }

  public TabControlDesigner()
  {
    this._verbs = new DesignerVerbCollection(new DesignerVerb[1]
    {
      new DesignerVerb("Add &Tab", new EventHandler(this.AddNewTab))
    });
  }

  protected override bool GetHitTest(Point point)
  {
    ISelectionService service = (ISelectionService) this.GetService(typeof (ISelectionService));
    Point client = this.Control.PointToClient(point);
    IComponent component = this.Component;
    return service.GetComponentSelected((object) component) && this._tabControl.TabStripBounds.Contains(client) || base.GetHitTest(point);
  }

  public override void Initialize(IComponent component)
  {
    base.Initialize(component);
    if (!(component is TabControl))
      DockLanguage.ShowCachedAssemblyError(component.GetType().Assembly, this.GetType().Assembly);
    this._tabControl = (TabControl) component;
  }

  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    this.AddNewTab((object) null, (EventArgs) null);
  }

  public override DesignerVerbCollection Verbs => this._verbs;
}
