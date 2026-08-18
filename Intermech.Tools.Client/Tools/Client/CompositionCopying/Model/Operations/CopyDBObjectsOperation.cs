// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyDBObjectsOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Remoting.Sponsors;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.CompositionCopying;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class CopyDBObjectsOperation : LongRunningOperation
{
  private Dictionary<DBObjectGraphVertex, DBObjectRecord> result;
  private ICopyingSessionServices services;
  private DBObjectGraph dbObjectGraph;
  private CopyingSession session;
  private List<UserWorkItem> copyDocumentsUserWork;

  public CopyDBObjectsOperation()
  {
    this.result = new Dictionary<DBObjectGraphVertex, DBObjectRecord>();
  }

  public Dictionary<DBObjectGraphVertex, DBObjectRecord> Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  public void Invoke(CopyingSession session, ICollection<DBObjectGraphVertex> vertices)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (vertices == null)
      throw new ArgumentNullException(nameof (vertices));
    this.Result.Clear();
    this.ErrorsBuilder.Clear();
    try
    {
      this.InitializeCore(session);
      this.InvokeCore(vertices);
    }
    finally
    {
      this.CleanupCore();
    }
  }

  private void InitializeCore(CopyingSession session)
  {
    this.session = session;
    this.services = session.Services;
    this.dbObjectGraph = session.Graph;
    this.copyDocumentsUserWork = new List<UserWorkItem>(0);
  }

  private void CleanupCore()
  {
    this.session = (CopyingSession) null;
    this.services = (ICopyingSessionServices) null;
    this.dbObjectGraph = (DBObjectGraph) null;
  }

  private void InvokeCore(ICollection<DBObjectGraphVertex> vertices)
  {
    this.CheckCancellationOperation();
    try
    {
      this.Copy(vertices);
    }
    catch (Exception ex)
    {
      if (ex is AbortException)
        return;
      throw;
    }
  }

  private void Copy(ICollection<DBObjectGraphVertex> vertices)
  {
    using (RemoteLock remoteLock = new RemoteLock())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.ReportLogMessage("Начало копирования...");
        int fileAttributeId = sessionKeeper.Session.IdentHelper.FileAttributeID;
        ICADDocumentCopyingServerService service = ServiceUtils.GetService<ICADDocumentCopyingServerService>((object) sessionKeeper.Session, true);
        this.CheckCancellationOperation();
        Dictionary<DBObjectGraphVertex, IDBObject> dictionary = new Dictionary<DBObjectGraphVertex, IDBObject>();
        double num1 = 90.0 / ((double) vertices.Count * 2.0);
        int num2 = 0;
        try
        {
          foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
          {
            try
            {
              this.ReportLogMessage($"Копирование объекта '{vertex.Caption}'...");
              this.CheckCancellationOperation();
              IDBObject objToLock = service.CloneObject(sessionKeeper.Session, vertex.ObjectTypeId, vertex.ObjectId);
              remoteLock.Add((object) objToLock);
              dictionary.Add(vertex, objToLock);
              this.ReportLogMessage("Копирования атрибутов...");
              foreach (DBObjectAttributeEntry attribute in (IEnumerable<DBObjectAttributeEntry>) vertex.Attributes)
              {
                this.CheckCancellationOperation();
                if (!attribute.IsCopyingDisallowed)
                {
                  IDBAttribute attributeById = objToLock.GetAttributeByID(attribute.AttributeId);
                  if (attributeById == null)
                  {
                    if (attribute.AttributeId > 0)
                    {
                      object[] array = attribute.NewValues.Where<object>((Func<object, bool>) (x => !string.IsNullOrEmpty(x.ToString()))).ToArray<object>();
                      if (array.Length != 0)
                      {
                        IDBAttribute dbAttribute = objToLock.Attributes.AddAttribute(attribute.AttributeId, false);
                        for (int index = 0; index < array.Length; ++index)
                        {
                          this.CheckCancellationOperation();
                          dbAttribute.Index = index;
                          dbAttribute.Value = array[index];
                        }
                      }
                    }
                  }
                  else
                  {
                    for (int index = 0; index < attribute.NewValues.Count; ++index)
                    {
                      this.CheckCancellationOperation();
                      if (!object.Equals(attribute.NewValues[index], attribute.OriginalValues[index]))
                      {
                        attributeById.Index = index;
                        attributeById.Value = attribute.NewValues[index];
                      }
                    }
                  }
                }
              }
              DBObjectAttributeEntry objectAttributeEntry = CollectionUtils.Find<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) vertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == this.session.Services.IntegratorsIDCache.PrivateFiles.Id));
              if (objectAttributeEntry != null && objectAttributeEntry.OriginalValues.Count != 0)
              {
                List<string> stringList = new List<string>(objectAttributeEntry.OriginalValues.Count);
                foreach (object originalValue1 in (IEnumerable<object>) objectAttributeEntry.OriginalValues)
                {
                  object originalValue = originalValue1;
                  DBObjectFileEntry dbObjectFileEntry = CollectionUtils.Find<DBObjectFileEntry>((IEnumerable<DBObjectFileEntry>) vertex.Files, (Predicate<DBObjectFileEntry>) (x => PathUtils.IsSamePath(x.OriginalName, (string) originalValue)));
                  if (dbObjectFileEntry != null)
                    stringList.Add(dbObjectFileEntry.NewName);
                }
                objToLock.Attributes.AddAttribute(objectAttributeEntry.AttributeId, true, (object[]) stringList.ToArray());
              }
              this.ReportLogMessage("Копирования атрибутов завершено...");
              if (vertex.Files.Count != 0)
              {
                this.ReportLogMessage("Копирования файлов...");
                IDBAttribute attributeById = objToLock.GetAttributeByID(fileAttributeId);
                remoteLock.Add((object) attributeById);
                List<UploadFileInfo> items = new List<UploadFileInfo>();
                for (int index = 0; index < vertex.Files.Count; ++index)
                {
                  this.CheckCancellationOperation();
                  DBObjectFileEntry file = vertex.Files[index];
                  if (!file.Content.IsMainFile)
                  {
                    if (file.Content.IsCADFile)
                    {
                      if (ServiceUtils.GetService<IModelDrawingsService>((object) this.session.Integrator, true).IsDrawingFileName(file.OriginalName))
                        this.copyDocumentsUserWork.Add(new UserWorkItem($"В документе '{objToLock.Caption}' ({objToLock.ObjectID}) требуется исправить атрибуты чертежа. Старое имя чертежа: '{file.OriginalName}'. Новое имя чертежа: '{file.NewName}'", vertex));
                    }
                    else if (this.session.RastrAndSubstrateExtensionsList.Contains(Path.GetExtension(file.OriginalName)))
                      this.copyDocumentsUserWork.Add(new UserWorkItem($"В скопированном документе '{objToLock.Caption}' ({objToLock.ObjectID}) требуется заменить прежнюю ссылку на файл '{file.OriginalName}' на '{file.NewName}'", vertex));
                  }
                  this.ReportLogMessage($"Копирование файла '{file.OriginalName}' в новый файл '{file.NewName}'...");
                  if (file.IsRenamed)
                    items.Add(new UploadFileInfo(file.NewName, Path.Combine(this.services.FileVaultService.WorkArea.AreaPath, file.NewName), file.ValueFileType));
                }
                new UploadFilesAction((IDBObjectRef) new DirectDBObjectRef(objToLock.ObjectID), (IList<UploadFileInfo>) items)
                {
                  FullRewriteMode = true
                }.Perform();
                this.ReportLogMessage("Копирования файлов завершено...");
              }
            }
            catch (Exception ex)
            {
              if (ex is AbortException)
                throw;
              this.ErrorsBuilder.AddError(new OperationError(ex.Message));
            }
            ++num2;
            this.ReportProgress((int) Math.Round(num1 * (double) num2));
          }
          this.CheckCancellationOperation();
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.services.IntegratorsIDCache.DocumentTree.Id);
          remoteLock.Add((object) relationCollection);
          this.ReportLogMessage("Создание новых связей...");
          foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
          {
            try
            {
              this.CheckCancellationOperation();
              IDBObject dbObject1 = dictionary[vertex];
              foreach (DBObjectGraphVertex verticesByOutEdge in (IEnumerable<DBObjectGraphVertex>) this.dbObjectGraph.GetVerticesByOutEdges(vertex))
              {
                this.CheckCancellationOperation();
                IDBObject dbObject2;
                relationCollection.Create(dbObject1.ObjectID, dictionary.TryGetValue(verticesByOutEdge, out dbObject2) ? dbObject2.ObjectID : verticesByOutEdge.ObjectId);
              }
            }
            catch (Exception ex)
            {
              if (ex is AbortException)
                throw;
              this.ErrorsBuilder.AddError(new OperationError(ex.Message));
            }
            ++num2;
            this.ReportProgress((int) Math.Round(num1 * (double) num2));
          }
          this.ReportLogMessage("Все требуемые связи созданы...");
          this.CheckCancellationOperation();
          this.ReportLogMessage("Завершение создания объектов...");
          if (this.Errors.Count == 0)
          {
            IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
            List<long> objectList = new List<long>();
            customService.StartTransaction();
            try
            {
              foreach (KeyValuePair<DBObjectGraphVertex, IDBObject> keyValuePair in dictionary)
              {
                this.CheckCancellationOperation();
                DBObjectGraphVertex key = keyValuePair.Key;
                IDBObject dbObject = keyValuePair.Value;
                dbObject.CommitCreation(true, true);
                DBObjectRecord dbObjectRecord = new DBObjectRecord(dbObject.ObjectID, key.ObjectTypeId, dbObject.Caption);
                this.result.Add(key, dbObjectRecord);
                objectList.Add(dbObjectRecord.ObjectId);
              }
              customService.Commit();
            }
            catch (Exception ex)
            {
              this.Result.Clear();
              this.ErrorsBuilder.AddError(new OperationError(ex.Message));
              customService.Rollback();
              throw;
            }
            if (objectList.Count != 0)
              this.services.FileVaultService.WorkArea.Attach((IList<long>) objectList);
          }
          this.ReportProgress(10);
        }
        catch (AbortException ex)
        {
          this.ErrorsBuilder.AddError(new OperationError("Операция копирования была прервана пользователем."));
        }
        finally
        {
          if (this.Errors.Count > 0 && dictionary.Count > 0)
          {
            this.ReportLogMessage("Копирование завершено с ошибками...");
            foreach (KeyValuePair<DBObjectGraphVertex, IDBObject> keyValuePair in dictionary)
            {
              string caption = keyValuePair.Value.Caption;
              try
              {
                keyValuePair.Value.Delete(0L);
              }
              catch (Exception ex)
              {
                this.ErrorsBuilder.AddError(new OperationError($"При удалении заготовки '{caption}' произошла ошибка: {ex.Message}"));
              }
            }
          }
          else
            this.ReportLogMessage("Копирование завершено...");
        }
      }
    }
  }

  public List<UserWorkItem> CopyDocumentsUserWork
  {
    [DebuggerStepThrough] get => this.copyDocumentsUserWork;
  }
}
