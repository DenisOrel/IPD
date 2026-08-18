// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ObjectExistenceStatusComparer
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class ObjectExistenceStatusComparer : 
  IComparer<ObjectExistenceStatus>,
  IEqualityComparer<ObjectExistenceStatus>
{
  public int Compare(ObjectExistenceStatus x, ObjectExistenceStatus y) => x.CompareTo((object) y);

  public bool Equals(ObjectExistenceStatus x, ObjectExistenceStatus y) => x == y;

  public int GetHashCode(ObjectExistenceStatus obj) => obj.GetHashCode();
}
