// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.GtcPlugin
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.BackgroundTask;
using Intermech.GTC.Server.BrowseFileFolderDialog;
using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace Intermech.GTC.Server;

public class GtcPlugin : IPackage, IUpdatable
{
  public void Load(IServiceProvider serviceProvider)
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service1))
      throw new Exception("Service ICustomServices not found");
    BackgroundTaskService serviceInstance1 = new BackgroundTaskService();
    ServerServices.AddService(typeof (IServiceForBackgroundTask), (object) serviceInstance1);
    service1.AddService(typeof (IServiceForBackgroundTask), (object) serviceInstance1);
    BrowseFileFolder serviceInstance2 = new BrowseFileFolder();
    ServerServices.AddService(typeof (IBrowseFileFolder), (object) serviceInstance2);
    service1.AddService(typeof (IBrowseFileFolder), (object) serviceInstance2);
    IUserSession session = (IUserSession) null;
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service2))
      return;
    session = service2.GetSystemSessionTemporaryClone("GtcPlugin.Load");
    Action<Guid> action = new Action<Guid>(this.FillClassificationCatalog);
    action.BeginInvoke(session.SessionGUID, (AsyncCallback) (iar =>
    {
      try
      {
        action.EndInvoke(iar);
      }
      catch (Exception ex)
      {
        session.EventLog.AddToTrace($"Ошибка восстановления каталога Классификатор GTC:{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceError, string.Empty);
        if (AdminUtilsService.ServerRunMode != ServerRunModes.Console)
          return;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Ошибка восстановления каталога Классификатор GTC:");
          Console.WriteLine(ex.Message);
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      finally
      {
        session?.Logout("GtcPlugin.Load");
      }
    }), (object) null);
  }

  public void Unload()
  {
  }

  public string Name => Const.PluginName;

  public string[] GetUpdateScripts()
  {
    return new string[5]
    {
      "GtcAttributeGroups.xml",
      "GtcBaseAttributes.xml",
      "GtcPlibAttributes.xml",
      "GtcObjectTypes.xml",
      "GtcObjects.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
  }

  private void FillClassificationCatalog(Guid sessionGuid)
  {
    IUserSession session = (IUserSession) null;
    try
    {
      session = UserSession.GetSessionByID(sessionGuid).Clone("GtcPlugin.FillClassificationCatalog");
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      Encoding encoding = Encoding.GetEncoding(1251);
      string end;
      using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Intermech.GTC.Server.Resources.GtcClassificatorCatalog.txt"))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream, encoding))
          end = streamReader.ReadToEnd();
      }
      Dictionary<string, ClassCatalogHierarchyItem> dictionary = ((IEnumerable<string>) end.Split(new string[1]
      {
        Environment.NewLine
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string[]>((System.Func<string, string[]>) (x => x.Split('='))).Where<string[]>((System.Func<string[], bool>) (x => x.Length == 3)).Select<string[], ClassCatalogHierarchyItem>((System.Func<string[], ClassCatalogHierarchyItem>) (x => new ClassCatalogHierarchyItem(x[0], x[1], x[2]))).GroupBy<ClassCatalogHierarchyItem, string>((System.Func<ClassCatalogHierarchyItem, string>) (x => x.BsuCode)).ToDictionary<IGrouping<string, ClassCatalogHierarchyItem>, string, ClassCatalogHierarchyItem>((System.Func<IGrouping<string, ClassCatalogHierarchyItem>, string>) (x => x.Key), (System.Func<IGrouping<string, ClassCatalogHierarchyItem>, ClassCatalogHierarchyItem>) (x => x.First<ClassCatalogHierarchyItem>()));
      ClassCatalogHierarchyItem rootDescriptor = new ClassCatalogHierarchyItem("Root", "MainRoot", "NoParent");
      foreach (ClassCatalogHierarchyItem catalogHierarchyItem1 in dictionary.Values)
      {
        ClassCatalogHierarchyItem catalogHierarchyItem2;
        if (dictionary.TryGetValue(catalogHierarchyItem1.ParentBsuCode, out catalogHierarchyItem2))
          catalogHierarchyItem2.Children.Add(catalogHierarchyItem1);
        else
          rootDescriptor.Children.Add(catalogHierarchyItem1);
      }
      IDBObject dbObject = session.GetObject(Const.CatalogClassifObjGuid, true);
      Dictionary<string, long> existFolderGtcIdsCache = new Dictionary<string, long>();
      this.FillCache(session, dbObject.ObjectID, existFolderGtcIdsCache);
      this.CreateHierarchyFolders(rootDescriptor, session, dbObject.ObjectID, existFolderGtcIdsCache);
    }
    finally
    {
      session?.Logout("GtcPlugin.FillClassificationCatalog");
    }
  }

  private void FillCache(
    IUserSession session,
    long catalogObjId,
    Dictionary<string, long> existFolderGtcIdsCache)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Const.ImbaseFolderObjectTypeId);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Const.ClassifFolderKeyAttributeTypeId, RelationalOperators.StartString, (object) Const.ClassifFolderKey, LogicalOperators.AND, 0, false),
      new ConditionStructure(Const.BsuAttributeTypeId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) -2,
      (object) Const.BsuAttributeTypeId
    });
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string key = row[1].ToString();
      if (!existFolderGtcIdsCache.ContainsKey(key))
        existFolderGtcIdsCache.Add(key, int64);
    }
  }

  private void CreateHierarchyFolders(
    ClassCatalogHierarchyItem rootDescriptor,
    IUserSession session,
    long catalogObjId,
    Dictionary<string, long> existFolderGtcIdsCache)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(Const.SimpleWithSortRelationTypeId);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Const.ImbaseFolderObjectTypeId);
    foreach (ClassCatalogHierarchyItem child in rootDescriptor.Children)
    {
      try
      {
        long num1;
        if (existFolderGtcIdsCache.TryGetValue(child.BsuCode, out num1))
          throw new ObjectAlreadyExists(num1, MetaDataHelper.GetAttributeTypeName(Const.BsuAttributeTypeId), session.GetObject(num1).NameInMessages, string.Empty);
        IDBObject dbObject = objectCollection.Create();
        dbObject.Attributes.AddAttribute(Const.NameAttributeTypeId, false).AsString = child.Name;
        dbObject.Attributes.AddAttribute(Const.BsuAttributeTypeId, false).AsString = child.BsuCode;
        long num2;
        relationCollection.Create(existFolderGtcIdsCache.TryGetValue(rootDescriptor.BsuCode, out num2) ? num2 : catalogObjId, dbObject.ObjectID);
        dbObject.CommitCreation(true);
        existFolderGtcIdsCache.Add(child.BsuCode, dbObject.ObjectID);
      }
      catch (ObjectAlreadyExists ex)
      {
        if (!existFolderGtcIdsCache.ContainsKey(child.BsuCode))
          existFolderGtcIdsCache.Add(child.BsuCode, ex.ObjectID);
      }
      finally
      {
        this.CreateHierarchyFolders(child, session, catalogObjId, existFolderGtcIdsCache);
      }
    }
  }
}
