// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseSelectFromTreeAnalyzer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Imbase.Views;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Commands;

public class ImbaseSelectFromTreeAnalyzer : SelectedItemsAnalyzer
{
  protected List<int> _objTypes = new List<int>();
  protected ISelectedItems _treeSelectedItems;

  public ImbaseSelectFromTreeAnalyzer(List<int> objectTypes)
  {
    this._objTypes.AddRange((IEnumerable<int>) objectTypes);
    this._objTypes.Sort();
  }

  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = base.Analyze(sender, itemsHost);
    if (itemsAnalyzerResult == SelectedItemsAnalyzerResult.Enabled)
    {
      ISelectedItems selectedItems = itemsHost.SelectedItems;
      if (!(selectedItems is ImbaseTableView.TableSelectedItems))
      {
        this._treeSelectedItems = selectedItems;
        bool flag = false;
        for (int index = 0; index < selectedItems.Count; ++index)
        {
          if (!(selectedItems.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData) || this._objTypes.BinarySearch(itemData.Value) < 0)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          itemsAnalyzerResult = SelectedItemsAnalyzerResult.Disabled;
          this._treeSelectedItems = (ISelectedItems) null;
        }
      }
    }
    else
      this._treeSelectedItems = (ISelectedItems) null;
    return itemsAnalyzerResult;
  }

  public ISelectedItems TreeSelectedItems => this._treeSelectedItems;
}
