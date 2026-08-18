// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomDisplayName
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

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
