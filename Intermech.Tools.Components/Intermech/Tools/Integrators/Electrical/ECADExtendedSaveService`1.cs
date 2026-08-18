// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADExtendedSaveService`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Controls;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Expert;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Сервис интегратора, реализующий расширенное сохранение документов.
/// </summary>
public abstract class ECADExtendedSaveService<TSettingsService> : 
  ExtendedSaveService<TSettingsService>
  where TSettingsService : IIntegratorSettingsService
{
  private AppMechanicalDriver captureDriver;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public ECADExtendedSaveService(IIntegrator owner)
    : base(owner)
  {
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.captureDriver = this.CreateMechanicalDriver();
  }

  protected abstract AppMechanicalDriver CreateMechanicalDriver();

  protected abstract IList<LocalId<int>> supportedDocumentTypes { get; }

  /// <summary>
  /// Собирает коллекцию типов документов, которые поддерживают расширенное сохранение.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  protected override IList<LocalId<int>> CollectSupportedDocumentTypes()
  {
    IList<LocalId<int>> localIdList = base.CollectSupportedDocumentTypes();
    foreach (LocalId<int> supportedDocumentType in (IEnumerable<LocalId<int>>) this.supportedDocumentTypes)
      localIdList.Add(supportedDocumentType);
    return localIdList;
  }

  /// <summary>
  /// Возвращает экземпляр драйвера для захвата изменений в документах интегрируемого приложения. Метод обязательно должен вернуть созданный объект.
  /// </summary>
  /// <returns>Объект драйвера</returns>
  protected override ICaptureChangesDriver GetCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) this.captureDriver;
  }

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением.
  /// </summary>
  /// <param name="objectId">Идентификатор документа</param>
  /// <param name="options">Опции выполнения</param>
  protected override void SetCaptureChangesParameters(long objectId, ExtendedSaveOptions options)
  {
    base.SetCaptureChangesParameters(objectId, options);
    this.captureDriver.SaveChangesMode = options.Mode;
    this.captureDriver.UpdateArticles = this.CalculateUpdateArticlesParameter(objectId, options);
    this.captureDriver.RecalculateMass = options.RecalculateMass;
    this.captureManager.WorkAreaPolicy = options.WorkAreaPolicy;
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением.
  /// </summary>
  protected override void ResetCaptureChangesParameters()
  {
    base.ResetCaptureChangesParameters();
    this.captureManager.WorkAreaPolicy = (IReplaceFilePolicy) null;
  }

  protected override void OnPostProcessCaptureChanges(CaptureChangesResult result)
  {
    List<Tuple<Guid, long>> tupleList = new List<Tuple<Guid, long>>();
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) result.Database.Query((IQueryCondition) new CodeCondition((Predicate<IEntity>) (dbItem =>
    {
      SectionEntity sectionEntity = (SectionEntity) dbItem;
      return sectionEntity.Sections.Contains<ObjectSection>() && sectionEntity.Sections.Contains<AssemblyIDSection>();
    }))))
      tupleList.Add(new Tuple<Guid, long>(sectionEntity.Sections.Get<AssemblyIDSection>().Guid, sectionEntity.Sections.Get<ObjectSection>().ObjectId));
    SectionEntity sectionEntity1 = result.Database.QueryFirst((IQueryCondition) new CodeCondition((Predicate<IEntity>) (dbItem =>
    {
      SectionEntity sectionEntity2 = (SectionEntity) dbItem;
      return sectionEntity2.Sections.Contains<ObjectSection>() && sectionEntity2.Sections.Contains<ElectricalSchemeDescriptors>();
    })));
    if (sectionEntity1 == null)
      return;
    ElectricalSchemeDescriptors schemeDescriptors = sectionEntity1.Sections.Get<ElectricalSchemeDescriptors>(new ElectricalSchemeDescriptors(0));
    if (schemeDescriptors.Count == 0)
      return;
    foreach (ElectricalSchemeDescriptor schemeDescriptor in (List<ElectricalSchemeDescriptor>) schemeDescriptors)
    {
      foreach (PrintBoardDescriptor printBoard in schemeDescriptor.PrintBoards)
      {
        PrintBoardDescriptor asm = printBoard;
        Tuple<Guid, long> tuple = tupleList.Find((Predicate<Tuple<Guid, long>>) (x => x.Item1.Equals(asm.Guid)));
        if (tuple != null)
          asm.AssemblyID = tuple.Item2;
      }
    }
    ObjectSection objectSection = sectionEntity1.Sections.Get<ObjectSection>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectSection.ObjectId);
      IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(ElectricalConsts.attributeProjectData) ?? dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(ElectricalConsts.attributeProjectData), false);
      using (ImChunkedStream serializationStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) schemeDescriptors);
        IBlobWriter blobWriter = dbAttribute as IBlobWriter;
        if (blobWriter.OpenBlob(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty), false))
          blobWriter.WriteDataBlock(serializationStream.ToArray());
      }
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.DocRelationTypeID);
      DataTable dataTable1 = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }), objectSection.ObjectId);
      if (dataTable1.Rows.Count <= 0)
        return;
      List<long> longList1 = new List<long>();
      List<int> elementListTypeIds = ElectricalTypesHelper.ElementListTypeIDs;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
      });
      bool flag = false;
      Dictionary<long, List<long>> elListsToProcess = new Dictionary<long, List<long>>();
      foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
      {
        long int64 = Convert.ToInt64(row1[0]);
        DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, int64);
        if (dataTable2.Rows.Count != 0)
        {
          foreach (DataRow row2 in (InternalDataCollectionBase) dataTable2.Rows)
          {
            if (elementListTypeIds.Contains(Convert.ToInt32(row2[0])))
            {
              List<long> longList2 = elListsToProcess.ContainsKey(int64) ? elListsToProcess[int64] : new List<long>();
              longList2.Add(Convert.ToInt64(row2[1]));
              if (!elListsToProcess.ContainsKey(int64))
                elListsToProcess[int64] = longList2;
              flag = true;
            }
            else
              longList1.Add(int64);
          }
        }
      }
      if (!flag || new NeedRefreshElementListDlg().ShowDialog() != DialogResult.OK)
        return;
      List<string> errors;
      List<long> longList3 = this.RefreshElementsLists(elListsToProcess, schemeDescriptors, out errors);
      string Message = $"Результат операции:\r\n{elListsToProcess.Values.SelectMany<List<long>, long>((System.Func<List<long>, IEnumerable<long>>) (v => (IEnumerable<long>) v)).Distinct<long>().Count<long>()} перечней элементов обнаружено\r\n" + $"{longList3.Count} перечней элементов пересоздано,\r\n" + $"{errors.Count} ошибок.";
      if (errors.Count > 0)
        result.Errors.AddRange((IEnumerable<string>) errors);
      int num = (int) IMMessageBox.Show("Пересоздание перечней элементов", Message, MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
    }
  }

  private List<long> CreateElementsLists(
    List<long> asmsToProcess,
    out int totalCount,
    out int createdCount,
    out int recreatedCount,
    out List<string> errors)
  {
    totalCount = 0;
    createdCount = 0;
    recreatedCount = 0;
    errors = new List<string>();
    List<long> elementsLists = new List<long>();
    if (asmsToProcess == null || asmsToProcess.Count == 0)
      return elementsLists;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>();
      foreach (long projId in asmsToProcess.Distinct<long>())
      {
        IUserSession session = sessionKeeper.Session;
        DataTable childSostavData = DataHelper.GetChildSostavData(projId, session, (IEnumerable<int>) new List<int>()
        {
          AvsIDCache.Relation_Document
        }, false);
        if (childSostavData != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
          {
            long int64_1 = Convert.ToInt64(row["F_OBJECT_ID"]);
            long int64_2 = Convert.ToInt64(row["F_OBJECT_TYPE"]);
            if (!longList.Contains(int64_1))
            {
              longList.Add(int64_1);
              IFactory service = ServiceUtils.GetService<IFactory>((object) ServicesManager.ServiceContainer, true);
              if (service != null)
              {
                ICommandsProvider[] commandsProviders = service.GetCommandsProviders();
                if (commandsProviders != null)
                {
                  foreach (ICommandsProvider commandsProvider1 in commandsProviders)
                  {
                    if (commandsProvider1 is ECADCommandsProvider commandsProvider2 && (long) commandsProvider2.ObjType == int64_2)
                    {
                      (int num1, int num2, int num3, List<string> collection) = commandsProvider2.CreateElementList(sessionKeeper.Session, int64_1);
                      createdCount += num2;
                      recreatedCount += num3;
                      totalCount += num1;
                      errors.AddRange((IEnumerable<string>) collection);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return elementsLists;
  }

  private List<long> RefreshElementsLists(
    Dictionary<long, List<long>> elListsToProcess,
    ElectricalSchemeDescriptors schemes,
    out List<string> errors)
  {
    errors = new List<string>();
    List<long> longList1 = new List<long>();
    if (elListsToProcess == null || elListsToProcess.Keys.Count == 0)
      return longList1;
    List<NotificationEventArgs> notificationEventArgsList = new List<NotificationEventArgs>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IArticleService customService = sessionKeeper.Session.GetCustomService(typeof (IArticleService)) as IArticleService;
      IObjectsCheckOutService service1 = (IObjectsCheckOutService) ServicesManager.GetService(typeof (IObjectsCheckOutService));
      IElementListCreatorService service2 = ServicesManager.GetService(typeof (IElementListCreatorService)) as IElementListCreatorService;
      List<long> longList2 = new List<long>();
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
      foreach (long key in elListsToProcess.Keys)
      {
        long asmId = key;
        List<long> listInstances = customService.GetListInstances(asmId, (object) sessionKeeper.Session.SessionGUID);
        foreach (long num in elListsToProcess[asmId])
        {
          IList<long> longList3 = service1.CheckOut(sessionKeeper.Session, (IList<long>) new long[1]
          {
            num
          }, true);
          IDBObject dbObject = (IDBObject) ElementList.ClearAvsDocumentFile(sessionKeeper.Session.GetObject(longList3[0]));
          notificationEventArgsList.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", Math.Abs(dbObject.ObjectID)));
          DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
          {
            (object) -2,
            (object) -20
          }), dbObject.ObjectID);
          IEnumerable<\u003C\u003Ef__AnonymousType0<long, long>> datas = dataTable != null ? dataTable.Rows.OfType<DataRow>().Select(r => new
          {
            AsmId = Convert.ToInt64(r[0]),
            RelationId = Convert.ToInt64(r[1])
          }) : null;
          List<long> assemblyIDs = new List<long>();
          if (datas != null)
          {
            foreach (var data in datas)
            {
              if (!listInstances.Contains(data.AsmId))
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(data.RelationId, false);
                if (relation != null)
                {
                  relation.Delete(0L);
                  notificationEventArgsList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", relation.RelationID, data.AsmId, relationCollection.RelationTypeID));
                }
              }
              else
                assemblyIDs.Add(data.AsmId);
            }
          }
          if (assemblyIDs.Count == 0)
            assemblyIDs.Add(asmId);
          if (dbObject.IsCreationMode)
            dbObject.CommitCreation(true);
          List<SimpleRecord> records = (schemes != null ? schemes.Count : 0) == 1 ? schemes[0].SimpleRecords : (schemes != null ? schemes.Where<ElectricalSchemeDescriptor>((System.Func<ElectricalSchemeDescriptor, bool>) (s => s.PrintBoards.Any<PrintBoardDescriptor>((System.Func<PrintBoardDescriptor, bool>) (b => b.AssemblyID == asmId)))).FirstOrDefault<ElectricalSchemeDescriptor>()?.SimpleRecords : (List<SimpleRecord>) null);
          try
          {
            long objectId = dbObject.ObjectID;
            if (!longList1.Contains(objectId))
            {
              service2.CreateElementList(sessionKeeper.Session, dbObject.ObjectID, dbObject.ObjectType, assemblyIDs, records);
              longList1.Add(dbObject.ObjectID);
            }
          }
          catch (Exception ex)
          {
            errors.Add($"Ошибка пересоздания ПЭ {dbObject.Caption}: {ex.Message}");
          }
        }
      }
    }
    if (notificationEventArgsList.Count > 0)
    {
      INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      foreach (NotificationEventArgs e in notificationEventArgsList)
        service.FireEvent((object) null, e);
    }
    return longList1;
  }
}
