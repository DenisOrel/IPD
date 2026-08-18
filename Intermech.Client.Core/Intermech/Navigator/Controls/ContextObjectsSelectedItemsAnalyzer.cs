
// Type: Intermech.Navigator.Controls.ContextObjectsSelectedItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Анализатор разрешает кнопку "ОК" только в том случае, если
/// выделены объекты типов, которые можно добавлять в контексты редактирования
/// </summary>
public class ContextObjectsSelectedItemsAnalyzer : SelectedItemsAnalyzer
{
  /// <summary>
  /// Словарик проверенных в текущем сеансе типов данных
  /// (тип данных) =&gt; (допустим к выбору)
  /// </summary>
  private Dictionary<int, bool> _enabled = new Dictionary<int, bool>();

  /// <summary>
  /// Выполнить анализирование указанной коллекции элементов, выделенных в окне
  /// </summary>
  /// <param name="sender">Окно, в котором осуществляется выбор элементов</param>
  /// <param name="itemsHost">Служба окна, которая предоставляет коллекцию выделенных элементов</param>
  /// <returns>Результат проверки</returns>
  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    SelectedItemsAnalyzerResult itemsAnalyzerResult = base.Analyze(sender, itemsHost);
    if (itemsAnalyzerResult == SelectedItemsAnalyzerResult.Disabled)
      return itemsAnalyzerResult;
    ISelectedItems selectedItems = itemsHost.SelectedItems;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (!(selectedItems.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData))
        return SelectedItemsAnalyzerResult.Disabled;
      int num = itemData.Value;
      if (this._enabled.ContainsKey(num))
      {
        if (!this._enabled[num])
          return SelectedItemsAnalyzerResult.Disabled;
      }
      else
      {
        if (MetaDataHelper.GetObjectType(num) == null)
          return SelectedItemsAnalyzerResult.Disabled;
        this._enabled.Add(num, MetaDataHelper.CanAddObjTypeToEditingContext(num, false));
        if (!this._enabled[num])
          return SelectedItemsAnalyzerResult.Disabled;
      }
    }
    return SelectedItemsAnalyzerResult.Enabled;
  }
}
