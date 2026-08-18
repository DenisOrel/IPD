
// Type: Intermech.Navigator.ContextCommands.SaveToDiskClassifierClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;


namespace Intermech.Navigator.ContextCommands;

/// <summary>
/// Класс для команды "Сохранить на диск" для классификаторов и их папок
/// </summary>
public class SaveToDiskClassifierClass : CustomBackgroundTask
{
  private string selectedPath = string.Empty;
  private bool baseVersions = true;
  private List<IDBTypedObjectID> dbTypedObjectIDs;
  private int simpleSortRelationId = MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545");
  private int classifierFolderObjectType = MetaDataHelper.GetObjectTypeID(new Guid("cad00150-306c-11d8-b4e9-00304f19f545"));
  private int documentObjectType = MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));

  public SaveToDiskClassifierClass(
    string selectedPath,
    bool baseVersions,
    List<IDBTypedObjectID> dbTypedObjectIDs)
  {
    this._name = LocalizationHolder.rm.GetString("Client.Core_1214") + selectedPath;
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    this._maxValue = 100;
    this.selectedPath = selectedPath;
    this.baseVersions = baseVersions;
    this.dbTypedObjectIDs = dbTypedObjectIDs;
  }

  public void Saving()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service)
      service.ClearText(LocalizationHolder.rm.GetString("OutputView_SavingToDisk"));
    try
    {
      using (new SessionKeeper())
      {
        for (int index = 0; index < this.dbTypedObjectIDs.Count; ++index)
        {
          if (this._state == BackgroundTaskState.Stopped)
            return;
          DirectoryInfo di = new DirectoryInfo(Path.Combine(this.selectedPath, this.ReplaceInvalidSymbols(this.dbTypedObjectIDs[index].Caption)));
          this.ProcessClassifier(this.dbTypedObjectIDs[index].ObjectID, di);
        }
      }
      this._state = BackgroundTaskState.Terminated;
      this.OnChanged(BackgroundTaskChangedType.State);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      this._state = BackgroundTaskState.Error;
      this.OnChanged(BackgroundTaskChangedType.State);
      this._result = (object) ex.Message;
      this.OnChanged(BackgroundTaskChangedType.Result);
    }
    if (this._state == BackgroundTaskState.Terminated)
    {
      this._result = (object) LocalizationHolder.rm.GetString("Client.Core_1360");
      this.OnChanged(BackgroundTaskChangedType.Result);
      Thread.Sleep(1000);
      this.OnChanged(BackgroundTaskChangedType.Dispose);
    }
    this._canStop = false;
  }

  /// <summary>
  /// Замена символов, не подходящих для имен файловой системы
  /// </summary>
  /// <param name="s"></param>
  /// <returns></returns>
  private string ReplaceInvalidSymbols(string s) => FileNameHelper.ReplaceInvalidFileNameChars(s);

  private void ProcessClassifier(long classifierObjID, DirectoryInfo di)
  {
    if (!di.Exists)
      di.Create();
    List<Tuple<long, string>> tupleList = new List<Tuple<long, string>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      conditionStructureList.Add(new ConditionStructure(0, RelationalOperators.InSelection, (object) classifierObjID, LogicalOperators.AND, 0, false));
      if (this.baseVersions)
        conditionStructureList.Add(new ConditionStructure(-16, RelationalOperators.Equal, (object) 1, LogicalOperators.AND, 0, false));
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(this.documentObjectType, new DBRecordSetParams(conditionStructureList.ToArray(), new object[5]
      {
        (object) -2,
        (object) -50,
        (object) -3,
        (object) -5,
        (object) -16
      }, new object[2]{ (object) -3, (object) -5 }, (SortOrders[]) null));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DirectoryInfo di1 = di;
        DataRow row = dataTable.Rows[index];
        if (Convert.ToInt32(row[4]) != 1)
        {
          string str = string.Empty;
          if (Convert.ToInt32(row[3]) > 0)
            str = $" [{Convert.ToString(row[3])}]";
          di1 = new DirectoryInfo(Path.Combine(di.FullName, this.ReplaceInvalidSymbols(Convert.ToString(row[1])) + str));
        }
        this.ProcessDocument(Convert.ToInt64(row[0]), di1);
      }
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.simpleSortRelationId);
      relationCollection.ObjectTypeID = this.classifierFolderObjectType;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -2,
        (object) -50,
        (object) -7
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, classifierObjID).Rows)
        tupleList.Add(new Tuple<long, string>(Convert.ToInt64(row[0]), Convert.ToString(row[1])));
    }
    for (int index = 0; index < tupleList.Count; ++index)
    {
      DirectoryInfo di2 = new DirectoryInfo(Path.Combine(di.FullName, this.ReplaceInvalidSymbols(tupleList[index].Item2)));
      this.ProcessClassifier(tupleList[index].Item1, di2);
    }
  }

  /// <summary>Сохраняем файлы документа на диск</summary>
  /// <param name="docID"></param>
  /// <param name="di"></param>
  private void ProcessDocument(long docID, DirectoryInfo di)
  {
    if (!di.Exists)
      di.Create();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(docID).GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return;
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        attributeByGuid.Index = index;
        if (!(attributeByGuid is IBlobReader blobReader))
          break;
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        string fileName = Path.GetFileName(blobInformation.FileName);
        if (blobInformation.FileName.Trim() != string.Empty)
        {
          using (FileStream aDestStream = new FileStream(Path.Combine(di.FullName, fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        }
      }
    }
  }
}
