
// Type: Intermech.Bars.w
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class w : n
    {
      private Control _control;
      private Point _b;
      private int _c;
      private int _d;
      private bool _gragging;

      public w(PopupMenu popupMenu, Control control)
        : base(popupMenu)
      {
        this._control = (Control) null;
        this._gragging = false;
        this._control = control;
        popupMenu.AllowDrop = true;
        popupMenu.MouseMove += new MouseEventHandler(this.PopupMenu_MouseMove);
        popupMenu.MouseDown += new MouseEventHandler(this.PopupMenu_MouseDown);
        popupMenu.DragEnter += new DragEventHandler(this.PopupMenu_DragEnter);
        popupMenu.DragLeave += new EventHandler(this.PopupMenu_DragLeave);
        popupMenu.DragDrop += new DragEventHandler(this.PopupMenu_DragDrop);
        popupMenu.DragOver += new DragEventHandler(this.PopupMenu_DragOver);
        popupMenu.DoubleClick += new EventHandler(this.PopupMenu_DoubleClick);
        popupMenu.Paint += new PaintEventHandler(this.PopupMenu_Paint);
      }

      protected internal override Rectangle ConstraintArea() => this._control.ClientRectangle;

      private void a(Graphics A_0)
      {
        Rectangle rect = new Rectangle(2, this._d - 1, this.PopupMenu.ClientRectangle.Width - 4, 2);
        rect.Inflate(-3, 0);
        A_0.FillRectangle(SystemBrushes.ControlText, rect);
        A_0.DrawLine(SystemPens.ControlText, rect.X - 1, rect.Y - 2, rect.X - 1, rect.Y + 3);
        A_0.DrawLine(SystemPens.ControlText, rect.X, rect.Y - 1, rect.X, rect.Y + 2);
        A_0.DrawLine(SystemPens.ControlText, rect.Right, rect.Y - 2, rect.Right, rect.Y + 3);
        A_0.DrawLine(SystemPens.ControlText, rect.Right - 1, rect.Y - 1, rect.Right - 1, rect.Y + 2);
      }

      protected internal override Rectangle ModifyParentBounds(Rectangle A_0)
      {
        A_0 = new Rectangle(this._control.PointToClient(A_0.Location), A_0.Size);
        return A_0;
      }

      protected internal override bool ShouldHighlightItem(MenuButtonItem A_0)
      {
        ISelectionService serviceInternal = (ISelectionService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (ISelectionService));
        return A_0.PopupMenu != null || serviceInternal.GetComponentSelected((object) A_0);
      }

      private bool a(MenuItemBase A_0)
      {
        for (MenuItemBase menuItemBase = this.PopupMenu.MenuItem; menuItemBase.ParentMenu != null; menuItemBase = menuItemBase.ParentMenu)
        {
          if (menuItemBase == A_0)
            return true;
        }
        return false;
      }

      protected internal override void Show(ref int maximumMenuCount, MenuAnimation desiredAnimation)
      {
        Win32.SetWindowPos(this.PopupMenu.Handle, 0, 0, 0, 0, 0, 87);
      }

      private void PopupMenu_DoubleClick(object A_0, EventArgs A_1)
      {
        MenuItemBase itemAt = (MenuItemBase) this.PopupMenu.GetItemAt(this.PopupMenu.PointToClient(Cursor.Position));
        if (itemAt == null)
          return;
        ((IDesignerHost) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (IDesignerHost)))?.GetDesigner((IComponent) itemAt).DoDefaultAction();
      }

      private void PopupMenu_DragDrop(object A_0, DragEventArgs dea)
      {
        bool flag = dea.Effect == DragDropEffects.Move;
        if (!dea.Data.GetDataPresent(typeof (MenuButtonItem[]).FullName))
          return;
        MenuButtonItem[] data = (MenuButtonItem[]) dea.Data.GetData(typeof (MenuButtonItem[]).FullName);
        MenuItemBase parentMenu = data[0].ParentMenu;
        int c = this._c;
        if (parentMenu == this.PopupMenu.MenuItem & flag)
        {
          foreach (MenuButtonItem menuButtonItem in data)
          {
            if (parentMenu.Items.IndexOf((ToolbarItemBase) menuButtonItem) < this._c)
              --c;
          }
        }
        IDesignerHost serviceInternal1 = (IDesignerHost) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (IDesignerHost));
        ISelectionService serviceInternal2 = (ISelectionService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (ISelectionService));
        IComponentChangeService serviceInternal3 = (IComponentChangeService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (IComponentChangeService));
        DesignerTransaction designerTransaction = !flag ? serviceInternal1.CreateTransaction("Copy Menu Items") : serviceInternal1.CreateTransaction("Move Menu Items");
        if (flag)
        {
          serviceInternal3.OnComponentChanging((object) parentMenu, (MemberDescriptor) TypeDescriptor.GetProperties((object) parentMenu)["Items"]);
          foreach (MenuButtonItem menuButtonItem in data)
            parentMenu.Items.Remove((ToolbarItemBase) menuButtonItem);
          serviceInternal3.OnComponentChanged((object) parentMenu, (MemberDescriptor) TypeDescriptor.GetProperties((object) parentMenu)["Items"], (object) null, (object) null);
        }
        if (!flag)
        {
          ToolbarItemBaseDesigner.InsertingItem = true;
          for (int index = 0; index < data.Length; ++index)
          {
            data[index] = (MenuButtonItem) data[index].CloneItem();
            w.a((MenuItemBase) data[index], serviceInternal1);
            serviceInternal1.Container.Add((IComponent) data[index]);
          }
          ToolbarItemBaseDesigner.InsertingItem = false;
        }
        serviceInternal3.OnComponentChanging((object) this.PopupMenu.MenuItem, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.PopupMenu.MenuItem)["Items"]);
        for (int index = data.Length - 1; index >= 0; --index)
          this.PopupMenu.MenuItem.Items.Insert(c, (ToolbarItemBase) data[index]);
        serviceInternal3.OnComponentChanged((object) this.PopupMenu.MenuItem, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.PopupMenu.MenuItem)["Items"], (object) null, (object) null);
        designerTransaction.Commit();
        this._gragging = false;
        this.PopupMenu.Invalidate();
        object[] components = new object[1]
        {
          (object) this.PopupMenu.MenuItem
        };
        serviceInternal2.SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
      }

      private void PopupMenu_MouseMove(object A_0, MouseEventArgs A_1)
      {
        Rectangle rectangle = this.PopupMenu.ChevronItem.ButtonBounds;
        if (rectangle.Contains(A_1.X, A_1.Y))
          this.PopupMenu.Cursor = Cursors.Hand;
        else
          this.PopupMenu.Cursor = Cursors.Default;
        if (A_1.Button != MouseButtons.Left)
          return;
        rectangle = new Rectangle(this._b.X, this._b.Y, SystemInformation.DragSize.Width, SystemInformation.DragSize.Height);
        rectangle.Offset(-(SystemInformation.DragSize.Width / 2), -(SystemInformation.DragSize.Height / 2));
        if (rectangle.Contains(A_1.X, A_1.Y))
          return;
        ISelectionService serviceInternal = (ISelectionService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (ISelectionService));
        foreach (object selectedComponent in (IEnumerable) serviceInternal.GetSelectedComponents())
        {
          if (!(selectedComponent is MenuButtonItem))
          {
            this._b = Point.Empty;
            return;
          }
          if (!this.PopupMenu.MenuItem.Items.Contains((ToolbarItemBase) selectedComponent))
          {
            this._b = Point.Empty;
            return;
          }
        }
        ArrayList arrayList = new ArrayList();
        foreach (MenuButtonItem component in (CollectionBase) this.PopupMenu.MenuItem.Items)
        {
          if (serviceInternal.GetComponentSelected((object) component))
            arrayList.Add((object) component);
        }
        MenuButtonItem[] data = new MenuButtonItem[arrayList.Count];
        arrayList.CopyTo((Array) data);
        int num = (int) this.PopupMenu.DoDragDrop((object) data, DragDropEffects.Copy | DragDropEffects.Move);
      }

      private void PopupMenu_Paint(object A_0, PaintEventArgs A_1)
      {
        if (!this._gragging)
          return;
        this.a(A_1.Graphics);
      }

      public static void a(MenuItemBase A_0, IDesignerHost A_1)
      {
        if (!A_0.HasChildren)
          return;
        foreach (MenuButtonItem A_0_1 in (CollectionBase) A_0.Items)
        {
          A_1.Container.Add((IComponent) A_0_1);
          w.a((MenuItemBase) A_0_1, A_1);
        }
      }

      protected internal override bool AllowLowImportanceMenuItems() => false;

      private void PopupMenu_DragLeave(object A_0, EventArgs A_1)
      {
        if (!this._gragging)
          return;
        this._gragging = false;
        this.PopupMenu.Invalidate();
      }

      private void PopupMenu_DragOver(object A_0, DragEventArgs A_1)
      {
        if (!A_1.Data.GetDataPresent(typeof (MenuButtonItem[]).FullName))
          return;
        foreach (MenuItemBase A_0_1 in (MenuButtonItem[]) A_1.Data.GetData(typeof (MenuButtonItem[]).FullName))
        {
          if (this.a(A_0_1))
            return;
        }
        MenuButtonItem itemAt = this.PopupMenu.GetItemAt(this.PopupMenu.PointToClient(new Point(A_1.X, A_1.Y)));
        if (itemAt != null && this.PopupMenu.MenuItem.Items.Contains((ToolbarItemBase) itemAt))
          ((ISelectionService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (ISelectionService))).SetSelectedComponents((ICollection) new object[1]
          {
            (object) itemAt
          }, SelectionTypes.Replace);
        A_1.Effect = (A_1.KeyState & 8) != 8 ? DragDropEffects.Move : DragDropEffects.Copy;
        this._b = this.PopupMenu.PointToClient(new Point(A_1.X, A_1.Y));
        this._c = 0;
        this._d = 5;
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.PopupMenu.MenuItem.Items)
        {
          int y1 = this._b.Y;
          Rectangle buttonBounds = menuButtonItem.ButtonBounds;
          int y2 = buttonBounds.Y;
          buttonBounds = menuButtonItem.ButtonBounds;
          int num1 = buttonBounds.Height / 2;
          int num2 = y2 + num1;
          if (y1 < num2)
          {
            this._c = this.PopupMenu.MenuItem.Items.IndexOf((ToolbarItemBase) menuButtonItem);
            buttonBounds = menuButtonItem.ButtonBounds;
            this._d = buttonBounds.Y - 1;
            break;
          }
          this._c = this.PopupMenu.MenuItem.Items.IndexOf((ToolbarItemBase) menuButtonItem) + 1;
          buttonBounds = menuButtonItem.ButtonBounds;
          this._d = buttonBounds.Bottom;
        }
        this.PopupMenu.Invalidate();
      }

      private void PopupMenu_MouseDown(object A_0, MouseEventArgs mea)
      {
        IDesignerHost serviceInternal1 = (IDesignerHost) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (IDesignerHost));
        MenuButtonItem itemAt = this.PopupMenu.GetItemAt(new Point(mea.X, mea.Y));
        if (itemAt == this.PopupMenu.ChevronItem)
        {
          serviceInternal1?.GetDesigner((IComponent) this.PopupMenu.MenuItem)?.Verbs[0].Invoke();
        }
        else
        {
          if (itemAt == null)
            return;
          ISelectionService serviceInternal2 = (ISelectionService) this.PopupMenu.Host.ToolBar.GetServiceInternal(typeof (ISelectionService));
          if (serviceInternal2.GetComponentSelected((object) itemAt))
            return;
          object[] components = new object[1]{ (object) itemAt };
          serviceInternal2.SetSelectedComponents((ICollection) components, SelectionTypes.MouseDown | SelectionTypes.Click);
          this._b = new Point(mea.X, mea.Y);
          this.PopupMenu.Invalidate();
        }
      }

      private void PopupMenu_DragEnter(object A_0, DragEventArgs A_1) => this._gragging = true;
    }
}
