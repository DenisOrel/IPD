// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Nodes.SelectedFavoritesAnalizer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Navigator.Controls;

#nullable disable
namespace Intermech.Imbase.Nodes;

internal class SelectedFavoritesAnalizer : SelectedItemsAnalyzer
{
  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = base.Analyze(sender, itemsHost);
    if (itemsAnalyzerResult == SelectedItemsAnalyzerResult.Enabled)
      itemsAnalyzerResult = !(itemsHost.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? SelectedItemsAnalyzerResult.Disabled : SelectedItemsAnalyzerResult.Enabled;
    return itemsAnalyzerResult;
  }
}
