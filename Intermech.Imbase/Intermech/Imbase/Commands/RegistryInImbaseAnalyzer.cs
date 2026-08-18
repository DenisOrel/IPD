// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.RegistryInImbaseAnalyzer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Commands;

public class RegistryInImbaseAnalyzer : SelectedItemsAnalyzer
{
  protected List<int> _objTypes = new List<int>();

  public RegistryInImbaseAnalyzer(List<int> objectTypes)
  {
    this._objTypes.AddRange((IEnumerable<int>) objectTypes);
  }

  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = base.Analyze(sender, itemsHost);
    if (itemsAnalyzerResult == SelectedItemsAnalyzerResult.Enabled)
    {
      ISelectedItems selectedItems = itemsHost.SelectedItems;
      itemsAnalyzerResult = selectedItems.Count <= 0 ? SelectedItemsAnalyzerResult.Disabled : (!(selectedItems.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData) || !this._objTypes.Contains(itemData.Value) ? SelectedItemsAnalyzerResult.Disabled : SelectedItemsAnalyzerResult.Enabled);
    }
    return itemsAnalyzerResult;
  }
}
