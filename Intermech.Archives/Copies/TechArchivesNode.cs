// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechArchivesNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Узел для все документов всех архивов.
/// С возможностью фильтрации документов
/// </summary>
public class TechArchivesNode : ArchivesNode
{
  /// <summary>
  /// Создает и возвращает часть элемента, отвечающую за документы, находящиеся
  /// в любом из существующих архивов.
  /// </summary>
  /// <returns>Интерфейс части</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new AllDocumsPart((IConditionsProvider) (this.Services.GetService(typeof (TechDocumentFilter)) as TechDocumentFilter), this.Services));
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
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection defaultColumns = base.GetDefaultColumns(content);
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.InventoryNumberID));
    return defaultColumns;
  }
}
