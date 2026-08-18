// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node.NumNodeObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;

/// <summary>Summary description for NumNodeObjectCreatorService.</summary>
public class NumNodeObjectCreatorService : IObjectCreatorCustomService
{
  /// <summary>CreateObjectDialog</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="TemplateObjectID"></param>
  /// <param name="RelationTypeIDs"></param>
  /// <param name="RelatedObjectIDs"></param>
  /// <param name="StartDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public long CreateObjectDialog(
    int objectTypeId,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    long objectDialog = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
      IDBObject dbObject = TemplateObjectID == 0L || TemplateObjectID == -1L ? objectCollection.Create() : objectCollection.Create(TemplateObjectID);
      if (dbObject != null)
        objectDialog = dbObject.ObjectID;
    }
    if (objectDialog == 0L)
      return objectDialog;
    bool flag = NumNodeEditor.ShowDialog(objectDialog);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectDialog);
      if (flag)
      {
        TechcardClientUtils.StartCreateRelations((IEnumerable<long>) RelatedObjectIDs, sessionKeeper.Session);
        try
        {
          TechcardClientUtils.CreateRelations(sessionKeeper.Session, dbObject.ObjectID, RelationTypeIDs, RelatedObjectIDs, StartDate, TechCreateRelMode.tcrmEnterIn);
        }
        finally
        {
          TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
        }
        dbObject.CommitCreation(false);
        return dbObject.ObjectID;
      }
      dbObject.Delete(0L);
      return 0;
    }
  }
}
