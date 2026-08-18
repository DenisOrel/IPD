// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.SqlDataProviderCreator
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

public sealed class SqlDataProviderCreator : IDbDataProviderCreator
{
  public string Name
  {
    [DebuggerStepThrough] get => "Sql";
  }

  public IDbDataProvider CreateDataProvider() => (IDbDataProvider) new SqlDataProvider();
}
