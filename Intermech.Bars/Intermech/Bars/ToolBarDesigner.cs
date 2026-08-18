
// Type: Intermech.Bars.ToolBarDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Bars
{
    [Serializable]
    internal class ToolBarDesigner : ControlDesigner
    {
      private ToolBar _toolbar;
      private DesignerVerbCollection _verbs;
      private DesignerTransaction _transaction;
      private bool _dragging;
      private Point _dragPoint;
      private int _f;
      private int _g;
      private bool _h;
      private IDesignerHost _designerHost;
      private ISelectionService _selectionService;
      private IComponentChangeService _componentChangeService;

      public ToolBarDesigner()
      {
        this._transaction = (DesignerTransaction) null;
        this._dragging = false;
        this._f = 0;
        this._g = 0;
        this._h = false;
        this._designerHost = (IDesignerHost) null;
        this._selectionService = (ISelectionService) null;
        this._componentChangeService = (IComponentChangeService) null;
      }

      private void AddTypedVerbs(DesignerVerbCollection A_0)
      {
        foreach (System.Type designableType in this.DesignableTypes)
          A_0.Add((DesignerVerb) new TypedDesignerVerb("Add &" + designableType.Name, designableType, new EventHandler(this.Verb_Execute)));
      }

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        this._designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        this._selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
        this._componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        this._toolbar = (ToolBar) component;
        this._componentChangeService.ComponentAdding += new ComponentEventHandler(this.ChangeService_ComponentAdding);
        this._componentChangeService.ComponentAdded += new ComponentEventHandler(this.ChangeService_ComponentAdded);
        this._componentChangeService.ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
        this._componentChangeService.ComponentRemoved += new ComponentEventHandler(this.ChangeService_ComponentRemoved);
        if ((ToolBarDesignerKeyboardHelper) this.GetService(typeof (ToolBarDesignerKeyboardHelper)) == null)
        {
          ToolBarDesignerKeyboardHelper serviceInstance = new ToolBarDesignerKeyboardHelper((System.IServiceProvider) this.Component.Site);
          try
          {
            this._designerHost.AddService(typeof (ToolBarDesignerKeyboardHelper), (object) serviceInstance);
          }
          catch
          {
          }
        }
        this.EnableDragDrop(true);
      }

      private void PaintHorAdornments(Graphics g)
      {
        Rectangle rect;
        if (this._f < this._toolbar.Items.Count)
        {
          Rectangle buttonBounds = this._toolbar.Items[this._f].ButtonBounds;
          rect = new Rectangle(buttonBounds.Left - 1, buttonBounds.Top, 2, buttonBounds.Height);
        }
        else
        {
          Rectangle buttonBounds = this._toolbar.Items[this._f - 1].ButtonBounds;
          rect = new Rectangle(buttonBounds.Right, buttonBounds.Top, 2, buttonBounds.Height);
        }
        rect.Inflate(0, -3);
        g.FillRectangle(SystemBrushes.ControlText, rect);
        g.DrawLine(SystemPens.ControlText, rect.X - 2, rect.Y, rect.X + 3, rect.Y);
        g.DrawLine(SystemPens.ControlText, rect.X - 1, rect.Y + 1, rect.X + 2, rect.Y + 1);
        g.DrawLine(SystemPens.ControlText, rect.X - 2, rect.Bottom, rect.X + 3, rect.Bottom);
        g.DrawLine(SystemPens.ControlText, rect.X - 1, rect.Bottom - 1, rect.X + 2, rect.Bottom - 1);
      }

      protected override void WndProc(ref Message msg)
      {
        switch (msg.Msg)
        {
          case 515:
            if (!(this._selectionService.PrimarySelection is ToolbarItemBase))
            {
              base.WndProc(ref msg);
              break;
            }
            this.d();
            break;
          case 516:
            ToolbarItemBase itemAt = this._toolbar.GetItemAt(this._toolbar.PointToClient(Cursor.Position));
            if (itemAt == null)
            {
              base.WndProc(ref msg);
              break;
            }
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) itemAt
            }, SelectionTypes.MouseDown | SelectionTypes.Click);
            break;
          default:
            base.WndProc(ref msg);
            break;
        }
      }

      private void AddItem(System.Type type)
      {
        DesignerTransaction designerTransaction = this._designerHost.CreateTransaction("Add Item");
        try
        {
          if (!this.IsToolBarOrItemSelected)
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) this._toolbar
            }, SelectionTypes.Replace);
          (this._designerHost.GetDesigner(this._designerHost.CreateComponent(type)) as ComponentDesigner).OnSetComponentDefaults();
        }
        catch
        {
          designerTransaction.Cancel();
          designerTransaction = (DesignerTransaction) null;
        }
        finally
        {
          designerTransaction?.Commit();
        }
      }

      protected override void OnPaintAdornments(PaintEventArgs pe)
      {
        if (this._dragging)
        {
          if (this._toolbar.Flow == ToolBarLayout.Vertical)
            this.PaintVertAdornments(pe.Graphics);
          else
            this.PaintHorAdornments(pe.Graphics);
        }
        if (this._toolbar == null)
          return;
        foreach (ToolbarItemBase component in (CollectionBase) this._toolbar.Items)
        {
          if (component is ControlContainerItem)
          {
            DrawItemState state = DrawItemState.Default;
            if (this._selectionService.GetComponentSelected((object) component))
              state |= DrawItemState.HotLight;
            if (!component.Enabled)
              state |= DrawItemState.Disabled;
            ((ControlContainerItem) component).DrawDesignTimeControl(this._toolbar.WorkingRenderer, pe.Graphics, state);
          }
          else if (component is ButtonItemBase)
          {
            ButtonItemBase buttonItemBase = (ButtonItemBase) component;
            if (buttonItemBase.Text.Length == 0 && buttonItemBase.Image == null && buttonItemBase.Icon == null && (buttonItemBase.ImageList == null || buttonItemBase.ImageIndex < 0 || buttonItemBase.ImageIndex > buttonItemBase.ImageList.Images.Count - 1))
            {
              using (Pen pen = new Pen(SystemColors.ControlDark))
              {
                pen.DashStyle = DashStyle.Dot;
                pe.Graphics.DrawRectangle(pen, component.ButtonBounds);
              }
            }
          }
        }
      }

      public virtual void a(ToolbarItemBase toolbarItem)
      {
        bool flag = true;
        if (toolbarItem is MenuItemBase)
          flag = toolbarItem is TopLevelMenuItemBase;
        if (!flag)
          return;
        this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"]);
        this._toolbar.Items.Add(toolbarItem);
        this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"], (object) null, (object) null);
      }

      private void ChangeService_ComponentAdded(object A_0, ComponentEventArgs A_1)
      {
        if (!(A_1.Component is ToolbarItemBase) || !this._h)
          return;
        ToolbarItemBase component = (ToolbarItemBase) A_1.Component;
        this._h = false;
        try
        {
          this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"]);
          if (this._selectionService.PrimarySelection is ToolbarItemBase && ((ToolbarItemBase) this._selectionService.PrimarySelection).ToolBar == this._toolbar)
            this._toolbar.Items.Insert(this._toolbar.Items.IndexOf((ToolbarItemBase) this._selectionService.PrimarySelection), component);
          else
            this._toolbar.Items.Add(component);
        }
        finally
        {
          this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"], (object) null, (object) null);
        }
        if (this._transaction == null)
          return;
        this._transaction.Commit();
        this._transaction = (DesignerTransaction) null;
      }

      internal void Verb_Execute(object sender, EventArgs e)
      {
        this.AddItem(((TypedDesignerVerb) sender).Type);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._componentChangeService.ComponentAdding -= new ComponentEventHandler(this.ChangeService_ComponentAdding);
          this._componentChangeService.ComponentAdded -= new ComponentEventHandler(this.ChangeService_ComponentAdded);
          this._componentChangeService.ComponentRemoving -= new ComponentEventHandler(this.ChangeService_ComponentRemoving);
          this._componentChangeService.ComponentRemoved -= new ComponentEventHandler(this.ChangeService_ComponentRemoved);
          this._designerHost = (IDesignerHost) null;
          this._selectionService = (ISelectionService) null;
          this._componentChangeService = (IComponentChangeService) null;
        }
        base.Dispose(disposing);
      }

      private void PaintVertAdornments(Graphics g)
      {
        Rectangle rect;
        if (this._f < this._toolbar.Items.Count)
        {
          Rectangle buttonBounds = this._toolbar.Items[this._f].ButtonBounds;
          rect = new Rectangle(buttonBounds.Left, buttonBounds.Top - 1, buttonBounds.Width, 2);
        }
        else
        {
          Rectangle buttonBounds = this._toolbar.Items[this._f - 1].ButtonBounds;
          rect = new Rectangle(buttonBounds.Left, buttonBounds.Bottom, buttonBounds.Width, 2);
        }
        rect.Inflate(-3, 0);
        g.FillRectangle(SystemBrushes.ControlText, rect);
        g.DrawLine(SystemPens.ControlText, rect.X - 1, rect.Y - 2, rect.X - 1, rect.Y + 3);
        g.DrawLine(SystemPens.ControlText, rect.X, rect.Y - 1, rect.X, rect.Y + 2);
        g.DrawLine(SystemPens.ControlText, rect.Right, rect.Y - 2, rect.Right, rect.Y + 3);
        g.DrawLine(SystemPens.ControlText, rect.Right - 1, rect.Y - 1, rect.Right - 1, rect.Y + 2);
      }

      protected override void OnMouseDragBegin(int x, int y)
      {
        Point client = this._toolbar.PointToClient(new Point(x, y));
        ToolbarItemBase itemAt = this._toolbar.GetItemAt(client);
        if (itemAt != null)
        {
          this._selectionService.SetSelectedComponents((ICollection) new object[1]
          {
            (object) itemAt
          }, SelectionTypes.MouseDown | SelectionTypes.Click);
          this._dragPoint = new Point(x, y);
        }
        else if (this._toolbar.DrawActionsButton && this._toolbar.ActionsButton.ButtonBounds.Contains(client))
          this.AddItem(typeof (ButtonItem));
        else
          base.OnMouseDragBegin(x, y);
      }

      protected override void OnMouseDragEnd(bool A_0)
      {
        this._toolbar.Capture = false;
        if (this._toolbar.GetItemAt(this._toolbar.PointToClient(Cursor.Position)) != null)
          return;
        this._dragPoint = ControlDesigner.InvalidPoint;
        base.OnMouseDragEnd(A_0);
      }

      protected override void OnDragEnter(DragEventArgs A_0) => this._dragging = true;

      protected override void OnMouseDragMove(int x, int y)
      {
        if (this._dragPoint != ControlDesigner.InvalidPoint)
        {
          Rectangle rectangle;
          ref Rectangle local1 = ref rectangle;
          int x1 = this._dragPoint.X;
          int y1 = this._dragPoint.Y;
          Size dragSize = SystemInformation.DragSize;
          int width = dragSize.Width;
          dragSize = SystemInformation.DragSize;
          int height = dragSize.Height;
          local1 = new Rectangle(x1, y1, width, height);
          ref Rectangle local2 = ref rectangle;
          dragSize = SystemInformation.DragSize;
          int x2 = -(dragSize.Width / 2);
          int y2 = -(SystemInformation.DragSize.Height / 2);
          local2.Offset(x2, y2);
          if (rectangle.Contains(x, y))
            return;
          foreach (IComponent selectedComponent in (IEnumerable) this._selectionService.GetSelectedComponents())
          {
            if (!(selectedComponent is ToolbarItemBase))
            {
              this._dragPoint = ControlDesigner.InvalidPoint;
              return;
            }
            if (!this._toolbar.Items.Contains((ToolbarItemBase) selectedComponent))
            {
              this._dragPoint = ControlDesigner.InvalidPoint;
              return;
            }
          }
          ToolBarDesigner.ItemArrayList data = new ToolBarDesigner.ItemArrayList();
          foreach (ToolbarItemBase component in (CollectionBase) this._toolbar.Items)
          {
            if (this._selectionService.GetComponentSelected((object) component))
              data.Add((object) component);
          }
          int num = (int) this._toolbar.DoDragDrop((object) data, DragDropEffects.Copy | DragDropEffects.Move);
        }
        else
          base.OnMouseDragMove(x, y);
      }

      protected string deToString(DragEventArgs e)
      {
        return $"\nAllow : {e.AllowedEffect.ToString()}\nEff   : {(object) e.Effect}\nX     : {(object) e.X}\nY     : {(object) e.Y}";
      }

      protected override void OnDragOver(DragEventArgs de)
      {
        base.OnDragOver(de);
        ToolbarItemBase itemAt = this._toolbar.GetItemAt(this._toolbar.PointToClient(new Point(de.X, de.Y)));
        if (itemAt is TopLevelMenuItemBase && de.Data.GetDataPresent(typeof (MenuButtonItem[]).FullName))
        {
          this._selectionService.SetSelectedComponents((ICollection) new object[1]
          {
            (object) itemAt
          }, SelectionTypes.Replace);
        }
        else
        {
          if (!de.Data.GetDataPresent(typeof (ToolBarDesigner.ItemArrayList).FullName))
            return;
          de.Effect = (Control.ModifierKeys & Keys.Control) != Keys.Control ? DragDropEffects.Move : DragDropEffects.Copy;
          this._dragPoint = this._toolbar.PointToClient(new Point(de.X, de.Y));
          this._f = 0;
          this._g = 5;
          foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._toolbar.Items)
          {
            if (this._toolbar.Flow == ToolBarLayout.Vertical)
            {
              int y1 = this._dragPoint.Y;
              Rectangle buttonBounds = toolbarItemBase.ButtonBounds;
              int y2 = buttonBounds.Y;
              buttonBounds = toolbarItemBase.ButtonBounds;
              int num1 = buttonBounds.Height / 2;
              int num2 = y2 + num1;
              if (y1 < num2)
              {
                this._f = this._toolbar.Items.IndexOf(toolbarItemBase);
                this._g = toolbarItemBase.ButtonBounds.Y - 1;
                break;
              }
              this._f = this._toolbar.Items.IndexOf(toolbarItemBase) + 1;
              this._g = toolbarItemBase.ButtonBounds.Bottom;
            }
            else
            {
              int x1 = this._dragPoint.X;
              Rectangle buttonBounds = toolbarItemBase.ButtonBounds;
              int x2 = buttonBounds.X;
              buttonBounds = toolbarItemBase.ButtonBounds;
              int num3 = buttonBounds.Width / 2;
              int num4 = x2 + num3;
              if (x1 < num4)
              {
                this._f = this._toolbar.Items.IndexOf(toolbarItemBase);
                this._g = toolbarItemBase.ButtonBounds.X - 1;
                break;
              }
              this._f = this._toolbar.Items.IndexOf(toolbarItemBase) + 1;
              this._g = toolbarItemBase.ButtonBounds.Right;
            }
          }
          this._toolbar.Invalidate();
        }
      }

      protected override void OnDragDrop(DragEventArgs dea)
      {
        bool flag = dea.Effect == DragDropEffects.Move;
        if (!dea.Data.GetDataPresent(typeof (ToolBarDesigner.ItemArrayList).FullName))
          return;
        ToolBarDesigner.ItemArrayList data = (ToolBarDesigner.ItemArrayList) dea.Data.GetData(typeof (ToolBarDesigner.ItemArrayList).FullName);
        if (((ToolbarItemBase) data[0]).ToolBar == null)
          return;
        ToolBar toolBar = ((ToolbarItemBase) data[0]).ToolBar;
        int f = this._f;
        if (toolBar == this._toolbar & flag)
        {
          foreach (ToolbarItemBase toolbarItemBase in (ArrayList) data)
          {
            if (this._toolbar.Items.IndexOf(toolbarItemBase) < this._f)
              --f;
          }
        }
        DesignerTransaction designerTransaction = !flag ? this._designerHost.CreateTransaction("Copy Buttons") : this._designerHost.CreateTransaction("Move Buttons");
        if (flag)
        {
          this._componentChangeService.OnComponentChanging((object) toolBar, (MemberDescriptor) TypeDescriptor.GetProperties((object) toolBar)["Items"]);
          foreach (ToolbarItemBase toolbarItemBase in (ArrayList) data)
            toolBar.Items.Remove(toolbarItemBase);
          this._componentChangeService.OnComponentChanged((object) toolBar, (MemberDescriptor) TypeDescriptor.GetProperties((object) toolBar)["Items"], (object) null, (object) null);
        }
        if (!flag)
        {
          ToolbarItemBaseDesigner.InsertingItem = true;
          for (int index = 0; index < data.Count; ++index)
          {
            data[index] = (object) ((ToolbarItemBase) data[index]).CloneItem();
            if (data[index] is MenuItemBase)
              w.a((MenuItemBase) data[index], this._designerHost);
            this._designerHost.Container.Add((IComponent) data[index]);
          }
          ToolbarItemBaseDesigner.InsertingItem = false;
        }
        this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"]);
        for (int index = data.Count - 1; index >= 0; --index)
          this._toolbar.Items.Insert(f, (ToolbarItemBase) data[index]);
        this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"], (object) null, (object) null);
        designerTransaction.Commit();
        this._selectionService.SetSelectedComponents((ICollection) data, SelectionTypes.Replace);
        this._dragging = false;
        this._toolbar.Invalidate();
      }

      protected override void OnDragLeave(EventArgs A_0)
      {
        if (!this._dragging)
          return;
        this._dragging = false;
        this._toolbar.Invalidate();
      }

      private void ChangeService_ComponentRemoved(object A_0, ComponentEventArgs A_1)
      {
        if (!(A_1.Component is ToolbarItemBase) || ((ToolbarItemBase) A_1.Component).ToolBar != this._toolbar)
          return;
        ToolbarItemBase component = (ToolbarItemBase) A_1.Component;
        try
        {
          if (!this._toolbar.Items.Contains(component))
            return;
          this._toolbar.Items.Remove(component);
          this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"], (object) null, (object) null);
        }
        finally
        {
          if (this._transaction != null)
          {
            this._transaction.Commit();
            this._transaction = (DesignerTransaction) null;
          }
        }
      }

      private void ChangeService_ComponentRemoving(object A_0, ComponentEventArgs A_1)
      {
        if (!(A_1.Component is ToolbarItemBase))
          return;
        if (((ToolbarItemBase) A_1.Component).ToolBar != this._toolbar)
          return;
        try
        {
          this._transaction = this._designerHost.CreateTransaction("Remove Item");
          this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._toolbar)["Items"]);
        }
        catch
        {
          if (this._transaction == null)
            return;
          this._transaction.Cancel();
          this._transaction = (DesignerTransaction) null;
        }
      }

      private void d()
      {
        ToolbarItemBase primarySelection = (ToolbarItemBase) this._selectionService.PrimarySelection;
        switch (primarySelection)
        {
          case ButtonItem _ when ((ButtonItem) primarySelection).BuddyMenu != null:
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) ((ButtonItem) primarySelection).BuddyMenu
            }, SelectionTypes.Replace);
            this._designerHost.GetDesigner((IComponent) ((ButtonItem) primarySelection).BuddyMenu).DoDefaultAction();
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) primarySelection
            });
            break;
          case ButtonItemBase _:
            this._designerHost.GetDesigner((IComponent) primarySelection).DoDefaultAction();
            break;
        }
      }

      private void ChangeService_ComponentAdding(object A_0, ComponentEventArgs A_1)
      {
        if (!(A_1.Component is ToolbarItemBase) || !ToolBar.ToolBarItemCollection.IsComponentSuitableForToolBar((ToolbarItemBase) A_1.Component) || ToolbarItemBaseDesigner.InsertingItem || this._h || !this.IsToolBarOrItemSelected)
          return;
        this._h = true;
        if (this._transaction != null)
          return;
        this._transaction = this._designerHost.CreateTransaction("Add Item");
      }

      protected override void OnSetCursor()
      {
        if (this._toolbar.DrawActionsButton)
        {
          if (this._toolbar.ActionsButton.ButtonBounds.Contains(this._toolbar.PointToClient(Cursor.Position)))
            Cursor.Current = Cursors.Hand;
          else
            base.OnSetCursor();
        }
        else
          base.OnSetCursor();
      }

      public override ICollection AssociatedComponents => (ICollection) this._toolbar.Items;

      protected virtual System.Type[] DesignableTypes
      {
        get
        {
          return new System.Type[4]
          {
            typeof (ButtonItem),
            typeof (ComboBoxItem),
            typeof (DropDownMenuItem),
            typeof (LabelItem)
          };
        }
      }

      private bool IsToolBarOrItemSelected
      {
        get
        {
          object primarySelection = this._selectionService.PrimarySelection;
          if (primarySelection == this._toolbar)
            return true;
          return primarySelection is ToolbarItemBase && ((ToolbarItemBase) primarySelection).ToolBar == this._toolbar;
        }
      }

      public override SelectionRules SelectionRules
      {
        get
        {
          if (this._toolbar.Situation == ToolBarSituation.Contained)
            return SelectionRules.Moveable | SelectionRules.Visible;
          return this._toolbar.Flow == ToolBarLayout.Horizontal ? SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.LeftSizeable | SelectionRules.RightSizeable : SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
        }
      }

      public override DesignerVerbCollection Verbs
      {
        get
        {
          if (this._verbs == null)
          {
            this._verbs = new DesignerVerbCollection();
            this.AddTypedVerbs(this._verbs);
          }
          return this._verbs;
        }
      }

      private class ItemArrayList : ArrayList
      {
      }
    }
}
