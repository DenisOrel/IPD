// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.GtcProcessor
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.BackgroundTask;
using Intermech.GTC.Server.Exceptions;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.GTC.Server.Processors;

public abstract class GtcProcessor
{
  protected bool Hierarchy;
  protected bool Data;
  protected IImportConfig ImportConfig;
  protected IUserSession Session;
  protected BaseTaskForBackgroundTaskService Task;
  protected string RootPath = string.Empty;
  protected bool RootIsZip;
  protected long CatalogObjId;
  protected string CatalogName = string.Empty;
  protected IDBObjectCollection ImbaseFolderObjCollection;
  protected FileFolderPathHolder FileFolderPathHolder = new FileFolderPathHolder();
  protected Dictionary<string, long> ExistFolderGtcIdsCache = new Dictionary<string, long>();
  protected Dictionary<string, long> ExistItemObjectGtcIdsCache = new Dictionary<string, long>();
  protected HierarchyDescriptor RootHierarchyDescriptor;
  protected List<ListItemDescriptor> ListDescriptorItems = new List<ListItemDescriptor>();
  protected const string ImportSessionName = "GtcProcessor.Import";

  private void CreateHierarchyFolders(HierarchyDescriptor rootDescriptor)
  {
    IDBRelationCollection relationCollection = this.Session.GetRelationCollection(Intermech.GTC.Server.Const.SimpleWithSortRelationTypeId);
    IFileNameGenerator service = ServiceUtils.GetService<IFileNameGenerator>((object) this.Session, true);
    foreach (HierarchyDescriptor child in rootDescriptor.Children)
    {
      if (this.IsProcessStopping)
        throw new StopTaskException();
      try
      {
        long num1;
        if (this.ExistFolderGtcIdsCache.TryGetValue(child.GtcId, out num1))
          throw new ObjectAlreadyExists(num1, MetaDataHelper.GetAttributeTypeName(Intermech.GTC.Server.Const.GtcIdAttributeTypeId), this.Session.GetObject(num1).NameInMessages, string.Empty);
        IDBObject dbObject = this.ImbaseFolderObjCollection.Create();
        dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.NameAttributeTypeId, false).AsString = child.Caption;
        dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.GtcIdAttributeTypeId, false).AsString = child.GtcId;
        if (child.FileUrls != null)
        {
          IDBAttribute aIDBAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
          if (aIDBAttribute != null)
          {
            int num2 = 0;
            foreach (Tuple<string, string> fileUrl in child.FileUrls)
            {
              string Extention = Path.GetExtension(fileUrl.Item1);
              if (Extention.Length > 1)
                Extention = Extention.Substring(1, Extention.Length - 1);
              if (num2 > 0)
                num2 = aIDBAttribute.AddValue((object) null);
              aIDBAttribute.Index = num2;
              try
              {
                using (FileStream aSourceStream = new FileStream(fileUrl.Item1, FileMode.Open, FileAccess.Read))
                {
                  BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, service.GenerateFileName((object) this.Session, "", Extention), ArcMethods.ZLibPacked, fileUrl.Item2, FileTypes.ftNormal, this.Session.UserID);
                  new BlobProcWriter(aIDBAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(this.Session);
                  ++num2;
                }
              }
              catch (Exception ex)
              {
                if (aIDBAttribute.Index > 0)
                  aIDBAttribute.DeleteValue();
                else
                  aIDBAttribute.Clear();
                this.Task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
              }
            }
          }
        }
        long num3;
        relationCollection.Create(this.ExistFolderGtcIdsCache.TryGetValue(rootDescriptor.GtcId, out num3) ? num3 : this.CatalogObjId, dbObject.ObjectID);
        dbObject.CommitCreation(true);
        this.ExistFolderGtcIdsCache.Add(child.GtcId, dbObject.ObjectID);
      }
      catch (ObjectAlreadyExists ex)
      {
        if (!this.ExistFolderGtcIdsCache.ContainsKey(child.GtcId))
          this.ExistFolderGtcIdsCache.Add(child.GtcId, ex.ObjectID);
      }
      catch (Exception ex)
      {
        this.Task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
      }
      finally
      {
        this.Task.Next();
        this.CreateHierarchyFolders(child);
      }
    }
  }

  private string CheckMandatoryFileFoldersInternal(
    IEnumerable<string> folders,
    IEnumerable<string> files)
  {
    string empty = string.Empty;
    List<string> list1 = folders.Where<string>((System.Func<string, bool>) (subfolder => !Directory.Exists(subfolder))).ToList<string>();
    List<string> list2 = files.Where<string>((System.Func<string, bool>) (infile => !File.Exists(infile))).ToList<string>();
    if (list1.Count > 0)
      empty += $"Не найдены папки: {string.Join(", ", (IEnumerable<string>) list1)}{Environment.NewLine}";
    if (list2.Count > 0)
      empty += $"Не найдены файлы: {string.Join(", ", (IEnumerable<string>) list2)}{Environment.NewLine}";
    return empty;
  }

  private void FillExistFolderGtcIdsCache()
  {
    this.ExistFolderGtcIdsCache.Clear();
    IDBAttribute attributeById = this.Session.GetObject(this.CatalogObjId).GetAttributeByID(Intermech.GTC.Server.Const.ClassificatorKeyAttributeTypeId);
    if (attributeById == null || attributeById.AsString == string.Empty)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) this.ImbaseFolderObjCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.GTC.Server.Const.ClassificatorKeyAttributeTypeId, RelationalOperators.StartString, (object) attributeById.AsString, LogicalOperators.NONE, 0, true)
    }, new object[2]
    {
      (object) -2,
      (object) Intermech.GTC.Server.Const.GtcIdAttributeTypeId
    })).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string key = row[1].ToString();
      if (!this.ExistFolderGtcIdsCache.ContainsKey(key))
        this.ExistFolderGtcIdsCache.Add(key, int64);
    }
  }

  protected virtual void GetRootFolder()
  {
    this.Task.ResetCounter();
    this.Task.Name = string.Format(ServiceHolder.Rm.GetString("GTC_3"), (object) this.CatalogName);
    this.RootPath = ZipExtractor.ExtractRootItem(this.ImportConfig.Path, out this.RootIsZip);
    this.FileFolderPathHolder.SetRoot(this.RootPath);
  }

  protected virtual string CheckFolders()
  {
    string empty = string.Empty;
    string errorMessage1;
    this.Hierarchy = this.CheckMandatoryHierarchyFileFolders(out errorMessage1);
    string errorMessage2;
    this.Data = this.CheckMandatoryDataFolders(out errorMessage2);
    string str1 = empty;
    string str2;
    if (errorMessage1.Equals(string.Empty))
      str2 = string.Empty;
    else
      str2 = string.Format(ServiceHolder.Rm.GetString("GTC_4"), (object) this.CatalogName, (object) Environment.NewLine, (object) errorMessage1, (object) Environment.NewLine);
    string message = str1 + str2 + (!errorMessage2.Equals(string.Empty) ? string.Format(ServiceHolder.Rm.GetString("GTC_5"), (object) this.CatalogName, (object) Environment.NewLine, (object) errorMessage2) : string.Empty);
    if (this.Hierarchy || this.Data)
      return message;
    throw new Exception(message);
  }

  protected virtual bool CheckMandatoryHierarchyFileFolders(out string errorMessage)
  {
    string[] mandatoryFolders = this.FileFolderPathHolder.GetHierarchyMandatoryFolders();
    string[] hierarchyMandatoryFiles = this.FileFolderPathHolder.GetHierarchyMandatoryFiles();
    errorMessage = this.CheckMandatoryFileFoldersInternal((IEnumerable<string>) mandatoryFolders, (IEnumerable<string>) hierarchyMandatoryFiles);
    return errorMessage.Equals(string.Empty);
  }

  protected virtual bool CheckMandatoryDataFolders(out string errorMessage)
  {
    string[] mandatoryFolders = this.FileFolderPathHolder.GetDataMandatoryFolders();
    string[] dataMandatoryFiles = this.FileFolderPathHolder.GetDataMandatoryFiles();
    errorMessage = this.CheckMandatoryFileFoldersInternal((IEnumerable<string>) mandatoryFolders, (IEnumerable<string>) dataMandatoryFiles);
    return errorMessage.Equals(string.Empty);
  }

  protected virtual void PopulateRootHierarchyDescriptor()
  {
    this.RootHierarchyDescriptor = new HierarchyDescriptor("Root", string.Empty);
  }

  protected virtual void PopulateListDescriptorItems()
  {
    this.Task.ResetCounter();
    this.Task.Name = string.Format(ServiceHolder.Rm.GetString("GTC_25"), (object) this.CatalogName);
  }

  protected virtual void ImportHierarchy()
  {
    this.Task.ResetCounter();
    this.Task.Name = string.Format(ServiceHolder.Rm.GetString("GTC_6"), (object) this.CatalogName);
    this.ImbaseFolderObjCollection = this.Session.GetObjectCollection(Intermech.GTC.Server.Const.ImbaseFolderObjectTypeId);
    this.FillExistFolderGtcIdsCache();
    this.PopulateRootHierarchyDescriptor();
    this.CreateHierarchyFolders(this.RootHierarchyDescriptor);
  }

  protected virtual void ImportData()
  {
    this.PopulateListDescriptorItems();
    this.Task.ResetCounter();
    this.Task.Name = string.Format(ServiceHolder.Rm.GetString("GTC_7"), (object) this.CatalogName);
    this.Task.CountElements = this.ListDescriptorItems.Count;
    GtcItemObjectFactory itemObjectFactory = new GtcItemObjectFactory(this.Session, this.Task, this.ImportConfig, this.FileFolderPathHolder, this.ExistFolderGtcIdsCache, this.Hierarchy);
    foreach (ListItemDescriptor listDescriptorItem in this.ListDescriptorItems)
    {
      if (this.IsProcessStopping)
        throw new StopTaskException();
      try
      {
        string message = itemObjectFactory.CreateObject(listDescriptorItem.FilePath, listDescriptorItem.FolderGtcId);
        if (message.Length > 0)
          this.Task.Result.Messages.Add(new BackgroundTaskMessage(message));
      }
      catch (Exception ex)
      {
        this.Task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
      }
      finally
      {
        this.Task.Next();
      }
    }
  }

  public void Import()
  {
    this.Task.Running = true;
    try
    {
      this.CatalogObjId = this.ImportConfig.CatalogId;
      this.CatalogName = this.Session.GetObject(this.CatalogObjId).Caption;
      this.GetRootFolder();
      string message = this.CheckFolders();
      if (message != string.Empty)
        this.Task.Result.Messages.Add(new BackgroundTaskMessage(message));
      if (this.Hierarchy)
        this.ImportHierarchy();
      if (!this.Data)
        return;
      this.ImportData();
    }
    catch (StopTaskException ex)
    {
      this.Task.Result.Messages.Add(new BackgroundTaskMessage(ServiceHolder.Rm.GetString("GTC_2")));
    }
    catch (Exception ex)
    {
      this.Task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
    }
    finally
    {
      if (this.RootIsZip)
      {
        Action act = (Action) (() => Directory.Delete(this.RootPath, true));
        act.BeginInvoke((AsyncCallback) (iar =>
        {
          try
          {
            act.EndInvoke(iar);
          }
          catch (Exception ex)
          {
          }
        }), (object) null);
      }
      this.Task.Stopped = true;
      this.Session.Logout("GtcProcessor.Import");
    }
  }

  protected bool IsProcessStopping
  {
    get
    {
      while (this.Task.Paused && !this.Task.Stopping)
        Thread.Sleep(1000);
      return this.Task.Stopping;
    }
  }
}
