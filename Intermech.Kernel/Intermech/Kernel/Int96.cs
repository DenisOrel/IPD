// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Int96
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Diagnostics;


namespace Intermech.Kernel;

internal class Int96
{
  private readonly long _id;
  private readonly long _objectId;

  public Int96(long id, long objectId)
  {
    this._id = id;
    this._objectId = objectId;
  }

  public long ID
  {
    [DebuggerStepThrough] get => this._id;
  }

  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectId;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is Int96 int96))
      return base.Equals(obj);
    return int96._id == this._id && int96._objectId == this._objectId;
  }

  public override int GetHashCode()
  {
    return (23 * 31 /*0x1F*/ + this._id.GetHashCode()) * 31 /*0x1F*/ + this._objectId.GetHashCode();
  }
}
