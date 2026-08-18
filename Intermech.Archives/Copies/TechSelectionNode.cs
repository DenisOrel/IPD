// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechSelectionNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Selections.Implementation;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Узел для документов выборок и классификаторов.
/// С возможностью фильтрации документов.
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="selTypeID">Тип объекта</param>
/// <param name="selObjID">Идентификатор версии объекта</param>
/// <param name="binding">Привязки</param>
/// <param name="externalConditions">Внешние условия</param>
public class TechSelectionNode(
  int selTypeID,
  long selObjID,
  IBinding binding,
  IConditionsProvider externalConditions) : SelectionNode(selTypeID, selObjID, binding, externalConditions)
{
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    TechDocumentFilter service = this.Services.GetService(typeof (TechDocumentFilter)) as TechDocumentFilter;
    if (this._binding == null)
      return (List<PartSlot>) null;
    this._externalConditions = (IConditionsProvider) service;
    return this.SlotsFromSinglePart(this._binding.GetPart((IConditionsProvider) this));
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
