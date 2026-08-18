// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ObjectsFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

public class ObjectsFilter : IDataFormatFilter, ICloneable
{
  public bool Join(IDataFormatFilter filter) => filter is ObjectsFilter;

  public bool Disjoin(IDataFormatFilter filter) => filter is ObjectsFilter;

  public bool CanPassData(object data) => data is IDBObjectID || data is IDBTypedObjectID;

  public object Clone() => (object) new ObjectsFilter();
}
