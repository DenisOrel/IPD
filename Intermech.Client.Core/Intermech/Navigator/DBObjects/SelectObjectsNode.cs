
// Type: Intermech.Navigator.DBObjects.SelectObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный узел, реализующий элемент навигации для выбора из списка объектов
/// </summary>
/// <summary>Создать экземпляр класса</summary>
public sealed class SelectObjectsNode(IList objects) : ObjectsListNode(objects)
{
  /// <summary>Название виртуального узла - "Объекты"</summary>
  public static readonly string NodeName = LocalizationHolder.rm.GetString("Client.Core_1351");

  /// <summary>Вернуть слоты-папки</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(Intermech.Navigator.Consts.CategorySelectObjectsNodeGuid, (INodePart) new SelectObjectsListPart(this.objectIDs, this.Services))
    };
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return Utils.DefaultColumnsObjects();
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return Utils.DefaultSupportedColumnsObjects();
  }
}
