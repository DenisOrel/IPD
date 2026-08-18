// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ScenarioFunc
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>
/// Вспомогательные статические функции формирования документов и отчетов
/// </summary>
public class ScenarioFunc
{
  /// <summary>Получить таблицу с количествами объектов в составе</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="rootObjectID">Идентификатор версии корневого объекта</param>
  /// <param name="rootObjectTypeID">Идентификатор типа корневого объекта</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="enableChildObjTypes">Типы искомых объектов состава</param>
  /// <param name="relationTypes">Типы связей по которым ищется состав</param>
  /// <returns>Таблица, где первой колонкой будет количество, во второй идентификатор версии в составе</returns>
  public static DataTable GetQuantitiesTable(
    IUserSession session,
    long rootObjectID,
    int rootObjectTypeID,
    string filtrationOwnerID,
    List<int> enableChildObjTypes,
    List<int> relationTypes)
  {
    ICompositionLoadService customService = (ICompositionLoadService) session.GetCustomService(typeof (ICompositionLoadService));
    ColumnDescriptor[] collection = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    return customService.LoadComposition((object) session.SessionGUID, rootObjectID, rootObjectTypeID, (IEnumerable<int>) relationTypes, (IEnumerable<int>) enableChildObjTypes, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) collection), true, true, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, filtrationOwnerID, (HybridDictionary) null, -1);
  }

  /// <summary>Создать новый элемент в рабочей области документа</summary>
  /// <param name="document">Формируемый документ</param>
  /// <param name="workArea">Рабочая область</param>
  /// <param name="nodeID">Идентификатор создаваемого элемента</param>
  /// <returns></returns>
  public static DocumentTreeNode CreateNode(
    ImDocumentData document,
    DocumentTreeNode workArea,
    string nodeID)
  {
    DocumentTreeNode child = document.Template.FindNode(nodeID).CloneFromTemplate(true, true);
    workArea.AddChildNode(child, false, false);
    return child;
  }

  /// <summary>Запись ячеек</summary>
  /// <param name="node">Элемент</param>
  /// <param name="values">Имена и значения ячеек</param>
  public static void WriteNodeRow(DocumentTreeNode node, params string[] values)
  {
    if (values == null || values.Length == 0)
      return;
    TextData textData = (TextData) null;
    for (int index = 0; index < values.Length; ++index)
    {
      if (index % 2 == 0)
        textData = node.FindFirstNodeFromTemplate_Recursive(values[index]) as TextData;
      else
        textData?.AssignText(values[index], false, false, false);
    }
  }
}
