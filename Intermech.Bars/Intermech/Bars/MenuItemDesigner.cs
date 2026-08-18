
// Type: Intermech.Bars.MenuItemDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.Bars
{
    [Serializable]
    internal class MenuItemDesigner : ToolbarItemBaseDesigner
    {
      private MenuItemBase _item;
      private DesignerVerbCollection _verbs;
      private bool _templateSelected;
      private static bool _staticTemplateSelected = false;
      private DesignerTransaction _transaction;
      private bool f;
      private IDesignerHost _designerHost;
      private ISelectionService _selectionService;
      private IComponentChangeService _componentChangeService;

      public MenuItemDesigner()
      {
        this._templateSelected = false;
        this._transaction = (DesignerTransaction) null;
        this.f = false;
        this._designerHost = (IDesignerHost) null;
        this._selectionService = (ISelectionService) null;
        this._componentChangeService = (IComponentChangeService) null;
      }

      [Obsolete]
      public override void OnSetComponentDefaults()
      {
        base.OnSetComponentDefaults();
        this._item.Text = this._item.Site.Name;
      }

      protected override void Dispose(bool A_0)
      {
        if (A_0)
        {
          this._componentChangeService.ComponentRemoving -= new ComponentEventHandler(this.OnComponentRemoving);
          this._componentChangeService.ComponentRemoved -= new ComponentEventHandler(this.OnComponentRemoved);
          this._componentChangeService.ComponentAdding -= new ComponentEventHandler(this.OnComponentAdding);
          this._componentChangeService.ComponentAdded -= new ComponentEventHandler(this.OnComponentAdded);
          this._selectionService.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
          this._designerHost = (IDesignerHost) null;
          this._selectionService = (ISelectionService) null;
          this._componentChangeService = (IComponentChangeService) null;
        }
        base.Dispose(A_0);
      }

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        this._designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        this._selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
        this._componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        this._item = (MenuItemBase) component;
        this._componentChangeService.ComponentRemoving += new ComponentEventHandler(this.OnComponentRemoving);
        this._componentChangeService.ComponentRemoved += new ComponentEventHandler(this.OnComponentRemoved);
        this._componentChangeService.ComponentAdding += new ComponentEventHandler(this.OnComponentAdding);
        this._componentChangeService.ComponentAdded += new ComponentEventHandler(this.OnComponentAdded);
        this._selectionService.SelectionChanged += new EventHandler(this.OnSelectionChanged);
      }

      public void a(int A_0, bool A_1)
      {
        DesignerTransaction designerTransaction = this._designerHost.CreateTransaction("Insert Menu Item");
        try
        {
          if (this._selectionService.PrimarySelection != this.Component)
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) this.Component
            }, SelectionTypes.Replace);
          MenuButtonItem component = (MenuButtonItem) this._designerHost.CreateComponent(this._item.DefaultChildType);
          (this._designerHost.GetDesigner((IComponent) component) as ComponentDesigner).OnSetComponentDefaults();
          this._selectionService.SetSelectedComponents((ICollection) new object[1]
          {
            (object) component
          }, SelectionTypes.Replace);
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

      private void OnComponentAdded(object sender, ComponentEventArgs cea)
      {
        if (!(cea.Component is MenuButtonItem) || !this.f)
          return;
        MenuButtonItem component = (MenuButtonItem) cea.Component;
        this.f = false;
        try
        {
          this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._item)["Items"]);
          this._item.Items.Add((ToolbarItemBase) component);
        }
        finally
        {
          this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._item)["Items"], (object) null, (object) null);
        }
        if (this._transaction == null)
          return;
        this._transaction.Commit();
        this._transaction = (DesignerTransaction) null;
      }

      private void OnSelectionChanged(object sender, EventArgs e)
      {
        if (!this.TemplateSelected || ((ISelectionService) this.GetService(typeof (ISelectionService))).PrimarySelection is MenuItemDesigner.MenuItemWrapper)
          return;
        this.TemplateSelected = false;
      }

      private void OnComponentAdding(object sender, ComponentEventArgs cea)
      {
        if (!(cea.Component is MenuButtonItem) || ((MenuItemBase) cea.Component).ParentMenu != null || this._selectionService.PrimarySelection != this._item || ToolbarItemBaseDesigner.InsertingItem || this.f)
          return;
        this.f = true;
        if (this._transaction != null)
          return;
        this._transaction = this._designerHost.CreateTransaction("Add Item");
      }

      internal void Verb_Execute(object sender, EventArgs ea) => this.a(this._item.Items.Count, true);

      private void OnComponentRemoved(object sender, ComponentEventArgs cea)
      {
        if (!(cea.Component is MenuButtonItem) || ((MenuItemBase) cea.Component).ParentMenu != this._item)
          return;
        MenuButtonItem component = (MenuButtonItem) cea.Component;
        try
        {
          if (!this._item.Items.Contains((ToolbarItemBase) component))
            return;
          this._item.Items.Remove((ToolbarItemBase) component);
          this.RaiseComponentChanged((MemberDescriptor) TypeDescriptor.GetProperties((object) this._item)["Items"], (object) null, (object) null);
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

      private void OnComponentRemoving(object sender, ComponentEventArgs cea)
      {
        if (!(cea.Component is MenuButtonItem))
          return;
        if (((MenuItemBase) cea.Component).ParentMenu != this._item)
          return;
        try
        {
          this._transaction = this._designerHost.CreateTransaction("Remove Item");
          this.RaiseComponentChanging((MemberDescriptor) TypeDescriptor.GetProperties((object) this._item)["Items"]);
        }
        catch
        {
          if (this._transaction == null)
            return;
          this._transaction.Cancel();
          this._transaction = (DesignerTransaction) null;
        }
      }

      public override ICollection AssociatedComponents
      {
        get => this._item.HasChildren ? (ICollection) this._item.Items : (ICollection) new object[0];
      }

      public static bool StaticTemplateSelected => MenuItemDesigner._staticTemplateSelected;

      public bool TemplateSelected
      {
        get => this._templateSelected;
        set
        {
          this._templateSelected = value;
          MenuItemDesigner._staticTemplateSelected = value;
          if (value)
          {
            MenuItemDesigner.MenuItemWrapper menuItemWrapper = new MenuItemDesigner.MenuItemWrapper(this._item);
            ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new object[1]
            {
              (object) menuItemWrapper
            }, SelectionTypes.Replace);
          }
          if (this._item.PopupMenu == null)
            return;
          this._item.PopupMenu.Invalidate();
        }
      }

      public override DesignerVerbCollection Verbs
      {
        get
        {
          if (this._verbs == null)
          {
            this._verbs = new DesignerVerbCollection();
            this._verbs.Add(new DesignerVerb("Add &" + this._item.DefaultChildType.Name, new EventHandler(this.Verb_Execute)));
          }
          return this._verbs;
        }
      }

      internal class MenuItemWrapper
      {
        private MenuItemBase _menuItem;

        public MenuItemWrapper(MenuItemBase item) => this._menuItem = item;

        public MenuItemBase GetItem() => this._menuItem;
      }
    }
}
