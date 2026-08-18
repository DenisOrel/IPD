
// Type: Intermech.Docking.DockControlDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Docking;

[Serializable]
internal class DockControlDesigner : ParentControlDesigner
{
  private DesignerVerbCollection _verbs;
  private DockControl _dockControl;

  public DockControlDesigner()
  {
    this._verbs = (DesignerVerbCollection) null;
    this._dockControl = (DockControl) null;
    this._verbs = new DesignerVerbCollection();
    this._verbs.Add(new DesignerVerb("Add Control", new EventHandler(this.OnAddControl)));
    this._verbs.Add(new DesignerVerb("Insert Control (Top)", new EventHandler(this.OnInsertControl)));
    this._verbs.Add(new DesignerVerb("Insert Control (Bottom)", new EventHandler(this.OnInsertControl)));
    this._verbs.Add(new DesignerVerb("Insert Control (Left)", new EventHandler(this.OnInsertControl)));
    this._verbs.Add(new DesignerVerb("Insert Control (Right)", new EventHandler(this.OnInsertControl)));
  }

  protected override void Dispose(bool disposing)
  {
    IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    this._dockControl.ControlAdded -= new ControlEventHandler(this.Control_AddedRemoved);
    this._dockControl.ControlRemoved -= new ControlEventHandler(this.Control_AddedRemoved);
    ComponentEventHandler componentEventHandler = new ComponentEventHandler(this.Component_Removing);
    service.ComponentRemoving -= componentEventHandler;
    base.Dispose(disposing);
  }

  public override void Initialize(IComponent comp)
  {
    base.Initialize(comp);
    IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    this._dockControl = (DockControl) comp;
    ComponentEventHandler componentEventHandler = new ComponentEventHandler(this.Component_Removing);
    service.ComponentRemoving += componentEventHandler;
    this._dockControl.ControlAdded += new ControlEventHandler(this.Control_AddedRemoved);
    this._dockControl.ControlRemoved += new ControlEventHandler(this.Control_AddedRemoved);
  }

  protected override void OnPaintAdornments(PaintEventArgs A_0)
  {
    base.OnPaintAdornments(A_0);
    if (this._dockControl.BorderStyle == Intermech.Docking.Rendering.BorderStyle.None)
    {
      using (Pen pen = new Pen(SystemColors.ControlDark))
      {
        pen.DashStyle = DashStyle.Dash;
        Rectangle clientRectangle = this._dockControl.ClientRectangle;
        --clientRectangle.Width;
        --clientRectangle.Height;
        A_0.Graphics.DrawRectangle(pen, clientRectangle);
      }
    }
    if (this._dockControl.Controls.Count != 0)
      return;
    using (Font font = new Font("Tahoma", 6.75f))
    {
      Rectangle clientRectangle = this._dockControl.ClientRectangle;
      clientRectangle.Inflate(-10, -10);
      using (StringFormat format = new StringFormat(StringFormat.GenericDefault))
      {
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        A_0.Graphics.DrawString("To redock controls, click and drag their tabs or titlebars to other locations on your form.", font, SystemBrushes.ControlDarkDark, (RectangleF) clientRectangle, format);
      }
    }
  }

  private void Component_Removing(object A_0, ComponentEventArgs A_1)
  {
    if (A_1.Component != this._dockControl || this._dockControl._layoutSystem == null || this._dockControl._layoutSystem.DockContainer == null)
      return;
    IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    DockContainer dockContainer = this._dockControl._layoutSystem.DockContainer;
    service.OnComponentChanging((object) dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) dockContainer)["LayoutSystem"]);
    DockHelper.DetachDockControl(this._dockControl);
    service.OnComponentChanged((object) dockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) dockContainer)["LayoutSystem"], (object) null, (object) null);
  }

  private void OnAddControl(object A_0, EventArgs A_1)
  {
    IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    ISelectionService service3 = (ISelectionService) this.GetService(typeof (ISelectionService));
    DesignerTransaction transaction = service2.CreateTransaction("Add Dock Control");
    DockControl component = (DockControl) service2.CreateComponent(typeof (DockControl));
    ((ComponentDesigner) service2.GetDesigner((IComponent) component)).InitializeNewComponent((IDictionary) null);
    service1.OnComponentChanging((object) this._dockControl._layoutSystem.DockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockControl._layoutSystem.DockContainer)["LayoutSystem"]);
    this._dockControl._layoutSystem.Controls.Add(component);
    this._dockControl._layoutSystem.SelectedControl = component;
    service1.OnComponentChanged((object) this._dockControl._layoutSystem.DockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockControl._layoutSystem.DockContainer)["LayoutSystem"], (object) null, (object) null);
    transaction.Commit();
    object[] components = new object[1]
    {
      (object) component
    };
    service3.SetSelectedComponents((ICollection) components);
  }

  private void Control_AddedRemoved(object A_0, ControlEventArgs A_1)
  {
    this._dockControl.Invalidate();
  }

  private void OnInsertControl(object A_0, EventArgs A_1)
  {
    IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
    IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
    ISelectionService service3 = (ISelectionService) this.GetService(typeof (ISelectionService));
    DesignerVerb designerVerb = (DesignerVerb) A_0;
    DesignerTransaction transaction = service2.CreateTransaction("Insert Dock Control");
    DockControl component = (DockControl) service2.CreateComponent(typeof (DockControl));
    ((ComponentDesigner) service2.GetDesigner((IComponent) component)).InitializeNewComponent((IDictionary) null);
    ControlLayoutSystem newLayoutSystem = this._dockControl._layoutSystem.CreateNewLayoutSystem((int) this._dockControl._layoutSystem._workingSize.Width, (int) this._dockControl._layoutSystem._workingSize.Height, new DockControl[1]
    {
      component
    }, component);
    DockSide side = DockSide.Top;
    string text;
    if ((text = designerVerb.Text) != null)
    {
      string str = string.IsInterned(text);
      if (str != "Insert Control (Bottom)")
      {
        switch (str)
        {
          case "Insert Control (Left)":
            side = DockSide.Left;
            break;
          case "Insert Control (Right)":
            side = DockSide.Right;
            break;
        }
      }
      else
        side = DockSide.Bottom;
    }
    service1.OnComponentChanging((object) this._dockControl._layoutSystem.DockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockControl._layoutSystem.DockContainer)["LayoutSystem"]);
    this._dockControl._layoutSystem.SplitForLayoutSystem((LayoutSystemBase) newLayoutSystem, side);
    service1.OnComponentChanged((object) this._dockControl._layoutSystem.DockContainer, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._dockControl._layoutSystem.DockContainer)["LayoutSystem"], (object) null, (object) null);
    transaction.Commit();
  }

  public override SelectionRules SelectionRules => SelectionRules.Visible;

  public override DesignerVerbCollection Verbs => this._verbs;
}
