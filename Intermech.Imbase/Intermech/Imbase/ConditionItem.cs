// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ConditionItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

[Serializable]
public class ConditionItem
{
  public int AttId;
  public Condition Condition;
  public string Data;
  public string Data2;

  public object StringData
  {
    get
    {
      return this.Condition == Condition.Between || this.Condition == Condition.NotBetween ? (object) $"{this.Data};{this.Data2}" : (object) this.Data;
    }
  }

  internal static ConditionItem Find(List<ConditionItem> conditions, int attributeID)
  {
    return conditions == null || conditions.Count == 0 ? (ConditionItem) null : conditions.Find((Predicate<ConditionItem>) (x => x.AttId == attributeID));
  }
}
