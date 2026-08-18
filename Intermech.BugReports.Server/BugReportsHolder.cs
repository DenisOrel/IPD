// Decompiled with JetBrains decompiler
// Type: Intermech.BugReports.BugReportsHolder
// Assembly: Intermech.BugReports.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D5496885-D5AE-45E1-887A-E42A46AB4DD0
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.BugReports.Server.dll

using System;

#nullable disable
namespace Intermech.BugReports;

internal static class BugReportsHolder
{
  public static class OT
  {
    public static readonly Guid BugObjectType = new Guid("cad00700-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugStatObjectType = new Guid("cadd9641-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugStatSetupObjectType = new Guid("cadd9640-306c-11d8-b4e9-00304f19f545");
  }

  public static class AT
  {
    public static readonly Guid BugOwner = new Guid("cad00701-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid FixData = new Guid("cad00702-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid CheckData = new Guid("cad00703-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugClassif = new Guid("cad00704-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid CheckComm = new Guid("cad00705-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid ShortInfo = new Guid("cad00706-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid MustFixUser = new Guid("cad00707-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid FixUser = new Guid("cad00708-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid FindUser = new Guid("cad00709-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid CheckUser = new Guid("cad0070a-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid Module = new Guid("cad0070b-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugInfo = new Guid("cad0070c-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugPriority = new Guid("cad0070d-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid CheckResult = new Guid("cad0070e-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugStatus = new Guid("cad0070f-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid BugFile = new Guid("cad0071f-306c-11d8-b4e9-00304f19f545");
    public static readonly Guid Enterprise = new Guid("77b86610-055e-4a9f-ad1b-9b735ed6323b");
    public static readonly Guid HelpdeskID = new Guid("32793b80-58bf-4cb7-916b-ad831240dd75");
  }
}
