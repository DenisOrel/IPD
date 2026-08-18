// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MetadataUpdateKeeper
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Kernel;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces;

internal class MetadataUpdateKeeper : IMetadataUpdateKeeper, IDisposable
{
  [NotNull]
  private readonly UserSession _session;

  [NotNull]
  public string ModuleName { get; }

  public int UpdateVersion { get; }

  public int UpdateRevision { get; }

  public bool SaveNewDbVersion { get; }

  [CanBeNull]
  public ITransactionKeeper Transaction { get; private set; }

  internal MetadataUpdateKeeper(
    [NotNull] UserSession session,
    [NotNull, NotWhitespace] string moduleName,
    bool inTransaction,
    [ZeroOrPositiveNumber] int updateVersion,
    [ZeroOrPositiveNumber] int updateRevision,
    bool saveNewDbVersion = true)
  {
    saveNewDbVersion = true;
    this._session = session;
    this.ModuleName = moduleName;
    this.UpdateVersion = updateVersion;
    this.UpdateRevision = updateRevision;
    this.SaveNewDbVersion = saveNewDbVersion;
    if (!inTransaction)
      return;
    this.Transaction = session.Transaction($"{nameof (MetadataUpdateKeeper)}.ctor(moduleName=\"{moduleName}\", {updateVersion}, {updateRevision}, {saveNewDbVersion})", callerFilePath: "D:\\JenkinsSlave\\Data\\workspace\\IPS7\\imbuilder\\Var\\src\\Kernel\\Intermech.Extensions.Server\\Common\\MetadataUpdateKeeper.cs");
  }

  private void UpdateDbVersion()
  {
    this._session.SetDBVersion(this.ModuleName, this.UpdateVersion, this.UpdateRevision, string.Empty);
  }

  public void Dispose()
  {
    if (this.SaveNewDbVersion && Marshal.GetExceptionPointers() == IntPtr.Zero && Marshal.GetExceptionCode() == 0)
      this.UpdateDbVersion();
    if (this.Transaction == null)
      return;
    this.Transaction.Dispose();
    this.Transaction = (ITransactionKeeper) null;
  }
}
