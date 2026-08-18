// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomDescriptionBase
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Resources;

#nullable disable
namespace Intermech.Extensions;

[CLSCompliant(false)]
public abstract class CustomDescriptionBase : DescriptionAttribute
{
  protected internal CustomDescriptionBase([NotNull] ResourceManager resourceManager, [NotNull, NotWhitespace] string description)
  {
    this.DescriptionValue = resourceManager.GetString(description);
  }
}
