
// Type: Intermech.Bars.ToolbarItemBaseDesigner
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
    internal class ToolbarItemBaseDesigner : ComponentDesigner
    {
      private ToolbarItemBase _component;
      private static bool _insertingItem = false;
      private bool _selected;
      private IDesignerHost _designerHost;
      private ISelectionService _selectionService;
      private IComponentChangeService _componentChangeService;

      public ToolbarItemBaseDesigner()
      {
        this._selected = false;
        this._designerHost = (IDesignerHost) null;
        this._selectionService = (ISelectionService) null;
        this._componentChangeService = (IComponentChangeService) null;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._selectionService.SelectionChanged -= new EventHandler(this.SelectionService_SelectionChanged);
          this._designerHost = (IDesignerHost) null;
          this._selectionService = (ISelectionService) null;
          this._componentChangeService = (IComponentChangeService) null;
        }
        base.Dispose(disposing);
      }

      private void CreateItem(Type type)
      {
        DesignerTransaction designerTransaction = this._designerHost.CreateTransaction("Insert Item");
        try
        {
          if (this._selectionService.PrimarySelection != this.Component)
            this._selectionService.SetSelectedComponents((ICollection) new object[1]
            {
              (object) this.Component
            }, SelectionTypes.Replace);
          (this._designerHost.GetDesigner(this._designerHost.CreateComponent(type)) as ToolbarItemBaseDesigner).OnSetComponentDefaults();
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

      private void SelectionService_SelectionChanged(object A_0, EventArgs A_1)
      {
        bool componentSelected = this._selectionService.GetComponentSelected((object) this.Component);
        if (componentSelected == this._selected)
          return;
        this._selected = componentSelected;
        this._component.Invalidate();
      }

      private void b(object A_0, EventArgs A_1) => this.CreateItem(((TypedDesignerVerb) A_0).Type);

      public override void Initialize(IComponent component)
      {
        base.Initialize(component);
        this._designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        this._selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
        this._componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
        this._selectionService.SelectionChanged += new EventHandler(this.SelectionService_SelectionChanged);
        this._component = (ToolbarItemBase) component;
        this.Visible = this._component.Visible;
        this._component.Visible = true;
      }

      protected override void PreFilterProperties(IDictionary properties)
      {
        base.PreFilterProperties(properties);
        Attribute[] attributeArray = new Attribute[0];
        properties[(object) "Visible"] = (object) TypeDescriptor.CreateProperty(typeof (ToolbarItemBaseDesigner), (PropertyDescriptor) properties[(object) "Visible"], attributeArray);
      }

      public static bool InsertingItem
      {
        get => ToolbarItemBaseDesigner._insertingItem;
        set => ToolbarItemBaseDesigner._insertingItem = value;
      }

      public override DesignerVerbCollection Verbs
      {
        get
        {
          if (this._component == null)
            return base.Verbs;
          DesignerVerb[] designerVerbArray = (DesignerVerb[]) null;
          if (this._component.ToolBar != null)
          {
            DesignerVerbCollection verbs = this._designerHost.GetDesigner((IComponent) this._component.ToolBar).Verbs;
            designerVerbArray = new DesignerVerb[verbs.Count];
            ((ICollection) verbs).CopyTo((Array) designerVerbArray, 0);
          }
          return new DesignerVerbCollection(designerVerbArray);
        }
      }

      public bool Visible
      {
        get => (bool) this.ShadowProperties[nameof (Visible)];
        set => this.ShadowProperties[nameof (Visible)] = (object) value;
      }
    }
}
