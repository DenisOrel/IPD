
// Type: Intermech.Client.Core.CompositionView.cvCopyButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// Данная кнопка не используется на панели редактора,
/// юзается только при вставке элементов в дерево по контекстному меню
/// </summary>
[Serializable]
public class cvCopyButton : CVButtonBase
{
  /// <summary>Cоздание нового объекта по прототипу</summary>
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
    IDBObjectCollection objectCollection = session.GetObjectCollection(objectId.ObjectType);
    if (objectCollection != null)
      return objectCollection.Create(objectId.ObjectID);
    errorString = $"IDBObjectCollection for object type = {(object) objectId.ObjectType} not found";
    return (IDBObject) null;
  }

  /// <summary>Завершение создания объекта typedObject</summary>
  /// <param name="typedObject"></param>
  /// <param name="session"></param>
  public override void DoCommitObject(IDBObject typedObject, IUserSession session)
  {
    if (typedObject == null || !typedObject.IsCreationMode)
      return;
    typedObject.CommitCreation(true);
  }
}
