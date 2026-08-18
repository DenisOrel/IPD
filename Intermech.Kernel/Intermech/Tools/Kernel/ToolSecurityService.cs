// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.ToolSecurityService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;


namespace Intermech.Tools.Kernel;

internal sealed class ToolSecurityService : LongLifeObject, IToolSecurity
{
  private IUserSession systemSession;

  public ToolSecurityService(IUserSession systemSession)
  {
    this.systemSession = (systemSession as IServerSession).Clone(true, nameof (ToolSecurityService));
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public ToolSecurityGroup GetUserGroup()
  {
    RBSServer.AuthenticateCaller();
    IPSPrincipal currentPrincipal = IPSPrincipal.CurrentPrincipal;
    long securityData = this.FindSecurityData(currentPrincipal.Identity.UserId);
    if (securityData != 0L)
      return DBAttributeReader.GetToolSecurityGroup(this.systemSession.GetObject(securityData, true));
    return (currentPrincipal.IsInRole(IPSBuiltInRole.Administrator) ? 1 : (currentPrincipal.IsInRole(IPSBuiltInRole.Server) ? 1 : 0)) == 0 ? ToolSecurityGroup.NormalUser : ToolSecurityGroup.Administrator;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public ToolSecurityRights GetUserRights()
  {
    RBSServer.AuthenticateCaller();
    ToolSecurityGroup userGroup = this.GetUserGroup();
    switch (userGroup)
    {
      case ToolSecurityGroup.Administrator:
        return ToolSecurityRights.All;
      case ToolSecurityGroup.NormalUser:
        return ToolSecurityRights.EditPersonalSettings;
      case ToolSecurityGroup.RestrictedUser:
        return ToolSecurityRights.None;
      default:
        throw new NotSupportedException($"Value '{userGroup}' is not supported.");
    }
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<UserSecurityData> GetSecurityData()
  {
    RBSServer.AuthenticateCaller();
    DataTable dataTable = this.systemSession.GetObjectCollection(Consts.UserSecurityObjectType).Select(new DBRecordSetParams()
    {
      RecordCount = -1,
      Columns = new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) Consts.ToolSecurityGroupAttr,
        (object) Consts.UserRefAttr
      },
      Contents = new ColumnContents[3]
      {
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.ID
      }
    });
    List<UserSecurityData> securityData = new List<UserSecurityData>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      ToolSecurityGroup toolSecurityGroup = DBAttributeReader.GetToolSecurityGroup(row, 1, (object) Convert.ToInt64(row[0]));
      long int64 = Convert.ToInt64(row[2]);
      securityData.Add(new UserSecurityData(int64, toolSecurityGroup));
    }
    return securityData;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void SaveSecurityData(UserSecurityData securityData)
  {
    if (securityData == null)
      throw new ArgumentNullException();
    RBSServer.AuthenticateCaller();
    RBSServer.AuthorizeAsAdmin();
    long securityData1 = this.FindSecurityData(securityData.UserId);
    IDBObject dbObj;
    if (securityData1 != 0L)
    {
      dbObj = this.systemSession.GetObject(securityData1, true);
    }
    else
    {
      dbObj = this.systemSession.GetObjectCollection(Consts.UserSecurityObjectType).Create();
      DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.UserRefAttr, (object) securityData.UserId);
    }
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.ToolSecurityGroupAttr, (object) (int) securityData.SecurityGroup);
    if (!dbObj.IsCreationMode)
      return;
    dbObj.CommitCreation(true);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void RemoveSecurityData(long userId)
  {
    if (userId == 0L)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    RBSServer.AuthorizeAsAdmin();
    long securityData = this.FindSecurityData(userId);
    if (securityData == 0L)
      return;
    this.systemSession.GetObject(securityData, true).Delete(0L);
  }

  private long FindSecurityData(long userId)
  {
    ConditionStructure conditionStructure = new ConditionStructure(Consts.UserRefAttr, RelationalOperators.Equal, (object) userId, LogicalOperators.NOT, 0);
    DataTable dataTable = this.systemSession.GetObjectCollection(Consts.UserSecurityObjectType).Select(new DBRecordSetParams()
    {
      RecordCount = 1,
      Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      },
      Conditions = new ConditionStructure[1]
      {
        conditionStructure
      }
    });
    return dataTable.Rows.Count != 1 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  public string EncodeTarget(ITarget target)
  {
    switch (target)
    {
      case AllUsersTarget _:
        return "ALL_USERS";
      case UserTarget _:
        return $"USER:{((UserTarget) target).UserGuid}";
      default:
        throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Kernel_1132"), (object) target.GetType()));
    }
  }

  public ITarget DecodeTarget(string targetCode)
  {
    targetCode = targetCode.ToUpper();
    if (targetCode == "ALL_USERS")
      return (ITarget) AllUsersTarget.Value;
    Guid guid = targetCode.StartsWith("USER:") ? new Guid(targetCode.Substring(5, targetCode.Length - 5).Trim()) : throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Kernel_1133"), (object) targetCode));
    return (ITarget) new UserTarget(this.systemSession.GetObject(guid, true).ObjectID, guid);
  }

  public bool IsSubset(ITarget superset, ITarget subset)
  {
    switch (superset)
    {
      case AllUsersTarget _:
        return subset is AllUsersTarget || subset is UserTarget;
      case UserTarget _:
        if (subset is UserTarget)
          return ((UserTarget) superset).UserGuid == ((UserTarget) subset).UserGuid;
        break;
    }
    return false;
  }

  public List<ITarget> GetParentTargets(ITarget target)
  {
    List<ITarget> parentTargets = new List<ITarget>(1);
    if (target is UserTarget)
      parentTargets.Add((ITarget) AllUsersTarget.Value);
    return parentTargets;
  }

  public void CheckWriteAccess(ITarget target)
  {
    if (!this.GrantWriteAccess(target))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1134"), (object) this.EncodeTarget(target)));
  }

  public bool GrantWriteAccess(ITarget target)
  {
    ToolSecurityGroup userGroup = this.GetUserGroup();
    switch (userGroup)
    {
      case ToolSecurityGroup.Administrator:
        return true;
      case ToolSecurityGroup.NormalUser:
        if (!(target is UserTarget userTarget))
          return false;
        IPSPrincipal currentPrincipal = IPSPrincipal.CurrentPrincipal;
        return userTarget.UserId == currentPrincipal.Identity.UserId;
      case ToolSecurityGroup.RestrictedUser:
        return false;
      default:
        throw new NotSupportedException($"Value '{userGroup}' is not supported.");
    }
  }
}
