// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.SelectorFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

internal class SelectorFilter : ISelectorFilter
{
  public HashSet<int> IdsList = new HashSet<int>();

  public bool IsInFilter(int category, object id) => this.IdsList.Contains(Convert.ToInt32(id));
}
