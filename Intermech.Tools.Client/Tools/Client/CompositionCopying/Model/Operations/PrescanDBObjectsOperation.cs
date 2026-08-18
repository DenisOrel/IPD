// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.PrescanDBObjectsOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class PrescanDBObjectsOperation : LongRunningOperation
{
  private List<PrescanDBObjectRecord> result;
  private int fileAttributeId;
  private CopyingSession session;

  public PrescanDBObjectsOperation()
  {
    this.result = new List<PrescanDBObjectRecord>();
    this.fileAttributeId = 0;
  }

  public List<PrescanDBObjectRecord> Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  public void Invoke(CopyingSession session, List<DBObjectGraphVertex> verticles)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (verticles == null)
      throw new ArgumentNullException(nameof (verticles));
    this.result.Clear();
    this.ErrorsBuilder.Clear();
    try
    {
      this.Initialize(session);
      if (verticles.Count == 0 || this.IsCancellationRequested)
        return;
      this.ScanDBObjects(verticles);
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void Initialize(CopyingSession session)
  {
    this.session = session;
    this.fileAttributeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).FileAttributeID;
  }

  private void Cleanup()
  {
    this.session = (CopyingSession) null;
    this.fileAttributeId = 0;
  }

  private void ScanDBObjects(List<DBObjectGraphVertex> vertices)
  {
    double num1 = 100.0 / (double) vertices.Count;
    int num2 = 0;
    this.ReportLogMessage("Подключение к серверу приложений");
    foreach (DBObjectGraphVertex vertex in vertices)
    {
      try
      {
        this.ScanDBObject(vertex);
        ICollection<DBObjectGraphVertex> verticesByInEdges = this.session.Graph.GetVerticesByInEdges(vertex, (Predicate<DBObjectGraphVertex>) (y => y.IsArticle()));
        if (verticesByInEdges.Count > 0)
        {
          foreach (DBObjectGraphVertex dbObjectVertex in (IEnumerable<DBObjectGraphVertex>) verticesByInEdges)
            this.ScanDBObject(dbObjectVertex, false);
        }
      }
      catch (Exception ex)
      {
        this.ErrorsBuilder.AddError(new OperationError(ex.Message));
      }
      ++num2;
      this.ReportProgress((int) Math.Round(num1 * (double) num2));
      if (this.IsCancellationRequested)
      {
        this.ReportLogMessage("Прерывание сканирования...");
        break;
      }
    }
    this.ReportLogMessage("Отключение от сервера приложений");
  }

  private void ScanDBObject(DBObjectGraphVertex dbObjectVertex, bool getFiles = true)
  {
    this.ReportLogMessage($"Сканирование объекта '{dbObjectVertex.Caption}' (ид. версии {dbObjectVertex.ObjectId})");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbObjectVertex.ObjectId, true);
      List<DBObjectAttributeEntry> attributeRecords = this.CreateDBObjectAttributeRecords(dbObjectVertex, dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeCaption));
      List<DBObjectFileEntry> files = (List<DBObjectFileEntry>) null;
      if (getFiles)
      {
        IDBFileAttribute attributeById = (IDBFileAttribute) dbObject.GetAttributeByID(this.fileAttributeId);
        if (attributeById != null)
          files = this.CreateDBObjectFileRecords(dbObjectVertex, attributeById.GetBlobInformation());
      }
      if (files == null)
        files = new List<DBObjectFileEntry>(0);
      this.result.Add(new PrescanDBObjectRecord(dbObjectVertex, attributeRecords, files));
    }
  }

  private List<DBObjectAttributeEntry> CreateDBObjectAttributeRecords(
    DBObjectGraphVertex dbObjectVertex,
    AttributeValues[] values)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<DBObjectAttributeEntry> attributeRecords = new List<DBObjectAttributeEntry>(values.Length);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObjectVertex.ObjectTypeId, true);
      foreach (AttributeValues attributeValues in values)
      {
        bool isUniqueValuesRequired = false;
        bool isCopyingDisallowed = false;
        bool isEditableAttribute = false;
        if (attributeValues.AttributeID > 0)
        {
          IDBAttributeType attributeType = objectType.GetAttributeType(attributeValues.AttributeID);
          isUniqueValuesRequired = attributeType.UniqueMode != 0;
          isCopyingDisallowed = (attributeType.Options & AttributeOptions.DontCopyPrototypeValue) != 0;
          isEditableAttribute = (attributeType.AttributeType == FieldTypes.ftString || attributeType.AttributeType == FieldTypes.ftMemo) && attributeType.MultipleValued == MultiValueModes.SingleValue && attributeType.Computed == ComputeValueModes.NotComputableValue;
        }
        attributeRecords.Add(new DBObjectAttributeEntry(attributeValues.AttributeID, attributeValues.AttributeName, isUniqueValuesRequired, isCopyingDisallowed, isEditableAttribute, attributeValues.Values));
      }
      return attributeRecords;
    }
  }

  private List<DBObjectFileEntry> CreateDBObjectFileRecords(
    DBObjectGraphVertex vertex,
    BlobInformation[] fileValues)
  {
    List<DBObjectFileEntry> objectFileRecords = new List<DBObjectFileEntry>(fileValues.Length);
    for (int valueIndex = 0; valueIndex < fileValues.Length; ++valueIndex)
    {
      BlobInformation fileValue = fileValues[valueIndex];
      if (!string.IsNullOrEmpty(fileValue.FileName) && fileValue.FileType != FileTypes.ftRedlining && fileValue.FileType != FileTypes.ftAuthentical)
        objectFileRecords.Add(new DBObjectFileEntry(valueIndex, fileValue.FileType, fileValue.FileName));
    }
    return objectFileRecords;
  }
}
