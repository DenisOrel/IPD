// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.PublishTableLinksHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal sealed class PublishTableLinksHandler : LinkedObjectsHandler, ILinkedObjectsHandler
{
  public List<int> HandleTypes
  {
    get
    {
      return new List<int>((IEnumerable<int>) new int[2]
      {
        Intermech.Imbase.Consts.ImbaseTableRefTypeID,
        Intermech.Imbase.Consts.ImbaseTableTypeID
      });
    }
  }

  public List<int> OutputTypes
  {
    get
    {
      return new List<int>((IEnumerable<int>) new int[2]
      {
        Intermech.Imbase.Consts.ImbaseTableTypeID,
        Intermech.Imbase.Consts.ImbaseTableRefTypeID
      });
    }
  }

  public List<LinkedObject> Handle(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID)
  {
    IDBObject dbObject = session.GetObject(objectID);
    if (dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
      if (attributeById != null && attributeById.AsInteger != 0L)
        return new List<LinkedObject>()
        {
          new LinkedObject(attributeById.AsInteger)
        };
    }
    else if (dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
    {
      DataTable dataTable = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) objectID, LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 }));
      if (dataTable.Rows.Count > 0)
      {
        List<LinkedObject> linkedObjectList = new List<LinkedObject>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          linkedObjectList.Add(new LinkedObject(Convert.ToInt64(row[0])));
        return linkedObjectList;
      }
    }
    return (List<LinkedObject>) null;
  }

  public string Name => "Модуль Imbase";

  protected override void OnReloadTypes()
  {
  }

  bool ILinkedObjectsHandler.IsTypesChanged(IUserSession session) => this.IsTypesChanged(session);

  void ILinkedObjectsHandler.UpdateHandleAndOutputTypes(IUserSession session, bool force)
  {
    this.UpdateHandleAndOutputTypes(session, force);
  }
}
