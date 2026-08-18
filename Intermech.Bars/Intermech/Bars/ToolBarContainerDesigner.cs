
// Type: Intermech.Bars.ToolBarContainerDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Bars
{
    [Serializable]
    internal class ToolBarContainerDesigner : ParentControlDesigner
    {
      private ToolBarContainer _a;
      private DesignerVerbCollection _b;

      public ToolBarContainerDesigner()
      {
        this._b = new DesignerVerbCollection();
        this._b.Add(new DesignerVerb("Add ToolBar", new EventHandler(this.a)));
        this._b.Add(new DesignerVerb("Add ContainerBar", new EventHandler(this.c)));
        this._b.Add(new DesignerVerb("Add MenuBar", new EventHandler(this.b)));
      }

      internal MenuBar a()
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        if (this._a.Manager != null && this._a.Manager.MenuBar != null)
          throw new InvalidOperationException("Only one MenuBar should be added to each toolbar layout.");
        MenuBar component = (MenuBar) service1.CreateComponent(typeof (MenuBar));
        (service1.GetDesigner((IComponent) component) as ComponentDesigner).OnSetComponentDefaults();
        service2.OnComponentChanging((object) this._a, (MemberDescriptor) null);
        this._a.SuspendLayout();
        this._a.Controls.Add((Control) component);
        this._a.Controls.SetChildIndex((Control) component, 0);
        this._a.ResumeLayout();
        service2.OnComponentChanged((object) this._a, (MemberDescriptor) null, (object) null, (object) null);
        return component;
      }

      public override void Initialize(IComponent A_0)
      {
        base.Initialize(A_0);
        this._a = (ToolBarContainer) A_0;
      }

      public void a(System.Type A_0)
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        ToolBar component = (ToolBar) service1.CreateComponent(A_0);
        ((ComponentDesigner) service1.GetDesigner((IComponent) component)).OnSetComponentDefaults();
        component.DockLine = this._a.GetNextFreeDockLine();
        service2.OnComponentChanging((object) this._a, (MemberDescriptor) null);
        this._a.Controls.Add((Control) component);
        service2.OnComponentChanged((object) this._a, (MemberDescriptor) null, (object) null, (object) null);
      }

      public override bool CanParent(Control A_0) => A_0 is ToolBar;

      public override bool CanParent(ControlDesigner A_0) => A_0.Control is ToolBar;

      public void a(object A_0, EventArgs A_1) => this.a(typeof (ToolBar));

      public void b(object A_0, EventArgs A_1) => this.a();

      public void c(object A_0, EventArgs A_1) => this.a(typeof (ContainerBar));

      public override SelectionRules SelectionRules => SelectionRules.Visible | SelectionRules.Locked;

      public override DesignerVerbCollection Verbs => this._b;
    }
}
