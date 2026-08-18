// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertFolder
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Concurrent;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertFolder : 
  ExpertFormulable,
  IExpertCond,
  IExpertFormulable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public ExpertFolder(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._objType = ExpertObjType.Formula;
  }

  public override void SetTempFormula(TempFormula tf)
  {
    base.SetTempFormula(tf);
    ConcurrentDictionary<long, ESFolderInfo> folderDict = ESFolderKeeper.Keeper.folderDict;
    if (folderDict == null)
      return;
    long num = Math.Abs(this.ObjectID);
    ESFolderInfo newValue = new ESFolderInfo(num, this.Caption, tf);
    if (folderDict.ContainsKey(this.ObjectID))
    {
      newValue.CopyParents(folderDict[num]);
      folderDict.TryUpdate(num, newValue, folderDict[num]);
    }
    else
      folderDict.GetOrAdd(this.ObjectID, newValue);
  }

  public override void DoAfterCreateRelation(IDBRelation relation)
  {
    this.RemoveRelObjectFromCache(relation);
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    this.RemoveRelObjectFromCache(relation);
  }

  protected void RemoveRelObjectFromCache(IDBRelation relation)
  {
    long num = relation.PartObjectID;
    if (num == 0L)
    {
      IDBObject objectById = this.Session.GetObjectByID(relation.PartID, false);
      if (objectById != null)
        num = objectById.ObjectID;
    }
    if (num == 0L)
      return;
    ESFolderKeeper.Keeper.RemoveFromFolderCache(num);
    ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheObjFromFolder, num, 0L, this.UserSession.DataManager);
  }
}
