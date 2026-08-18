
// Type: Intermech.Bars.ToolbarStructure
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Collections;


namespace Intermech.Bars
{
    internal class ToolbarStructure
    {
      private ArrayList _items;
      private ToolbarItemBase _toolbarItem;

      private ToolbarStructure()
      {
        this._items = (ArrayList) null;
        this._toolbarItem = (ToolbarItemBase) null;
      }

      private ToolbarStructure(ToolbarItemBase item)
      {
        this._items = (ArrayList) null;
        this._toolbarItem = item;
      }

      public ToolbarItemBase GetItem() => this._toolbarItem;

      public static ToolbarStructure Create(IButtonsSite site)
      {
        ToolbarStructure structure = new ToolbarStructure();
        ToolbarStructure.CopyStructure(structure, site.Items);
        return structure;
      }

      public void RestoreItems(ToolbarItemBaseCollection items)
      {
        items.Clear();
        ToolbarItemBase[] items1 = new ToolbarItemBase[this.GetItems().Count];
        for (int index = 0; index < items1.Length; ++index)
        {
          ToolbarStructure toolbarStructure = (ToolbarStructure) this.GetItems()[index];
          items1[index] = toolbarStructure.GetItem();
          if (toolbarStructure.HasItems())
            toolbarStructure.RestoreItems((ToolbarItemBaseCollection) ((MenuItemBase) items1[index]).Items);
        }
        items.AddRange(items1);
      }

      private static void CopyStructure(ToolbarStructure structure, ToolbarItemBaseCollection items)
      {
        ToolbarStructure[] c = new ToolbarStructure[items.Count];
        for (int index = 0; index < items.Count; ++index)
        {
          c[index] = new ToolbarStructure(items[index]);
          if (items[index] is IButtonsSite)
            ToolbarStructure.CopyStructure(c[index], ((IButtonsSite) items[index]).Items);
        }
        structure.GetItems().AddRange((ICollection) c);
      }

      public bool HasItems() => this._items != null && this._items.Count != 0;

      public ArrayList GetItems()
      {
        if (this._items == null)
          this._items = new ArrayList();
        return this._items;
      }
    }
}
