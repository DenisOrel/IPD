// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.PrescanDBObjectFileNamesOperation
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

internal sealed class PrescanDBObjectFileNamesOperation : LongRunningOperation
{
  private List<PrescanDBObjectRecord> result;
  private int fileAttributeId;
  private CopyingSession session;

  public PrescanDBObjectFileNamesOperation()
  {
    this.result = new List<PrescanDBObjectRecord>();
    this.fileAttributeId = 0;
  }

  public List<PrescanDBObjectRecord> Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  public void Invoke(CopyingSession session, ICollection<DBObjectGraphVertex> verticles)
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
      this.ScanDBObjectFileNames(verticles);
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

  private void ScanDBObjectFileNames(ICollection<DBObjectGraphVertex> vertices)
  {
    double num1 = 100.0 / (double) vertices.Count;
    int num2 = 0;
    this.ReportLogMessage("Подключение к серверу приложений");
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
    {
      try
      {
        this.ScanDBObjectFileNames(vertex);
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

  private void ScanDBObjectFileNames(DBObjectGraphVertex dbObjectVertex, bool getFiles = true)
  {
    this.ReportLogMessage($"Сканирование имен файлов объекта '{dbObjectVertex.Caption}' (ид. версии {dbObjectVertex.ObjectId})");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbObjectVertex.ObjectId, true);
      List<DBObjectFileEntry> files = (List<DBObjectFileEntry>) null;
      if (getFiles)
      {
        IDBFileAttribute attributeById = (IDBFileAttribute) dbObject.GetAttributeByID(this.fileAttributeId);
        if (attributeById != null)
          files = this.CreateDBObjectFileRecords(dbObjectVertex, attributeById.GetBlobInformation());
      }
      if (files == null)
        files = new List<DBObjectFileEntry>(0);
      this.result.Add(new PrescanDBObjectRecord(dbObjectVertex, new List<DBObjectAttributeEntry>(0), files));
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
