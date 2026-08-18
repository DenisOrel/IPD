
// Type: Intermech.Bars.ToolBarDesignerKeyboardHelper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;


namespace Intermech.Bars
{
    internal class ToolBarDesignerKeyboardHelper
    {
      private IServiceProvider _serviceProvider;
      private MenuCommand _b;
      private MenuCommand _c;
      private MenuCommand _d;
      private MenuCommand _e;
      private MenuCommand _f;
      private MenuCommand g;
      private MenuCommand h;
      private MenuCommand i;
      private MenuCommand j;
      private MenuCommand k;
      private IMenuCommandService l;
      private ISelectionService _selectionService;
      private IComponent n;

      public ToolBarDesignerKeyboardHelper(IServiceProvider serviceProvider)
      {
        this.l = (IMenuCommandService) null;
        this._selectionService = (ISelectionService) null;
        this._serviceProvider = serviceProvider;
        this.n = ((IDesignerHost) serviceProvider.GetService(typeof (IDesignerHost))).RootComponent;
        this.n.Disposed += new EventHandler(this.a);
        this.g = new MenuCommand(new EventHandler(this.OnKeyCancel), MenuCommands.KeyCancel);
        this.a(ref this._b, this.g, MenuCommands.KeyCancel);
        this.h = new MenuCommand(new EventHandler(this.OnKeyMoveUp), MenuCommands.KeyMoveUp);
        this.a(ref this._c, this.h, MenuCommands.KeyMoveUp);
        this.i = new MenuCommand(new EventHandler(this.OnKeyMoveDown), MenuCommands.KeyMoveDown);
        this.a(ref this._d, this.i, MenuCommands.KeyMoveDown);
        this.j = new MenuCommand(new EventHandler(this.OnKeyMoveLeft), MenuCommands.KeyMoveLeft);
        this.a(ref this._e, this.j, MenuCommands.KeyMoveLeft);
        this.k = new MenuCommand(new EventHandler(this.OnKeyMoveRight), MenuCommands.KeyMoveRight);
        this.a(ref this._f, this.k, MenuCommands.KeyMoveRight);
      }

      private ISelectionService GetSelectionService()
      {
        if (this._selectionService == null)
          this._selectionService = (ISelectionService) this._serviceProvider.GetService(typeof (ISelectionService));
        return this._selectionService;
      }

      private bool a(bool A_0)
      {
        bool flag = false;
        if (this.GetSelectionService().PrimarySelection is MenuItemBase)
        {
          MenuItemBase primarySelection = (MenuItemBase) this.GetSelectionService().PrimarySelection;
          if (A_0)
          {
            if (primarySelection.ParentMenu == null)
            {
              object[] components = new object[1]
              {
                (object) this.a(primarySelection, A_0)
              };
              this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            }
            else if (primarySelection.HasChildren)
            {
              object[] components = new object[1]
              {
                (object) primarySelection.Items[0]
              };
              this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            }
            else
              (((IDesignerHost) this._serviceProvider.GetService(typeof (IDesignerHost))).GetDesigner((IComponent) primarySelection) as MenuItemDesigner).TemplateSelected = true;
            return true;
          }
          if (A_0)
            return flag;
          if (primarySelection.ParentMenu != null && !(primarySelection.ParentMenu is TopLevelMenuItemBase))
          {
            object[] components = new object[1]
            {
              (object) primarySelection.ParentMenu
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
          }
          else
          {
            object[] components = new object[1]
            {
              (object) this.a(primarySelection, A_0)
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
          }
          return true;
        }
        if (this.GetSelectionService().PrimarySelection is MenuItemDesigner.MenuItemWrapper)
        {
          MenuItemDesigner.MenuItemWrapper primarySelection = (MenuItemDesigner.MenuItemWrapper) this.GetSelectionService().PrimarySelection;
          if (A_0)
          {
            object[] components = new object[1]
            {
              (object) this.a(primarySelection.GetItem(), A_0)
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            return flag;
          }
          if (primarySelection.GetItem() is TopLevelMenuItemBase)
          {
            object[] components = new object[1]
            {
              (object) this.a(primarySelection.GetItem(), A_0)
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            return flag;
          }
          object[] components1 = new object[1]
          {
            (object) primarySelection.GetItem()
          };
          this.GetSelectionService().SetSelectedComponents((ICollection) components1, SelectionTypes.Replace);
        }
        return flag;
      }

      private void a(object A_0, EventArgs A_1)
      {
        this.b().RemoveCommand(this.g);
        this.b().RemoveCommand(this.h);
        this.b().RemoveCommand(this.i);
        this.b().RemoveCommand(this.j);
        this.b().RemoveCommand(this.k);
        this.n.Disposed -= new EventHandler(this.a);
        this.n = (IComponent) null;
      }

      private MenuItemBase a(MenuItemBase A_0, bool A_1)
      {
        ToolBar A_1_1;
        TopLevelMenuItemBase A_2;
        this.a(A_0, out A_1_1, out A_2);
        TopLevelMenuItemBase[] topLevelMenuItems = A_1_1.TopLevelMenuItems;
        int num = Array.IndexOf<TopLevelMenuItemBase>(topLevelMenuItems, A_2);
        int index = !A_1 ? num - 1 : num + 1;
        if (index == topLevelMenuItems.Length)
          index = 0;
        else if (index == -1)
          index = topLevelMenuItems.Length - 1;
        return (MenuItemBase) topLevelMenuItems[index];
      }

      private void a(ref MenuCommand A_0, MenuCommand A_1, CommandID A_2)
      {
        A_0 = this.b().FindCommand(A_2);
        if (A_0 == null)
          return;
        this.b().RemoveCommand(A_0);
        this.b().AddCommand(A_1);
      }

      private void a(MenuItemBase A_0, out ToolBar A_1, out TopLevelMenuItemBase A_2)
      {
        while (A_0.ParentMenu != null)
          A_0 = A_0.ParentMenu;
        A_1 = A_0.ToolBar;
        A_2 = (TopLevelMenuItemBase) A_0;
      }

      private IMenuCommandService b()
      {
        if (this.l == null)
          this.l = (IMenuCommandService) this._serviceProvider.GetService(typeof (IMenuCommandService));
        return this.l;
      }

      private bool b(bool A_0)
      {
        bool flag = false;
        if (this.GetSelectionService().PrimarySelection is MenuItemBase)
        {
          MenuItemBase primarySelection = (MenuItemBase) this.GetSelectionService().PrimarySelection;
          if (primarySelection.ParentMenu != null)
          {
            int num = primarySelection.ParentMenu.Items.IndexOf((ToolbarItemBase) primarySelection);
            int index = !A_0 ? num - 1 : num + 1;
            if (index == primarySelection.ParentMenu.Items.Count)
              (((IDesignerHost) this._serviceProvider.GetService(typeof (IDesignerHost))).GetDesigner((IComponent) primarySelection.ParentMenu) as MenuItemDesigner).TemplateSelected = true;
            else if (index == -1)
            {
              object[] components = new object[1]
              {
                (object) primarySelection.ParentMenu
              };
              this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            }
            else
            {
              object[] components = new object[1]
              {
                (object) primarySelection.ParentMenu.Items[index]
              };
              this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            }
            return true;
          }
          if (primarySelection is TopLevelMenuItemBase)
          {
            int count = !A_0 ? primarySelection.Items.Count : 0;
            if (count < 0 || count >= primarySelection.Items.Count)
            {
              (((IDesignerHost) this._serviceProvider.GetService(typeof (IDesignerHost))).GetDesigner((IComponent) primarySelection) as MenuItemDesigner).TemplateSelected = true;
              return flag;
            }
            object[] components = new object[1]
            {
              (object) primarySelection.Items[count]
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
          }
          return flag;
        }
        if (this.GetSelectionService().PrimarySelection is MenuItemDesigner.MenuItemWrapper)
        {
          MenuItemDesigner.MenuItemWrapper primarySelection = (MenuItemDesigner.MenuItemWrapper) this.GetSelectionService().PrimarySelection;
          if (A_0 || !primarySelection.GetItem().HasChildren)
          {
            object[] components = new object[1]
            {
              (object) primarySelection.GetItem()
            };
            this.GetSelectionService().SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
            return flag;
          }
          object[] components1 = new object[1]
          {
            (object) primarySelection.GetItem().Items[primarySelection.GetItem().Items.Count - 1]
          };
          this.GetSelectionService().SetSelectedComponents((ICollection) components1, SelectionTypes.Replace);
        }
        return flag;
      }

      private void OnKeyMoveRight(object A_0, EventArgs A_1)
      {
        if (this.a(true))
          return;
        this._f.Invoke();
      }

      private void OnKeyMoveLeft(object A_0, EventArgs A_1)
      {
        if (this.a(false))
          return;
        this._e.Invoke();
      }

      private void OnKeyMoveDown(object A_0, EventArgs A_1)
      {
        if (this.b(true))
          return;
        this._d.Invoke();
      }

      private void OnKeyMoveUp(object A_0, EventArgs A_1)
      {
        if (this.b(false))
          return;
        this._c.Invoke();
      }

      private void OnKeyCancel(object A_0, EventArgs A_1) => this._b.Invoke();
    }
}
