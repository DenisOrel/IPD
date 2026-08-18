// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.BeforeObjectsCollectionSelectEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Interfaces.Server;

public sealed class BeforeObjectsCollectionSelectEventArgs : BeforeRecordsSelectEventArgs
{
  public int ObjectType { get; private set; }

  public BeforeObjectsCollectionSelectEventArgs(
    int objectType,
    DBRecordSetParams parameters,
    IUserSession session)
    : base(parameters, session)
  {
    this.ObjectType = objectType;
  }
}
