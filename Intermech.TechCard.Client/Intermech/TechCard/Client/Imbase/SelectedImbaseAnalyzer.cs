// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.SelectedImbaseAnalyzer
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
internal class SelectedImbaseAnalyzer : SelectedItemsAnalyzer
{
  /// <summary>
  /// Список идентификаторов типов, объекты которых можно выбирать.
  /// </summary>
  private readonly IEnumerable<int> _allowedTypeIDs;

  /// <summary>Конструктор.</summary>
  /// <param name="allowedObjTypeIDs"> Список идентификаторов типов, объекты которых можно выбирать</param>
  public SelectedImbaseAnalyzer(IEnumerable<int> allowedObjTypeIDs = null)
  {
    this._allowedTypeIDs = allowedObjTypeIDs;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="itemsHost"></param>
  /// <returns></returns>
  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = SelectedItemsAnalyzerResult.Disabled;
    if (base.Analyze(sender, itemsHost) == SelectedItemsAnalyzerResult.Enabled)
    {
      ISelectedItems selectedItems = itemsHost.SelectedItems;
      if (selectedItems == null)
        return SelectedItemsAnalyzerResult.Disabled;
      if (itemsHost is ImbaseTableView && selectedItems.Count != 0)
        return SelectedItemsAnalyzerResult.Enabled;
      if (itemsHost.SelectedItems != null)
      {
        INodeID itemId = itemsHost.SelectedItems.GetItemID(0);
        itemsAnalyzerResult = itemId == null || !this.IsAllowedType(itemId.TypeID) ? itemsAnalyzerResult : SelectedItemsAnalyzerResult.Enabled;
      }
      if (!(selectedItems.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData1))
        return SelectedItemsAnalyzerResult.Disabled;
      if (!itemData1.Full)
        itemData1.Fetch();
      if (itemData1.HasChildren)
        return SelectedItemsAnalyzerResult.Disabled;
      bool flag = false;
      for (int index = 0; index < selectedItems.Count; ++index)
      {
        if (!(selectedItems.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData2) || !this._allowedTypeIDs.Contains<int>(itemData2.Value))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        itemsAnalyzerResult = SelectedItemsAnalyzerResult.Disabled;
    }
    return itemsAnalyzerResult;
  }

  /// <summary>
  /// Проверка типа объекта, на принадлежность типам, которые можно выбирать.
  /// </summary>
  /// <param name="typeId">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  private bool IsAllowedType(int typeId)
  {
    return this._allowedTypeIDs == null || this._allowedTypeIDs.Contains<int>(typeId) || this._allowedTypeIDs.Any<int>((Func<int, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(typeId, x)));
  }
}
