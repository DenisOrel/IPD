// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.SyncronizeDirectoryTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Localization;
using System;


namespace Intermech.Ldap;

internal class SyncronizeDirectoryTask : DBCustomManualScheduledService
{
  public override Guid GUID => new Guid("cadd93f2-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName
  {
    get => LocalizationHolder.rm.GetString(nameof (SyncronizeDirectoryTask));
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    bool flag = true;
    if (this.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService)
      flag = customService.SynchronizeDirectoryProcess(this.Session.SessionGUID) == 0;
    return flag;
  }
}
