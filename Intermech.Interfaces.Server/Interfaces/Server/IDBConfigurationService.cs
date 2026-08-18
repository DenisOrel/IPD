// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBConfigurationService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBConfigurationService
{
  IDBConfigurations GetDBConfigurations(IUserSession uSession);

  IDBConfigurations GetDBConfigurations(
    IUserSession uSession,
    IDBConfigurations parentConfigurations);

  object GetValue(string moduleName, string sectionName, string paramName, object defaultValue);
}
