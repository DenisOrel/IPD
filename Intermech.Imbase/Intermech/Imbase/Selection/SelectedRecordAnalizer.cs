// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.SelectedRecordAnalizer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class SelectedRecordAnalizer : SelectedItemsAnalyzer
{
  private List<int> _rootTypeIDs;

  public SelectedRecordAnalizer(List<int> rootObjTypeIDs)
  {
    this._rootTypeIDs = rootObjTypeIDs != null ? rootObjTypeIDs : new List<int>(0);
  }

  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = SelectedItemsAnalyzerResult.Disabled;
    if (base.Analyze(sender, itemsHost) != SelectedItemsAnalyzerResult.Disabled)
    {
      ISelectedItems selectedItems = (ISelectedItems) (itemsHost.SelectedItems as ImbaseTableView.TableSelectedItems);
      if (selectedItems != null && selectedItems.Count == 1)
        itemsAnalyzerResult = SelectedItemsAnalyzerResult.Enabled;
      else if (itemsHost.SelectedItems != null)
      {
        INodeID itemId = itemsHost.SelectedItems.GetItemID(0);
        if (itemId != null)
          itemsAnalyzerResult = this.IsAllowedType(itemId.TypeID) ? SelectedItemsAnalyzerResult.Enabled : itemsAnalyzerResult;
      }
    }
    return itemsAnalyzerResult;
  }

  private bool IsAllowedType(int typeID)
  {
    return this._rootTypeIDs.Contains(typeID) || this._rootTypeIDs.Any<int>((Func<int, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(typeID, x)));
  }
}
