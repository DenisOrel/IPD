// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.ObjTypeSelectorFilter
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.PropertyEditors;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents;

internal class ObjTypeSelectorFilter : ISelectorFilter
{
  public ObjTypeSelectorFilter([NotNull] Func<int, bool> onFilterPredicate)
  {
    this.OnFilter = onFilterPredicate;
  }

  public Func<int, bool> OnFilter { get; }

  public bool IsInFilter(int category, object id) => category == 4 && this.OnFilter((int) id);
}
