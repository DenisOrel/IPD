// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.StandaloneView.StandaloneViewServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.StandaloneView;
using Intermech.IO;
using Intermech.Pools;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Services.StandaloneView;

internal sealed class StandaloneViewServerService : LongLifeObject, IStandaloneViewServerService
{
  private const string SettingsFileNameOnly = "StandaloneViewSettings.dat";
  private const string SettingsModuleName = "KERNEL";
  private const string SettingsSectionName = "StandaloneViewSettings";
  private const string WriteSeqParam = "WriteSeq";
  private IObjectPool<IFormatter> formatterPool;

  public StandaloneViewServerService()
  {
    this.formatterPool = new StackPool<IFormatter>(4, (Func<IFormatter>) (() => this.CreateFormatter())).Synchronized<IFormatter>();
  }

  public StandaloneViewObjectTypeSettings GetEffectiveSettings(Guid sessionGuid, int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    RBSServer.AuthenticateCaller();
    using (UserSessionContext.CaptureSession(sessionGuid))
      return this.GetEffectiveSettings(objectType);
  }

  private StandaloneViewObjectTypeSettings GetEffectiveSettings(int objectType)
  {
    StandaloneViewObjectTypeSettings effectiveSettings = this.TryLoadSettings(objectType) ?? new StandaloneViewObjectTypeSettings();
    if (!effectiveSettings.IsFullyDefined)
    {
      List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(objectType);
      while (!effectiveSettings.IsFullyDefined && parentsIdReverse.Count != 0)
      {
        int index = parentsIdReverse.Count - 1;
        StandaloneViewObjectTypeSettings other = this.TryLoadSettings(parentsIdReverse[index]);
        if (other != null)
          effectiveSettings.MergeWith(other);
        parentsIdReverse.RemoveAt(index);
      }
      effectiveSettings.MakeFullDefined();
    }
    return effectiveSettings;
  }

  public StandaloneViewObjectTypeSettings TryLoadSettings(Guid sessionGuid, int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    RBSServer.AuthenticateCaller();
    using (UserSessionContext.CaptureSession(sessionGuid))
      return this.TryLoadSettings(objectType);
  }

  private StandaloneViewObjectTypeSettings TryLoadSettings(int objectType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject containerForObjectType = ServiceUtils.GetService<IContainerService>((object) sessionKeeper.Session, true).GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, objectType, false);
      if (containerForObjectType != null)
      {
        IDBAttribute fileAttribute = this.GetFileAttribute(containerForObjectType, false);
        if (fileAttribute != null)
        {
          StandaloneViewObjectTypeSettings objectTypeSettings = this.TryLoadSettings(fileAttribute);
          if (objectTypeSettings != null)
            return objectTypeSettings;
        }
      }
    }
    return (StandaloneViewObjectTypeSettings) null;
  }

  private StandaloneViewObjectTypeSettings TryLoadSettings(IDBAttribute dbFileAttribute)
  {
    string settingsFileName = this.GetActualSettingsFileName(dbFileAttribute);
    int aIndex = Array.IndexOf<string>(dbFileAttribute.Descriptions, settingsFileName);
    if (aIndex < 0)
      return (StandaloneViewObjectTypeSettings) null;
    using (Stream stream = (Stream) new ImChunkedStream())
    {
      new BlobProcReader(dbFileAttribute.DBObjectID, AttributableElements.Object, dbFileAttribute.AttributeID, aIndex, 0, stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      stream.Position = 0L;
      return this.DeserializeSettings(stream);
    }
  }

  private StandaloneViewObjectTypeSettings DeserializeSettings(Stream st)
  {
    IFormatter formatter = this.formatterPool.Allocate();
    try
    {
      return (StandaloneViewObjectTypeSettings) formatter.Deserialize(st);
    }
    finally
    {
      this.formatterPool.Release(formatter);
    }
  }

  public void SaveSettings(
    Guid sessionGuid,
    int objectType,
    StandaloneViewObjectTypeSettings settings)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    RBSServer.AuthenticateCaller();
    RBSServer.AuthorizeAsAdmin();
    using (UserSessionContext.CaptureSession(sessionGuid))
      this.SaveSettings(objectType, settings);
  }

  private void SaveSettings(int objectType, StandaloneViewObjectTypeSettings settings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.SaveSettings(this.GetFileAttribute(ServiceUtils.GetService<IContainerService>((object) sessionKeeper.Session, true).GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, objectType, true), true), settings);
      this.IncWriteSeq(sessionKeeper.Session);
    }
  }

  private void SaveSettings(IDBAttribute dbFileAttribute, StandaloneViewObjectTypeSettings settings)
  {
    string settingsFileName = this.GetActualSettingsFileName(dbFileAttribute);
    string[] descriptions = dbFileAttribute.Descriptions;
    int aIndex = Array.IndexOf<string>(descriptions, settingsFileName);
    if (aIndex < 0)
      aIndex = descriptions.Length != 1 || !dbFileAttribute.IsNull ? dbFileAttribute.AddValue((object) FileTypes.ftNormal) : 0;
    using (Stream stream = (Stream) new ImChunkedStream())
    {
      this.SerializeSettings(stream, settings);
      stream.Flush();
      DateTime modifyDate = DateTime.UtcNow + dbFileAttribute.Session.TimeZoneOffset;
      BlobInformation aBlobInformation = new BlobInformation(stream.Length, 0L, modifyDate, settingsFileName, ArcMethods.ZLibPacked, string.Empty);
      dbFileAttribute.Index = aIndex;
      aBlobInformation.BlobID = dbFileAttribute.AsInteger;
      aBlobInformation.FileType = FileTypes.ftNormal;
      stream.Position = 0L;
      new BlobProcWriter(dbFileAttribute.DBObjectID, AttributableElements.Object, dbFileAttribute.AttributeID, aIndex, 0, aBlobInformation, stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
    }
  }

  private void SerializeSettings(Stream st, StandaloneViewObjectTypeSettings settings)
  {
    IFormatter formatter = this.formatterPool.Allocate();
    try
    {
      formatter.Serialize(st, (object) settings);
    }
    finally
    {
      this.formatterPool.Release(formatter);
    }
  }

  public void RemoveSettings(Guid sessionGuid, int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    RBSServer.AuthenticateCaller();
    RBSServer.AuthorizeAsAdmin();
    using (UserSessionContext.CaptureSession(sessionGuid))
      this.RemoveSettings(objectType);
  }

  private void RemoveSettings(int objectType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject containerForObjectType = ServiceUtils.GetService<IContainerService>((object) sessionKeeper.Session, true).GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, objectType, false);
      if (containerForObjectType == null)
        return;
      IDBAttribute fileAttribute = this.GetFileAttribute(containerForObjectType, false);
      if (fileAttribute == null || !this.RemoveSettings(fileAttribute))
        return;
      this.IncWriteSeq(sessionKeeper.Session);
    }
  }

  private bool RemoveSettings(IDBAttribute dbFileAttribute)
  {
    string settingsFileName = this.GetActualSettingsFileName(dbFileAttribute);
    int num = Array.IndexOf<string>(dbFileAttribute.Descriptions, settingsFileName);
    if (num < 0)
      return false;
    dbFileAttribute.Index = num;
    if (dbFileAttribute.ValuesCount == 1)
      dbFileAttribute.Clear();
    else
      dbFileAttribute.DeleteValue();
    return true;
  }

  private IDBAttribute GetFileAttribute(IDBObject dbContainer, bool createIfNotExists)
  {
    int fileAttributeId = dbContainer.Session.IdentHelper.FileAttributeID;
    IDBAttribute fileAttribute = dbContainer.GetAttributeByID(fileAttributeId);
    if (fileAttribute == null & createIfNotExists)
      fileAttribute = dbContainer.Attributes.AddAttribute(fileAttributeId, true);
    return fileAttribute;
  }

  private string GetActualSettingsFileName(IDBAttribute dbFileAttribute)
  {
    return Path.Combine(Math.Abs(dbFileAttribute.DBObjectID).ToString(), "StandaloneViewSettings.dat");
  }

  private IFormatter CreateFormatter()
  {
    return (IFormatter) new BinaryFormatter()
    {
      AssemblyFormat = FormatterAssemblyStyle.Simple,
      FilterLevel = TypeFilterLevel.Full,
      Context = new StreamingContext(StreamingContextStates.File)
    };
  }

  public long GetWriteSequence(Guid sessionGuid)
  {
    RBSServer.AuthenticateCaller();
    using (UserSessionContext.CaptureSession(sessionGuid))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return this.ReadWriteSeq(sessionKeeper.Session);
    }
  }

  private long ReadWriteSeq(IUserSession session)
  {
    long result;
    return !long.TryParse(session.Configurations.ReadStringNoCache("KERNEL", "StandaloneViewSettings", "WriteSeq", true), out result) ? 0L : result;
  }

  private void UpdateWriteSeq(IUserSession session, long newValue)
  {
    session.Configurations.WriteString("KERNEL", "StandaloneViewSettings", "WriteSeq", Convert.ToString(newValue), 0L);
  }

  private void IncWriteSeq(IUserSession session)
  {
    this.UpdateWriteSeq(session, this.ReadWriteSeq(session) + 1L);
  }
}
