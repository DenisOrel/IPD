// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DBObjectHelper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Вспомогательный класс для работы с базой данных</summary>
public class DBObjectHelper
{
  /// <summary>Установить аттрибут у  связи</summary>
  /// <param name="session">Сессия</param>
  /// <param name="relationId">Id связи</param>
  /// <param name="attrId">Id аттрибута</param>
  /// <param name="values">значения</param>
  /// <returns>true если операция выполнена успешно</returns>
  public static bool SetRelationAttributeValue(
    IUserSession session,
    long relationId,
    int attrId,
    object[] values)
  {
    bool flag1 = false;
    IDBRelation relation = session.GetRelation(relationId, false);
    if (relation != null)
    {
      IDBObject dbObject = session.GetObject(relation.ProjID, false);
      if (dbObject != null)
      {
        bool flag2 = false;
        if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          dbObject = dbObject.CheckOut();
          flag2 = true;
        }
        if (dbObject.CheckoutBy != session.UserID)
          return false;
        try
        {
          IDBAttribute attributeById = relation.GetAttributeByID(attrId);
          if (attributeById == null)
          {
            if (values != null)
            {
              relation.Attributes.AddAttribute(attrId, false, values);
              flag1 = true;
            }
          }
          else
          {
            if (values != null)
              attributeById.Values = values;
            else
              attributeById.ClearValues();
            flag1 = true;
          }
        }
        catch
        {
          flag1 = false;
        }
        finally
        {
          if (flag2)
            dbObject.CheckIn();
        }
      }
    }
    return flag1;
  }

  /// <summary>Получить значение аттрибута у связи</summary>
  /// <param name="session">Сессия</param>
  /// <param name="relationId">Id связи</param>
  /// <param name="attrId">Id аттрибута</param>
  /// <returns></returns>
  public static object[] GetRelationAttributeValue(
    IUserSession session,
    long relationId,
    int attrId)
  {
    IDBRelation relation = session.GetRelation(relationId, false);
    if (relation != null)
    {
      IDBAttribute attributeById = relation.GetAttributeByID(attrId);
      if (attributeById != null)
        return attributeById.Values;
    }
    return (object[]) null;
  }

  public static AttributeValues[] Filter(IDBObject dbObj, AttributeValues[] values)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in values)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObj.ObjectType, attributeValues.AttributeID);
      if (attribute4ObjectType != null && attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue)
        attributeValuesList.Add(attributeValues);
    }
    return attributeValuesList.ToArray();
  }

  /// <summary>Записываем поток в файловый аттрибут</summary>
  /// <param name="objID"></param>
  /// <param name="fileName"></param>
  /// <param name="attrFileId"></param>
  /// <param name="fileIndex">Если -1 то добавить новый</param>
  /// <param name="stream"></param>
  public static void SaveStreamToFileAttribute(
    IDBObject dbObj,
    string fileName,
    int attrFileId,
    int fileIndex,
    Stream stream)
  {
    if (fileIndex == -1)
    {
      IDBAttribute attributeById = dbObj.GetAttributeByID(attrFileId);
      attributeById.AddValue((object) null);
      fileIndex = attributeById.ValuesCount - 1;
    }
    BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, fileName, ArcMethods.ZLibPacked, string.Empty);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      BlobWriterStream destination = new BlobWriterStream(dbObj.ObjectID, AttributableElements.Object, attrFileId, fileIndex, 0, info, sessionKeeper.Session);
      try
      {
        stream.CopyTo((Stream) destination);
      }
      finally
      {
        destination.Commit();
      }
    }
  }

  public static bool SetDBAttributeValues(IDBObject dbObj, AttributeValues[] values)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in values)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObj.ObjectType, attributeValues.AttributeID);
      if (attribute4ObjectType != null && attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue)
        attributeValuesList.Add(attributeValues);
    }
    if (attributeValuesList.Count <= 0)
      return false;
    dbObj.SetAttributesValues(attributeValuesList.ToArray());
    return true;
  }

  /// <summary>Установить аттрибут у  объекта</summary>
  /// <param name="session">Сессия</param>
  /// <param name="objectId">Id объекта</param>
  /// <param name="attrId">Id аттрибута</param>
  /// <param name="values">значения</param>
  /// <returns>true если операция выполнена успешно</returns>
  public static bool SetObjectAttributeValue(
    IUserSession session,
    long objectId,
    int attrId,
    object[] values)
  {
    bool flag1 = false;
    IDBObject dbObject = session.GetObject(objectId, false);
    if (dbObject != null)
    {
      bool flag2 = false;
      if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        dbObject = dbObject.CheckOut();
        flag2 = true;
      }
      if (dbObject.CheckoutBy != session.UserID)
        return false;
      try
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(attrId);
        if (attributeById == null)
        {
          if (values != null)
          {
            dbObject.Attributes.AddAttribute(attrId, false, values);
            flag1 = true;
          }
        }
        else
        {
          if (values != null)
            attributeById.Values = values;
          else
            attributeById.ClearValues();
          flag1 = true;
        }
      }
      catch
      {
        flag1 = false;
      }
      finally
      {
        if (flag2)
          dbObject.CheckIn();
      }
    }
    return flag1;
  }
}
