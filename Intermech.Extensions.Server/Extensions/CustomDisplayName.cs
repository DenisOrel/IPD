// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomDisplayName
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

internal class CustomDisplayName : CustomDisplayNameBase
{
  protected internal CustomDisplayName([NotNull, NotWhitespace] string displayName)
    : base(Localization.AttributeResources, displayName)
  {
  }
}
