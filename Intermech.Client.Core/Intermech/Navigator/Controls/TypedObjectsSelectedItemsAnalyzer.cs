
// Type: Intermech.Navigator.Controls.TypedObjectsSelectedItemsAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Анализатор разрешает кнопку "ОК" только в том случае, если выделены объекты (не типы объектов) допустимых типов (или их наследники)
/// </summary>
public class TypedObjectsSelectedItemsAnalyzer : ObjectTypesSelectedItemsAnalyzer
{
  /// <summary>
  /// Создать анализатор, добавить в список к выбору объекты только указанных типов
  /// </summary>
  /// <param name="objectType">Разрешённый к выбору тип объекта</param>
  /// <param name="inheritance">true - допускается выбирать объекты, тип которых унаследован от указанного типа</param>
  public TypedObjectsSelectedItemsAnalyzer(int objectType, bool inheritance)
    : base(objectType, inheritance)
  {
  }

  /// <summary>
  /// Создать анализатор, добавить в список к выбору объекты указанных типов
  /// </summary>
  /// <param name="objectTypes">Разрешённые к выбору типы объектов</param>
  /// <param name="inheritance">true - допускается выбирать объекты, тип которых унаследован от любого из указанных типов</param>
  public TypedObjectsSelectedItemsAnalyzer(List<int> objectTypes, bool inheritance)
    : base(objectTypes, inheritance)
  {
  }

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
      if (!(selectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID))
        return SelectedItemsAnalyzerResult.Disabled;
    }
    return SelectedItemsAnalyzerResult.Enabled;
  }
}
