// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElementList
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Базовый класс перечня элементов для электрических CAD</summary>
public abstract class ElementList
{
  /// <summary>Создать Перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="selObj">Документ, по которому создается ПЭ</param>
  public void Create(IUserSession session, IDBTypedObjectID selObj)
  {
    this.Create(session, selObj.ObjectID);
  }

  /// <summary>Создать Перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="documentID">Идентификатор версии документа, по которому создается ПЭ</param>
  public (int, int, int) Create(IUserSession session, long documentID, bool silent = false)
  {
    IDBObject dbObject = session.GetObject(documentID);
    int objectType = dbObject.ObjectType;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(ElectricalConsts.attributeProjectData);
    if (attributeByGuid == null || attributeByGuid.IsNull)
    {
      string text = "Не найдены данные проекта ECAD, выполните расширенное сохранение.";
      if (this.IsErrorCollectionOn)
      {
        this.Errors.Add(text);
      }
      else
      {
        int num = (int) MessageBox.Show(text);
      }
      return (0, 0, 0);
    }
    ElectricalSchemeDescriptors schemes = (ElectricalSchemeDescriptors) null;
    IBlobReader blobReader = attributeByGuid as IBlobReader;
    if (blobReader.OpenBlob(0).RealFileSize > 0L)
    {
      byte[] buffer = blobReader.ReadDataBlock(0);
      if (buffer != null && buffer.Length != 0)
      {
        using (MemoryStream serializationStream = new MemoryStream(buffer))
        {
          serializationStream.Position = 0L;
          schemes = (ElectricalSchemeDescriptors) new BinaryFormatter().Deserialize((Stream) serializationStream);
          serializationStream.Flush();
        }
      }
    }
    if (schemes != null)
      return this.Create(session, schemes, documentID, objectType, silent);
    string text1 = "Не удалось получить данные проекта ECAD, выполните расширенное сохранение.";
    if (this.IsErrorCollectionOn)
    {
      this.Errors.Add(text1);
    }
    else
    {
      int num1 = (int) MessageBox.Show(text1);
    }
    return (0, 0, 0);
  }

  /// <summary>Создать Перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  private (int, int, int) Create(
    IUserSession session,
    ElectricalSchemeDescriptors schemes,
    long documentID,
    int mainDocumentType,
    bool silent = false)
  {
    IDBObjectCollection assemblyCollection = this.GetAssemblyCollection(session, mainDocumentType);
    IArticleService customService1 = session.GetCustomService(typeof (IArticleService)) as IArticleService;
    List<Tuple<string, string, int, List<SimpleRecord>, List<long>, List<long>>> tupleList1 = new List<Tuple<string, string, int, List<SimpleRecord>, List<long>, List<long>>>();
    Dictionary<string, List<Tuple<long, string, string, string>>> nodes = new Dictionary<string, List<Tuple<long, string, string, string>>>();
    foreach (ElectricalSchemeDescriptor scheme in (List<ElectricalSchemeDescriptor>) schemes)
    {
      if (scheme.PrintBoards.Count == 0)
      {
        string message = "Для проекта отсутствуют сборки. Необходимо выполнить расширенное сохранение.";
        if (!this.IsErrorCollectionOn)
          throw new Exception(message);
        this.Errors.Add(message);
      }
      PrintBoardDescriptor printBoardDescriptor = scheme.PrintBoards.Count > 1 ? scheme.PrintBoards.Find((Predicate<PrintBoardDescriptor>) (x => x.Root)) : scheme.PrintBoards[0];
      if (printBoardDescriptor == null)
      {
        string message = "Не найдена главная сборка проекта!";
        if (!this.IsErrorCollectionOn)
          throw new Exception(message);
        this.Errors.Add(message);
      }
      if (printBoardDescriptor.AssemblyID == 0L && (!string.IsNullOrEmpty(printBoardDescriptor.Designation) || !string.IsNullOrEmpty(printBoardDescriptor.Name)))
      {
        string designation = printBoardDescriptor.Designation;
        if (printBoardDescriptor.IsVirtual)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDocumentTypeSettingsService customService2 = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
            if (customService2 != null)
            {
              DocumentTypeSettings settings = customService2.GetSettings(sessionKeeper.Session.SessionGUID, mainDocumentType);
              if (settings.DocumentTypeCodeInDesignation)
              {
                if (settings.DocumentTypeCodeInDesignation)
                {
                  if (settings.DocumentTypeCode != string.Empty)
                    designation = DocumentsHelper.AppendDocCode(session, designation, settings.DocumentTypeCode);
                }
              }
            }
          }
        }
        printBoardDescriptor.AssemblyID = this.FindAssembly(session, assemblyCollection, printBoardDescriptor.Name, designation);
      }
      if (printBoardDescriptor.AssemblyID == 0L)
      {
        string message = $"Не найдена сборка {printBoardDescriptor.Designation}({printBoardDescriptor.Name}). Выполните расширенное сохранение!";
        if (!this.IsErrorCollectionOn)
          throw new Exception(message);
        this.Errors.Add(message);
      }
      else
      {
        List<long> listInstances = customService1.GetListInstances(printBoardDescriptor.AssemblyID, (object) session.SessionGUID);
        if (listInstances.Count > 1)
        {
          List<Tuple<long, string, string, string>> tupleList2 = new List<Tuple<long, string, string, string>>();
          foreach (long objectID in listInstances)
          {
            IDBObject dbObject = session.GetObject(objectID);
            IDBAttribute attributeById1 = dbObject.GetAttributeByID(session.IdentHelper.NameID);
            IDBAttribute attributeById2 = dbObject.GetAttributeByID(session.IdentHelper.DesignationID);
            tupleList2.Add(new Tuple<long, string, string, string>(objectID, dbObject.Caption, attributeById2 != null ? attributeById2.AsString : string.Empty, attributeById1 != null ? attributeById1.AsString : string.Empty));
          }
          nodes.Add(scheme.Designation, tupleList2);
        }
        else
        {
          List<long> longList1 = new List<long>((IEnumerable<long>) new long[1]
          {
            listInstances[0]
          });
          List<long> longList2 = new List<long>();
          if (scheme.PrintBoards.Count > 1)
          {
            foreach (PrintBoardDescriptor printBoard in scheme.PrintBoards)
            {
              if (!printBoard.Root)
              {
                long assembly = this.FindAssembly(session, assemblyCollection, printBoard.Name, printBoard.Designation);
                longList2.Add(assembly);
              }
            }
          }
          string suffix = ElectricalTypesHelper.GetSuffix(scheme.Designation);
          Guid elementListType = ElectricalTypesHelper.GetElementListType(suffix);
          if (elementListType == Guid.Empty && !ElectricalTypesHelper.SelectElementListType(ref elementListType, scheme.Designation, scheme.Name))
          {
            string str = $"Не определен тип перечня элементов для схемы {scheme.Designation}({scheme.Name})";
            if (!this.IsErrorCollectionOn)
              return (0, 0, 0);
            this.Errors.Add(str);
          }
          else
          {
            int objectTypeId = MetaDataHelper.GetObjectTypeID(elementListType);
            string str = DocumentDesignationHelper.AppendDocCode(string.IsNullOrEmpty(suffix) ? scheme.Designation : DocumentsHelper.RemoveDocCode(session, scheme.Designation, suffix), objectTypeId);
            tupleList1.Add(new Tuple<string, string, int, List<SimpleRecord>, List<long>, List<long>>(str, printBoardDescriptor.Name, objectTypeId, scheme.SimpleRecords, longList1, longList2));
          }
        }
      }
    }
    if (nodes.Count > 0)
    {
      using (ElementListSettingsForm listSettingsForm = new ElementListSettingsForm())
      {
        listSettingsForm.LoadData(nodes);
        listSettingsForm.IsBatchMode = silent;
        if (!listSettingsForm.IsBatchMode)
        {
          if (listSettingsForm.ShowDialog() != DialogResult.OK)
            goto label_70;
        }
        foreach (KeyValuePair<string, List<CreatedElementList>> createdElementList1 in listSettingsForm.CreatedElementLists)
        {
          KeyValuePair<string, List<CreatedElementList>> createdElementList = createdElementList1;
          ElectricalSchemeDescriptor schemeDescriptor = schemes.Find((Predicate<ElectricalSchemeDescriptor>) (x => x.Designation.Equals(createdElementList.Key)));
          foreach (CreatedElementList createdElementList2 in createdElementList.Value)
          {
            List<long> longList = new List<long>();
            foreach (Tuple<long, string, string, string> assembly1 in createdElementList2.Assemblies)
            {
              if (assembly1.Item1 != 0L)
              {
                longList.Add(assembly1.Item1);
              }
              else
              {
                long assembly2 = this.FindAssembly(session, assemblyCollection, assembly1.Item4, assembly1.Item3);
                longList.Add(assembly2);
              }
            }
            tupleList1.Add(new Tuple<string, string, int, List<SimpleRecord>, List<long>, List<long>>(createdElementList2.Designation, createdElementList2.Name, createdElementList2.Type, schemeDescriptor.SimpleRecords, longList, (List<long>) null));
          }
        }
      }
    }
label_70:
    int num1 = 0;
    int num2 = 0;
    int count = tupleList1.Count;
    IElementListCreatorService service1 = ServicesManager.GetService(typeof (IElementListCreatorService)) as IElementListCreatorService;
    List<NotificationEventArgs> notificationEventArgsList = new List<NotificationEventArgs>();
    IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
    foreach (Tuple<string, string, int, List<SimpleRecord>, List<long>, List<long>> tuple in tupleList1)
    {
      long num3 = 0;
      if (tuple.Item1 != string.Empty)
        num3 = this.FindPresentElementList(session, tuple.Item1, tuple.Item3, tuple.Item5);
      bool flag = num3 == 0L;
      IDBObject docElementList;
      if (flag)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(tuple.Item3);
        DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(session.IdentHelper.DesignationID, RelationalOperators.Equal, (object) tuple.Item1, LogicalOperators.NONE, 0, false)
        }, new object[2]{ (object) -2, (object) -5 }, new object[1]
        {
          (object) -5
        }, new SortOrders[1]{ SortOrders.DESC }));
        long int64 = dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
        docElementList = int64 != 0L ? objectCollection.CreateVersion(int64) : objectCollection.Create();
        if (!int64.IsUndefinedId())
          docElementList = (IDBObject) ElementList.ClearAvsDocumentFile(docElementList);
        docElementList.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = tuple.Item2;
        docElementList.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString = tuple.Item1;
        notificationEventArgsList.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", Math.Abs(docElementList.ObjectID)));
        foreach (long num4 in tuple.Item5)
        {
          if (session.GetObject(num4, false) != null)
          {
            IDBRelation dbRelation = relationCollection.Create(num4, docElementList.ObjectID);
            dbRelation.SetAttributesValues(new AttributeValues[2]
            {
              new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(docElementList.ObjectID)),
              new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd9609-306c-11d8-b4e9-00304f19f545"), (object) 1L)
            });
            notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, num4, relationCollection.RelationTypeID));
          }
        }
      }
      else
      {
        IList<long> longList3 = ((IObjectsCheckOutService) ServicesManager.GetService(typeof (IObjectsCheckOutService))).CheckOut(session, (IList<long>) new long[1]
        {
          num3
        }, true);
        docElementList = (IDBObject) ElementList.ClearAvsDocumentFile(session.GetObject(longList3[0]));
        notificationEventArgsList.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", Math.Abs(docElementList.ObjectID)));
        DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -2,
          (object) -20
        }), docElementList.ObjectID);
        List<long> longList4 = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!tuple.Item5.Contains(int64))
          {
            IDBRelation relation = session.GetRelation(Convert.ToInt64(row[1]), false);
            if (relation != null)
            {
              relation.Delete(0L);
              notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", relation.RelationID, int64, relationCollection.RelationTypeID));
            }
          }
          else
            longList4.Add(int64);
        }
        foreach (long num5 in tuple.Item5)
        {
          if (!longList4.Contains(num5) && session.GetObject(num5, false) != null)
          {
            IDBRelation dbRelation = relationCollection.Create(num5, docElementList.ObjectID);
            dbRelation.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(docElementList.ObjectID))
            });
            notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, num5, relationCollection.RelationTypeID));
          }
        }
      }
      if (tuple.Item6 != null && tuple.Item6.Count > 0)
        tuple.Item5.AddRange((IEnumerable<long>) tuple.Item6);
      if (docElementList.IsCreationMode)
        docElementList.CommitCreation(true);
      try
      {
        service1.CreateElementList(session, docElementList.ObjectID, docElementList.ObjectType, tuple.Item5, tuple.Item4);
        if (!flag)
          ++num2;
        else
          ++num1;
      }
      catch (Exception ex)
      {
        if (this.IsErrorCollectionOn)
          this.Errors.Add($"Ошибка создания ПЭ {tuple.Item1}({tuple.Item2}): {ex.Message}");
        else
          throw;
      }
    }
    if (notificationEventArgsList.Count > 0)
    {
      INotificationService service2 = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      foreach (NotificationEventArgs e in notificationEventArgsList)
        service2.FireEvent((object) null, e);
    }
    return (count, num1, num2);
  }

  public static IDBAVSDocumentObject ClearAvsDocumentFile(IDBObject docElementList)
  {
    IDBAttribute attributeByGuid = docElementList.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid != null && !attributeByGuid.IsNull)
      attributeByGuid.Delete(0L);
    IDBAVSDocumentObject dbavsDocumentObject = AvsIDCache.GetDBAVSDocumentObject(docElementList);
    dbavsDocumentObject.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) null)
    }, true);
    return dbavsDocumentObject;
  }

  private long FindAssembly(
    IUserSession session,
    IDBObjectCollection assemblyCollection,
    string name,
    string designation)
  {
    ConditionStructure[] assembly = this.ConditionStructuresForFindAssembly(session, name, designation);
    DataTable dataTable = assemblyCollection.Select(new DBRecordSetParams(assembly, new object[1]
    {
      (object) -2
    }));
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
  }

  private ConditionStructure[] ConditionStructuresForFindAssembly(
    IUserSession session,
    string name,
    string designation)
  {
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (!string.IsNullOrEmpty(name))
      conditionStructureList.Add(new ConditionStructure(session.IdentHelper.NameID, RelationalOperators.Equal, (object) name, LogicalOperators.AND, 0, false));
    if (!string.IsNullOrEmpty(designation))
      conditionStructureList.Add(new ConditionStructure(session.IdentHelper.DesignationID, RelationalOperators.Equal, (object) designation, LogicalOperators.AND, 0, false));
    return conditionStructureList.ToArray();
  }

  private IDBObjectCollection GetAssemblyCollection(IUserSession session, int mainDocumentType)
  {
    DocumentTypeSettings settings = ((IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService))).GetSettings(session.SessionGUID, mainDocumentType);
    string[] strArray1;
    if (string.IsNullOrEmpty(settings.OutputObjectTypes))
      strArray1 = new string[0];
    else
      strArray1 = settings.OutputObjectTypes.Split(',');
    string[] strArray2 = strArray1;
    return session.GetObjectCollection(new Guid(strArray2[0]));
  }

  /// <summary>Поиск уже существующего ПЭ</summary>
  private long FindPresentElementList(
    IUserSession session,
    string designation,
    int typeID,
    List<long> asmIDs)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
    foreach (long asmId in asmIDs)
    {
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(session.IdentHelper.DesignationID, RelationalOperators.Equal, (object) designation, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object),
        new ConditionStructure(-7, RelationalOperators.Equal, (object) typeID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      }, new object[1]{ (object) -2 }), asmId);
      if (dataTable.Rows.Count > 0)
        return Convert.ToInt64(dataTable.Rows[0][0]);
    }
    return 0;
  }

  /// <summary>Идентификатор типа проекта</summary>
  protected abstract int projectTypeID { get; }

  /// <summary>Поиск головного проекта</summary>
  protected virtual long GetProject(IUserSession session, long schemaID)
  {
    DataTable dataTable = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545")).EntersIn(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) this.projectTypeID, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }), schemaID);
    return dataTable.Rows.Count == 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  /// <summary>Поиск головной сборки</summary>
  private IDBObject GetRootAssembly(IUserSession session, long schemaID)
  {
    DataTable dataTable = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545")).EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), schemaID);
    return dataTable.Rows.Count == 0 ? (IDBObject) null : session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
  }

  public List<string> Errors { get; protected set; } = new List<string>();

  public bool IsErrorCollectionOn { get; protected set; }

  /// <summary>Варианты создания ПЭ</summary>
  private enum CreateTypes
  {
    /// <summary>Отмена создания</summary>
    None = -1, // 0xFFFFFFFF
    /// <summary>Новый объект</summary>
    NewObject = 0,
    /// <summary>Пересоздание файла</summary>
    RefreshFile = 1,
    /// <summary>Выпуск версии</summary>
    CreateVersion = 2,
  }

  /// <summary>Единица состава</summary>
  private class CompositionItem
  {
    /// <summary>Идентификатор версии</summary>
    public long ObjectID { get; private set; }

    /// <summary>Позиционное обозначение</summary>
    public string PosDesignation { get; private set; }

    /// <summary>Количество</summary>
    public string Quantity { get; private set; }

    public CompositionItem(long objectID, string posDesignation, string quantity)
    {
      this.ObjectID = objectID;
      this.PosDesignation = posDesignation;
      this.Quantity = quantity;
    }
  }
}
