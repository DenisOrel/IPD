
// Type: Intermech.NavBars.NavBarDesigner
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


namespace Intermech.NavBars
{
    [Serializable]
    internal class NavBarDesigner : ParentControlDesigner
    {
      private DesignerVerbCollection _a;
      private NavigationBar _b;

      public NavBarDesigner()
      {
        this._b = (NavigationBar) null;
        this._a = new DesignerVerbCollection();
        this._a.Add(new DesignerVerb("Add Navigation &Pane", new EventHandler(this.AddNewPaneClick)));
      }

      public override void Initialize(IComponent A_0)
      {
        base.Initialize(A_0);
        this._b = (NavigationBar) A_0;
      }

      protected override void WndProc(ref Message A_0)
      {
        if (A_0.Msg == 516)
        {
          ISelectionService service = (ISelectionService) this.GetService(typeof (ISelectionService));
          Point client = this._b.PointToClient(Cursor.Position);
          NavigationPane paneAt = this._b.GetPaneAt(client.X, client.Y);
          if (paneAt != null)
          {
            object[] components = new object[1]
            {
              (object) paneAt
            };
            service.SetSelectedComponents((ICollection) components, SelectionTypes.MouseDown | SelectionTypes.Click);
          }
          else
            base.WndProc(ref A_0);
        }
        else
          base.WndProc(ref A_0);
      }

      protected override void OnMouseDragEnd(bool A_0)
      {
        Point client = this._b.PointToClient(Cursor.Position);
        if (this._b.GetPaneAt(client.X, client.Y) != null)
          return;
        base.OnMouseDragEnd(A_0);
      }

      public override bool CanParent(Control A_0) => A_0 is NavigationPane;

      public override bool CanParent(ControlDesigner A_0) => A_0 is NavigationPaneDesigner;

      protected override void OnMouseDragBegin(int A_0, int A_1)
      {
        ISelectionService service1 = (ISelectionService) this.GetService(typeof (ISelectionService));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        Point client = this._b.PointToClient(new Point(A_0, A_1));
        NavigationPane paneAt = this._b.GetPaneAt(client.X, client.Y);
        if (paneAt != null)
        {
          service2.OnComponentChanging((object) this._b, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._b)["SelectedPane"]);
          this._b.SelectedPane = paneAt;
          service2.OnComponentChanged((object) this._b, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._b)["SelectedPane"], (object) null, (object) null);
          object[] components = new object[1]{ (object) paneAt };
          service1.SetSelectedComponents((ICollection) components, SelectionTypes.MouseDown | SelectionTypes.Click);
        }
        else
          base.OnMouseDragBegin(A_0, A_1);
      }

      private void AddNewPaneClick(object A_0, EventArgs A_1)
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        DesignerTransaction transaction = service1.CreateTransaction("Add Navigation Pane");
        service2.OnComponentChanging((object) this._b, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._b)["Controls"]);
        this._b.Controls.Add((Control) service1.CreateComponent(typeof (NavigationPane)));
        service2.OnComponentChanged((object) this._b, (MemberDescriptor) TypeDescriptor.GetProperties((object) this._b)["Controls"], (object) null, (object) null);
        transaction.Commit();
      }

      public override DesignerVerbCollection Verbs => this._a;
    }
}
