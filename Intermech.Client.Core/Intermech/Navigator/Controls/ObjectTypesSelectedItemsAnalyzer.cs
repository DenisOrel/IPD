
// Type: Intermech.Navigator.Controls.ObjectTypesSelectedItemsAnalyzer
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
/// Анализатор разрешает кнопку "ОК" только в том случае, если выделены элементы (объекты или типы объектов) допустимых типов (или их наследники)
/// </summary>
public class ObjectTypesSelectedItemsAnalyzer : SelectedItemsAnalyzer
{
  /// <summary>Список допустимых к выбору типов объектов</summary>
  protected List<int> objectTypes = new List<int>();
  /// <summary>Разрешить ли к выбору и унаследованные типы объектов</summary>
  protected bool inheritance = true;

  /// <summary>
  /// Создать анализатор, добавить в список к выбору только указанный тип объекта
  /// </summary>
  /// <param name="objectType">Разрешённый к выбору тип объекта</param>
  /// <param name="inheritance">true - допускается выбирать объекты, тип которых унаследован от указанного типа</param>
  public ObjectTypesSelectedItemsAnalyzer(int objectType, bool inheritance)
  {
    this.objectTypes.Add(objectType);
    this.inheritance = inheritance;
  }

  /// <summary>
  /// Создать анализатор, добавить в список к выбору указанные типы объектов
  /// </summary>
  /// <param name="objectTypes">Разрешённые к выбору типы объектов</param>
  /// <param name="inheritance">true - допускается выбирать объекты, тип которых унаследован от любого из указанных типов</param>
  public ObjectTypesSelectedItemsAnalyzer(List<int> objectTypes, bool inheritance)
  {
    this.objectTypes.AddRange((IEnumerable<int>) objectTypes);
    this.inheritance = inheritance;
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
    for (int index1 = 0; index1 < selectedItems.Count && selectedItems.GetItemData(index1, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData; ++index1)
    {
      for (int index2 = 0; index2 < this.objectTypes.Count; ++index2)
      {
        int objectType = this.objectTypes[index2];
        if (itemData.ObjectType == objectType || this.inheritance && MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, objectType))
          return SelectedItemsAnalyzerResult.Enabled;
      }
    }
    return SelectedItemsAnalyzerResult.Disabled;
  }
}
