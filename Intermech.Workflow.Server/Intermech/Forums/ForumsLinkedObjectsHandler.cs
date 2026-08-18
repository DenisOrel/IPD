// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumsLinkedObjectsHandler
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Forums;

internal sealed class ForumsLinkedObjectsHandler : LinkedObjectsHandler, ILinkedObjectsHandler
{
  protected override void OnReloadTypes()
  {
    this.HandleTypes = MetaDataHelper.GetObjectTypesList().Where<IMSObjectType>((System.Func<IMSObjectType, bool>) (x => (x.Options & ObjectTypeOptions.ForumEnabled) == ObjectTypeOptions.ForumEnabled)).ToList<IMSObjectType>().ConvertAll<int>((Converter<IMSObjectType, int>) (x => x.ObjectTypeID));
  }

  public List<int> HandleTypes { get; private set; }

  public List<int> OutputTypes
  {
    get
    {
      return new List<int>((IEnumerable<int>) new int[1]
      {
        ForumsConsts.forumObjectTypeID
      });
    }
  }

  public string Name => "Модуль Workflow";

  public List<LinkedObject> Handle(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID)
  {
    QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
    if (objectInfo.Empty)
      return (List<LinkedObject>) null;
    DataTable dataTable = session.GetObjectCollection(ForumsConsts.forumObjectTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ForumsConsts.discussedObjectGuidAttributeID, RelationalOperators.Equal, (object) objectInfo.VersionGuid, LogicalOperators.AND, 0, false),
      new ConditionStructure(new Guid("cad01501-306c-11d8-b4e9-00304f19f545"), RelationalOperators.NotExistsOrEmpty, (object) 0, LogicalOperators.NONE, 0)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    if (dataTable.Rows.Count <= 0)
      return (List<LinkedObject>) null;
    return new List<LinkedObject>()
    {
      new LinkedObject(Convert.ToInt64(dataTable.Rows[0][0]))
    };
  }

  bool ILinkedObjectsHandler.IsTypesChanged(IUserSession session) => this.IsTypesChanged(session);

  void ILinkedObjectsHandler.UpdateHandleAndOutputTypes(IUserSession session, bool force)
  {
    this.UpdateHandleAndOutputTypes(session, force);
  }
}
