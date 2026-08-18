
// Type: Intermech.Interfaces.Data.Actions.UploadFilesAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Intermech.Interfaces.Data.Actions;

public sealed class UploadFilesAction : IAction, IObjectFilesUploadResult
{
  private IDBObjectRef objRef;
  private IList<UploadFileInfo> items;
  private List<FileState> uploadedFileStates;
  private bool fullRewriteMode;

  public UploadFilesAction(IDBObjectRef objRef, IList<UploadFileInfo> items)
  {
    if (objRef == null)
      throw new ArgumentNullException();
    if (items == null)
      throw new ArgumentNullException();
    this.objRef = objRef;
    this.items = items;
  }

  /// <summary>
  /// Включает и выключает режим полной перезаписи атрибута.
  /// По умолчанию режим выключен.
  /// </summary>
  public bool FullRewriteMode
  {
    get => this.fullRewriteMode;
    set => this.fullRewriteMode = value;
  }

  /// <summary>
  /// Возвращает коллекцию состояний файлов непосредственно после записи в базу IPS. Значение свойства может быть null,
  /// если запись файлов еще не была выполнена.
  /// </summary>
  public ICollection<FileState> UploadedFileStates
  {
    get => (ICollection<FileState>) this.uploadedFileStates;
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID);
      if (attributeById == null)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("SR_1655"), (object) dbObject.NameInMessages, (object) dbObject.ObjectID)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      if (this.fullRewriteMode)
        attributeById.ClearValues();
      List<string> fileNames = new List<string>((IEnumerable<string>) attributeById.Descriptions);
      this.uploadedFileStates = new List<FileState>(this.items.Count);
      for (int i = 0; i < this.items.Count; i++)
      {
        FileInfo fileInfo = new FileInfo(this.items[i].FullFileName);
        DateTime modifyDate = fileInfo.LastWriteTimeUtc.TruncateToSecond() + sessionKeeper.Session.TimeZoneOffset;
        BlobInformation aBlobInformation = new BlobInformation(fileInfo.Length, 0L, modifyDate, this.items[i].FileName, ArcMethods.ZLibPacked, string.Empty);
        int aIndex = fileNames.FindIndex((Predicate<string>) (fileNameInArray => PathUtils.IsSamePath(this.items[i].FileName, fileNameInArray)));
        if (aIndex >= 0)
        {
          attributeById.Index = aIndex;
          aBlobInformation.BlobID = attributeById.AsInteger;
          aBlobInformation.FileType = ((IDBFileAttribute) attributeById).FileType;
        }
        else if (this.IsNullFileAttribute(fileNames))
        {
          aBlobInformation.BlobID = attributeById.AsInteger;
          aBlobInformation.FileType = this.items[i].FileType;
          fileNames.Clear();
          fileNames.Add(this.items[i].FileName);
          aIndex = 0;
        }
        else
        {
          aIndex = attributeById.AddValue((object) null);
          aBlobInformation.BlobID = attributeById.AsInteger;
          aBlobInformation.FileType = this.items[i].FileType;
          fileNames.Add(this.items[i].FileName);
        }
        using (Stream aSourceStream = (Stream) new FileStream(this.items[i].FullFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          new BlobProcWriter(objectId, AttributableElements.Object, sessionKeeper.Session.IdentHelper.FileAttributeID, aIndex, 0, aBlobInformation, aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        this.uploadedFileStates.Add(new FileState(aBlobInformation.FileName, aBlobInformation.ModifyDate - sessionKeeper.Session.TimeZoneOffset, aBlobInformation.RealFileSize));
      }
    }
  }

  private bool IsNullFileAttribute(List<string> fileNames)
  {
    if (fileNames.Count == 0)
      return true;
    return fileNames.Count == 1 && string.IsNullOrEmpty(fileNames[0]);
  }

  private int IndexOf(string fileName, List<string> fileNames)
  {
    for (int index = 0; index < fileNames.Count; ++index)
    {
      if (PathUtils.IsSamePath(fileName, fileNames[index]))
        return index;
    }
    return -1;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder(64 /*0x40*/ + this.items.Count * 16 /*0x10*/);
    stringBuilder.Append(LocalizationHolder.rm.GetString("SR_1656"));
    stringBuilder.Append(' ');
    stringBuilder.Append('{');
    if (this.items.Count > 0)
    {
      stringBuilder.Append(this.items[0].FileName);
      for (int index = 1; index < this.items.Count; ++index)
      {
        stringBuilder.Append(',');
        stringBuilder.Append(' ');
        stringBuilder.Append(this.items[index].FileName);
      }
    }
    stringBuilder.Append('}');
    return stringBuilder.ToString();
  }
}
