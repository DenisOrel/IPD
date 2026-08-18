
// Type: Intermech.Bars.TopLevelMenuItemDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Serializable]
    internal class TopLevelMenuItemDesigner : MenuItemDesigner
    {
      private TopLevelMenuItemBase _item;
      private ArrayList _b;

      public TopLevelMenuItemDesigner() => this._b = new ArrayList();

      [Obsolete]
      public override void OnSetComponentDefaults()
      {
        base.OnSetComponentDefaults();
        this._item.Text = "Menu";
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
          ISelectionService service2 = (ISelectionService) this.GetService(typeof (ISelectionService));
          service1.ComponentRemoving -= new ComponentEventHandler(this.ComponentChangeService_ComponentRemoving);
          EventHandler eventHandler = new EventHandler(this.SelectionService_SelectionChanged);
          service2.SelectionChanged -= eventHandler;
        }
        base.Dispose(disposing);
      }

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        this._item = (TopLevelMenuItemBase) component;
        IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        ISelectionService service2 = (ISelectionService) this.GetService(typeof (ISelectionService));
        service1.ComponentRemoving += new ComponentEventHandler(this.ComponentChangeService_ComponentRemoving);
        EventHandler eventHandler = new EventHandler(this.SelectionService_SelectionChanged);
        service2.SelectionChanged += eventHandler;
      }

      private bool a(MenuItemBase A_0)
      {
        foreach (MenuItemBase A_0_1 in (CollectionBase) this._item.Items)
        {
          if (A_0_1 == A_0 || this.a(A_0_1, A_0))
            return true;
        }
        return false;
      }

      private void ComponentChangeService_ComponentRemoving(object A_0, ComponentEventArgs A_1)
      {
        if (A_1.Component != this._item || this._b.Count == 0)
          return;
        this.b();
      }

      private void SelectionService_SelectionChanged(object A_0, EventArgs A_1)
      {
        ISelectionService service = (ISelectionService) this.GetService(typeof (ISelectionService));
        bool flag;
        if (service.PrimarySelection is MenuItemDesigner.MenuItemWrapper)
        {
          MenuItemBase A_0_1 = (service.PrimarySelection as MenuItemDesigner.MenuItemWrapper).GetItem();
          flag = A_0_1 == this._item || this.a(A_0_1);
        }
        else
          flag = service.PrimarySelection is MenuItemBase && (service.PrimarySelection == this._item || this.a((MenuItemBase) service.PrimarySelection));
        if (this._b.Count != 0 && !flag)
        {
          this.b();
          if (this._item.ToolBar == null)
            return;
          this._item.ToolBar.p = (TopLevelMenuItemBase) null;
        }
        else
        {
          if (!flag)
            return;
          if (this._item.ToolBar != null)
            this._item.ToolBar.p = this._item;
          this.c();
        }
      }

      private void a(MenuItemBase A_0, Control A_1)
      {
        if (A_0 == this._item)
          this.a((TopLevelMenuItemBase) A_0, A_1);
        else
          this.a(A_0, A_0.ParentMenu, A_1);
        this._b.Insert(0, (object) A_0);
      }

      private bool a(MenuItemBase A_0, MenuItemBase A_1)
      {
        if (A_0.HasChildren)
        {
          foreach (MenuItemBase A_0_1 in (CollectionBase) A_0.Items)
          {
            if (A_0_1 == A_1 || this.a(A_0_1, A_1))
              return true;
          }
        }
        return false;
      }

      private void a(TopLevelMenuItemBase topMenu, Control A_1)
      {
        PopupMenu popupMenu = topMenu.CreatePopupMenu((IPopupMenuHost) this._item.ToolBar);
        popupMenu.a(A_1);
        topMenu.PopupMenu = popupMenu;
        Win32.SetParent(popupMenu.Handle, A_1.Handle);
        popupMenu.CalcMenuSize(false);
        popupMenu.ShowMenu(MenuAnimation.None);
      }

      private void a(MenuItemBase A_0, MenuItemBase A_1, Control A_2)
      {
        PopupMenu popupMenu = A_0.CreatePopupMenu((IPopupMenuHost) this._item.ToolBar);
        popupMenu.a(A_2);
        A_0.PopupMenu = popupMenu;
        Win32.SetParent(popupMenu.Handle, A_2.Handle);
        popupMenu.CalcMenuSize(true);
        popupMenu.ShowMenu(MenuAnimation.None);
      }

      private void b()
      {
        while (this._b.Count != 0)
        {
          this.b((MenuItemBase) this._b[0]);
          this._b.RemoveAt(0);
        }
      }

      private void b(MenuItemBase A_0) => A_0.HidePopupMenu();

      private void c()
      {
        ISelectionService service1 = (ISelectionService) this.GetService(typeof (ISelectionService));
        IDesignerHost service2 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        MenuItemBase A_0_1 = !(service1.PrimarySelection is MenuItemDesigner.MenuItemWrapper) ? (MenuItemBase) service1.PrimarySelection : (service1.PrimarySelection as MenuItemDesigner.MenuItemWrapper).GetItem();
        if (!(service2.RootComponent is Control))
          return;
        Control parent = ((Control) service2.RootComponent).Parent;
        this.c(A_0_1);
        ArrayList arrayList = new ArrayList();
        MenuItemBase menuItemBase1 = A_0_1;
        do
        {
          arrayList.Insert(0, (object) menuItemBase1);
          menuItemBase1 = menuItemBase1.ParentMenu;
        }
        while (menuItemBase1 != null);
        arrayList.RemoveAt(arrayList.Count - 1);
        foreach (MenuItemBase A_0_2 in arrayList)
        {
          if (A_0_2.PopupMenu == null)
            this.a(A_0_2, parent);
          A_0_2.PopupMenu.Invalidate();
        }
        if (A_0_1.PopupMenu == null)
          this.a(A_0_1, parent);
        MenuItemBase menuItemBase2 = (MenuItemBase) this._b[0];
        menuItemBase2.HighlightedItem = (MenuButtonItem) null;
        menuItemBase2.PopupMenu.Invalidate();
      }

      private void c(MenuItemBase A_0)
      {
        while (this._b.Count != 0 && this._b[0] != A_0 && this._b[0] != A_0.ParentMenu)
        {
          this.b((MenuItemBase) this._b[0]);
          this._b.RemoveAt(0);
        }
      }
    }
}
