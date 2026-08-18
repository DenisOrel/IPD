// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertCond
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertCond(UserSession uSession, DataTable objectsTable) : 
  ExpertFormulable(uSession, objectsTable),
  IExpertCond,
  IExpertFormulable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public override IDBObject DoCheckout()
  {
    IDBObject dbObject = base.DoCheckout();
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(ExpertConsts.Consts.objObject);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrCondObj, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    foreach (DataRow dataRow in objectCollection.Select(paramSet).Select())
      this.UserSession.GetObject(Convert.ToInt64(dataRow[0])).CheckOut();
    return dbObject;
  }
}
