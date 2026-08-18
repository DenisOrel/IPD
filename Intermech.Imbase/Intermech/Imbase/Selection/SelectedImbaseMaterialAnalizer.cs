// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.SelectedImbaseMaterialAnalizer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class SelectedImbaseMaterialAnalizer : SelectedItemsAnalyzer
{
  private List<int> _rootTypeIDs;

  public SelectedImbaseMaterialAnalizer(List<int> rootObjTypeIDs = null)
  {
    this._rootTypeIDs = rootObjTypeIDs;
  }

  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = SelectedItemsAnalyzerResult.Disabled;
    if (base.Analyze(sender, itemsHost) == SelectedItemsAnalyzerResult.Enabled)
    {
      bool flag = false;
      bool selected = false;
      if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service)
        flag = service.IsMaterialsHandbookItem(itemsHost.SelectedItems, out selected);
      if (flag)
      {
        itemsAnalyzerResult = selected ? SelectedItemsAnalyzerResult.Enabled : SelectedItemsAnalyzerResult.Disabled;
      }
      else
      {
        ISelectedItems selectedItems = (ISelectedItems) (itemsHost.SelectedItems as ImbaseTableView.TableSelectedItems);
        if (selectedItems != null && selectedItems.Count == 1)
          itemsAnalyzerResult = SelectedItemsAnalyzerResult.Enabled;
        else if (itemsHost.SelectedItems != null)
        {
          INodeID itemId = itemsHost.SelectedItems.GetItemID(0);
          itemsAnalyzerResult = itemId == null || !this.IsAllowedType(itemId.TypeID) || itemId.CategoryID != 1 ? itemsAnalyzerResult : SelectedItemsAnalyzerResult.Enabled;
        }
      }
    }
    return itemsAnalyzerResult;
  }

  private bool IsAllowedType(int typeID)
  {
    if (this._rootTypeIDs == null)
      return false;
    return this._rootTypeIDs.Contains(typeID) || this._rootTypeIDs.Any<int>((Func<int, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(typeID, x)));
  }
}
