// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.CompositionView.cvTechCopyButton
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.CompositionView;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard.Imbase;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.CompositionView;

/// <summary>Перекроем стандартный класс копирования объектов</summary>
[Serializable]
public class cvTechCopyButton : CVTechcardButtonBase
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="ownerObjId"></param>
  /// <param name="dbTypedObjectIds"></param>
  /// <param name="session"></param>
  public override void DoBeforeAllCreation(
    IDBTypedObjectID ownerObjId,
    List<IDBTypedObjectID> dbTypedObjectIds,
    IUserSession session)
  {
    if (dbTypedObjectIds == null || dbTypedObjectIds.Count == 0)
      return;
    IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) session, false);
    if (service == null)
      return;
    Dictionary<long, int> objects = new Dictionary<long, int>(dbTypedObjectIds.Count);
    foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIds)
    {
      if (dbTypedObjectId != null)
        objects[dbTypedObjectId.ObjectID] = dbTypedObjectId.ObjectType;
    }
    if (service.GetCreationMode((IDictionary<long, int>) objects, session.SessionGUID, out this._imObjectInfoList))
      return;
    this._imObjectInfoList = (Dictionary<long, ImbaseObjCreateInfo>) null;
  }

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="ownerObjId"></param>
  /// <param name="objectId"></param>
  /// <param name="relationHash"></param>
  /// <param name="session"></param>
  /// <param name="throwException"></param>
  /// <param name="errorString"></param>
  /// <returns></returns>
  public override IDBObject DoCreateObject(
    IDBTypedObjectID ownerObjId,
    IDBTypedObjectID objectId,
    Dictionary<int, List<cvRelationInfo>> relationHash,
    IUserSession session,
    bool throwException,
    out string errorString)
  {
    errorString = "";
    if (objectId == null || session == null)
      return (IDBObject) null;
    ImbaseObjCreateInfo imbaseObjCreateInfo;
    imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmUnknown;
    if (!this._imObjectInfoList.TryGetValue(objectId.ObjectID, out imbaseObjCreateInfo))
      imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmCreateNew;
    IDBObject dbObject = (IDBObject) null;
    switch (imbaseObjCreateInfo.CreateMode)
    {
      case ImbaseObjCreateMode.iocmUnknown:
      case ImbaseObjCreateMode.iocmCreateNew:
        IDBObjectCollection objectCollection = session.GetObjectCollection(objectId.ObjectType);
        if (objectCollection == null)
        {
          errorString = $"{sc_19306.ssp_techcard_19311()}{(object) objectId.ObjectType} not found";
          return (IDBObject) null;
        }
        dbObject = objectCollection.Create(objectId.ObjectID);
        break;
      case ImbaseObjCreateMode.iocmUseExists:
        dbObject = session.GetObject(objectId.ObjectID, false);
        break;
    }
    return dbObject;
  }

  /// <summary>Завершение создания объекта typedObject</summary>
  /// <param name="dbObject"></param>
  /// <param name="session"></param>
  public override void DoCommitObject(IDBObject dbObject, IUserSession session)
  {
    if (dbObject == null || !dbObject.IsCreationMode || session == null)
      return;
    ImbaseObjCreateInfo imbaseObjCreateInfo;
    imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmUnknown;
    if (!this._imObjectInfoList.TryGetValue(dbObject.ObjectID, out imbaseObjCreateInfo))
      imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmCreateNew;
    if (imbaseObjCreateInfo.CreateMode == ImbaseObjCreateMode.iocmUseExists || !dbObject.IsCreationMode)
      return;
    dbObject.CommitCreation(true, UISettings.AutoCheckOutNewObjects);
  }
}
