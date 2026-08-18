// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.SelectedOpenInNewWindowAnalizer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Forums;

/// <summary>
/// Ананлизатор для окна выбора объектов.
/// Кнопка Ок доступна, если все выбранные объекты могут быть открыты в новом окне.
/// Используется для вставки ссылок на объекты в обсуждениях
/// </summary>
public class SelectedOpenInNewWindowAnalizer : SelectedItemsAnalyzer
{
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
      if (!(selectedItems.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData) || !Utils.EnableOpenInNewWindow(itemData.Value))
        return SelectedItemsAnalyzerResult.Disabled;
    }
    return SelectedItemsAnalyzerResult.Enabled;
  }
}
