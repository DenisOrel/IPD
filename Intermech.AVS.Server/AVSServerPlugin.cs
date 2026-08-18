// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.AVSServerPlugin
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Diagnostics;
using Intermech.Document.DBCore;
using Intermech.Expert;
using Intermech.Expert.Server;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.AVS.Server;

[Serializable]
public class AVSServerPlugin : IPackage, IUpdatable
{
  private Dictionary<long, (long SpecificationID, long DeleteMode)> specificationsByDeletingProducts = new Dictionary<long, (long, long)>();
  private IServiceProvider _servProvider;
  private Dictionary<long, AVSServerPlugin.razdInfo> razdObjects = new Dictionary<long, AVSServerPlugin.razdInfo>();
  public static readonly string TshtkGUID = "cadd93a5-306c-11d8-b4e9-00304f19f545";
  public static readonly string TpzGUID = "cad005da-306c-11d8-b4e9-00304f19f545";
  public static readonly string TshtGUID = "cad005e5-306c-11d8-b4e9-00304f19f545";
  public static readonly string OTNormirovanie = "cad00174-306c-11d8-b4e9-00304f19f545";
  public static readonly string OTIzdelie = "cad00268-306c-11d8-b4e9-00304f19f545";
  public static readonly string OTOperation = "cad00178-306c-11d8-b4e9-00304f19f545";
  public static readonly string VarX = "cad003e1-306c-11d8-b4e9-00304f19f545";
  public static readonly string Kol_1 = "cad014ed-306c-11d8-b4e9-00304f19f545";
  public static readonly string Quan1 = "cad014ed-306c-11d8-b4e9-00304f19f545";
  public static readonly string Quan2 = "cad014ee-306c-11d8-b4e9-00304f19f545";
  public static readonly string Quan3 = "cad014ef-306c-11d8-b4e9-00304f19f545";
  public static readonly string Quan4 = "cad014f0-306c-11d8-b4e9-00304f19f545";
  public static readonly string Quan5 = "cad014f1-306c-11d8-b4e9-00304f19f545";
  public static readonly string RegPercent = "cad014f2-306c-11d8-b4e9-00304f19f545";
  public static readonly string RegQuan = "cad007a6-306c-11d8-b4e9-00304f19f545";
  public static readonly string GOST = "cad003de-306c-11d8-b4e9-00304f19f545";
  public static readonly string OKP_Code = "cad0038a-306c-11d8-b4e9-00304f19f545";
  public static readonly string Supplier = "cad01519-306c-11d8-b4e9-00304f19f545";
  public static readonly string _Quantity = "cad00267-306c-11d8-b4e9-00304f19f545";
  public static readonly string _Position = "cad00270-306c-11d8-b4e9-00304f19f545";
  public static readonly string numZamen = "cad001c1-306c-11d8-b4e9-00304f19f545";
  public static readonly string linkSostavIzd = "cad00023-306c-11d8-b4e9-00304f19f545";
  public static readonly string hierLevel = "cadd934d-306c-11d8-b4e9-00304f19f545";
  public static readonly string CLASS = "cad008d8-306c-11d8-b4e9-00304f19f545";
  public static readonly string RAZM_PARM = "cad00211-306c-11d8-b4e9-00304f19f545";
  public static readonly string objTypeRSP = "cadd93cc-306c-11d8-b4e9-00304f19f545";
  public static readonly string specRazdel = "cad00266-306c-11d8-b4e9-00304f19f545";
  public static readonly string specRazdelStr = "cad00210-306c-11d8-b4e9-00304f19f545";
  public static readonly string specSorting = "cad00202-306c-11d8-b4e9-00304f19f545";
  public static readonly string specRazdelNum = "cad00279-306c-11d8-b4e9-00304f19f545";
  public static readonly string[] razdelTypes = new string[9]
  {
    "cad00256-306c-11d8-b4e9-00304f19f545",
    "cad00257-306c-11d8-b4e9-00304f19f545",
    "cad00258-306c-11d8-b4e9-00304f19f545",
    "cad00259-306c-11d8-b4e9-00304f19f545",
    "cad0025a-306c-11d8-b4e9-00304f19f545",
    "cad0025b-306c-11d8-b4e9-00304f19f545",
    "cad0025с-306c-11d8-b4e9-00304f19f545",
    "cad0025d-306c-11d8-b4e9-00304f19f545",
    "cad00271-306c-11d8-b4e9-00304f19f545"
  };
  public static readonly string[] razdelNames = new string[9]
  {
    "Документация",
    "Комплексы",
    "Сборочные единицы",
    "Детали",
    "Стандартные изделия",
    "Прочие изделия",
    "Материалы",
    "Комплекты",
    "Комплектовочные единицы"
  };
  public static readonly string guidIzdel = "cad00268-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidMaterial = "cad00170-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidClass = "cad008d8-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidRazmParm = "cad00211-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidPokupnoj = "cad007a5-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidStandIzd = "cad00252-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidProchIzd = "cad0038d-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrEntranceLevel = "cadd934d-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrSpecRazdel = "cad00266-306c-11d8-b4e9-00304f19f545";
  public static readonly string linkZagot = "cadd9404-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrMaterial = "cad0038c-306c-11d8-b4e9-00304f19f545";
  public static readonly string attributeClass = "cad008d8-306c-11d8-b4e9-00304f19f545";
  public static readonly string attributeRazmParms = "cad00211-306c-11d8-b4e9-00304f19f545";
  private Dictionary<int, DocumentTypeSettings> _docTypeToDocTypeName = new Dictionary<int, DocumentTypeSettings>();
  private IDocumentTypeSettingsService _docTypeService;
  public static readonly string Flag3 = "cadd93cf-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagA = "cadd9448-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagB = "cadd9449-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagV = "cadd944a-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagG = "cadd944b-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagD = "cadd944c-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagE = "cadd944d-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagOA = "cadd944e-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagOB = "cadd944f-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagOV = "cadd9450-306c-11d8-b4e9-00304f19f545";
  public static readonly string flagOG = "cadd944e-306c-11d8-b4e9-00304f19f545";
  protected static string ispAttrGUID = "cae0e49f-14fd-4b33-8fe1-75a05d314056";

  public string Name => "Серверная часть AVS";

  public void Load(IServiceProvider serviceProvider)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      AVSDocumentsSettings.Instance.LoadFromDB(sessionKeeper.Session);
    ICustomServices service1 = (ICustomServices) serviceProvider.GetService(typeof (ICustomServices));
    this._servProvider = serviceProvider;
    AVSParametersService serviceInstance1 = new AVSParametersService();
    service1.AddService(typeof (IAppSettingsService<AvsSettings>), (object) serviceInstance1);
    ServerServices.AddService(typeof (IAppSettingsService<AvsSettings>), (object) serviceInstance1);
    AVSServerService serviceInstance2 = new AVSServerService();
    service1.AddService(typeof (IAVSServerService), (object) serviceInstance2);
    if (serviceProvider.GetService(typeof (IDBObjectService)) is ICreatorContainer service2)
    {
      DBAVSDocumentObjectCreator creatorInstance = new DBAVSDocumentObjectCreator();
      List<Guid> guidList = new List<Guid>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        guidList = AVSDocumentsSettings.Instance.GetDBObjectTypesForAllAVSDocuments(sessionKeeper.Session);
      foreach (Guid guid in guidList)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(guid, new Guid("cad00070-306c-11d8-b4e9-00304f19f545")))
          service2.AddCreator((object) guid, (object) creatorInstance);
      }
    }
    AvsIDCache.InitTypeNameDictionary();
    IPluginManager service3 = (IPluginManager) serviceProvider.GetService(typeof (IPluginManager));
    if (service3 != null)
      service3.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service4))
      return;
    service4.AfterCheckoutEvent += new ObjectEventHandler(serviceInstance2.AfterCheckOutSpecification);
    service4.AfterCheckinEvent += new ObjectEventHandler(serviceInstance2.AfterCheckInSpecification);
    service4.AfterSaveToArcCopy += new ObjectEventHandler(serviceInstance2.AfterSaveToArcCopy);
    service4.AfterDeleteRelationEvent += new DeleteRelationHandler(this.eHelper_DeleteRelationEvent);
    service4.AfterUndoCheckoutExEvent += new CheckoutEventHandler(serviceInstance2.AfterUndoCheckOutSpecification);
    service4.AfterCreateRelationExEvent += new CreateRelationExHandler(this.eHelper_AfterCreateRelationExEvent);
    service4.BeforeNextLCStepEvent += new NextLCStepHandler(this.eHelper_BeforeNextLCStepEvent);
    service4.AfterNextLCStepEvent += new NextLCStepHandler(this.eHelper_AfterNextLcStepEvent);
    service4.BeforePurgeObjectExtendedEvent += new ObjectDeleteEventHandler(this.EventLogHelper_BeforePurgeObjectExtendedEvent);
    service4.AfterPurgeObjectEvent += new ObjectEventHandler(this.eventLogHelper_AfterPurgeObjectEvent);
  }

  private void eHelper_AfterCreateRelationExEvent(
    IDBRelation sender,
    IUserSession session,
    int assignMode)
  {
    if (sender.RelationType != AvsIDCache.Relation_Document || Intermech.Consts.IsUndefinedObjectId(sender.PartObjectID) || !AvsIDCache.IsSpecification(session.GetObjectInfo(sender.PartObjectID).ObjectTypeID))
      return;
    sender.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(sender.PartObjectID))
    });
  }

  private void eHelper_BeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (nextstep.LevelID != session.IdentHelper.DeletedID || !AvsIDCache.IsProductForSpecification(sender.ObjectType))
      return;
    long assemblyProducts = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
    {
      sender.ObjectID
    }, "", true);
    if (Intermech.Consts.IsUndefinedObjectId(assemblyProducts))
      return;
    session.GetObject(assemblyProducts, false)?.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(AvsIDCache.Attr_NeedUpdateDoc, (object) true)
    });
    if (this.specificationsByDeletingProducts.ContainsKey(sender.ObjectID))
      return;
    this.specificationsByDeletingProducts.Add(sender.ObjectID, (assemblyProducts, 0L));
  }

  private void eHelper_AfterNextLcStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (nextstep.LevelID != session.IdentHelper.DeletedID)
      return;
    this.DeleteFreeSpecificationForDeletedProduct(sender, session);
  }

  private void EventLogHelper_BeforePurgeObjectExtendedEvent(
    IDBObject sender,
    ObjectDeleteEventArgs args)
  {
    if (!AvsIDCache.IsProductForSpecification(sender.ObjectType))
      return;
    long assemblyProducts = AvsIDCache.FindSpecificationForAssemblyProducts(args.Session, (IList<long>) new long[1]
    {
      sender.ObjectID
    }, "", true);
    if (Intermech.Consts.IsUndefinedObjectId(assemblyProducts) || this.specificationsByDeletingProducts.ContainsKey(sender.ObjectID))
      return;
    this.specificationsByDeletingProducts.Add(sender.ObjectID, (assemblyProducts, args.DeleteMode));
  }

  private void eventLogHelper_AfterPurgeObjectEvent(IDBObject sender, IUserSession session)
  {
    this.DeleteFreeSpecificationForDeletedProduct(sender, session);
  }

  private void DeleteFreeSpecificationForDeletedProduct(IDBObject sender, IUserSession session)
  {
    (long SpecificationID, long DeleteMode) tuple;
    if (!AvsIDCache.IsProductForSpecification(sender.ObjectType) || !this.specificationsByDeletingProducts.TryGetValue(sender.ObjectID, out tuple))
      return;
    AVSServerPlugin.DeleteSpecificationWithoutProduct(session, tuple.SpecificationID, tuple.DeleteMode);
    this.specificationsByDeletingProducts.Remove(sender.ObjectID);
  }

  private static void DeleteSpecificationWithoutProduct(
    IUserSession session,
    long specificationID,
    long deleteMode)
  {
    IDBObject dbObject = session.GetObject(specificationID, false);
    if (dbObject == null)
      return;
    List<long> specificationByRelations = AvsIDCache.FindProductForSpecificationByRelations(session, specificationID, (string) null);
    if ((specificationID >= 0L || specificationByRelations.Any<long>((System.Func<long, bool>) (p => p < 0L))) && (specificationID <= 0L || specificationByRelations.Any<long>()))
      return;
    long num = 2064;
    if ((deleteMode & num) == num)
    {
      if (dbObject.CheckoutBy.IsDefinedId() && dbObject.CheckoutBy != session.UserID)
      {
        dbObject.CancelChanges(true);
        dbObject = session.GetObject(Math.Abs(specificationID), false);
      }
      dbObject?.Delete(0L);
    }
    else
    {
      dbObject.Delete(0L);
      if (specificationID >= 0L)
        return;
      AVSServerPlugin.DeleteSpecificationWithoutProduct(session, -specificationID, deleteMode);
    }
  }

  private void eHelper_DeleteRelationEvent(
    IDBRelation sender,
    long deleteMode,
    IUserSession session)
  {
    if (sender.RelationType != AvsIDCache.Relation_Document || !MetaDataHelper.IsObjectTypeChildOf(session.GetObjectInfo(sender.PartObjectID).ObjectTypeID, AvsIDCache.ObjType_Specification))
      return;
    IDBAttribute attributeById = sender.GetAttributeByID(AvsIDCache.Attr_VersionInRelation);
    if (attributeById == null || attributeById.Value == DBNull.Value)
      return;
    long int64 = Convert.ToInt64(attributeById.Value);
    session.GetObject(int64, false)?.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(AvsIDCache.Attr_NeedUpdateDoc, (object) true)
    });
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    if (ServerServices.GetService(typeof (IECOServer)) is IECOServer service1)
      service1.SubscribeToIncludeIntoECO(AvsIDCache.ObjType_Specification, new System.Action<IUserSession, long, long, long>(this.IncludeIntoECOAction));
    if (!(ServerServices.GetService(typeof (IExpertServer)) is IExpertServer service2))
      return;
    service2.RegUserProc("Считать количества для ВП", new ScriptProcHandler(this.CalcVPQuantities));
    service2.RegUserProc("Получить информацию для ВП из IMBASE", new ScriptProcHandler(this.GetImbaseInfo));
    service2.RegUserProc("Проверить количества", new ScriptProcHandler(this.CheckQuantities));
    service2.RegUserProc("Проверить количества (изделия и материалы)", new ScriptProcHandler(this.CheckTechQuantities));
    service2.RegUserProc("Получить информацию об исполнениях", new ScriptProcHandler(this.CalcIspoln));
    service2.RegUserProc("Посчитать общее количество для текущего исполнения", new ScriptProcHandler(this.CalcVSQuantities));
    service2.RegUserProc("Рассчитать уровни иерархии и пробелы", new ScriptProcHandler(this.AddHiers));
    service2.RegUserProc("Рассчитать штучно-калькуляционное время", new ScriptProcHandler(this.CalcS_CalcTime));
    service2.RegUserProc("Общее количество объектов в объекте 1", new ScriptProcHandler(this.CalcQuan1));
    service2.RegUserProc("Проверить атрибуты Класс и Размеры и параметры для ВП", new ScriptProcHandler(this.CheckClassParm));
    service2.RegUserProc("Записать разделы спецификации", new ScriptProcHandler(this.MakeSPRazdels));
    service2.RegUserProc("Пометить объекты со входящими ведомостями", new ScriptProcHandler(this.MarkEntersIn));
    service2.RegUserProc("Убрать допустимые замены", new ScriptProcHandler(this.RemoveZamens));
    service2.RegUserProc("Обработать заготовки для ВП", new ScriptProcHandler(this.PerformZags));
    service2.RegUserProc("Пометить связи для текущего исполнения", new ScriptProcHandler(this.MarkBaseLinks));
    service2.RegUserProc("Удалить пустые строки", new ScriptProcHandler(this.RemoveEmptyStrings));
    service2.RegUserProc("Исправить двойные ссылки", new ScriptProcHandler(this.RemoveDuplicateLinks));
    service2.RegUserProc("Добавить ведомости в таблицу", new ScriptProcHandler(this.AddVedomsToGlobal));
    service2.RegUserProc("Расширить номер до 8 символов", new ScriptProcHandler(this.ExpandAttr));
    service2.RegUserProc("Удалить связи без количества", new ScriptProcHandler(this.RemoveLinksWithoutQuantity));
    service2.RegUserProc("Получить тексты о доп. заменах", new ScriptProcHandler(this.GetDopZamenTexts));
    service2.RegUserProc("Привести атрибуты с назначенными значениями к Описанию", new ScriptProcHandler(this.ReplaceValuesWithDescriptions));
    service2.RegUserProc("Вывести сообщение", new ScriptProcHandler(this.WriteReport));
  }

  private void IncludeIntoECOAction(IUserSession session, long ecoId, long relId, long objId)
  {
    List<IDBObject> productsToCheckOut = (((ICustomServices) this._servProvider.GetService(typeof (ICustomServices))).GetService(typeof (IAVSServerService)) as AVSServerService).GetProductsToCheckOut(objId, session);
    IDBAttribute dbAttribute = session.GetRelation(relId).Attributes.AddAttribute(AvsIDCache.Attr_AuxLinks, false);
    object[] values = dbAttribute.Values;
    foreach (IDBObject dbObject in productsToCheckOut)
    {
      bool flag = false;
      if (values != null && values.Length != 0)
      {
        foreach (object obj in values)
        {
          if (obj != null && obj != DBNull.Value && Math.Abs(Convert.ToInt64(obj)) == dbObject.ObjectID)
            flag = true;
        }
      }
      if (!flag)
      {
        dbAttribute.AddValue((object) dbObject.ObjectID);
        values = dbAttribute.Values;
        if (values != null && values.Length != 0 && values[0] == DBNull.Value)
        {
          dbAttribute.Index = 0;
          dbAttribute.DeleteValue();
        }
      }
    }
  }

  public void Unload()
  {
  }

  internal void CalcQuan1(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    long int64_1 = Convert.ToInt64((string) objectList[0]);
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.Kol_1))
      ti.savedData.AddColumn(AVSServerPlugin.Kol_1, typeof (MeasuredValue));
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      double aValue = 0.0;
      long int64_2 = Convert.ToInt64(hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"]);
      if (Math.Abs(int64_1) == Math.Abs(int64_2))
        aValue = 1.0;
      hybridRowExp[AVSServerPlugin.Kol_1] = (object) new MeasuredValue(aValue, ExpertConsts.Consts.measureShtuk);
    }
    List<long> longList;
    if (ti.app != null)
    {
      longList = ti.currentIsp == -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(ti.ispList[0])) : new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]));
    }
    else
    {
      longList = new List<long>();
      HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_1);
      if (hybridRowExpArray != null)
      {
        foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
          longList.Add(Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]));
      }
    }
    MeasuredValue rootVal = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
    List<long> stack = new List<long>();
    foreach (long linkId in longList)
      this.PerformKol1Link(ti, linkId, rootVal, stack);
  }

  internal void PerformKol1Link(
    ExpertServer.ExpServTask ti,
    long linkId,
    MeasuredValue rootVal,
    List<long> stack)
  {
    HybridRowExp row = ti.savedLinksByIdIndex(linkId);
    if (row == null)
      return;
    long int64_1 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
    MeasuredValue quantity = this.GetQuantity(row);
    HybridRowExp hybridRowExp1 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp1 == null)
      return;
    long int64_2 = Convert.ToInt64(hybridRowExp1[0]);
    MeasuredValue measuredValue = this.MultQuan(ti.userReport, quantity, rootVal);
    MeasuredValue q1 = (MeasuredValue) hybridRowExp1[AVSServerPlugin.Kol_1];
    hybridRowExp1[AVSServerPlugin.Kol_1] = (object) this.AddQuan(ti.userReport, q1, measuredValue);
    int int32_1 = Convert.ToInt32(hybridRowExp1["cad0002e-306c-11d8-b4e9-00304f19f545"]);
    bool flag = AVSServerPlugin.TypeHolder.th.typesOperation.Contains(int32_1);
    if (flag)
    {
      if (hybridRowExp1[AVSServerPlugin.TpzGUID] != null && hybridRowExp1[AVSServerPlugin.TpzGUID] != DBNull.Value)
        flag = false;
      else if (hybridRowExp1[AVSServerPlugin.TshtGUID] != null && hybridRowExp1[AVSServerPlugin.TshtGUID] != DBNull.Value)
        flag = false;
    }
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp2 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp2["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.PerformKol1Link(ti, int64_3, measuredValue, stack);
        if (flag)
        {
          long int64_4 = Convert.ToInt64(ti.savedLinksByIdIndex(int64_3)["cad00035-306c-11d8-b4e9-00304f19f545"]);
          HybridRowExp hybridRowExp3 = ti.savedDataByPartId(int64_4);
          int int32_2 = Convert.ToInt32(hybridRowExp3["cad0002e-306c-11d8-b4e9-00304f19f545"]);
          if (AVSServerPlugin.TypeHolder.th.typesNorm.Contains(int32_2))
          {
            hybridRowExp1[AVSServerPlugin.TpzGUID] = hybridRowExp3[AVSServerPlugin.TpzGUID];
            hybridRowExp1[AVSServerPlugin.TshtGUID] = hybridRowExp3[AVSServerPlugin.TshtGUID];
          }
        }
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal void WriteReport(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) obj;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    string str = Convert.ToString(objectList[0]);
    expServTask.userReport.Add(str);
  }

  internal void GetQuantity12(
    object obj,
    long[] context,
    DataTable dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(AVSServerPlugin.VarX));
    ExpertServer.es.InnerSetParm(ti, attributeTypeId, (object) new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk));
    if (ti.savedData == null || ti.savedLinks == null || Value == null || Value.GetType() != typeof (List<object>))
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    long int64_1 = Convert.ToInt64((string) objectList[0]);
    long int64_2 = Convert.ToInt64((string) objectList[1]);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_1);
    List<long> stack = new List<long>();
    if (hybridRowExpArray == null)
      return;
    MeasuredValue q1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
    {
      long int64_3 = Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]);
      stack.Add(int64_3);
      try
      {
        MeasuredValue izd2Quant = this.GetIzd2Quant(ti, int64_3, int64_2, stack);
        q1 = this.AddQuan(ti.userReport, q1, izd2Quant);
      }
      finally
      {
        stack.RemoveAt(stack.Count - 1);
      }
    }
    ExpertServer.es.InnerSetParm(ti, attributeTypeId, (object) q1);
  }

  internal MeasuredValue GetIzd2Quant(
    ExpertServer.ExpServTask ti,
    long linkId,
    long Izd2VerId,
    List<long> stack)
  {
    MeasuredValue q1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    HybridRowExp row = ti.savedLinksByIdIndex(linkId);
    if (row == null)
      return q1;
    long int64_1 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
    HybridRowExp hybridRowExp1 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp1 == null)
      return q1;
    long int64_2 = Convert.ToInt64(hybridRowExp1[0]);
    MeasuredValue quantity = this.GetQuantity(row);
    if (int64_2 == Izd2VerId)
      return quantity;
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return q1;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp2 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp2["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
        {
          MeasuredValue izd2Quant = this.GetIzd2Quant(ti, int64_3, Izd2VerId, stack);
          if (Math.Abs(izd2Quant.Value) > 1E-05)
          {
            MeasuredValue q2 = this.MultQuan(ti.userReport, quantity, izd2Quant);
            q1 = this.AddQuan(ti.userReport, q1, q2);
          }
        }
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
    return q1;
  }

  internal void CalcS_CalcTime(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null || !ti.savedData.Columns.Contains(AVSServerPlugin.TpzGUID) || !ti.savedData.Columns.Contains(AVSServerPlugin.TshtGUID))
      return;
    if (!ti.savedData.Columns.Contains(ExpertAttrGUIDs.attrTotalForProduct))
      ti.savedData.AddColumn(ExpertAttrGUIDs.attrTotalForProduct, typeof (MeasuredValue));
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.TshtkGUID))
      ti.savedData.AddColumn(AVSServerPlugin.TshtkGUID, typeof (MeasuredValue));
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      double aValue = 0.0;
      long int64 = Convert.ToInt64(hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"]);
      if ((ti.ispList == null || ti.ispList.Count == 0) && ti.context.Contains(int64) || ti.ispList != null && ti.ispList.Count > 0 && ti.currentIsp == -1 && ti.ispList.Contains(int64))
        aValue = 1.0;
      if (ti.currentIsp != -1 && ti.ispList != null && ti.ispList[ti.currentIsp] == int64)
        aValue = 1.0;
      hybridRowExp[ExpertAttrGUIDs.attrTotalForProduct] = (object) new MeasuredValue(aValue, ExpertConsts.Consts.measureShtuk);
    }
    List<long> longList1;
    if (ti.app != null)
    {
      longList1 = ti.currentIsp == -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(ti.ispList[0])) : new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]));
    }
    else
    {
      longList1 = new List<long>();
      foreach (long projId in context)
      {
        HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(projId);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
            longList1.Add(Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]));
        }
      }
    }
    List<long> longList2 = new List<long>();
    int index1 = 0;
    while (index1 < longList1.Count)
    {
      HybridRowExp row1 = ti.savedLinksByIdIndex(longList1[index1]);
      if (row1 == null)
      {
        longList1.RemoveAt(index1);
      }
      else
      {
        long int64 = Convert.ToInt64(row1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        int index2 = longList2.IndexOf(int64);
        if (index2 < 0)
        {
          longList2.Add(int64);
          ++index1;
        }
        else
        {
          HybridRowExp row2 = ti.savedLinksByIdIndex(longList1[index2]);
          MeasuredValue quantity1 = this.GetQuantity(row1);
          MeasuredValue quantity2 = this.GetQuantity(row2);
          row2[ExpertAttrGUIDs.attrQuantity] = (object) this.AddQuan(ti.userReport, quantity1, quantity2);
          longList1.RemoveAt(index1);
        }
      }
    }
    List<long> stack = new List<long>();
    MeasuredValue rootVal = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
    foreach (long linkId in longList1)
    {
      stack.Add(linkId);
      try
      {
        this.PerformVSLink(ti, linkId, rootVal, stack, (List<int>) null, (TempFormula) null);
      }
      finally
      {
        stack.RemoveAt(stack.Count - 1);
      }
    }
    foreach (long linkId in longList1)
    {
      stack.Add(linkId);
      try
      {
        HybridRowExp hybridRowExp1 = ti.savedLinksByIdIndex(linkId);
        string mValue = Convert.ToString(hybridRowExp1[ExpertAttrGUIDs.attrQuantity]);
        MeasuredValue KZAK = mValue != string.Empty ? MeasureHelper.ConvertToMeasuredValue(mValue) : new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
        long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(ti.savedDataByPartId(int64_1)[0]);
        HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp hybridRowExp2 in hybridRowExpArray)
            this.CalcTimes(ti, int64_1, stack, KZAK, out MeasuredValue _, out MeasuredValue _, out MeasuredValue _);
        }
      }
      finally
      {
        stack.RemoveAt(stack.Count - 1);
      }
    }
  }

  internal void CalcTimes(
    ExpertServer.ExpServTask ti,
    long partId,
    List<long> stack,
    MeasuredValue KZAK,
    out MeasuredValue Tpz,
    out MeasuredValue TSht,
    out MeasuredValue TShtk)
  {
    Tpz = TSht = TShtk = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    HybridRowExp hybridRowExp1 = ti.savedDataByPartId(partId);
    if (hybridRowExp1 == null)
      return;
    if (hybridRowExp1[AVSServerPlugin.TpzGUID] != DBNull.Value && hybridRowExp1[AVSServerPlugin.TpzGUID] != null)
    {
      double aValue = Convert.ToDouble(hybridRowExp1[AVSServerPlugin.TpzGUID]);
      Tpz = new MeasuredValue(aValue, ExpertConsts.Consts.measureMinute);
    }
    if (hybridRowExp1[AVSServerPlugin.TshtGUID] != DBNull.Value && hybridRowExp1[AVSServerPlugin.TshtGUID] != null)
    {
      double aValue = Convert.ToDouble(hybridRowExp1[AVSServerPlugin.TshtGUID]);
      TSht = new MeasuredValue(aValue, ExpertConsts.Consts.measureMinute);
    }
    if (hybridRowExp1[AVSServerPlugin.TshtkGUID] != DBNull.Value && hybridRowExp1[AVSServerPlugin.TshtkGUID] != null)
    {
      string mValue = Convert.ToString(hybridRowExp1[AVSServerPlugin.TshtkGUID]);
      TShtk = MeasureHelper.ConvertToMeasuredValue(mValue);
    }
    HybridRowExp hybridRowExp2 = ti.savedDataByPartId(partId);
    if (hybridRowExp2 == null)
      return;
    long int64_1 = Convert.ToInt64(hybridRowExp2[0]);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_1);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    foreach (HybridRowExp hybridRowExp3 in hybridRowExpArray)
    {
      long int64_2 = Convert.ToInt64(hybridRowExp3["cad00033-306c-11d8-b4e9-00304f19f545"]);
      if (!stack.Contains(int64_2))
      {
        MeasuredValue Tpz1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
        MeasuredValue TSht1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
        MeasuredValue TShtk1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
        HybridRowExp row = ti.savedLinksByIdIndex(int64_2);
        if (row != null)
        {
          long int64_3 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
          stack.Add(int64_2);
          try
          {
            this.CalcTimes(ti, int64_3, stack, KZAK, out Tpz1, out TSht1, out TShtk1);
            if (Tpz1.Value == 0.0)
            {
              if (TSht1.Value == 0.0)
                continue;
            }
            MeasuredValue q = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
            if (row[AVSServerPlugin._Quantity] != null && row[AVSServerPlugin._Quantity] != DBNull.Value)
            {
              MeasuredValue quantity = this.GetQuantity(row);
              if (quantity.Value != 0.0)
                q = quantity;
            }
            Tpz = this.AddQuan(ti.userReport, Tpz, this.MultQuan(ti.userReport, q, Tpz1));
            TSht = this.AddQuan(ti.userReport, TSht, this.MultQuan(ti.userReport, q, TSht1));
            TShtk = this.AddQuan(ti.userReport, TShtk, TShtk1);
          }
          finally
          {
            stack.RemoveAt(stack.Count - 1);
          }
        }
      }
    }
    int int32 = Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]);
    if (AVSServerPlugin.TypeHolder.th.typesOperation.Contains(int32))
    {
      MeasuredValue measuredValue = (MeasuredValue) hybridRowExp2[ExpertAttrGUIDs.attrTotalForProduct];
      MeasuredValue q2_1 = this.MultQuan(ti.userReport, measuredValue, KZAK);
      MeasuredValue q2_2 = this.DivideQuan(ti.userReport, Tpz, q2_1);
      MeasuredValue q = this.AddQuan(ti.userReport, TSht, q2_2);
      MeasuredValue q2_3 = this.MultQuan(ti.userReport, q, measuredValue);
      TShtk = this.AddQuan(ti.userReport, TShtk, q2_3);
      hybridRowExp2[AVSServerPlugin.TpzGUID] = (object) Tpz.Value;
      hybridRowExp2[AVSServerPlugin.TshtGUID] = (object) TSht.Value;
    }
    if (!AVSServerPlugin.TypeHolder.th.typesIzdelie.Contains(int32))
      return;
    hybridRowExp2[AVSServerPlugin.TshtkGUID] = (object) TShtk;
  }

  internal void AddTshtk(
    ExpertServer.ExpServTask ti,
    long linkId,
    MeasuredValue KZAK,
    List<long> stack)
  {
    HybridRowExp hybridRowExp1 = ti.savedLinksByIdIndex(linkId);
    if (hybridRowExp1 == null)
      return;
    long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
    HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp2 == null)
      return;
    if (hybridRowExp2[AVSServerPlugin.TpzGUID] != null && hybridRowExp2[AVSServerPlugin.TpzGUID] != DBNull.Value && hybridRowExp2[AVSServerPlugin.TshtGUID] != null)
    {
      object obj = hybridRowExp2[AVSServerPlugin.TshtGUID];
      DBNull dbNull = DBNull.Value;
    }
    MeasuredValue measuredValue = (MeasuredValue) hybridRowExp1[ExpertAttrGUIDs.attrTotalForProduct];
    long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp3 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp3["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.AddTshtk(ti, int64_3, KZAK, stack);
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal bool GetNorms(
    ExpertServer.ExpServTask ti,
    long projId,
    out MeasuredValue TpzVal,
    out MeasuredValue TshtVal)
  {
    TpzVal = TshtVal = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.TpzGUID) || !ti.savedData.Columns.Contains(AVSServerPlugin.TshtGUID))
      return false;
    foreach (HybridRowExp hybridRowExp1 in ti.savedLinksByProjId(projId))
    {
      long int64 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
      HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64);
      if (hybridRowExp2 != null)
      {
        int int32 = Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]);
        if (AVSServerPlugin.TypeHolder.th.typesNorm.IndexOf(int32) >= 0)
        {
          string mValue1 = Convert.ToString(hybridRowExp2[AVSServerPlugin.TpzGUID]);
          TpzVal = MeasureHelper.ConvertToMeasuredValue(mValue1);
          string mValue2 = Convert.ToString(hybridRowExp2[AVSServerPlugin.TshtGUID]);
          TshtVal = MeasureHelper.ConvertToMeasuredValue(mValue2);
          return true;
        }
      }
    }
    return false;
  }

  internal void LoadNorms(ExpertServer.ExpServTask ti)
  {
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.TpzGUID))
      ti.savedData.AddColumn(AVSServerPlugin.TshtkGUID, typeof (MeasuredValue));
    ExpertServer.es.GetSession(ti).GetRelationCollection(ExpertConsts.Consts.linkTechSostId);
    int[] array = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(AVSServerPlugin.OTNormirovanie)).ToArray();
    int num1 = 0;
    while (num1 < ti.savedData.RowsCount)
    {
      int num2 = ti.savedData.RowsCount - num1;
      if (num2 > 50)
        num2 = 50;
      try
      {
        ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
        {
          new ConditionStructure(ExpertConsts.Consts.attrProjId, RelationalOperators.In, (object) null, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
          new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) array, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        };
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[4]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
          new ColumnDescriptor((object) ExpertConsts.Consts.attrObjCompRef, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_NAME, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
        };
      }
      finally
      {
        num1 += num2;
      }
    }
  }

  internal void PerformNormLink(
    ExpertServer.ExpServTask ti,
    long linkId,
    MeasuredValue rootVal,
    List<long> stack)
  {
    HybridRowExp row = ti.savedLinksByIdIndex(linkId);
    if (row == null)
      return;
    long int64_1 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
    MeasuredValue quantity = this.GetQuantity(row);
    HybridRowExp hybridRowExp1 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp1 == null)
      return;
    long int64_2 = Convert.ToInt64(hybridRowExp1[0]);
    MeasuredValue measuredValue = this.MultQuan(ti.userReport, quantity, rootVal);
    MeasuredValue q1 = (MeasuredValue) hybridRowExp1[ExpertAttrGUIDs.attrTotalForProduct];
    hybridRowExp1[ExpertAttrGUIDs.attrTotalForProduct] = (object) this.AddQuan(ti.userReport, q1, measuredValue);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp2 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp2["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.PerformNormLink(ti, int64_3, measuredValue, stack);
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal MeasuredValue GetQuantity(HybridRowExp row, bool leaveZeros = false)
  {
    object obj = row[ExpertAttrGUIDs.attrQuantity];
    if (obj == null)
      return (MeasuredValue) null;
    if (obj is MeasuredValue quantity)
      return quantity;
    return !leaveZeros ? ExpertConsts.OneShtuka : ExpertConsts.NolShtuk;
  }

  internal void CalcAdditionalQuantities(
    object obj,
    long rootObj,
    DataTable dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    IUserSession sessionById = UserSession.GetSessionByID(ExpertServer.GetSessionGuid(ti));
    int count = ti.savedLinks.Columns.Count;
    if (!ti.savedLinks.Columns.Contains(AVSServerPlugin.Quan1))
      ti.savedLinks.AddColumn(AVSServerPlugin.Quan1, typeof (MeasuredValue));
    if (!ti.savedLinks.Columns.Contains(AVSServerPlugin.Quan2))
      ti.savedLinks.AddColumn(AVSServerPlugin.Quan2, typeof (MeasuredValue));
    int indexByName1 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.Quan1);
    int indexByName2 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.Quan2);
    int indexByName3 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.RegQuan);
    int indexByName4 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.RegPercent);
    MeasuredValue measuredValue1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      savedLink[indexByName1] = savedLink[indexByName2] = (object) measuredValue1;
      if (savedLink[indexByName3] == DBNull.Value || savedLink[indexByName3] == null)
        savedLink[indexByName3] = (object) measuredValue1;
    }
    bool useIsps = true;
    HybridRowExp hybridRowExp1 = ti.savedDataByObjId(rootObj);
    if (hybridRowExp1 != null)
    {
      bool flag = ExpertServer.IsTypeDescendant(ExpertConsts.Consts.izdComplect, Convert.ToInt32(hybridRowExp1["cad0002e-306c-11d8-b4e9-00304f19f545"]));
      List<long> stack = new List<long>();
      this.AddToLinks(ti, rootObj, useIsps, new MeasuredValue(flag ? 0.0 : 1.0, ExpertConsts.Consts.measureShtuk), new MeasuredValue(flag ? 1.0 : 0.0, ExpertConsts.Consts.measureShtuk), stack);
    }
    IImbaseServer customService = (IImbaseServer) sessionById.GetCustomService(typeof (IImbaseServer));
    for (int index1 = 0; index1 < ti.savedLinks.RowsCount; ++index1)
    {
      HybridRowExp savedLink1 = ti.savedLinks[index1];
      object obj1 = savedLink1[indexByName3];
      if (obj1.Equals((object) measuredValue1) || obj1.Equals((object) measuredValue1.ToString()))
      {
        if (savedLink1[indexByName4] != DBNull.Value)
        {
          double num = Convert.ToDouble(savedLink1[AVSServerPlugin.RegPercent]);
          MeasuredValue q1 = (MeasuredValue) savedLink1[AVSServerPlugin.Quan1];
          MeasuredValue q2 = (MeasuredValue) savedLink1[AVSServerPlugin.Quan2];
          MeasuredValue measuredValue2 = this.AddQuan(ti.userReport, q1, q2);
          savedLink1[AVSServerPlugin.RegQuan] = (object) new MeasuredValue(measuredValue2.Value * num / 100.0, measuredValue2.MeasureID);
        }
        else
        {
          long int64_1 = Convert.ToInt64(savedLink1["cad00035-306c-11d8-b4e9-00304f19f545"]);
          double num = 0.0;
          HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
          if (hybridRowExp2 != null)
          {
            long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
            IDBObject dbObject = sessionById.GetObject(int64_2);
            if (dbObject != null)
            {
              IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid(AVSServerPlugin.RegPercent));
              if (attributeByGuid1 != null && attributeByGuid1.Value != DBNull.Value)
              {
                num = Convert.ToDouble(attributeByGuid1.Value);
              }
              else
              {
                try
                {
                  IDBObject prototypeObject = customService.GetPrototypeObject(sessionById.SessionGUID, (object) int64_2);
                  if (prototypeObject != null)
                  {
                    IDBAttribute attributeByGuid2 = prototypeObject.GetAttributeByGuid(new Guid(AVSServerPlugin.RegPercent), false);
                    if (attributeByGuid2 != null)
                      num = Convert.ToDouble(attributeByGuid2.Value);
                  }
                }
                catch
                {
                }
              }
              MeasuredValue q1 = (MeasuredValue) savedLink1[AVSServerPlugin.Quan1];
              MeasuredValue q2 = (MeasuredValue) savedLink1[AVSServerPlugin.Quan2];
              MeasuredValue measuredValue3 = this.AddQuan(ti.userReport, q1, q2);
              savedLink1[indexByName3] = (object) new MeasuredValue(measuredValue3.Value * num / 100.0, measuredValue3.MeasureID);
            }
            for (int index2 = 0; index2 < ti.savedLinks.RowsCount; ++index2)
            {
              HybridRowExp savedLink2 = ti.savedLinks[index2];
              if (useIsps)
                Convert.ToInt64(savedLink2["cad00033-306c-11d8-b4e9-00304f19f545"]);
              long int64_3 = Convert.ToInt64(savedLink1["cad00035-306c-11d8-b4e9-00304f19f545"]);
              MeasuredValue quantity = this.GetQuantity(savedLink1);
              MeasuredValue measuredValue4 = this.MultQuan(ti.userReport, (MeasuredValue) savedLink1[AVSServerPlugin.Quan1], quantity);
              MeasuredValue q2 = this.MultQuan(ti.userReport, (MeasuredValue) savedLink1[AVSServerPlugin.Quan2], quantity);
              savedLink1[AVSServerPlugin.Quan1] = (object) this.AddQuan(ti.userReport, (MeasuredValue) savedLink1[AVSServerPlugin.Quan1], measuredValue4);
              savedLink1[AVSServerPlugin.Quan2] = (object) this.AddQuan(ti.userReport, (MeasuredValue) savedLink1[AVSServerPlugin.Quan2], q2);
              HybridRowExp hybridRowExp3 = ti.savedDataByPartId(int64_3);
              if (hybridRowExp3 != null)
              {
                Convert.ToInt64(hybridRowExp3[0]);
                if (ExpertServer.IsTypeDescendant(ExpertConsts.Consts.izdComplect, Convert.ToInt32(hybridRowExp3["cad0002e-306c-11d8-b4e9-00304f19f545"])))
                {
                  this.AddQuan(ti.userReport, measuredValue4, q2);
                  MeasuredValue measuredValue5 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
                }
              }
            }
          }
        }
      }
    }
  }

  internal void CheckQuantities(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objDocRoot);
    List<AVSServerPlugin.NoQuantInfo> noQuantInfoList = new List<AVSServerPlugin.NoQuantInfo>();
    IUserSession session = ExpertServer.es.GetSession(ti);
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj1 = savedLink[AVSServerPlugin._Quantity];
      if (obj1 == null || obj1 == DBNull.Value)
      {
        string P = "???";
        if (ti.savedLinks.Columns.Contains(AVSServerPlugin._Position))
        {
          object obj2 = savedLink[AVSServerPlugin._Position];
          if (obj2 != null && obj2 != DBNull.Value)
            P = Convert.ToString(obj2);
        }
        long int64_1 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(savedLink["cad00034-306c-11d8-b4e9-00304f19f545"]);
        HybridRowExp hybridRowExp1 = ti.savedDataByObjId(int64_2);
        string PN = hybridRowExp1 == null ? $"{Convert.ToString(ti.savedData[0]["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(ti.savedData[0]["cad0001f-306c-11d8-b4e9-00304f19f545"])}]" : $"{Convert.ToString(hybridRowExp1["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(hybridRowExp1["cad0001f-306c-11d8-b4e9-00304f19f545"])}]";
        HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
        if (hybridRowExp2 != null)
        {
          int int32 = Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]);
          if (!childrenIdRecursive.Contains(int32))
          {
            string N = $"{Convert.ToString(hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(hybridRowExp2["cad0001f-306c-11d8-b4e9-00304f19f545"])}]";
            noQuantInfoList.Add(new AVSServerPlugin.NoQuantInfo(int64_2, int64_1, Convert.ToInt64(savedLink["cad00033-306c-11d8-b4e9-00304f19f545"]), P, PN, N));
          }
        }
      }
    }
    if (ti.ispList != null)
    {
      noQuantInfoList.Sort();
      List<long> longList = new List<long>();
      long num = -1;
      int index1 = 0;
      bool flag1 = false;
      for (int index2 = 0; index2 < noQuantInfoList.Count; ++index2)
      {
        AVSServerPlugin.NoQuantInfo noQuantInfo = noQuantInfoList[index2];
        if (noQuantInfo.partID == num)
        {
          longList.Add(noQuantInfo.projId);
        }
        else
        {
          foreach (long isp in ti.ispList)
          {
            if (longList.IndexOf(isp) < 0)
            {
              flag1 = true;
              break;
            }
          }
          if (!flag1)
            noQuantInfoList[index1].allIsps = true;
          index1 = index2;
          num = noQuantInfo.partID;
          longList.Add(noQuantInfo.projId);
        }
      }
      bool flag2 = false;
      foreach (long isp in ti.ispList)
      {
        if (longList.IndexOf(isp) < 0)
        {
          flag2 = true;
          break;
        }
      }
      if (!flag2)
        noQuantInfoList[index1].allIsps = true;
    }
    for (int index = 0; index < noQuantInfoList.Count; ++index)
    {
      AVSServerPlugin.NoQuantInfo noQuantInfo = noQuantInfoList[index];
      if (noQuantInfo.allIsps)
      {
        if (ti.ispList != null)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(ti.ispList[0]);
          ti.userReport.Add($"Отсутствует количество изделия \"{noQuantInfo.PartName}\" (спецификация {objectInfo.Caption} и все её исполнения, позиция {noQuantInfo.Pos})");
        }
        while (noQuantInfoList[index].partID == noQuantInfo.partID)
          ++index;
      }
      else
        ti.userReport.Add($"Отсутствует количество изделия \"{noQuantInfo.PartName}\" (спецификация {noQuantInfo.ProjName}, позиция {noQuantInfo.Pos})");
    }
  }

  internal void CheckTechQuantities(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null || !ti.savedData.Columns.Contains("cad0002e-306c-11d8-b4e9-00304f19f545"))
      return;
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID(AVSServerPlugin.guidIzdel);
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID(AVSServerPlugin.guidMaterial);
    ExpertServer.es.GetSession(ti);
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj1 = savedLink[AVSServerPlugin._Quantity];
      if (obj1 == null || obj1 == DBNull.Value)
      {
        long int64_1 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(savedLink["cad00034-306c-11d8-b4e9-00304f19f545"]);
        string str1 = "???";
        HybridRowExp hybridRowExp1 = ti.savedDataByPartId(int64_1);
        if (hybridRowExp1 != null)
        {
          List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(Convert.ToInt32(hybridRowExp1["cad0002e-306c-11d8-b4e9-00304f19f545"]));
          if (parentsIdReverse.Contains(objectTypeId1) || parentsIdReverse.Contains(objectTypeId2))
            str1 = $"{Convert.ToString(hybridRowExp1["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(hybridRowExp1["cad0001f-306c-11d8-b4e9-00304f19f545"])}]";
          else
            continue;
        }
        HybridRowExp hybridRowExp2 = ti.savedDataByObjId(int64_2);
        string str2 = hybridRowExp2 == null ? $"{Convert.ToString(ti.savedData[0]["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(ti.savedData[0]["cad0001f-306c-11d8-b4e9-00304f19f545"])}]" : $"{Convert.ToString(hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"])} [{Convert.ToString(hybridRowExp2["cad0001f-306c-11d8-b4e9-00304f19f545"])}]";
        ti.userReport.Add($"Отсутствует количество  \"{str1}\" в \"{str2}\"");
      }
    }
  }

  internal void CheckClassParm(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    int indexByName1 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.guidPokupnoj);
    int indexByName2 = ti.savedData.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
    int indexByName3 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.guidClass);
    int indexByName4 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.guidRazmParm);
    if (indexByName1 < 0 || indexByName2 < 0)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(AVSServerPlugin.guidStandIzd));
    childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(AVSServerPlugin.guidProchIzd)));
    IUserSession session = ExpertServer.es.GetSession(ti);
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      int num = hybridRowExp[indexByName1] == null || hybridRowExp[indexByName1] == DBNull.Value ? 0 : (Convert.ToBoolean(hybridRowExp[indexByName1]) ? 1 : 0);
      int int32 = Convert.ToInt32(hybridRowExp[indexByName2]);
      if (num != 0 || childrenIdRecursive.Contains(int32))
      {
        bool flag1 = indexByName3 >= 0 && hybridRowExp[indexByName3] != null && hybridRowExp[indexByName3] != DBNull.Value && Convert.ToString(hybridRowExp[indexByName3]) != "";
        bool flag2 = indexByName4 >= 0 && hybridRowExp[indexByName4] != null && hybridRowExp[indexByName4] != DBNull.Value && Convert.ToString(hybridRowExp[indexByName4]) != "";
        if (!flag1 || !flag2)
        {
          long int64 = Convert.ToInt64(hybridRowExp[0]);
          QuickObjectInfo objectInfo = session.GetObjectInfo(int64);
          string str1 = "В изделии ";
          string str2 = $"{objectInfo.Caption}\" [{int64.ToString()}] ";
          string str3 = flag1 || flag2 ? (flag1 ? $"{str1}{str2}не заполнен атрибут \"Размеры и Параметры\"!" : $"{str1}{str2}не заполнен атрибут \"Класс\"!") : $"{str1}{str2}не заполнены атрибуты \"Класс\" и \"Размеры и Параметры\"!";
          ti.userReport.Add(str3);
        }
      }
    }
  }

  internal void CalcVSQuantities(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    if (!ti.savedData.Columns.Contains(ExpertAttrGUIDs.attrTotalForProduct))
      ti.savedData.AddColumn(ExpertAttrGUIDs.attrTotalForProduct, typeof (MeasuredValue));
    List<int> zeroTypes = new List<int>();
    List<object> objectList = (List<object>) Value;
    if (objectList.Count > 0)
    {
      string str1 = (string) objectList[0];
      if (str1 != null)
      {
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in str1.Split(chArray))
        {
          foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(Convert.ToInt32(str2)))
          {
            if (!zeroTypes.Contains(num))
              zeroTypes.Add(num);
          }
        }
      }
    }
    TempFormula tempFormula = (TempFormula) null;
    if (objectList.Count >= 4 && objectList[3] != null)
      tempFormula = objectList[3] as TempFormula;
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      double aValue = 0.0;
      long int64 = Convert.ToInt64(hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"]);
      if ((ti.ispList == null || ti.ispList.Count == 0) && ti.context.Contains(int64) || ti.ispList != null && ti.ispList.Count > 0 && ti.currentIsp == -1 && ti.ispList.Contains(int64))
        aValue = 1.0;
      if (ti.currentIsp != -1 && ti.ispList != null && ti.ispList[ti.currentIsp] == int64)
        aValue = 1.0;
      hybridRowExp[ExpertAttrGUIDs.attrTotalForProduct] = (object) new MeasuredValue(aValue, ExpertConsts.Consts.measureShtuk);
    }
    List<long> longList1;
    if (ti.app != null)
    {
      longList1 = ti.currentIsp == -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(ti.ispList[0])) : new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]));
    }
    else
    {
      longList1 = new List<long>();
      foreach (long projId in context)
      {
        HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(projId);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp row in hybridRowExpArray)
          {
            long int64 = Convert.ToInt64(row["cad00033-306c-11d8-b4e9-00304f19f545"]);
            if (tempFormula == null || ti.CheckRowCond(int64, row, tempFormula))
              longList1.Add(int64);
          }
        }
      }
    }
    List<long> longList2 = new List<long>();
    int index1 = 0;
    while (index1 < longList1.Count)
    {
      HybridRowExp row1 = ti.savedLinksByIdIndex(longList1[index1]);
      if (row1 == null)
      {
        longList1.RemoveAt(index1);
      }
      else
      {
        long int64 = Convert.ToInt64(row1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        int index2 = longList2.IndexOf(int64);
        if (index2 < 0)
        {
          longList2.Add(int64);
          ++index1;
        }
        else
        {
          HybridRowExp row2 = ti.savedLinksByIdIndex(longList1[index2]);
          MeasuredValue quantity1 = this.GetQuantity(row1);
          MeasuredValue quantity2 = this.GetQuantity(row2);
          MeasuredValue measuredValue = this.AddQuan(ti.userReport, quantity1, quantity2);
          row2[ExpertAttrGUIDs.attrQuantity] = (object) measuredValue;
          row1[ExpertAttrGUIDs.attrQuantity] = (object) measuredValue;
          longList1.RemoveAt(index1);
        }
      }
    }
    MeasuredValue rootVal = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
    List<long> stack = new List<long>();
    foreach (long linkId in longList1)
      this.PerformVSLink(ti, linkId, rootVal, stack, zeroTypes, tempFormula);
  }

  internal void PerformVSLink(
    ExpertServer.ExpServTask ti,
    long linkId,
    MeasuredValue rootVal,
    List<long> stack,
    List<int> zeroTypes,
    TempFormula filter)
  {
    HybridRowExp row1 = ti.savedLinksByIdIndex(linkId);
    if (row1 == null || filter != null && !ti.CheckRowCond(linkId, row1, filter))
      return;
    long int64_1 = Convert.ToInt64(row1["cad00035-306c-11d8-b4e9-00304f19f545"]);
    HybridRowExp row2 = ti.savedDataByPartId(int64_1);
    if (row2 == null)
      return;
    long int64_2 = Convert.ToInt64(row2[0]);
    bool leaveZeros = false;
    if (zeroTypes != null)
    {
      int indexByName = ti.savedData.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
      int num = indexByName < 0 ? ExpertServer.es.GetSession(ti).GetObjectInfo(int64_2).ObjectTypeID : Convert.ToInt32(row2[indexByName]);
      if (num >= 0 && zeroTypes.Contains(num))
        leaveZeros = true;
    }
    MeasuredValue q = this.GetQuantity(row1, leaveZeros) ?? (leaveZeros ? ExpertConsts.NolShtuk : ExpertConsts.OneShtuka);
    MeasuredValue measuredValue = this.MultQuan(ti.userReport, q, rootVal);
    MeasuredValue q1 = (MeasuredValue) row2[ExpertAttrGUIDs.attrTotalForProduct];
    if (q1.Value == 0.0)
      row2[ExpertAttrGUIDs.attrTotalForProduct] = (object) measuredValue;
    else
      this.AddQuan(ref q1, measuredValue, ti.userReport, row2);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.PerformVSLink(ti, int64_3, measuredValue, stack, zeroTypes, filter);
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal void MakeSPRazdels(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.specRazdelStr))
      ti.savedData.AddColumn(AVSServerPlugin.specRazdelStr, typeof (string));
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.attrEntranceLevel))
      ti.savedData.AddColumn(AVSServerPlugin.attrEntranceLevel, typeof (long));
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.specRazdelNum))
      ti.savedData.AddColumn(AVSServerPlugin.specRazdelNum, typeof (long));
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      hybridRowExp[AVSServerPlugin.specRazdelStr] = (object) "";
      hybridRowExp[AVSServerPlugin.attrEntranceLevel] = (object) 0;
    }
    List<long> longList;
    if (ti.app != null)
    {
      longList = ti.currentIsp == -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(ti.ispList[0])) : new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]));
    }
    else
    {
      longList = new List<long>();
      foreach (long projId in context)
      {
        HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(projId);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
            longList.Add(Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]));
        }
      }
    }
    int objectTypeId = MetaDataHelper.GetObjectTypeID(AVSServerPlugin.objTypeRSP);
    int index1 = 0;
    while (index1 < longList.Count)
    {
      long linkId = longList[index1];
      HybridRowExp hybridRowExp = ti.savedLinksByIdIndex(linkId);
      if (hybridRowExp == null)
      {
        longList.RemoveAt(index1);
      }
      else
      {
        long int64 = Convert.ToInt64(hybridRowExp["cad00035-306c-11d8-b4e9-00304f19f545"]);
        int index2 = ti.savedDataByPartIdIndex(int64);
        if (index2 == -1)
        {
          longList.RemoveAt(index1);
        }
        else
        {
          if (Convert.ToInt32(ti.savedData[index2]["cad0002e-306c-11d8-b4e9-00304f19f545"]) == objectTypeId)
            longList[index1] = -1L;
          ++index1;
        }
      }
    }
    List<long> stack = new List<long>();
    foreach (long linkId in longList)
    {
      if (linkId != -1L)
        this.PerformRazdelLink(ti, linkId, stack);
    }
    string str1 = "";
    HybridRowExp hybridRowExp1 = ti.savedDataByObjId(context[0]);
    if (hybridRowExp1 != null)
      str1 = Convert.ToString(hybridRowExp1["cad0001f-306c-11d8-b4e9-00304f19f545"]);
    IUserSession session = ExpertServer.es.GetSession(ti);
    if (DocumentTypeWeightHelper.items == null)
      DocumentTypeWeightHelper.LoadSystemCollection(session);
    for (int index3 = 0; index3 < ti.savedData.RowsCount; ++index3)
    {
      HybridRowExp hybridRowExp2 = ti.savedData[index3];
      if (Convert.ToString(hybridRowExp2[AVSServerPlugin.specRazdelStr]) == "Документация")
      {
        string str2 = Convert.ToString(hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"]);
        int int32 = Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]);
        DocumentTypeSettings documentTypeSettings = new DocumentTypeSettings();
        DocumentTypeSettings docTypeSettings;
        if (!this._docTypeToDocTypeName.ContainsKey(int32))
        {
          if (this._docTypeService == null)
            this._docTypeService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
          docTypeSettings = this.GetDocTypeSettings(session, int32);
        }
        else
          docTypeSettings = this._docTypeToDocTypeName[int32];
        bool flag = Convert.ToString(hybridRowExp2["cad0001f-306c-11d8-b4e9-00304f19f545"]).StartsWith(str1);
        if (docTypeSettings.DocumentNameInStamp)
        {
          if (!flag)
          {
            if (!str2.EndsWith(docTypeSettings.DocumentTypeName))
              hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"] = (object) $"{str2}. {docTypeSettings.DocumentTypeName}";
          }
          else
            hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"] = (object) docTypeSettings.DocumentTypeName;
        }
        else
          hybridRowExp2["cad00020-306c-11d8-b4e9-00304f19f545"] = (object) str2;
        long weight = DocumentTypeWeightHelper.items.GetWeight(int32);
        if (!flag)
          weight += 1073741824L /*0x40000000*/;
        hybridRowExp2[AVSServerPlugin.attrEntranceLevel] = (object) weight;
      }
    }
  }

  internal void PerformRazdelLink(ExpertServer.ExpServTask ti, long linkId, List<long> stack)
  {
    HybridRowExp hybridRowExp1 = ti.savedLinksByIdIndex(linkId);
    if (hybridRowExp1 == null)
      return;
    long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
    long num = -1;
    if (hybridRowExp1[AVSServerPlugin.specRazdel] != null && hybridRowExp1[AVSServerPlugin.specRazdel] != DBNull.Value)
      num = Convert.ToInt64(hybridRowExp1[AVSServerPlugin.specRazdel]);
    AVSServerPlugin.razdInfo razdInfo = (AVSServerPlugin.razdInfo) null;
    if (this.razdObjects.ContainsKey(num))
      razdInfo = this.razdObjects[num];
    else if (num != -1L)
    {
      IUserSession session = ExpertServer.es.GetSession(ti);
      try
      {
        IDBObject dbObject = session.GetObject(num);
        razdInfo = new AVSServerPlugin.razdInfo(dbObject.Caption, Convert.ToInt64(dbObject.GetAttributeByGuid(new Guid(AVSServerPlugin.specRazdelNum)).Value));
        this.razdObjects.Add(num, razdInfo);
      }
      catch
      {
      }
    }
    HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp2 == null)
      return;
    long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
    if (razdInfo == null)
    {
      ti.InitDocTypes();
      QuickObjectInfo objectInfo = ExpertServer.es.GetSession(ti).GetObjectInfo(int64_2);
      if (!ti.docTypes.Contains(objectInfo.ObjectTypeID))
        ti.userReport.Add($"У объекта [{Convert.ToString(int64_2)}] отсутствует раздел спецификации!");
    }
    else
    {
      hybridRowExp2[AVSServerPlugin.specRazdelStr] = (object) razdInfo.Name;
      hybridRowExp2[AVSServerPlugin.specRazdelNum] = (object) razdInfo.razdNum;
    }
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp3 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp3["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.PerformRazdelLink(ti, int64_3, stack);
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal void CalcVPQuantities(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    IUserSession session = ExpertServer.es.GetSession(ti);
    int count = ti.savedLinks.Columns.Count;
    if (!ti.savedLinks.Columns.Contains(AVSServerPlugin.Quan1))
      ti.savedLinks.AddColumn(AVSServerPlugin.Quan1, typeof (MeasuredValue));
    if (!ti.savedLinks.Columns.Contains(AVSServerPlugin.Quan2))
      ti.savedLinks.AddColumn(AVSServerPlugin.Quan2, typeof (MeasuredValue));
    int indexByName1 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.Quan1);
    int indexByName2 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.Quan2);
    int indexByName3 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.RegQuan);
    int indexByName4 = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.RegPercent);
    MeasuredValue measuredValue1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      savedLink[indexByName1] = savedLink[indexByName2] = (object) measuredValue1;
      if (savedLink[indexByName3] == DBNull.Value || savedLink[indexByName3] == null)
        savedLink[indexByName3] = (object) measuredValue1;
    }
    bool useIsps = true;
    long isp = context[0];
    if (ti.currentIsp >= 0)
      isp = ti.ispList[ti.currentIsp];
    HybridRowExp hybridRowExp1 = ti.savedDataByObjId(isp);
    IImbaseServer customService = (IImbaseServer) session.GetCustomService(typeof (IImbaseServer));
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj1 = savedLink[indexByName3];
      if (obj1.Equals((object) measuredValue1) || obj1.Equals((object) measuredValue1.ToString()))
      {
        if (savedLink[indexByName4] != DBNull.Value)
        {
          double num = Convert.ToDouble(savedLink[AVSServerPlugin.RegPercent]);
          MeasuredValue q1 = (MeasuredValue) savedLink[AVSServerPlugin.Quan1];
          MeasuredValue q2 = (MeasuredValue) savedLink[AVSServerPlugin.Quan2];
          MeasuredValue measuredValue2 = this.AddQuan(ti.userReport, q1, q2);
          savedLink[AVSServerPlugin.RegQuan] = (object) new MeasuredValue(measuredValue2.Value * num / 100.0, measuredValue2.MeasureID);
        }
        else
        {
          long int64_1 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
          double num = 0.0;
          HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
          if (hybridRowExp2 != null)
          {
            long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
            IDBObject dbObject = session.GetObject(int64_2);
            if (dbObject != null)
            {
              IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid(AVSServerPlugin.RegPercent));
              if (attributeByGuid1 != null && attributeByGuid1.Value != DBNull.Value)
              {
                num = Convert.ToDouble(attributeByGuid1.Value);
              }
              else
              {
                try
                {
                  IDBObject prototypeObject = customService.GetPrototypeObject(session.SessionGUID, (object) int64_2);
                  if (prototypeObject != null)
                  {
                    IDBAttribute attributeByGuid2 = prototypeObject.GetAttributeByGuid(new Guid(AVSServerPlugin.RegPercent), false);
                    if (attributeByGuid2 != null)
                      num = Convert.ToDouble(attributeByGuid2.Value);
                  }
                }
                catch
                {
                }
              }
              if (Math.Abs(num - 1E-05) > 1E-05)
              {
                MeasuredValue q1 = (MeasuredValue) savedLink[AVSServerPlugin.Quan1];
                MeasuredValue q2 = (MeasuredValue) savedLink[AVSServerPlugin.Quan2];
                MeasuredValue measuredValue3 = this.AddQuan(ti.userReport, q1, q2);
                savedLink[indexByName3] = (object) new MeasuredValue(measuredValue3.Value * num / 100.0, measuredValue3.MeasureID);
              }
            }
          }
        }
      }
    }
    if (hybridRowExp1 == null)
      return;
    Convert.ToInt32(hybridRowExp1["cad0002e-306c-11d8-b4e9-00304f19f545"]);
    List<long> stack = new List<long>();
    this.AddToLinks(ti, isp, useIsps, new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk), new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk), stack);
  }

  internal void AddToLinks(
    ExpertServer.ExpServTask ti,
    long objId,
    bool useIsps,
    MeasuredValue quan1,
    MeasuredValue quan2,
    List<long> stack)
  {
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(objId);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    List<long> longList1;
    if (useIsps && ti.ispList != null && ti.ispList.Count > 0)
    {
      longList1 = ti.currentIsp != -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(objId)) : new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(objId));
    }
    else
    {
      longList1 = new List<long>();
      foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
        longList1.Add(Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]));
    }
    List<long> longList2 = new List<long>();
    int index1 = 0;
    while (index1 < longList1.Count)
    {
      HybridRowExp hybridRowExp1 = ti.savedLinksByIdIndex(longList1[index1]);
      if (hybridRowExp1 == null)
      {
        longList1.RemoveAt(index1);
      }
      else
      {
        long int64 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        int index2 = longList2.IndexOf(int64);
        if (index2 < 0)
        {
          longList2.Add(int64);
          ++index1;
        }
        else
        {
          HybridRowExp hybridRowExp2 = ti.savedLinksByIdIndex(longList1[index2]);
          MeasuredValue q1_1 = (MeasuredValue) hybridRowExp1[AVSServerPlugin.Quan1] ?? ExpertConsts.OneShtuka;
          MeasuredValue q2_1 = (MeasuredValue) hybridRowExp2[AVSServerPlugin.Quan1] ?? ExpertConsts.OneShtuka;
          hybridRowExp2[AVSServerPlugin.Quan1] = (object) this.AddQuan(ti.userReport, q1_1, q2_1);
          MeasuredValue q1_2 = (MeasuredValue) hybridRowExp1[AVSServerPlugin.Quan2] ?? ExpertConsts.OneShtuka;
          MeasuredValue q2_2 = (MeasuredValue) hybridRowExp2[AVSServerPlugin.Quan1] ?? ExpertConsts.OneShtuka;
          hybridRowExp2[AVSServerPlugin.Quan2] = (object) this.AddQuan(ti.userReport, q1_2, q2_2);
          MeasuredValue q1_3 = (MeasuredValue) hybridRowExp1[AVSServerPlugin.RegQuan] ?? ExpertConsts.OneShtuka;
          MeasuredValue q2_3 = (MeasuredValue) hybridRowExp2[AVSServerPlugin.RegQuan] ?? ExpertConsts.OneShtuka;
          hybridRowExp2[AVSServerPlugin.RegQuan] = (object) this.AddQuan(ti.userReport, q1_3, q2_3);
          hybridRowExp1[AVSServerPlugin.Quan1] = (object) new MeasuredValue(-1.0, ExpertConsts.Consts.measureShtuk);
          longList1.RemoveAt(index1);
        }
      }
    }
    stack.Add(objId);
    try
    {
      foreach (HybridRowExp row in hybridRowExpArray)
      {
        if (useIsps)
        {
          long int64 = Convert.ToInt64(row["cad00033-306c-11d8-b4e9-00304f19f545"]);
          if (!longList1.Contains(int64))
            continue;
        }
        long int64_1 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
        HybridRowExp hybridRowExp = ti.savedDataByPartId(int64_1);
        if (hybridRowExp != null)
        {
          bool flag = ExpertServer.IsTypeDescendant(ExpertConsts.Consts.izdComplect, Convert.ToInt32(hybridRowExp["cad0002e-306c-11d8-b4e9-00304f19f545"]));
          if (!flag && row.Columns.Contains(AVSServerPlugin.attrSpecRazdel))
          {
            object obj = row[AVSServerPlugin.attrSpecRazdel];
            if (obj != null && obj != DBNull.Value && Convert.ToInt32(obj) == 2692)
              flag = true;
          }
          MeasuredValue quan = this.GetQuantity(row) ?? ExpertConsts.OneShtuka;
          MeasuredValue measuredValue1 = this.MultQuan(ti.userReport, quan1, quan);
          MeasuredValue measuredValue2 = this.MultQuan(ti.userReport, quan2, quan);
          if (flag)
          {
            measuredValue2 = this.AddQuan(ti.userReport, measuredValue1, measuredValue2);
            measuredValue1 = new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
            row[AVSServerPlugin.Quan1] = (object) measuredValue1;
            row[AVSServerPlugin.Quan2] = (object) this.AddQuan(ti.userReport, this.AddQuan(ti.userReport, (MeasuredValue) row[AVSServerPlugin.Quan1], (MeasuredValue) row[AVSServerPlugin.Quan2]), measuredValue2);
          }
          else
          {
            row[AVSServerPlugin.Quan1] = (object) this.AddQuan(ti.userReport, (MeasuredValue) row[AVSServerPlugin.Quan1], measuredValue1);
            row[AVSServerPlugin.Quan2] = (object) this.AddQuan(ti.userReport, (MeasuredValue) row[AVSServerPlugin.Quan2], measuredValue2);
          }
          long int64_2 = Convert.ToInt64(hybridRowExp[0]);
          if (!stack.Contains(int64_2))
            this.AddToLinks(ti, int64_2, false, measuredValue1, measuredValue2, stack);
        }
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal void PerformZags(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    ExpertServer.es.GetSession(ti);
    int relationTypeId = MetaDataHelper.GetRelationTypeID(AVSServerPlugin.linkZagot);
    List<long> longList = new List<long>();
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    for (int index1 = 0; index1 < ti.savedLinks.RowsCount; ++index1)
    {
      HybridRowExp savedLink = ti.savedLinks[index1];
      if (Convert.ToInt32(savedLink["cad00036-306c-11d8-b4e9-00304f19f545"]) == relationTypeId)
      {
        long int64_1 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
        int index2 = ti.savedDataByPartIdIndex(int64_1);
        if (index2 >= 0)
        {
          long int64_2 = Convert.ToInt64(ti.savedData[index2][0]);
          if (!longList.Contains(int64_2))
          {
            longList.Add(int64_2);
            intList1.Add(index2);
            intList2.Add(index1);
          }
        }
      }
    }
    List<int> intList3 = new List<int>();
    int indexByName1 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.attrMaterial);
    int indexByName2 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.attributeClass);
    int indexByName3 = ti.savedData.Columns.GetIndexByName(AVSServerPlugin.attributeRazmParms);
    for (int index3 = 0; index3 < ti.savedData.RowsCount; ++index3)
    {
      HybridRowExp hybridRowExp1 = ti.savedData[index3];
      if (hybridRowExp1[indexByName1] != null && hybridRowExp1[indexByName1] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(hybridRowExp1[indexByName1]);
        int index4 = longList.IndexOf(int64);
        if (index4 >= 0)
        {
          intList3.Add(index4);
          HybridRowExp hybridRowExp2 = ti.savedData[intList1[index4]];
          string str1 = "";
          string str2 = "";
          if (hybridRowExp2[indexByName2] != null && hybridRowExp2[indexByName2] != DBNull.Value)
            str1 = Convert.ToString(hybridRowExp2[indexByName2]);
          if (hybridRowExp2[indexByName3] != null && hybridRowExp2[indexByName3] != DBNull.Value)
            str2 = Convert.ToString(hybridRowExp2[indexByName3]);
          if (str2 != "")
          {
            string str3 = $"{str1} {str2}";
            hybridRowExp2[indexByName2] = (object) str3;
          }
          string str4 = Convert.ToString(hybridRowExp1["cad0001f-306c-11d8-b4e9-00304f19f545"]);
          hybridRowExp2[indexByName3] = (object) $"(заготовка для {str4})";
        }
      }
    }
    bool flag = false;
    int index = intList2.Count - 1;
    while (index >= 0)
    {
      if (intList3.Contains(index))
      {
        --index;
      }
      else
      {
        ti.savedLinks.RemoveAt(intList2[index]);
        --index;
        flag = true;
      }
    }
    if (!flag)
      return;
    ExpertServer.CreateLinkIndex(ti);
  }

  internal int FindColumnByName(List<ColumnDescriptor> colList, string Name)
  {
    for (int index = 0; index < colList.Count; ++index)
    {
      if (colList[index].AttributeID.ToString() == Name)
        return index;
    }
    return -1;
  }

  internal void GetImbaseInfo(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    IUserSession session = ExpertServer.es.GetSession(ti);
    List<int> attrIds = new List<int>();
    List<int> intList = new List<int>();
    HybridColumnsExp columns = ti.savedData.Columns;
    if (columns.GetIndexByName(AVSServerPlugin.RegPercent) < 0)
    {
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.RegPercent, typeof (double)));
      int indexByName = columns.GetIndexByName(AVSServerPlugin.RegPercent);
      for (int index = 0; index < ti.savedData.RowsCount; ++index)
        ti.savedData[index][indexByName] = (object) 0.0f;
    }
    if (columns.GetIndexByName(AVSServerPlugin.RegQuan) < 0)
    {
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.RegQuan, typeof (string)));
      int indexByName = columns.GetIndexByName(AVSServerPlugin.RegQuan);
      for (int index = 0; index < ti.savedData.RowsCount; ++index)
        ti.savedData[index][indexByName] = (object) "";
    }
    if (columns.GetIndexByName(AVSServerPlugin.GOST) < 0)
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.GOST, typeof (string)));
    if (columns.GetIndexByName(AVSServerPlugin.OKP_Code) < 0)
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.OKP_Code, typeof (string)));
    if (columns.GetIndexByName(AVSServerPlugin.Supplier) < 0)
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.Supplier, typeof (string)));
    if (columns.GetIndexByName(AVSServerPlugin.CLASS) < 0)
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.CLASS, typeof (string)));
    if (columns.GetIndexByName(AVSServerPlugin.RAZM_PARM) < 0)
      columns.Add(new HybridColumnsExp.HybridColumnExp(AVSServerPlugin.RAZM_PARM, typeof (string)));
    for (int index = 0; index < columns.Count; ++index)
    {
      if (columns[index].ColumnName == AVSServerPlugin.RegPercent || columns[index].ColumnName == AVSServerPlugin.RegQuan || columns[index].ColumnName == AVSServerPlugin.GOST || columns[index].ColumnName == AVSServerPlugin.OKP_Code || columns[index].ColumnName == AVSServerPlugin.Supplier || columns[index].ColumnName == AVSServerPlugin.CLASS || columns[index].ColumnName == AVSServerPlugin.RAZM_PARM)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(columns[index].ColumnName));
        attrIds.Add(attributeType.AttributeID);
        intList.Add(index);
      }
    }
    for (int index1 = 0; index1 < ti.savedData.RowsCount; ++index1)
    {
      HybridRowExp hybridRowExp = ti.savedData[index1];
      long int64 = Convert.ToInt64(hybridRowExp[0]);
      List<int> indexes = (List<int>) null;
      IDBObject idbO = session.GetObject(int64);
      DataRow imbaseData = ExpertServer.GetImbaseData(session, idbO, attrIds, out indexes);
      if (imbaseData != null)
      {
        for (int index2 = 0; index2 < attrIds.Count; ++index2)
        {
          int columnIndex = indexes[index2];
          if (columnIndex >= 0)
          {
            object obj1 = imbaseData[columnIndex];
            if (obj1 != null && obj1 != DBNull.Value && (hybridRowExp[intList[index2]] == null || hybridRowExp[intList[index2]] == DBNull.Value || hybridRowExp[intList[index2]].Equals((object) "")))
              hybridRowExp[intList[index2]] = obj1;
          }
        }
      }
    }
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      string str1 = Convert.ToString(hybridRowExp[AVSServerPlugin.GOST]);
      if (!(str1 == ""))
      {
        string str2 = Convert.ToString(hybridRowExp["cad00020-306c-11d8-b4e9-00304f19f545"]);
        int startIndex = str2.IndexOf(str1);
        if (startIndex >= 0)
        {
          string str3 = str2.Remove(startIndex, str1.Length).Trim();
          hybridRowExp["cad00020-306c-11d8-b4e9-00304f19f545"] = (object) str3;
        }
      }
    }
  }

  internal MeasuredValue MultQuan(List<string> userReport, MeasuredValue q, MeasuredValue quan)
  {
    if (quan.Value == 0.0)
      return new MeasuredValue(0.0, q.MeasureID);
    if (q.Value == 0.0)
      return new MeasuredValue(0.0, quan.MeasureID);
    if (quan.Value == 1.0 && quan.MeasureID == ExpertConsts.Consts.measureShtuk)
      return (MeasuredValue) q.Clone();
    if (q.Value == 1.0 && q.MeasureID == ExpertConsts.Consts.measureShtuk)
      return (MeasuredValue) quan.Clone();
    try
    {
      return MeasureHelper.Multiply(q, quan);
    }
    catch (Exception ex)
    {
      userReport.Add("Ошибка умножения: " + ex.Message);
      long measureId = q.MeasureID;
      if (measureId == ExpertConsts.Consts.measureShtuk)
        measureId = quan.MeasureID;
      return new MeasuredValue(q.Value * quan.Value, measureId);
    }
  }

  internal MeasuredValue AddQuan(List<string> userReport, MeasuredValue q1, MeasuredValue q2)
  {
    try
    {
      return MeasureHelper.Add(q1, q2);
    }
    catch (Exception ex)
    {
      long measureId = q1.MeasureID;
      if (measureId == ExpertConsts.Consts.measureShtuk)
        measureId = q2.MeasureID;
      else
        userReport.Add("Ошибка сложения: " + ex.Message);
      return new MeasuredValue(q1.Value + q2.Value, measureId);
    }
  }

  internal MeasuredValue DivideQuan(List<string> userReport, MeasuredValue q1, MeasuredValue q2)
  {
    if (q1.Value == 0.0)
      return new MeasuredValue(0.0, q1.MeasureID);
    if (q2.Value == 0.0)
      return new MeasuredValue(q1.Value > 0.0 ? double.PositiveInfinity : double.NegativeInfinity, q1.MeasureID);
    try
    {
      return MeasureHelper.Divide(q1, q2);
    }
    catch (Exception ex)
    {
      userReport.Add("Ошибка деления: " + ex.Message);
      return new MeasuredValue(q1.Value / q2.Value, q1.MeasureID);
    }
  }

  internal string MultQuan(
    ref MeasuredValue q,
    MeasuredValue quan,
    List<string> userReport,
    HybridRowExp row)
  {
    if (quan.Value == 0.0)
    {
      q.Value = 0.0;
      return (string) null;
    }
    if (q.Value == 0.0)
      return (string) null;
    try
    {
      q.Multiply(quan);
      return (string) null;
    }
    catch (Exception ex)
    {
      long measureId = q.MeasureID;
      if (measureId == ExpertConsts.Consts.measureShtuk)
        measureId = quan.MeasureID;
      q = new MeasuredValue(q.Value * quan.Value, measureId);
      return this.AddUserReport("Ошибка умножения: " + ex.Message, userReport, row);
    }
  }

  internal string AddQuan(
    ref MeasuredValue q1,
    MeasuredValue q2,
    List<string> userReport,
    HybridRowExp row)
  {
    try
    {
      q1.Add(q2);
      return (string) null;
    }
    catch (Exception ex)
    {
      long measureId = q1.MeasureID;
      if (measureId == ExpertConsts.Consts.measureShtuk)
        measureId = q2.MeasureID;
      q1 = new MeasuredValue(q1.Value + q2.Value, measureId);
      return (q1.MeasureID == ExpertConsts.Consts.measureShtuk ? 1 : (q2.MeasureID == ExpertConsts.Consts.measureShtuk ? 1 : 0)) != 0 ? (string) null : this.AddUserReport("Ошибка сложения: " + ex.Message, userReport, row);
    }
  }

  internal string DivideQuan(
    MeasuredValue q1,
    MeasuredValue q2,
    List<string> userReport,
    HybridRowExp row)
  {
    if (q1.Value == 0.0)
      return (string) null;
    if (q2.Value == 0.0)
      q1 = new MeasuredValue(q1.Value > 0.0 ? double.PositiveInfinity : double.NegativeInfinity, q1.MeasureID);
    try
    {
      q1.Divide(q2);
      return (string) null;
    }
    catch (Exception ex)
    {
      q1.Value /= q2.Value;
      return this.AddUserReport("Ошибка деления: " + ex.Message, userReport, row);
    }
  }

  internal string AddUserReport(string s, List<string> userReport, HybridRowExp row)
  {
    if (s == null)
      return (string) null;
    s = $"{s} для объекта [{Convert.ToString(row[0])}] ";
    int indexByName = row.Columns.GetIndexByName("cad00047-306c-11d8-b4e9-00304f19f545");
    string str = row[indexByName] == null ? "" : Convert.ToString(row[indexByName]);
    if (indexByName >= 0)
      s = $"{s}\"{str}\"";
    userReport.Add(s);
    return s;
  }

  internal void CalcIspoln(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    IUserSession session1 = ExpertServer.es.GetSession(ti);
    IArticleService service = (IArticleService) this._servProvider.GetService(typeof (IArticleService));
    long articleID1 = context[0];
    long articleID2 = articleID1;
    IUserSession session2 = session1;
    service.GetListInstances(articleID2, (object) session2);
    IDBRelationType relationType = session1.GetRelationType(new Guid(AVSServerPlugin.linkSostavIzd));
    ((ISubstitutesService) this._servProvider.GetService(typeof (ISubstitutesService))).FindCommonAndVariableParts(ExpertServer.GetSessionGuid(ti), ti.verRuleOwnerId, articleID1, relationType.RelationType, AVSSpecificationForm.A);
  }

  internal void AddHiers(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.hierLevel))
      ti.savedData.AddColumn(AVSServerPlugin.hierLevel, typeof (long));
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
      ti.savedData[index][AVSServerPlugin.hierLevel] = (object) 0;
    this.SetMaxLevel(ti, context[0], 0L);
  }

  internal void SetMaxLevel(ExpertServer.ExpServTask ti, long objId, long curLevel)
  {
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(objId);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    foreach (HybridRowExp hybridRowExp1 in hybridRowExpArray)
    {
      long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
      HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
      if (hybridRowExp2 != null)
      {
        long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
        long curLevel1 = Convert.ToInt64(hybridRowExp2[AVSServerPlugin.hierLevel]);
        if (curLevel + 1L > curLevel1)
        {
          curLevel1 = curLevel + 1L;
          hybridRowExp2[AVSServerPlugin.hierLevel] = (object) curLevel1;
        }
        this.SetMaxLevel(ti, int64_2, curLevel1);
      }
    }
  }

  private DocumentTypeSettings GetDocTypeSettings(IUserSession ius, int docType)
  {
    DocumentTypeSettings docTypeSettings = new DocumentTypeSettings();
    if (this._docTypeToDocTypeName.TryGetValue(docType, out docTypeSettings) || this._docTypeService == null)
      return docTypeSettings;
    DocumentTypeSettings settings = this._docTypeService.GetSettings(ius.SessionGUID, docType);
    docTypeSettings = settings;
    this._docTypeToDocTypeName[docType] = settings;
    return docTypeSettings;
  }

  internal void MarkEntersIn(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    long int64_1 = Convert.ToInt64((string) objectList[0]);
    if (!ti.savedData.Columns.Contains(AVSServerPlugin.Flag3))
      ti.savedData.AddColumn(AVSServerPlugin.Flag3, typeof (bool));
    IDBRelationCollection relationCollection = ExpertServer.es.GetSession(ti).GetRelationCollection(-1);
    relationCollection.LocalTypesMode = true;
    for (int index = 0; index < ti.savedData.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = ti.savedData[index];
      long int64_2 = Convert.ToInt64(hybridRowExp[0]);
      if (ti.ispList.Contains(int64_2))
      {
        hybridRowExp[AVSServerPlugin.Flag3] = (object) false;
      }
      else
      {
        bool flag = relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.Equal, (object) int64_1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value)
        }, new object[3]
        {
          (object) -21,
          (object) -22,
          (object) -2
        })
        {
          Tags = ti.filtr()
        }, int64_2).Rows.Count > 0;
        hybridRowExp[AVSServerPlugin.Flag3] = (object) flag;
      }
    }
  }

  internal void AddVedomsToGlobal(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    long int64_1 = Convert.ToInt64((string) objectList[0]);
    IDBRelationCollection relationCollection = ExpertServer.es.GetSession(ti).GetRelationCollection(-1);
    bool flag = false;
    int rowsCount = ti.savedData.RowsCount;
    for (int index = 0; index < rowsCount; ++index)
    {
      long int64_2 = Convert.ToInt64(ti.savedData[index][0]);
      if ((ti.ispList == null || !ti.ispList.Contains(int64_2) && !ti.ispList.Contains(-int64_2)) && (ti.ispList != null || int64_2 != context[0] && int64_2 != -context[0]) && ti._notExpandedObjIds.Contains(Math.Abs(int64_2)))
      {
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.Equal, (object) int64_1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value)
        };
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(new DBRecordSetParams(conditions, new List<ColumnDescriptor>()
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) -22, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) ExpertConsts.Consts._attrObjDesign, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1)
        }.ToArray(), lastOrderValue: (object) 0, recordCount: 0)
        {
          Tags = ti.filtr()
        }, int64_2).Rows)
        {
          long int64_3 = Convert.ToInt64(row[0]);
          long int64_4 = Convert.ToInt64(row[3]);
          flag = true;
          if (!ti.dataObjIndex.ContainsKey(int64_3))
          {
            HybridRowExp hrow = ti.savedData.NewRow();
            hrow["cad00029-306c-11d8-b4e9-00304f19f545"] = (object) int64_3;
            hrow["cad0002e-306c-11d8-b4e9-00304f19f545"] = row[1];
            hrow["cad00035-306c-11d8-b4e9-00304f19f545"] = (object) int64_4;
            if (ti.savedData.Columns.Contains("cad00047-306c-11d8-b4e9-00304f19f545"))
              hrow["cad00047-306c-11d8-b4e9-00304f19f545"] = row[5];
            if (ti.savedData.Columns.Contains("cad00034-306c-11d8-b4e9-00304f19f545"))
              hrow["cad00034-306c-11d8-b4e9-00304f19f545"] = row[2];
            if (ti.savedData.Columns.Contains("cad00033-306c-11d8-b4e9-00304f19f545"))
              hrow["cad00033-306c-11d8-b4e9-00304f19f545"] = row[4];
            if (ti.savedData.Columns.Contains("cad0001f-306c-11d8-b4e9-00304f19f545") && row[6] != DBNull.Value)
              hrow["cad0001f-306c-11d8-b4e9-00304f19f545"] = row[6];
            ti.savedData.Add(hrow);
            int num = ti.savedData.RowsCount - 1;
            ti.dataObjIndex.Add(int64_3, num);
            ti.dataPartIndex.Add(int64_4, num);
          }
          HybridRowExp hrow1 = ti.savedLinks.NewRow();
          hrow1["cad00033-306c-11d8-b4e9-00304f19f545"] = row[4];
          hrow1["cad00034-306c-11d8-b4e9-00304f19f545"] = row[2];
          hrow1["cad00035-306c-11d8-b4e9-00304f19f545"] = (object) int64_4;
          if (ti.savedLinks.Columns.Contains("cad00029-306c-11d8-b4e9-00304f19f545"))
            hrow1["cad00029-306c-11d8-b4e9-00304f19f545"] = (object) int64_3;
          ti.savedLinks.Add(hrow1);
        }
      }
    }
    if (!flag)
      return;
    ExpertServer.CreateLinkIndex(ti);
  }

  internal void ExpandAttr(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 2)
      return;
    string anAttributeName1 = (string) objectList[0];
    string anAttributeName2 = (string) objectList[1];
    if (anAttributeName1 == "" || anAttributeName2 == "")
      return;
    IUserSession session = ExpertServer.es.GetSession(ti);
    IDBAttributeType attributeType1 = session.GetAttributeType(anAttributeName1, false);
    if (attributeType1 == null)
      return;
    string columnName = attributeType1.PropertiesStructure.AttributeGuid.ToString();
    if (!ti.savedLinks.Columns.Contains(columnName))
      return;
    IDBAttributeType attributeType2 = session.GetAttributeType(anAttributeName2, false);
    if (attributeType2 == null)
      return;
    string str1 = attributeType2.PropertiesStructure.AttributeGuid.ToString();
    if (!ti.savedLinks.Columns.Contains(str1))
      ti.savedLinks.AddColumn(str1, typeof (string));
    session.GetRelationCollection(-1).LocalTypesMode = true;
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      string str2 = Convert.ToString(savedLink[columnName]);
      while (str2.Length < 8)
        str2 = "!" + str2;
      savedLink[str1] = (object) str2;
    }
  }

  internal void RemoveZamens(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null || !ti.savedLinks.Columns.Contains(AVSServerPlugin.numZamen))
      return;
    ExpertServer.es.GetSession(ti);
    List<long> longList = new List<long>();
    int indexByName = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.numZamen);
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj1 = savedLink[indexByName];
      if (obj1 != null && obj1 != DBNull.Value && Convert.ToInt32(obj1) > 0)
        longList.Add(Convert.ToInt64(savedLink[0]));
    }
    if (longList.Count <= 0)
      return;
    int index1 = 0;
    int index2 = 0;
    while (index1 < ti.savedLinks.RowsCount)
    {
      if (Convert.ToInt64(ti.savedLinks[index1][0]) == longList[index2])
      {
        ti.savedLinks.RemoveAt(index1);
        ++index2;
        if (index2 >= longList.Count)
          break;
      }
      else
        ++index1;
    }
    ExpertServer.CreateLinkIndex(ti);
  }

  internal void MarkBaseLinks(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    if (!ti.savedLinks.Columns.Contains(AVSServerPlugin.flagOA))
      ti.savedLinks.AddColumn(AVSServerPlugin.flagOA, typeof (bool));
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
      ti.savedLinks[index][AVSServerPlugin.flagOA] = (object) false;
    if (ti.app == null)
      return;
    foreach (long key in ti.app.CommonParts.Keys)
    {
      foreach (long linkId in ti.app.CommonParts[key])
      {
        HybridRowExp hybridRowExp = ti.savedLinksByIdIndex(linkId);
        if (hybridRowExp != null)
          hybridRowExp[AVSServerPlugin.flagOA] = (object) true;
      }
    }
    List<long> longList = ti.currentIsp == -1 ? new List<long>((IEnumerable<long>) ti.app.GetArticleCommonPart(ti.ispList[0])) : new List<long>((IEnumerable<long>) ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]));
    List<long> stack = new List<long>();
    foreach (long linkId in longList)
      this.PerformMarkLink(ti, linkId, stack);
  }

  internal void PerformMarkLink(ExpertServer.ExpServTask ti, long linkId, List<long> stack)
  {
    HybridRowExp hybridRowExp1 = ti.savedLinksByIdIndex(linkId);
    if (hybridRowExp1 == null)
      return;
    hybridRowExp1[AVSServerPlugin.flagOA] = (object) true;
    long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
    HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
    if (hybridRowExp2 == null)
      return;
    long int64_2 = Convert.ToInt64(hybridRowExp2[0]);
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByProjId(int64_2);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return;
    stack.Add(linkId);
    try
    {
      foreach (HybridRowExp hybridRowExp3 in hybridRowExpArray)
      {
        long int64_3 = Convert.ToInt64(hybridRowExp3["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!stack.Contains(int64_3))
          this.PerformMarkLink(ti, int64_3, stack);
      }
    }
    finally
    {
      stack.RemoveAt(stack.Count - 1);
    }
  }

  internal void RemoveEmptyStrings(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) obj;
    if (expServTask.savedData == null || expServTask.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 2)
      return;
    string nodeName1 = (string) objectList[0];
    string nodeName2 = (string) objectList[1];
    ImDocumentData docData = expServTask.docData;
    DocumentTreeNode documentTreeNode = docData.Template.FindFirstNodeByName(nodeName1) ?? docData.Template.FindFirstNodeByName(nodeName2);
    if (documentTreeNode == null)
      return;
    DocumentTreeNode parent = documentTreeNode.Parent;
    DocumentTreeNode templateRecursive = expServTask.docData.FindFirstNodeFromTemplate_Recursive(parent);
    for (int index = templateRecursive.Nodes.Count - 1; index >= 0; --index)
    {
      DocumentTreeNode node = templateRecursive.Nodes[index];
      if (!(node.Name == nodeName1) && !(node.Name == nodeName2))
        break;
      templateRecursive.RemoveChildNodeAt(index, false, false);
    }
  }

  internal void RemoveLinksWithoutQuantity(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    int indexByName = ti.savedLinks.Columns.GetIndexByName(ExpertAttrGUIDs.attrQuantity);
    if (indexByName < 0)
      return;
    bool flag = false;
    for (int index = ti.savedLinks.RowsCount - 1; index >= 0; --index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      if (savedLink[indexByName] == null || savedLink[indexByName] == DBNull.Value)
      {
        ti.savedLinks.RemoveAt(index);
        flag = true;
      }
    }
    if (!flag)
      return;
    ExpertServer.CreateLinkIndex(ti);
  }

  internal void ReplaceValuesWithDescriptions(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) obj;
    if (expServTask.savedData == null || expServTask.savedLinks == null)
      return;
    List<object> objectList = (List<object>) Value;
    if (objectList.Count < 1)
      return;
    string[] strArray = ((string) objectList[0]).Split(',');
    if (strArray.Length == 0)
      return;
    List<int> intList = new List<int>();
    foreach (string columnName in strArray)
    {
      int indexByName = expServTask.savedData.Columns.GetIndexByName(columnName);
      if (indexByName >= 0)
        intList.Add(indexByName);
    }
    if (intList.Count == 0)
      return;
    for (int index1 = 0; index1 < intList.Count; ++index1)
    {
      int index2 = intList[index1];
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(expServTask.savedData.Columns[index2].ColumnName));
      Dictionary<object, int> dictionary = new Dictionary<object, int>();
      for (int index3 = 0; index3 < attributeType.PossibleValues.Count; ++index3)
        dictionary.Add(attributeType.PossibleValues[index3], index3);
      for (int index4 = 0; index4 < expServTask.savedData.RowsCount; ++index4)
      {
        HybridRowExp hybridRowExp = expServTask.savedData[index4];
        object key = hybridRowExp[index2];
        if (key != null && key != DBNull.Value && dictionary.ContainsKey(key))
        {
          int index5 = dictionary[key];
          hybridRowExp[index2] = attributeType.PossibleValuesDescriptions[index5];
        }
      }
    }
  }

  internal void GetDopZamenTexts(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    ISubstitutesRemarksService service1 = (ISubstitutesRemarksService) this._servProvider.GetService(typeof (ISubstitutesRemarksService));
    if (service1 == null)
      return;
    IUserSession session = ExpertServer.es.GetSession(ti);
    if (SubstituteObjects.Attrs == null || SubstituteObjects.Attrs.Count == 0)
      SubstituteObjects.InitStaticFields(session);
    RelationAttributesPackage relAttrs = new RelationAttributesPackage(SubstituteObjects.Attrs);
    List<long> relations = new List<long>();
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj1 = savedLink["cad001c0-306c-11d8-b4e9-00304f19f545"];
      int int32_1 = obj1 == null || obj1 == DBNull.Value ? 0 : Convert.ToInt32(savedLink["cad001c0-306c-11d8-b4e9-00304f19f545"]);
      if (int32_1 != 0)
      {
        long int64_1 = Convert.ToInt64(savedLink["cad00033-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
        HybridRowExp hybridRowExp = ti.savedDataByPartId(int64_2);
        if (hybridRowExp != null)
        {
          relations.Clear();
          relations.Add(int64_1);
          MeasuredValue quantity = this.GetQuantity(savedLink);
          relAttrs.SetRelationsAttrValue(relations, ExpertConsts.Consts.attrCount, (object) quantity);
          object obj2 = hybridRowExp["cad00020-306c-11d8-b4e9-00304f19f545"];
          string str1 = obj2 == null || obj2 == DBNull.Value ? "" : Convert.ToString(obj2);
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_Name, (object) str1);
          string str2 = Convert.ToString(hybridRowExp["cad0001f-306c-11d8-b4e9-00304f19f545"]);
          if (str2 == "")
            str2 = str1;
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_Designation, (object) str2);
          object obj3 = savedLink["cad00270-306c-11d8-b4e9-00304f19f545"];
          string str3 = obj3 == null || obj3 == DBNull.Value ? "" : Convert.ToString(obj3);
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_Position, (object) str3);
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_DopZamenGroupNum, (object) int32_1);
          int int32_2 = Convert.ToInt32(savedLink["cad001c1-306c-11d8-b4e9-00304f19f545"]);
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_DopZamenNumInGroup, (object) int32_2);
          object obj4 = savedLink["cad00654-306c-11d8-b4e9-00304f19f545"];
          int int32_3 = obj4 == null || obj4 == DBNull.Value ? 0 : Convert.ToInt32(obj4);
          relAttrs.SetRelationsAttrValue(relations, AvsIDCache.Attr_DesignerActualVariant, (object) int32_3);
        }
      }
    }
    ISubstitutesSettings service2 = this._servProvider.GetService(typeof (ISubstitutesSettings)) as ISubstitutesSettings;
    Dictionary<long, string> dictionary = service1.CalcSubstituteRemarks(service2, relAttrs);
    if (dictionary == null)
      return;
    if (!ti.savedLinks.Columns.Contains("cadd9438-306c-11d8-b4e9-00304f19f545"))
      ti.savedLinks.AddColumn("cadd9438-306c-11d8-b4e9-00304f19f545", typeof (string));
    int indexByName = ti.savedLinks.Columns.GetIndexByName("cadd9438-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      long int64 = Convert.ToInt64(savedLink["cad00033-306c-11d8-b4e9-00304f19f545"]);
      savedLink[indexByName] = !dictionary.ContainsKey(int64) ? (object) "" : (object) dictionary[int64];
    }
  }

  internal void RemoveDuplicateLinks(
    object obj,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) obj;
    if (ti.savedData == null || ti.savedLinks == null)
      return;
    Dictionary<long, int> dictionary = new Dictionary<long, int>();
    if (ti.ispList != null)
    {
      for (int index1 = 0; index1 < ti.ispList.Count; ++index1)
      {
        List<long> articleVariablePart = ti.app.GetArticleVariablePart(ti.ispList[index1]);
        for (int index2 = 0; index2 < articleVariablePart.Count; ++index2)
          dictionary.Add(articleVariablePart[index2], index1);
      }
      for (int index3 = 0; index3 < ti.ispList.Count; ++index3)
      {
        List<long> articleCommonPart = ti.app.GetArticleCommonPart(ti.ispList[index3]);
        for (int index4 = 0; index4 < articleCommonPart.Count; ++index4)
        {
          if (!dictionary.ContainsKey(articleCommonPart[index4]))
            dictionary.Add(articleCommonPart[index4], -1);
        }
      }
    }
    List<int> intList1 = new List<int>();
    for (int index5 = 0; index5 < ti.savedLinks.RowsCount; ++index5)
    {
      if (!intList1.Contains(index5))
      {
        HybridRowExp savedLink1 = ti.savedLinks[index5];
        long int64_1 = Convert.ToInt64(savedLink1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        List<int> intList2 = ti.savedLinksByPartIndex(int64_1);
        if (intList2.Count > 1)
        {
          long int64_2 = Convert.ToInt64(savedLink1["cad00034-306c-11d8-b4e9-00304f19f545"]);
          long int64_3 = Convert.ToInt64(savedLink1["cad00033-306c-11d8-b4e9-00304f19f545"]);
          for (int index6 = 0; index6 < intList2.Count; ++index6)
          {
            int index7 = intList2[index6];
            if (index7 != index5 && !intList1.Contains(index7))
            {
              HybridRowExp savedLink2 = ti.savedLinks[index7];
              long int64_4 = Convert.ToInt64(savedLink2["cad00034-306c-11d8-b4e9-00304f19f545"]);
              long int64_5 = Convert.ToInt64(savedLink2["cad00033-306c-11d8-b4e9-00304f19f545"]);
              int num1 = -1;
              int num2 = -1;
              if (dictionary.ContainsKey(int64_3))
                num1 = dictionary[int64_3];
              if (dictionary.ContainsKey(int64_5))
                num2 = dictionary[int64_5];
              if (num1 == num2 && int64_4 == int64_2 && int64_5 != int64_3)
              {
                MeasuredValue quantity1 = this.GetQuantity(savedLink1);
                MeasuredValue quantity2 = this.GetQuantity(savedLink2);
                MeasuredValue measuredValue = this.AddQuan(ti.userReport, quantity1, quantity2);
                savedLink1[ExpertAttrGUIDs.attrQuantity] = (object) measuredValue;
                intList1.Add(index7);
              }
            }
          }
        }
      }
    }
    intList1.Sort();
    if (intList1.Count > 0)
    {
      for (int index8 = intList1.Count - 1; index8 >= 0; --index8)
      {
        int index9 = intList1[index8];
        ti.savedLinks.RemoveAt(index9);
      }
      ExpertServer.CreateLinkIndex(ti);
    }
    int indexByName = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.ispAttrGUID);
    if (indexByName == -1)
    {
      ti.savedLinks.AddColumn(AVSServerPlugin.ispAttrGUID, typeof (int));
      indexByName = ti.savedLinks.Columns.GetIndexByName(AVSServerPlugin.ispAttrGUID);
    }
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      long int64 = Convert.ToInt64(savedLink["cad00033-306c-11d8-b4e9-00304f19f545"]);
      int num = -2;
      if (!dictionary.TryGetValue(int64, out num))
        num = -2;
      savedLink[indexByName] = (object) num;
    }
  }

  public string[] GetUpdateScripts()
  {
    return new string[1]{ "Intermech.AVS.xml" };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts([NotNull] IUserSession session)
  {
    OldChapterSettingsLoader.CopyOldSettingsToDBObjects(session);
  }

  public class razdInfo
  {
    public string Name;
    public long razdNum;

    public razdInfo(string N, long rN)
    {
      this.Name = N;
      this.razdNum = rN;
    }
  }

  internal class TypeHolder
  {
    public static AVSServerPlugin.TypeHolder th = new AVSServerPlugin.TypeHolder();
    public List<int> typesIzdelie;
    public List<int> typesNorm;
    public List<int> typesOperation;

    private TypeHolder()
    {
      int objectTypeId1 = MetaDataHelper.GetObjectTypeID(new Guid(AVSServerPlugin.OTIzdelie));
      this.typesIzdelie = MetaDataHelper.GetObjectTypeChildrenID(objectTypeId1);
      if (this.typesIzdelie.IndexOf(objectTypeId1) < 0)
        this.typesIzdelie.Add(objectTypeId1);
      int objectTypeId2 = MetaDataHelper.GetObjectTypeID(new Guid(AVSServerPlugin.OTNormirovanie));
      this.typesNorm = MetaDataHelper.GetObjectTypeChildrenID(objectTypeId2);
      if (this.typesNorm.IndexOf(objectTypeId2) < 0)
        this.typesNorm.Add(objectTypeId2);
      int objectTypeId3 = MetaDataHelper.GetObjectTypeID(new Guid(AVSServerPlugin.OTOperation));
      this.typesOperation = MetaDataHelper.GetObjectTypeChildrenID(objectTypeId3);
      if (this.typesOperation.IndexOf(objectTypeId3) >= 0)
        return;
      this.typesOperation.Add(objectTypeId3);
    }
  }

  internal class NoQuantInfo : IComparable
  {
    public long projId = -1;
    public long partID = -1;
    public long linkID = -1;
    public string Pos = "";
    public string ProjName = "";
    public string PartName = "";
    public bool allIsps;

    public NoQuantInfo(long Id, long pId, long lId, string P, string PN, string N)
    {
      this.projId = Id;
      this.partID = pId;
      this.linkID = lId;
      this.Pos = P;
      this.ProjName = PN;
      this.PartName = N;
    }

    public int CompareTo(object obj)
    {
      if (obj.GetType() != typeof (AVSServerPlugin.NoQuantInfo))
        return -1;
      AVSServerPlugin.NoQuantInfo noQuantInfo = (AVSServerPlugin.NoQuantInfo) obj;
      if (this.partID < noQuantInfo.partID)
        return -1;
      return this.partID > noQuantInfo.partID ? 1 : 0;
    }
  }
}
