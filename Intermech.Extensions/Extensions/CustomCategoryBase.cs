// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomCategoryBase
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
public abstract class CustomCategoryBase : CategoryAttribute
{
  [CanBeNull]
  protected readonly ResourceManager ResourceManager;

  protected internal CustomCategoryBase([CanBeNull] ResourceManager resourceManager, [NotNull] string category)
    : base(category)
  {
    this.ResourceManager = resourceManager;
  }

  [NotNull]
  protected override string GetLocalizedString([NotNull, NotWhitespace] string value)
  {
    return this.ResourceManager == null ? Localization.GetAttributeString(value) : this.ResourceManager.GetString(value) ?? value;
  }
}
