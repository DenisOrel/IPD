
// Type: Intermech.Docking.DocumentContainerDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;


namespace Intermech.Docking;

[Serializable]
internal class DocumentContainerDesigner : DockContainerDesigner
{
  private DesignerVerbCollection _verbs;
  private DocumentContainer _documentContainer;

  public DocumentContainerDesigner()
  {
    this._documentContainer = (DocumentContainer) null;
    this._verbs = new DesignerVerbCollection();
    this._verbs.Add(new DesignerVerb("Add Document", new EventHandler(this.AddVerb_Execute)));
  }

  public override void Initialize(IComponent A_0)
  {
    base.Initialize(A_0);
    this._documentContainer = (DocumentContainer) A_0;
  }

  protected override bool GetHitTest(Point pos)
  {
    pos = this._documentContainer.PointToClient(pos);
    LayoutSystemBase layoutSystemAt = this._documentContainer.GetLayoutSystemAt(pos);
    if (layoutSystemAt is DocumentLayoutSystem)
    {
      DocumentLayoutSystem documentLayoutSystem = (DocumentLayoutSystem) layoutSystemAt;
      if (documentLayoutSystem._scrollLeftButton._bounds.Contains(pos) || documentLayoutSystem._scrollRightButton._bounds.Contains(pos))
        return true;
    }
    return base.GetHitTest(pos);
  }

  private void AddVerb_Execute(object A_0, EventArgs A_1)
  {
    IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    ISelectionService service3 = (ISelectionService) this.GetService(typeof (ISelectionService));
    DesignerTransaction transaction = service2.CreateTransaction("Add Document");
    ControlLayoutSystem layoutSystem = this._documentContainer.GetLayoutSystem(this._documentContainer.LayoutSystem);
    if (layoutSystem == null)
    {
      layoutSystem = (ControlLayoutSystem) new DocumentLayoutSystem();
      service1.OnComponentChanging((object) this._documentContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._documentContainer)["LayoutSystem"]);
      this._documentContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem);
      service1.OnComponentChanged((object) this._documentContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._documentContainer)["LayoutSystem"], (object) null, (object) null);
    }
    DockControl component = (DockControl) service2.CreateComponent(typeof (DockControl));
    service1.OnComponentChanging((object) this._documentContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._documentContainer)["LayoutSystem"]);
    layoutSystem.Controls.Add(component);
    service1.OnComponentChanged((object) this._documentContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._documentContainer)["LayoutSystem"], (object) null, (object) null);
    transaction.Commit();
    object[] components = new object[1]
    {
      (object) component
    };
    service3.SetSelectedComponents((ICollection) components);
  }

  public override DesignerVerbCollection Verbs => this._verbs;
}
