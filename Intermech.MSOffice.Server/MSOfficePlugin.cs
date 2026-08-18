// Decompiled with JetBrains decompiler
// Type: Intermech.MSOffice.MSOfficePlugin
// Assembly: Intermech.MSOffice.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D19FBC55-F588-4D57-844C-DE1B05B4B055
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MSOffice.Server.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Plugins;

#nullable disable
namespace Intermech.MSOffice;

internal sealed class MSOfficePlugin : ServerModularPackage
{
  public MSOfficePlugin()
    : base("Серверная часть интегратора с MS Office")
  {
  }

  protected override void CreateSubModules(InitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    subModules.Add((InitializerModule) new MSOfficeIndexersModule());
  }
}
