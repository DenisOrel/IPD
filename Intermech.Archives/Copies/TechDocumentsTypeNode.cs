// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechDocumentsTypeNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Узел для документов выбранного типа.
/// С возможностью фильтрации документов
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="objTypeID"></param>
/// <param name="accessRights"></param>
public class TechDocumentsTypeNode(int objTypeID, AccessRights accessRights) : ObjectTypeNode(objTypeID, AccessRights.Enabled)
{
  /// <summary>Вернуть слоты-не-папки</summary>
  /// <returns>Слоты-не-папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    TechDocumentFilter techDocumentFilter = (TechDocumentFilter) null;
    if (this.Services != null)
      techDocumentFilter = this.Services.GetService(typeof (TechDocumentFilter)) as TechDocumentFilter;
    IObjectTypeNodeOptionsHolder service = this.Services != null ? this.Services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder : (IObjectTypeNodeOptionsHolder) null;
    if (service != null)
      service.Options = ObjectTypeNodeOptions.None;
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(this.ObjTypeID, (IConditionsProvider) techDocumentFilter, this.Services));
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
