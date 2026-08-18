// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.WrongPluginVersionException
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class WrongPluginVersionException : InvalidOperationException, ISerializable
{
  [NotNull]
  [NotWhitespace]
  public readonly string ModuleName;
  [PositiveNumber]
  public readonly int NeedVersionNumber;
  [CanBeNull]
  [ZeroOrPositiveNumber]
  public readonly int? NeedRevisionNumber;

  public WrongPluginVersionException(
    [NotNull, NotWhitespace] string moduleName,
    [PositiveNumber] int needVersionNumber,
    [CanBeNull, ZeroOrPositiveNumber] int? needRevisionNumber,
    [NotNull, NotWhitespace] string message)
    : base(message)
  {
    int num = needRevisionNumber.HasValue ? 1 : 0;
    this.ModuleName = moduleName;
    this.NeedVersionNumber = needVersionNumber;
    this.NeedRevisionNumber = needRevisionNumber;
  }

  public WrongPluginVersionException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ModuleName = info.GetNotWhitespaceString(nameof (ModuleName));
    this.NeedVersionNumber = info.GetInt32(nameof (NeedVersionNumber));
    this.NeedRevisionNumber = info.GetValue<int?>(nameof (NeedRevisionNumber));
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ModuleName", (object) this.ModuleName);
    info.AddValue("NeedVersionNumber", this.NeedVersionNumber);
    info.AddValue("NeedRevisionNumber", (object) this.NeedRevisionNumber);
  }
}
