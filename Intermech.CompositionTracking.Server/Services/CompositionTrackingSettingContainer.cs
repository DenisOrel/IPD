// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Services.CompositionTrackingSettingContainer
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using ImSSP;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Kernel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

#nullable disable
namespace Intermech.CompositionTracking.Server.Services;

internal class CompositionTrackingSettingContainer
{
  private readonly ReaderWriterLockSlim _synchLock = new ReaderWriterLockSlim();
  private readonly ConcurrentDictionary<int, List<CompositionTrackSettingData>> _registeredTypes;
  private readonly ConcurrentDictionary<int, CompositionTypeSettingDataList> _typeSettings;

  private bool LoadConfigData_V2(IDBConfigurations configuration, out bool errorOnLoad)
  {
    errorOnLoad = false;
    byte[] config_file = (byte[]) null;
    BlobInformation config_info = BlobInformation.EmptyBlobInformation();
    try
    {
      configuration.LoadConfigData("TypeSettings_Binary_V2", out config_info, out config_file, 0L);
    }
    catch (Exception ex)
    {
      IOutputView service = (IOutputView) CompositionTrackingServerHolder.ServiceProvider.GetService(typeof (IOutputView));
      if (service != null)
      {
        service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), LocalizationHolder.rm.GetString("CompositionTracking.Server_1"));
        service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), ex.Message);
      }
    }
    if (config_file == null || config_info.RealFileSize == 0L)
      return false;
    bool flag = false;
    using (MemoryStream memoryStream = new MemoryStream(config_file))
    {
      MemoryStream serializationStream = memoryStream;
      memoryStream.Position = 0L;
      if (config_info.RealFileSize != config_info.PackedFileSize)
      {
        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
        serializationStream = new MemoryStream(config_file.Length / 4);
        MemoryStream outStream = serializationStream;
        MemoryStream inStream = memoryStream;
        service.UnpackStream((Stream) outStream, (Stream) inStream);
        serializationStream.Position = 0L;
      }
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        object obj = (object) null;
        try
        {
          obj = binaryFormatter.Deserialize((Stream) serializationStream);
          flag = true;
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case SerializationException _:
            case ArgumentException _:
              break;
            default:
              throw;
          }
        }
        if (obj != null)
        {
          if (obj is IDictionary<int, CompositionTypeSettingDataList> values)
            this._typeSettings.AddRange<KeyValuePair<int, CompositionTypeSettingDataList>>((IEnumerable<KeyValuePair<int, CompositionTypeSettingDataList>>) values);
          else
            errorOnLoad = true;
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        errorOnLoad = true;
      }
    }
    return flag;
  }

  private bool LoadConfigData_V1(IDBConfigurations configuration, out bool errorOnLoad)
  {
    errorOnLoad = false;
    byte[] config_file = (byte[]) null;
    BlobInformation config_info = BlobInformation.EmptyBlobInformation();
    try
    {
      configuration.LoadConfigData("TypeSettings_Binary_V1", out config_info, out config_file, 0L);
    }
    catch (Exception ex)
    {
      IOutputView service = (IOutputView) CompositionTrackingServerHolder.ServiceProvider.GetService(typeof (IOutputView));
      if (service != null)
      {
        service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), LocalizationHolder.rm.GetString("CompositionTracking.Server_1"));
        service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), ex.Message);
      }
    }
    if (config_file == null || config_info.RealFileSize == (long) sc_5511.ssp_appserver_5512(634410466))
      return false;
    bool flag = false;
    using (MemoryStream serializationStream = new MemoryStream(config_file))
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        object obj = (object) null;
        try
        {
          obj = binaryFormatter.Deserialize((Stream) serializationStream);
          flag = true;
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case SerializationException _:
            case ArgumentException _:
              break;
            default:
              throw;
          }
        }
        if (obj != null)
        {
          if (obj is IDictionary<int, CompositionTypeSettingDataList> values)
            this._typeSettings.AddRange<KeyValuePair<int, CompositionTypeSettingDataList>>((IEnumerable<KeyValuePair<int, CompositionTypeSettingDataList>>) values);
          else
            errorOnLoad = true;
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        errorOnLoad = true;
      }
    }
    return flag;
  }

  private bool LoadConfigData_V0(IDBConfigurations configuration, out bool errorOnLoad)
  {
    errorOnLoad = false;
    BlobInformation config_info;
    byte[] config_file;
    configuration.LoadConfigData("TypeSettings_Binary", out config_info, out config_file, 0L);
    if (config_file == null || config_info.RealFileSize == (long) sc_5511.ssp_appserver_5513(1688512423))
      return false;
    bool flag = false;
    using (MemoryStream serializationStream = new MemoryStream(config_file))
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        if (binaryFormatter.Deserialize((Stream) serializationStream) is Dictionary<int, Dictionary<CompositionTrackSett, CompositionTrackingMode>> dictionary)
        {
          if (dictionary.Count != 0)
          {
            this._typeSettings.Clear();
            CompositionsTrackingSettings trackingSettings = new CompositionsTrackingSettings()
            {
              Commands = CompositionTrackingCommands.ctcUndoCheckOut | CompositionTrackingCommands.ctcCheckin | CompositionTrackingCommands.ctcNextLCStep
            };
            foreach (KeyValuePair<int, Dictionary<CompositionTrackSett, CompositionTrackingMode>> keyValuePair1 in dictionary)
            {
              flag = true;
              CompositionTypeSettingDataList typeSettingDataList = new CompositionTypeSettingDataList();
              this._typeSettings.TryAdd(keyValuePair1.Key, typeSettingDataList);
              foreach (KeyValuePair<CompositionTrackSett, CompositionTrackingMode> keyValuePair2 in keyValuePair1.Value)
              {
                if (keyValuePair2.Value != CompositionTrackingMode.ctmNone)
                  typeSettingDataList.Add((CompositionTrackSettingData) keyValuePair2.Key, trackingSettings);
              }
            }
          }
        }
        else
          errorOnLoad = true;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        errorOnLoad = true;
      }
    }
    return flag;
  }

  private void LoadConfigData_ConvertEnums()
  {
    foreach (Dictionary<CompositionTrackSettingData, CompositionsTrackingSettings> dictionary in (IEnumerable<CompositionTypeSettingDataList>) this._typeSettings.Values)
    {
      foreach (CompositionsTrackingSettings trackingSettings in dictionary.Values)
      {
        CompositionTrackingObjMode compositionTrackingObjMode = trackingSettings.Commands != CompositionTrackingCommands.ctcNone ? CompositionTrackingObjMode.ctomProceed : CompositionTrackingObjMode.ctcNone;
        trackingSettings.ObjMode = compositionTrackingObjMode;
      }
    }
  }

  internal CompositionTrackingSettingContainer()
  {
    this._registeredTypes = new ConcurrentDictionary<int, List<CompositionTrackSettingData>>();
    this._typeSettings = new ConcurrentDictionary<int, CompositionTypeSettingDataList>();
  }

  private void SaveConfigData(Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null || !sessionById.IsAdmin && !((UserSession) sessionById).IsSystemSession)
      return;
    this._synchLock.EnterWriteLock();
    try
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._typeSettings);
        IDBConfigurations configurations = sessionById.Configurations;
        BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "TypeSettings_Binary_V2", ArcMethods.NotPacked, string.Empty);
        try
        {
          configurations.WriteConfigData(config_info, serializationStream.ToArray(), 0L);
        }
        catch (Exception ex)
        {
          IOutputView service = (IOutputView) CompositionTrackingServerHolder.ServiceProvider.GetService(typeof (IOutputView));
          if (service == null)
            return;
          service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), LocalizationHolder.rm.GetString("CompositionTracking.Server_1"));
          service.WriteString(LocalizationHolder.rm.GetString("CompositionTracking.Server_3"), ex.Message);
        }
      }
    }
    finally
    {
      this._synchLock.ExitWriteLock();
    }
  }

  internal bool LoadConfigData(Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return false;
    bool flag1 = false;
    this._synchLock.EnterWriteLock();
    bool errorOnLoad;
    try
    {
      this._typeSettings.Clear();
      IDBConfigurations configurations = sessionById.Configurations;
      if (!this.LoadConfigData_V2(configurations, out errorOnLoad))
      {
        bool flag2 = this.LoadConfigData_V1(configurations, out errorOnLoad);
        if (!flag2)
          flag2 = this.LoadConfigData_V0(configurations, out errorOnLoad);
        if (flag2)
          this.LoadConfigData_ConvertEnums();
        flag1 = flag2 & !errorOnLoad;
      }
    }
    finally
    {
      this._synchLock.ExitWriteLock();
    }
    if (flag1)
      this.SaveConfigData(sessionGuid);
    return !errorOnLoad;
  }

  internal bool GetConfigValue(
    Guid sessionGuid,
    IObjectTypeApplicabilityContext objectTypeContext,
    out CompositionsTrackingSettings value)
  {
    if (objectTypeContext == null)
      throw new ArgumentNullException(nameof (objectTypeContext));
    this._synchLock.EnterReadLock();
    try
    {
      value = new CompositionsTrackingSettings();
      CompositionTypeSettingDataList typeSettingDataList;
      if (!this._typeSettings.TryGetValue(objectTypeContext.InObjectTypeId, out typeSettingDataList))
        return false;
      CompositionTrackSettingData other = new CompositionTrackSettingData(objectTypeContext);
      foreach (CompositionTrackSettingData key in typeSettingDataList.Keys)
      {
        if (key.CompareTo(other) == 0)
        {
          value = typeSettingDataList[key];
          return true;
        }
      }
    }
    finally
    {
      this._synchLock.ExitReadLock();
    }
    return false;
  }

  internal void SetConfigValue(
    Guid sessionGuid,
    IObjectTypeApplicabilityContext objectTypeContext,
    CompositionsTrackingSettings value)
  {
    if (objectTypeContext == null)
      throw new ArgumentNullException(nameof (objectTypeContext));
    if (UserSession.GetSessionByID(sessionGuid) == null)
      return;
    this._synchLock.EnterWriteLock();
    try
    {
      CompositionTrackSettingData key = new CompositionTrackSettingData(objectTypeContext);
      CompositionTypeSettingDataList typeSettingDataList;
      if (!this._typeSettings.TryGetValue(objectTypeContext.InObjectTypeId, out typeSettingDataList))
      {
        typeSettingDataList = new CompositionTypeSettingDataList();
        this._typeSettings[objectTypeContext.InObjectTypeId] = typeSettingDataList;
      }
      typeSettingDataList[key] = value;
    }
    finally
    {
      this._synchLock.ExitWriteLock();
    }
    this.SaveConfigData(sessionGuid);
  }

  internal void RegisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext)
  {
    if (objectTypeContext == null)
      throw new ArgumentNullException(nameof (objectTypeContext));
    List<CompositionTrackSettingData> trackSettingDataList;
    if (!this._registeredTypes.TryGetValue(objectTypeContext.InObjectTypeId, out trackSettingDataList))
    {
      trackSettingDataList = new List<CompositionTrackSettingData>();
      this._registeredTypes.TryAdd(objectTypeContext.InObjectTypeId, trackSettingDataList);
    }
    CompositionTrackSettingData trackSettingData = new CompositionTrackSettingData(objectTypeContext);
    if (trackSettingDataList.Contains(trackSettingData))
      return;
    trackSettingDataList.Add(trackSettingData);
  }

  internal void UnRegisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext)
  {
    if (objectTypeContext == null)
      throw new ArgumentNullException(nameof (objectTypeContext));
    List<CompositionTrackSettingData> trackSettingDataList;
    if (!this._registeredTypes.TryGetValue(objectTypeContext.InObjectTypeId, out trackSettingDataList))
      return;
    trackSettingDataList.Remove(new CompositionTrackSettingData(objectTypeContext));
  }

  internal bool IsRegisteredTrackConfig(
    IObjectTypeApplicabilityContext objectTypeContext,
    bool inheritMode)
  {
    if (objectTypeContext == null)
      throw new ArgumentNullException(nameof (objectTypeContext));
    List<int> intList = new List<int>();
    if (!inheritMode)
    {
      if (this._registeredTypes.ContainsKey(objectTypeContext.InObjectTypeId))
        intList.Add(objectTypeContext.InObjectTypeId);
    }
    else
    {
      List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectTypeContext.InObjectTypeId);
      if (!objectTypeParentsId.Contains(objectTypeContext.InObjectTypeId))
        objectTypeParentsId.Add(objectTypeContext.InObjectTypeId);
      foreach (int key in objectTypeParentsId)
      {
        if (this._registeredTypes.ContainsKey(key))
          intList.Add(key);
      }
      if (intList.Count == 0 && this._registeredTypes.ContainsKey(-1))
        intList.Add(-1);
    }
    if (intList.Count == 0)
      return false;
    CompositionTrackSettingData trackSettingData1 = new CompositionTrackSettingData(objectTypeContext);
    foreach (int key in intList)
    {
      foreach (CompositionTrackSettingData trackSettingData2 in this._registeredTypes[key])
      {
        if (trackSettingData2.CompareTo(trackSettingData1, inheritMode ? CompositionTrackingSettingInheritedComparer.Instance : CompositionTrackingSettingDirectComparer.Instance) == 0)
          return true;
      }
    }
    return false;
  }

  internal bool GetConfigValues(
    int objectTypeId,
    CompositionTrackingCommands command,
    out CompositionTypeSettingDataList trackSettList)
  {
    trackSettList = new CompositionTypeSettingDataList();
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectTypeId);
    if (!objectTypeParentsId.Contains(objectTypeId))
      objectTypeParentsId.Insert(0, objectTypeId);
    this._synchLock.EnterReadLock();
    try
    {
      HashSet<(int, int)> valueTupleSet = new HashSet<(int, int)>();
      foreach (int num in objectTypeParentsId)
      {
        CompositionTypeSettingDataList typeSettingDataList;
        if (num != -1 && this._typeSettings.TryGetValue(num, out typeSettingDataList) && typeSettingDataList != null && typeSettingDataList.Count != 0)
        {
          List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(num);
          foreach (KeyValuePair<CompositionTrackSettingData, CompositionsTrackingSettings> keyValuePair in (Dictionary<CompositionTrackSettingData, CompositionsTrackingSettings>) typeSettingDataList)
          {
            if (keyValuePair.Value != null)
            {
              bool flag1 = false;
              foreach (IMSApplicability imsApplicability in typeApplicabilities)
              {
                if (imsApplicability.RelationTypeID == keyValuePair.Key.ObjectTypeContext.RelationTypeId && imsApplicability.ChildObjectTypeID == keyValuePair.Key.ObjectTypeContext.ObjectTypeId)
                {
                  flag1 = imsApplicability.Public == InheritModes.Inherited;
                  break;
                }
              }
              if (!flag1)
              {
                (int, int) valueTuple = (keyValuePair.Key.ObjectTypeContext.RelationTypeId, keyValuePair.Key.ObjectTypeContext.ObjectTypeId);
                if (!valueTupleSet.Contains(valueTuple))
                {
                  if ((keyValuePair.Value.Commands & command) != command)
                  {
                    valueTupleSet.Add(valueTuple);
                  }
                  else
                  {
                    bool flag2 = false;
                    foreach (CompositionTrackSettingData key in trackSettList.Keys)
                    {
                      if (key.CompareTo(keyValuePair.Key) == 0)
                      {
                        flag2 = true;
                        break;
                      }
                    }
                    if (!flag2)
                      trackSettList.Add(keyValuePair.Key, keyValuePair.Value);
                  }
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      this._synchLock.ExitReadLock();
    }
    return trackSettList.Count > 0;
  }

  internal void ClearGarbage(IDBObjectType objType, IUserSession session)
  {
    if (objType == null || session == null)
      return;
    this._synchLock.EnterWriteLock();
    bool flag;
    try
    {
      flag = this._typeSettings.TryRemove(objType.ObjectType, out CompositionTypeSettingDataList _);
      List<CompositionTrackSettingData> trackSettingDataList = new List<CompositionTrackSettingData>();
      foreach (CompositionTypeSettingDataList typeSettingDataList in (IEnumerable<CompositionTypeSettingDataList>) this._typeSettings.Values)
      {
        trackSettingDataList.Clear();
        foreach (CompositionTrackSettingData key in typeSettingDataList.Keys)
        {
          if (key.ObjectTypeContext.ObjectTypeId == objType.ObjectType)
          {
            trackSettingDataList.Add(key);
            flag = true;
          }
        }
        foreach (CompositionTrackSettingData key in trackSettingDataList)
          typeSettingDataList.Remove(key);
      }
    }
    finally
    {
      this._synchLock.ExitWriteLock();
    }
    if (!flag)
      return;
    this.SaveConfigData(session.SessionGUID);
  }

  internal void ClearGarbage(
    RelationsApplicabilityProperties applicabilityProperties,
    IUserSession session)
  {
    if (session == null || CompositionTrackingServerHolder.TrackingService == null)
      return;
    bool flag = false;
    this._synchLock.EnterWriteLock();
    try
    {
      List<CompositionTrackSettingData> trackSettingDataList = new List<CompositionTrackSettingData>();
      foreach (CompositionTypeSettingDataList typeSettingDataList in (IEnumerable<CompositionTypeSettingDataList>) this._typeSettings.Values)
      {
        trackSettingDataList.Clear();
        foreach (CompositionTrackSettingData key in typeSettingDataList.Keys)
        {
          if (key.ObjectTypeContext.ObjectTypeId == applicabilityProperties.ObjectType && key.ObjectTypeContext.InObjectTypeId == applicabilityProperties.InObjectType && key.ObjectTypeContext.RelationTypeId == applicabilityProperties.RelationType)
          {
            trackSettingDataList.Add(key);
            flag = true;
          }
        }
        foreach (CompositionTrackSettingData key in trackSettingDataList)
          typeSettingDataList.Remove(key);
      }
    }
    finally
    {
      this._synchLock.ExitWriteLock();
    }
    if (!flag)
      return;
    this.SaveConfigData(session.SessionGUID);
  }
}
