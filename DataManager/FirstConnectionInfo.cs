// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.FirstConnectionInfo
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

#nullable disable
namespace Intermech.Server.Data;

internal sealed class FirstConnectionInfo
{
  public FirstConnectionInfo(string dataProviderName, string connectionName)
  {
    this.DataProviderName = dataProviderName;
    this.ConnectionName = connectionName;
  }

  public string DataProviderName { get; }

  public string ConnectionName { get; }
}
