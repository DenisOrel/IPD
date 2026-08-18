// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Rule.NumRuleObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.TcNumerationRules;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Rule;

/// <summary>Summary description for NumRuleObjectCreatorService.</summary>
public class NumRuleObjectCreatorService : IObjectCreatorCustomService
{
  /// <summary>Реализация CreateObjectDialog</summary>
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
    long objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
      objectId = (TemplateObjectID == 0L || TemplateObjectID == -1L ? objectCollection.Create() : objectCollection.Create(TemplateObjectID)).ObjectID;
    }
    bool flag = NumRuleEditor.ShowDialog(objectId, true);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
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
      dbObject.Delete((long) sc_19539.ssp_techcard_19540(1083195092));
      return 0;
    }
  }
}
