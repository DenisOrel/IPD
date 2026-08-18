// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseExtendedService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseExtendedService : LongLifeObject, IImbaseExtendedService
{
  private readonly ImbaseExtendedData _cacheData = new ImbaseExtendedData();

  public ImbaseExtendedService(Guid sessionGuid) => this.LoadConfigData(sessionGuid);

  public Dictionary<int, ImbaseExtendedItem> GetValues(int objTypeID)
  {
    ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo;
    lock (this)
      this._cacheData.ObjectTypeData.TryGetValue(objTypeID, out extendedObjectTypeInfo);
    return extendedObjectTypeInfo?.AttributeData as Dictionary<int, ImbaseExtendedItem>;
  }

  public bool LoadConfigData(Guid sessionGuid)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return false;
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("ImbaseManualSettings", out config_info, out config_file, 0L);
    if (config_info.RealFileSize == 0L)
      return false;
    lock (this)
    {
      this._cacheData.ObjectTypeData.Clear();
      try
      {
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          object deserializeObj = new BinaryFormatter().Deserialize((Stream) serializationStream);
          if (!this.LoadConfigDataFormatV3(deserializeObj) && !this.LoadConfigDataFormatV2(deserializeObj))
          {
            if (!this.LoadConfigDataFormatV1(deserializeObj))
              goto label_17;
          }
          return true;
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        return false;
      }
    }
label_17:
    return false;
  }

  private bool LoadConfigDataFormatV1(object deserializeObj)
  {
    if (!(deserializeObj is Dictionary<int, Dictionary<int, long>> dictionary))
      return false;
    foreach (KeyValuePair<int, Dictionary<int, long>> keyValuePair1 in dictionary)
    {
      ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo;
      if (!this._cacheData.ObjectTypeData.TryGetValue(keyValuePair1.Key, out extendedObjectTypeInfo))
      {
        extendedObjectTypeInfo = new ImbaseExtendedObjectTypeInfo();
        this._cacheData.ObjectTypeData[keyValuePair1.Key] = extendedObjectTypeInfo;
      }
      foreach (KeyValuePair<int, long> keyValuePair2 in keyValuePair1.Value)
        extendedObjectTypeInfo.AttributeData[keyValuePair2.Key] = new ImbaseExtendedItem(keyValuePair2.Value);
    }
    return true;
  }

  private bool LoadConfigDataFormatV2(object deserializeObj)
  {
    if (!(deserializeObj is Dictionary<int, Dictionary<int, ImbaseExtendedItem>> dictionary))
      return false;
    foreach (KeyValuePair<int, Dictionary<int, ImbaseExtendedItem>> keyValuePair1 in dictionary)
    {
      ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo = new ImbaseExtendedObjectTypeInfo();
      this._cacheData.ObjectTypeData[keyValuePair1.Key] = extendedObjectTypeInfo;
      foreach (KeyValuePair<int, ImbaseExtendedItem> keyValuePair2 in keyValuePair1.Value)
        extendedObjectTypeInfo.AttributeData[keyValuePair2.Key] = keyValuePair2.Value;
    }
    return true;
  }

  private bool LoadConfigDataFormatV3(object deserializeObj)
  {
    if (!(deserializeObj is ImbaseExtendedData imbaseExtendedData))
      return false;
    foreach (KeyValuePair<int, ImbaseExtendedObjectTypeInfo> keyValuePair1 in (IEnumerable<KeyValuePair<int, ImbaseExtendedObjectTypeInfo>>) imbaseExtendedData.ObjectTypeData)
    {
      ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo = new ImbaseExtendedObjectTypeInfo();
      this._cacheData.ObjectTypeData[keyValuePair1.Key] = extendedObjectTypeInfo;
      foreach (KeyValuePair<int, ImbaseExtendedItem> keyValuePair2 in (IEnumerable<KeyValuePair<int, ImbaseExtendedItem>>) keyValuePair1.Value.AttributeData)
        extendedObjectTypeInfo.AttributeData[keyValuePair2.Key] = keyValuePair2.Value;
    }
    return true;
  }

  public bool SaveConfigData(Guid sessionGuid)
  {
    if (!(ImbaseServer.GetSession(sessionGuid) is UserSession session) || !session.IsAdmin && !session.IsSystemSession)
      return false;
    lock (this)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._cacheData);
        try
        {
          session.Configurations.WriteConfigData(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "ImbaseManualSettings", ArcMethods.ZLibPacked, string.Empty), serializationStream.ToArray(), 0L);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return false;
        }
      }
    }
    return true;
  }

  public void SetValues(Guid sessionGuid, int objTypeID, IDictionary<int, ImbaseExtendedItem> dict)
  {
    if (ImbaseServer.GetSession(sessionGuid) == null)
      return;
    lock (this)
    {
      ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo1;
      if (this._cacheData.ObjectTypeData.TryGetValue(objTypeID, out extendedObjectTypeInfo1))
      {
        if (dict == null || dict.Count == 0)
        {
          this._cacheData.ObjectTypeData.Remove(objTypeID);
        }
        else
        {
          extendedObjectTypeInfo1.AttributeData.Clear();
          foreach (KeyValuePair<int, ImbaseExtendedItem> keyValuePair in (IEnumerable<KeyValuePair<int, ImbaseExtendedItem>>) dict)
            extendedObjectTypeInfo1.AttributeData[keyValuePair.Key] = keyValuePair.Value;
        }
      }
      else if (dict != null)
      {
        if (dict.Count != 0)
        {
          ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo2 = new ImbaseExtendedObjectTypeInfo();
          this._cacheData.ObjectTypeData[objTypeID] = extendedObjectTypeInfo2;
          foreach (KeyValuePair<int, ImbaseExtendedItem> keyValuePair in (IEnumerable<KeyValuePair<int, ImbaseExtendedItem>>) dict)
            extendedObjectTypeInfo2.AttributeData[keyValuePair.Key] = keyValuePair.Value;
        }
      }
    }
    this.SaveConfigData(sessionGuid);
  }

  public ImbaseExtendedData GetAllValues()
  {
    lock (this)
      return this._cacheData;
  }

  private void AfterDeleteAttributeTypeEvent(IDBAttributeType sender, IUserSession session)
  {
    if (sender == null || session == null)
      return;
    bool flag = false;
    lock (this)
    {
      foreach (ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo in (IEnumerable<ImbaseExtendedObjectTypeInfo>) this._cacheData.ObjectTypeData.Values)
        flag = flag || extendedObjectTypeInfo.AttributeData.Remove(sender.AttributeID);
    }
    if (!flag)
      return;
    this.SaveConfigData(session.SessionGUID);
  }

  private void AfterDeleteObjectTypeEvent(IDBObjectType sender, IUserSession session)
  {
    if (sender == null || session == null)
      return;
    bool flag;
    lock (this)
      flag = this._cacheData.ObjectTypeData.Remove(sender.ObjectType);
    if (!flag)
      return;
    this.SaveConfigData(session.SessionGUID);
  }

  public void SubscribeOnSystemlEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterDeleteObjectTypeEvent += new DeleteObjectTypeHandler(this.AfterDeleteObjectTypeEvent);
    eventHelper.AfterDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(this.AfterDeleteAttributeTypeEvent);
  }

  public void UnSubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterDeleteObjectTypeEvent -= new DeleteObjectTypeHandler(this.AfterDeleteObjectTypeEvent);
    eventHelper.AfterDeleteAttributeTypeEvent -= new DeleteAttributeTypeHandler(this.AfterDeleteAttributeTypeEvent);
  }
}
