// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Cache.UsersGroupsListCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Kernel.Cache;

public class UsersGroupsListCache : IUsersGroupsListCache
{
  private ConcurrentDictionary<long, List<long>> _GroupsDict = new ConcurrentDictionary<long, List<long>>();

  public long[] GetGroupsListRecursive(long userID)
  {
    List<long> longList;
    return this._GroupsDict.TryGetValue(userID, out longList) ? longList.ToArray() : new long[0];
  }

  public void LoadCache(IUserSession session)
  {
    List<long> longList = new List<long>((IEnumerable<long>) (session as UserSession).DBSecurity.GetGroupsList());
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("CVS.GetGroupsListRecursive");
    try
    {
      ICompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      DataTable dataTable = service.LoadComplexCompositions((object) sessionTemporaryClone, (IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        new ObjInfoItem(session.UserID, sessionTemporaryClone.IdentHelper.UsersTypeID)
      }, (IEnumerable<int>) new int[1]
      {
        sessionTemporaryClone.IdentHelper.SimpleRelationTypeID
      }, (IEnumerable<int>) new int[1]
      {
        sessionTemporaryClone.IdentHelper.GroupsTypeID
      }, (IEnumerable<ColumnDescriptor>) columns, false, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, -1);
      if (dataTable != null)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
          if (int64Value != 0L && !longList.Contains(int64Value))
            longList.Add(int64Value);
        }
      }
    }
    finally
    {
      sessionTemporaryClone.Logout("CVS.GetGroupsListRecursive");
    }
    this._GroupsDict[session.UserID] = longList;
  }
}
