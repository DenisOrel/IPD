// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ImportUsersProfileHolder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel;

public class ImportUsersProfileHolder
{
  public static string SystemSessionName = "Intermech.Kernel";
  public static Guid ObjectOwnerAttributeGuid = new Guid("cad0002f-306c-11d8-b4e9-00304f19f545");

  public static void RegisterService()
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    service.AddService(typeof (IImportUsersProfile), (object) new ImportUsersProfileServer());
  }
}
