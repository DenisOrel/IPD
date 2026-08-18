
// Type: Intermech.ButtonsPanel.ButtonsPanelDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.ButtonsPanel
{
    [Serializable]
    internal class ButtonsPanelDesigner : ParentControlDesigner
    {
      private Intermech.ButtonsPanel.ButtonsPanel _panel;

      protected override void Dispose(bool disposing)
      {
        ISelectionService service1 = (ISelectionService) this.GetService(typeof (ISelectionService));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        service1.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
        ComponentEventHandler componentEventHandler = new ComponentEventHandler(this.OnComponentRemoving);
        service2.ComponentRemoving -= componentEventHandler;
        base.Dispose(disposing);
      }

      protected override bool GetHitTest(Point point)
      {
        return this._panel.GetButtonAt(this._panel.PointToClient(point)) != null;
      }

      private void OnSelectionChanged(object sender, EventArgs e) => this._panel.Invalidate();

      private void OnComponentRemoving(object sender, ComponentEventArgs e)
      {
        IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        if (e.Component is PanelButton)
        {
          PanelButton component = (PanelButton) e.Component;
          service1.OnComponentChanging((object) this._panel, (MemberDescriptor) null);
          this._panel.Buttons.Remove(component);
          service1.OnComponentChanged((object) this._panel, (MemberDescriptor) null, (object) null, (object) null);
        }
        else
        {
          if (e.Component != this._panel)
            return;
          int count = this._panel.Buttons.Count;
          System.ComponentModel.Component[] componentArray = new System.ComponentModel.Component[this._panel.Buttons.Count];
          for (int Index = 0; Index < count; ++Index)
            componentArray[Index] = (System.ComponentModel.Component) this._panel.Buttons[Index];
          for (int index = 0; index < count; ++index)
          {
            System.ComponentModel.Component component = componentArray[index];
            service1.OnComponentChanging((object) this._panel, (MemberDescriptor) null);
            service2.DestroyComponent((IComponent) component);
            service1.OnComponentChanged((object) this._panel, (MemberDescriptor) null, (object) null, (object) null);
          }
        }
      }

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        this._panel = (Intermech.ButtonsPanel.ButtonsPanel) component;
        ISelectionService service1 = (ISelectionService) this.GetService(typeof (ISelectionService));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        service1.SelectionChanged += new EventHandler(this.OnSelectionChanged);
        ComponentEventHandler componentEventHandler = new ComponentEventHandler(this.OnComponentRemoving);
        service2.ComponentRemoving += componentEventHandler;
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 512 /*0x0200*/)
        {
          IntPtr num = m.LParam;
          int int32 = num.ToInt32();
          Point p = new Point(int32 % 65536 /*0x010000*/, int32 / 65536 /*0x010000*/);
          if (this.GetHitTest(this._panel.PointToScreen(p)))
          {
            num = m.WParam;
            if (num.ToInt32() == 1)
              this._panel.DoMouseMove(new MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0));
            else
              this._panel.DoMouseMove(new MouseEventArgs(MouseButtons.None, 1, p.X, p.Y, 0));
          }
          else
            base.WndProc(ref m);
        }
        else
          base.WndProc(ref m);
      }

      private void OnAddButton(object sender, EventArgs e)
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        DesignerTransaction transaction = service1.CreateTransaction("Add Button");
        service2.OnComponentChanging((object) this._panel, (MemberDescriptor) null);
        this._panel.Buttons.Add((PanelButton) service1.CreateComponent(typeof (PanelButton)));
        service2.OnComponentChanged((object) this._panel, (MemberDescriptor) null, (object) null, (object) null);
        transaction.Commit();
      }

      public override ICollection AssociatedComponents => (ICollection) this._panel.Buttons;

      public override DesignerVerbCollection Verbs
      {
        get
        {
          return new DesignerVerbCollection()
          {
            new DesignerVerb("&Add Button", new EventHandler(this.OnAddButton))
          };
        }
      }
    }
}
