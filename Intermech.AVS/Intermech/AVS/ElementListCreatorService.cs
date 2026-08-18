// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ElementListCreatorService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Сервис по созданию перечня элементов (ПЭ) для электрических CAD
/// </summary>
public sealed class ElementListCreatorService : IElementListCreatorService
{
  private const bool SilentModeForDebug = false;

  /// <summary>
  /// Создает единичный перечень элементов для выбранной сборочной единицы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="assemblyId">Идентификатор версии сборочной единицы</param>
  public void CreateElementList(IUserSession session, long assemblyId)
  {
    Guid elementListType = ElementListCreatorService.SelectElementListType("", "");
    if (!(elementListType != Guid.Empty))
      return;
    List<ProductInfo> source = AVSDocument.LoadProductsByGroupID(assemblyId, session);
    List<long> longList = new List<long>() { assemblyId };
    if (source.Count > 1)
    {
      List<ProductInfo> list = source.OrderBy<ProductInfo, string>((System.Func<ProductInfo, string>) (x => x.Designation)).ToList<ProductInfo>();
      longList.Clear();
      SelectElementListProductForm elementListProductForm = new SelectElementListProductForm(list);
      if (elementListProductForm.ShowDialog() == DialogResult.OK)
        longList = elementListProductForm.SelectedProducts.OrderBy<ProductInfo, string>((System.Func<ProductInfo, string>) (p => p.Designation)).Select<ProductInfo, long>((System.Func<ProductInfo, long>) (p => p.Id)).ToList<long>();
    }
    if (longList.Count <= 0)
      return;
    IDBObject emptyElementList = ElementListCreatorService.CreateEmptyElementList(session, longList, elementListType);
    this.CreateElementList(session, emptyElementList.ObjectID, emptyElementList.ObjectType, longList, (List<SimpleRecord>) null);
  }

  public static IDBObject CreateEmptyElementList(
    IUserSession session,
    List<long> articles,
    Guid elementListType)
  {
    List<NotificationEventArgs> notificationEventArgsList = new List<NotificationEventArgs>();
    int objectTypeId = MetaDataHelper.GetObjectTypeID(elementListType);
    long article1 = articles[0];
    IDBObject dbObject = session.GetObject(article1);
    IDBAttribute attributeById = dbObject.GetAttributeByID(session.IdentHelper.NameID);
    string asString1 = dbObject.GetAttributeByID(session.IdentHelper.DesignationID).AsString;
    string documentDesignation = ElementListCreatorService.GetNewDocumentDesignation(session, objectTypeId, asString1);
    string asString2 = attributeById.AsString;
    long num = 0;
    if (documentDesignation != string.Empty)
      num = ElementListCreatorService.FindPresentElementList(session, documentDesignation, objectTypeId);
    IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
    IDBObject emptyElementList;
    if (num == 0L)
    {
      emptyElementList = session.GetObjectCollection(objectTypeId).Create();
      emptyElementList.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = asString2;
      emptyElementList.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString = documentDesignation;
      notificationEventArgsList.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", Math.Abs(emptyElementList.ObjectID)));
      List<long> longList = new List<long>();
      List<int> relTypeIDs = new List<int>();
      List<long> projIDs = new List<long>();
      List<int> projTypeIDs = new List<int>();
      foreach (long article2 in articles)
      {
        IDBRelation dbRelation = relationCollection.Create(article2, emptyElementList.ObjectID);
        dbRelation.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(emptyElementList.ObjectID))
        });
        longList.Add(dbRelation.RelationID);
        relTypeIDs.Add(dbRelation.RelationType);
        projIDs.Add(article2);
        projTypeIDs.Add(dbRelation.ProjObject.ObjectType);
      }
      if (!longList.IsNullOrEmpty<long>())
        notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) longList, (IList<long>) projIDs, (IList<int>) projTypeIDs, (IList<int>) relTypeIDs));
    }
    else
    {
      IList<long> longList1 = ((IObjectsCheckOutService) ServicesManager.GetService(typeof (IObjectsCheckOutService))).CheckOut(session, (IList<long>) new long[1]
      {
        num
      }, true);
      emptyElementList = session.GetObject(longList1[0]);
      IDBAttribute attributeByGuid = emptyElementList.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid != null && !attributeByGuid.IsNull)
        attributeByGuid.Delete(0L);
      notificationEventArgsList.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", Math.Abs(emptyElementList.ObjectID)));
      DataTable dataTable = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -20
      }), emptyElementList.ID);
      List<long> longList2 = new List<long>();
      List<long> longList3 = new List<long>();
      List<int> relTypeIDs = new List<int>();
      List<long> projIDs = new List<long>();
      List<int> projTypeIDs = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (!articles.Contains(int64))
        {
          IDBRelation relation = session.GetRelation(Convert.ToInt64(row[1]));
          relation.Delete(0L);
          longList3.Add(relation.RelationID);
          relTypeIDs.Add(relation.RelationType);
          projIDs.Add(int64);
          projTypeIDs.Add(relation.ProjObject.ObjectType);
        }
        else
          longList2.Add(int64);
      }
      if (!longList3.IsNullOrEmpty<long>())
        notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) longList3, (IList<long>) projIDs, (IList<int>) projTypeIDs, (IList<int>) relTypeIDs));
      longList3.Clear();
      relTypeIDs.Clear();
      projIDs.Clear();
      projTypeIDs.Clear();
      foreach (long article3 in articles)
      {
        if (!longList2.Contains(article3))
        {
          IDBRelation dbRelation = relationCollection.Create(article3, emptyElementList.ObjectID);
          longList3.Add(dbRelation.RelationID);
          relTypeIDs.Add(dbRelation.RelationType);
          projIDs.Add(article3);
          projTypeIDs.Add(dbRelation.ProjObject.ObjectType);
        }
      }
      if (!longList3.IsNullOrEmpty<long>())
        notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) longList3, (IList<long>) projIDs, (IList<int>) projTypeIDs, (IList<int>) relTypeIDs));
    }
    if (emptyElementList.IsCreationMode)
      emptyElementList.CommitCreation(true);
    if (notificationEventArgsList.Count > 0)
    {
      INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      foreach (NotificationEventArgs e in notificationEventArgsList)
        service.FireEvent((object) null, e);
    }
    return emptyElementList;
  }

  /// <summary>Обозначение нового, добавляемого в состав документа</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="objectType">Тип документа</param>
  /// <param name="product">Изделие для которого создаётся документ</param>
  /// <returns></returns>
  private static string GetNewDocumentDesignation(
    IUserSession session,
    int objectType,
    string baseDesignation)
  {
    string code = (string) null;
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (customService != null)
    {
      DocumentTypeSettings settings = customService.GetSettings(session.SessionGUID, objectType);
      if (settings.DocumentTypeCodeInDesignation)
        code = settings.DocumentTypeCode;
    }
    return code != null ? DocumentsHelper.AppendDocCode(session, baseDesignation, code) : baseDesignation;
  }

  private static long FindPresentElementList(IUserSession session, string designation, int typeID)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(typeID);
    objectCollection.ShowAllModifications = true;
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(session.IdentHelper.DesignationID, RelationalOperators.Equal, (object) designation, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }));
    return dataTable.Rows.Count <= 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  /// <summary>
  /// Функция пользовательского определения типа перечня элементов
  /// </summary>
  /// <param name="createdElementListType">Глобальный идентификатор типа ПЭ</param>
  /// <returns>Определил пользователь тип или отказался</returns>
  public static Guid SelectElementListType(string designation, string name)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Типы объектов", typeof (ObjectTypeFolder), false);
    selectorForm.ExpandLevelsOnLoad = 4;
    selectorForm.SelectorFilter = (ISelectorFilter) new ElectricalSchemaElementListTypesFilter();
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new AvsNodeSelectorFilter("Не выбран конкретный тип перечня элементов.");
    return selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count == 1 ? MetaDataHelper.GetObjectTypeGuid((int) selectorForm.IDList[0]) : Guid.Empty;
  }

  /// <summary>Создать перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="documentID">Идентификатор версии документа ПЭ</param>
  /// <param name="documentTypeID">Идентификатор типа документа ПЭ</param>
  /// <param name="assemblyIDs">Список составообразующих сборок</param>
  /// <param name="records">Список элементов ПЭ, не связанных с объектами, например контактные площадки и т.п.</param>
  public void CreateElementList(
    IUserSession session,
    long documentID,
    int documentTypeID,
    List<long> assemblyIDs,
    List<SimpleRecord> records)
  {
    AVSDocument elementListByAssembly = ElementListCreatorService.GenerateElementListByAssembly(documentID, documentTypeID, assemblyIDs, records);
    if (MessageBox.Show($"Перечень элементов успешно создан {elementListByAssembly.DocumentDesignation} {elementListByAssembly.DocumentName}. Открыть документ?", "Создание ПЭ", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(elementListByAssembly.DocumentID));
  }

  public static AVSDocument GenerateElementListByAssembly(
    long documentID,
    int documentTypeID,
    List<long> assemblyIDs,
    List<SimpleRecord> records)
  {
    AVSDocument avsDocument = AVSPlugin.Instance.LoadAVSDocument(documentID, documentTypeID, false);
    avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    SpecificationSection commonDataChapter = avsDocument.commonDataChapter as SpecificationSection;
    AvsRowAttributeInfo attrInfo1 = avsDocument.Field_Name.Clone();
    attrInfo1.AttrSrc = FieldSource.DocumentRowField;
    AvsRowAttributeInfo attrInfo2 = avsDocument.Field_PosDesignation.Clone();
    attrInfo2.AttrSrc = FieldSource.DocumentRowField;
    AvsRowAttributeInfo attrInfo3 = avsDocument.Field_Count.Clone();
    attrInfo3.AttrSrc = FieldSource.DocumentRowField;
    if (records != null && records.Count > 0)
    {
      foreach (SimpleRecord record in records)
      {
        AVSRow row = new AVSRow(avsDocument);
        commonDataChapter.AddRow(row, false);
        avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
        row.SetFieldValue(attrInfo1, -1, -1, (object) record.Description, false, false, true, false, false, false);
        row.SetFieldValue(attrInfo2, -1, -1, (object) record.PosDesignation, false, false, true, false, false, false);
        row.SetFieldValue(attrInfo3, -1, -1, (object) "1", false, false, true, false, false, false);
        if (record is SimpleAttributableRecord)
        {
          List<Tuple<string, object>> attributes = ((SimpleAttributableRecord) record).Attributes;
          if (attributes != null)
          {
            foreach (Tuple<string, object> tuple in attributes)
            {
              AvsRowAttributeInfo.CreateDocRowFieldAttributeInfo(tuple.Item1);
              row.DocNode.SetAttributeValue(tuple.Item1, Convert.ToString(tuple.Item2));
            }
          }
        }
      }
    }
    avsDocument.SetProducts(assemblyIDs);
    avsDocument.ResortSpecification(true, true);
    avsDocument.SumPositionalDesignation();
    avsDocument.UpdateRowsGroupHeaders();
    avsDocument.SaveAVSDocumentToDB();
    return avsDocument;
  }
}
