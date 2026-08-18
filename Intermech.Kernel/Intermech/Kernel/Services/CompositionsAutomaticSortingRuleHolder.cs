// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CompositionsAutomaticSortingRuleHolder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services;

internal class CompositionsAutomaticSortingRuleHolder
{
  internal readonly CompositionsAutosortRule Rule;
  internal DateTime Placed;

  public CompositionsAutomaticSortingRuleHolder()
  {
  }

  public CompositionsAutomaticSortingRuleHolder(CompositionsAutosortRule rule)
    : this(rule, DateTime.UtcNow)
  {
  }

  public CompositionsAutomaticSortingRuleHolder(CompositionsAutosortRule rule, DateTime placed)
  {
    this.Rule = rule;
    this.Placed = placed;
  }
}
