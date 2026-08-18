// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.UnitContainer
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using System;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal class UnitContainer
{
  [NotNull]
  public static IDBObject GetContainer([NotNull] IUserSession session, long unitID)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(OfficeConsts.ObjtypeContainerID);
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(OfficeConsts.AttrUnitLinkID, RelationalOperators.Equal, (object) unitID, LogicalOperators.NONE, 0, true)
    }, new object[1]{ (object) -2 }));
    IDBObject container;
    if (dataTable.Rows.Count > 0)
    {
      container = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
    }
    else
    {
      container = objectCollection.Create();
      IDBObject dbObject = session.GetObject(unitID);
      container.Caption = "Контейнер для " + dbObject.NameInMessages;
      container.Attributes.AddAttribute(OfficeConsts.AttrUnitLinkID, false, new object[1]
      {
        (object) unitID
      });
      container.CommitCreation(true);
    }
    return container;
  }
}
