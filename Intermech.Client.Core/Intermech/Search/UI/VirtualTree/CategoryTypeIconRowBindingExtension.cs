
// Type: Intermech.Search.UI.VirtualTree.CategoryTypeIconRowBindingExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Search.CategoryTypeIcons;
using System;
using System.Windows.Forms;


namespace Intermech.Search.UI.VirtualTree;

public sealed class CategoryTypeIconRowBindingExtension : RowBindingExtensionBase
{
  public override void GetRowData(Row row, RowData rowData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (rowData == null)
      throw new ArgumentNullException(nameof (rowData));
    Tuple<ImageList, int> listImageIndexTuple = CategoryTypeIconsClientHelper.GetImageListImageIndexTuple(row.Item);
    rowData.ImageList = listImageIndexTuple.Item1;
    rowData.ImageIndex = listImageIndexTuple.Item2;
  }
}
