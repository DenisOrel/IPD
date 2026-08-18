
// Type: Intermech.Bars.BarManagerDesigner
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


namespace Intermech.Bars
{
    [Serializable]
    internal class BarManagerDesigner : ComponentDesigner
    {
      private BarManager _a;
      private DesignerVerbCollection _verbs;

      public BarManagerDesigner()
      {
        this._verbs = new DesignerVerbCollection();
        this._verbs.Add(new DesignerVerb("Add ToolBar", new EventHandler(this.OnAddNewToolbar)));
        this._verbs.Add(new DesignerVerb("Add MenuBar", new EventHandler(this.OnAddNewMenubar)));
      }

      private void a()
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IUIService service2 = (IUIService) this.GetService(typeof (IUIService));
        IEventBindingService service3 = (IEventBindingService) this.GetService(typeof (IEventBindingService));
        MainMenu mainMenu = (MainMenu) null;
        bool flag = false;
        foreach (IComponent component in (ReadOnlyCollectionBase) service1.Container.Components)
        {
          if (component is MainMenu)
            mainMenu = (MainMenu) component;
          if (component is ContextMenu)
            flag = true;
        }
        if (mainMenu == null && !flag || service2.ShowMessage($"The designer can automatically copy your existing menu system in to the new menu structure. All items on your MainMenu will be copied as well as all ContextMenus.{Environment.NewLine}{Environment.NewLine}Would you like to do this?", "Menu Conversion", MessageBoxButtons.YesNo) == DialogResult.No)
        {
          this.c();
        }
        else
        {
          MenuBar menuBar = this.c();
          menuBar.Items.Clear();
          bool A_4 = service2.ShowMessage("Would you also like to copy your menu names to the new menu structure, and rename your old menu items?", "Menu Conversion", MessageBoxButtons.YesNo) == DialogResult.Yes;
          if (mainMenu != null)
          {
            foreach (MenuItem menuItem in mainMenu.MenuItems)
            {
              MenuBarItem component = (MenuBarItem) service1.CreateComponent(typeof (MenuBarItem));
              component.Text = menuItem.Text;
              component.Checked = menuItem.Checked;
              component.Enabled = menuItem.Enabled;
              component.Visible = menuItem.Visible;
              if (A_4)
              {
                try
                {
                  string name = menuItem.Site.Name;
                  menuItem.Site.Name = "old_" + name;
                  component.Site.Name = name;
                }
                catch
                {
                }
              }
              menuBar.Items.Add((ToolbarItemBase) component);
              this.a((Menu) menuItem, (MenuItemBase) component, service1, service3, A_4);
            }
          }
          ArrayList arrayList = new ArrayList();
          foreach (IComponent component in (ReadOnlyCollectionBase) service1.Container.Components)
          {
            if (component is ContextMenu)
              arrayList.Add((object) component);
          }
          foreach (ContextMenu A_0 in arrayList)
          {
            MenuBarItem component = (MenuBarItem) service1.CreateComponent(typeof (ContextMenuBarItem));
            component.Text = $"({A_0.Site.Name})";
            TypeDescriptor.GetProperties((object) component)["Visible"].SetValue((object) component, (object) false);
            if (A_4)
            {
              try
              {
                string name = A_0.Site.Name;
                A_0.Site.Name = "old_" + name;
                component.Site.Name = name;
              }
              catch
              {
              }
            }
            menuBar.Items.Add((ToolbarItemBase) component);
            this.a((Menu) A_0, (MenuItemBase) component, service1, service3, A_4);
          }
        }
      }

      protected override void Dispose(bool disposing)
      {
        ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving -= new ComponentEventHandler(this.a);
        base.Dispose(disposing);
      }

      private void a(object A_0, ComponentEventArgs A_1)
      {
        if (A_1.Component != this._a)
          return;
        IComponentChangeService service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        ToolBarContainer[] toolBarContainerArray = new ToolBarContainer[this._a._containers.Count];
        this._a._containers.CopyTo((Array) toolBarContainerArray);
        foreach (ToolBarContainer toolBarContainer in toolBarContainerArray)
        {
          Control parent = toolBarContainer.Parent;
          if (parent != null)
          {
            service.OnComponentChanging((object) parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) parent)["Controls"]);
            toolBarContainer.Dispose();
            service.OnComponentChanged((object) parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) parent)["Controls"], (object) null, (object) null);
          }
        }
      }

      private void OnAddNewToolbar(object sender, EventArgs e)
      {
        IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        ToolBarContainer toolBarContainer = this._a.FindSuitableContainer(DockStyle.Top) ?? throw new InvalidOperationException("There is no Top Container associated with this toolbar layout.");
        ((ToolBarContainerDesigner) service.GetDesigner((IComponent) toolBarContainer))?.a((object) null, (EventArgs) null);
      }

      private void a(MenuItem A_0, MenuButtonItem A_1, IEventBindingService A_2)
      {
        EventDescriptor e1 = TypeDescriptor.GetEvents((object) A_0)["Click"];
        PropertyDescriptor eventProperty = A_2.GetEventProperty(e1);
        if (eventProperty == null)
          return;
        object obj = eventProperty.GetValue((object) A_0);
        if (obj == null)
          return;
        string str = (string) obj;
        EventDescriptor e2 = TypeDescriptor.GetEvents((object) A_1)["Click"];
        A_2.GetEventProperty(e2)?.SetValue((object) A_1, (object) str);
      }

      private ToolBarContainer a(IDesignerHost A_0, Control A_1, DockStyle A_2, string A_3)
      {
        ToolBarContainer toolBarContainer = new ToolBarContainer();
        toolBarContainer.Manager = this._a;
        toolBarContainer.Size = new Size(0, 0);
        toolBarContainer.Dock = A_2;
        A_0.Container.Add((IComponent) toolBarContainer, A_3);
        A_1.Controls.Add((Control) toolBarContainer);
        toolBarContainer.SendToBack();
        return toolBarContainer;
      }

      private void a(
        Menu A_0,
        MenuItemBase A_1,
        IDesignerHost A_2,
        IEventBindingService A_3,
        bool A_4)
      {
        bool flag = false;
        if (!A_0.IsParent)
          return;
        foreach (MenuItem menuItem in A_0.MenuItems)
        {
          if (menuItem.Text == "-")
          {
            flag = true;
          }
          else
          {
            MenuButtonItem component = (MenuButtonItem) A_2.CreateComponent(typeof (MenuButtonItem));
            component.Text = menuItem.Text;
            component.Checked = menuItem.Checked;
            component.Enabled = menuItem.Enabled;
            component.Visible = menuItem.Visible;
            component.BeginGroup = flag;
            component.Shortcut = menuItem.Shortcut;
            if (A_4)
            {
              try
              {
                string name = menuItem.Site.Name;
                menuItem.Site.Name = "old_" + name;
                component.Site.Name = name;
              }
              catch
              {
              }
            }
            this.a(menuItem, component, A_3);
            A_1.Items.Add((ToolbarItemBase) component);
            this.a((Menu) menuItem, (MenuItemBase) component, A_2, A_3, A_4);
            flag = false;
          }
        }
      }

      private void b()
      {
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IUIService service2 = (IUIService) this.GetService(typeof (IUIService));
        bool flag1 = false;
        foreach (IComponent component in (ReadOnlyCollectionBase) service1.Container.Components)
        {
          if (component is System.Windows.Forms.ToolBar)
          {
            flag1 = true;
            break;
          }
        }
        if (!flag1 || service2.ShowMessage($"The designer can automatically copy your existing toolbars in to the new toolbar structure.{Environment.NewLine}{Environment.NewLine}Would you like to do this?", "ToolBar Conversion", MessageBoxButtons.YesNo) == DialogResult.No)
        {
          this.OnAddNewToolbar((object) null, (EventArgs) null);
        }
        else
        {
          bool flag2 = service2.ShowMessage("Would you also like to copy your toolbar and button names to the new toolbar structure, and rename your old toolbar and button items?", "ToolBar Conversion", MessageBoxButtons.YesNo) == DialogResult.Yes;
          ArrayList arrayList = new ArrayList();
          foreach (IComponent component in (ReadOnlyCollectionBase) service1.Container.Components)
          {
            if (component is System.Windows.Forms.ToolBar)
              arrayList.Add((object) component);
          }
          ToolBarContainer suitableContainer = this._a.FindSuitableContainer(DockStyle.Top);
          foreach (System.Windows.Forms.ToolBar toolBar in arrayList)
          {
            ToolBar component = (ToolBar) service1.CreateComponent(typeof (ToolBar));
            component.DockLine = suitableContainer.GetNextFreeDockLine();
            component.ImageList = toolBar.ImageList;
            component.TextAlign = toolBar.TextAlign != System.Windows.Forms.ToolBarTextAlign.Right ? ToolBarTextAlign.Underneath : ToolBarTextAlign.Side;
            if (flag2)
            {
              try
              {
                string name = toolBar.Site.Name;
                toolBar.Site.Name = "old_" + name;
                component.Site.Name = name;
              }
              catch
              {
              }
            }
            bool flag3 = false;
            foreach (ToolBarButton button in toolBar.Buttons)
            {
              if (button.Style == ToolBarButtonStyle.Separator)
              {
                flag3 = true;
              }
              else
              {
                ButtonItemBase buttonItemBase = button.Style != ToolBarButtonStyle.DropDownButton ? (ButtonItemBase) service1.CreateComponent(typeof (ButtonItem)) : (ButtonItemBase) service1.CreateComponent(typeof (DropDownMenuItem));
                if (flag2)
                {
                  try
                  {
                    string name = button.Site.Name;
                    button.Site.Name = "old_" + name;
                    buttonItemBase.Site.Name = name;
                  }
                  catch
                  {
                  }
                }
                if (flag3)
                {
                  buttonItemBase.BeginGroup = true;
                  flag3 = false;
                }
                buttonItemBase.Checked = button.Pushed;
                buttonItemBase.Enabled = button.Enabled;
                buttonItemBase.ImageIndex = button.ImageIndex;
                buttonItemBase.Text = button.Text;
                buttonItemBase.ToolTipText = button.ToolTipText;
                buttonItemBase.Visible = button.Visible;
                component.Items.Add((ToolbarItemBase) buttonItemBase);
              }
            }
            suitableContainer.Controls.Add((Control) component);
          }
        }
      }

      private void OnAddNewMenubar(object A_0, EventArgs A_1) => this.c();

      private MenuBar c()
      {
        IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        ToolBarContainer toolBarContainer = this._a.FindSuitableContainer(DockStyle.Top) ?? throw new InvalidOperationException("There is no Top Container associated with this toolbar layout.");
        return ((ToolBarContainerDesigner) service.GetDesigner((IComponent) toolBarContainer))?.a();
      }

      [Obsolete]
      public override void OnSetComponentDefaults()
      {
        base.OnSetComponentDefaults();
        IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        if (!(service1.RootComponent is Control))
          return;
        Control rootComponent = (Control) service1.RootComponent;
        service2.OnComponentChanging((object) rootComponent, (MemberDescriptor) null);
        this.a(service1, rootComponent, DockStyle.Left, "leftBarDock");
        this.a(service1, rootComponent, DockStyle.Right, "rightBarDock");
        this.a(service1, rootComponent, DockStyle.Bottom, "bottomBarDock");
        this.a(service1, rootComponent, DockStyle.Top, "topBarDock");
        service2.OnComponentChanged((object) rootComponent, (MemberDescriptor) null, (object) null, (object) null);
        this.a();
        this.b();
      }

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving += new ComponentEventHandler(this.a);
        this._a = (BarManager) component;
      }

      public override ICollection AssociatedComponents => (ICollection) this._a._containers;

      public override DesignerVerbCollection Verbs => this._verbs;
    }
}
