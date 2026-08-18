// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Projects.DBProjectConsts
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Projects;

public class DBProjectConsts
{
  private static long ManagersGroupID = -1;
  private static long MembersGroupID = -1;

  public static long GetManagersGroupID(UserSession session)
  {
    if (DBProjectConsts.ManagersGroupID < 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cadd9b91-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo.Empty)
        DBProjectConsts.ManagersGroupID = objectInfo.ObjectID;
    }
    return DBProjectConsts.ManagersGroupID;
  }

  public static long GetMembersGroupID(UserSession session)
  {
    if (DBProjectConsts.MembersGroupID < 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cadd9b93-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo.Empty)
        DBProjectConsts.MembersGroupID = objectInfo.ObjectID;
    }
    return DBProjectConsts.MembersGroupID;
  }
}
