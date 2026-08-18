
// Type: Intermech.Search.UI.VirtualTree.TreeSelectedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.UI.VirtualTree;

public sealed class TreeSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;

  public TreeSelectedItems(Intermech.Search.UI.VirtualTree.VirtualTree tree)
  {
    this._tree = tree != null ? tree : throw new ArgumentNullException(nameof (tree));
  }

  public bool IsCollage => true;

  public INodeID GetItemID(int index) => (INodeID) null;

  public object GetParentData(int index, Type dataFormat)
  {
    if (index < 0 || index >= this._tree.SelectedRows.Count)
      throw new ArgumentException();
    if (dataFormat == (Type) null)
      throw new ArgumentNullException(nameof (dataFormat));
    Row selectedRow = this._tree.SelectedRows[index];
    return selectedRow.ParentRow == null || selectedRow.ParentRow.Item == null ? (object) null : this.GetDataInFormat(selectedRow.ParentRow.Item, dataFormat);
  }

  public NodeIDPath GetParentPath(int index) => (NodeIDPath) null;

  public int Count => this._tree.SelectedRows.Count;

  public object GetItemData(int index, Type dataFormat)
  {
    if (index < 0 || index >= this._tree.SelectedRows.Count)
      throw new ArgumentException();
    if (dataFormat == (Type) null)
      throw new ArgumentNullException(nameof (dataFormat));
    Row selectedRow = this._tree.SelectedRows[index];
    return selectedRow.Item == null ? (object) null : this.GetDataInFormat(selectedRow.Item, dataFormat);
  }

  private object GetDataInFormat(object dataItem, Type dataFormat)
  {
    if (dataFormat == typeof (_Object))
    {
      switch (dataItem)
      {
        case _Object _:
          return dataItem;
        case IObjectHolder _:
          return (object) ((IObjectHolder) dataItem).Object;
        default:
          return (object) null;
      }
    }
    else
    {
      if (!(dataFormat == typeof (IDBTypedObjectID)))
        return (object) null;
      switch (dataItem)
      {
        case _Object _:
          return (object) this.CreateTypedObjectIDFromObject((_Object) dataItem);
        case IObjectHolder _:
          return (object) this.CreateTypedObjectIDFromObject(((IObjectHolder) dataItem).Object);
        default:
          return (object) null;
      }
    }
  }

  private DBTypedObjectID CreateTypedObjectIDFromObject(_Object @object)
  {
    object attributeValue = @object.Attributes.GetAttributeValue(ObligatoryObjectAttributes.F_BASE_VERSION);
    baseVersion = 0L;
    if (!(attributeValue is long baseVersion))
      ;
    return new DBTypedObjectID(@object.TypeID, @object.VersionID, @object.ID, @object.Caption, @object.OwnerVersionID, (long) @object.VersionNumber, baseVersion, @object.Attributes.GetAttributeValue(ObligatoryObjectAttributes.F_SITE_ID) as string, @object.ModificationID);
  }
}
