// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationApplicabilityMappings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBRelationApplicabilityMappings
{
  private int count;
  private IDictionary<Tuple<int, int>, DBRelationApplicabilityMapping> mappings;

  public DBRelationApplicabilityMappings(
    IDictionary<Tuple<int, int>, DBRelationApplicabilityMapping> mappings)
  {
    this.count = mappings != null ? mappings.Count : throw new ArgumentNullException(nameof (mappings));
    this.mappings = mappings.IsReadOnly ? mappings : (IDictionary<Tuple<int, int>, DBRelationApplicabilityMapping>) new ReadOnlyDictionary<Tuple<int, int>, DBRelationApplicabilityMapping>(mappings);
  }

  public int Count
  {
    [DebuggerStepThrough] get => this.count;
  }

  public ICollection<DBRelationApplicabilityMapping> AsCollection
  {
    [DebuggerStepThrough] get => this.mappings.Values;
  }

  public DBRelationApplicabilityMapping TryGet(int parentDBObjectTypeId, int childDBObjectTypeId)
  {
    DBRelationApplicabilityMapping applicabilityMapping;
    this.mappings.TryGetValue(Tuple.Create<int, int>(parentDBObjectTypeId, childDBObjectTypeId), out applicabilityMapping);
    return applicabilityMapping;
  }
}
