// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.SupervisorHelper
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Office.Server;

internal static class SupervisorHelper
{
  public static bool UserIsSupervisor([NotNull] IUserSession session)
  {
    IOfficeGeneralSettingsService customService = (IOfficeGeneralSettingsService) session.GetCustomService(typeof (IOfficeGeneralSettingsService));
    long[] supervisorObjVerIds = customService.SupervisorObjVerIDs;
    return ((IEnumerable<long>) customService.SupervisorObjVerIDs).Intersect<long>((IEnumerable<long>) session.GetUserGroupsAndRoleID()).Count<long>() > 0;
  }
}
