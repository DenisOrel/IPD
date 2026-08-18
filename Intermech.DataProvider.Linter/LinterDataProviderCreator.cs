// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.LinterDataProviderCreator
// Assembly: Intermech.DataProvider.Linter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5976CE7B-8000-4C30-A078-1BBCAD6EB006
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.DataProvider.Linter.dll

using Intermech.Interfaces.Server;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

public sealed class LinterDataProviderCreator : IDbDataProviderCreator
{
  public string Name
  {
    [DebuggerStepThrough] get => "Linter";
  }

  public IDbDataProvider CreateDataProvider() => (IDbDataProvider) new LinterDataProvider();
}
