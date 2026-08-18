// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyMappings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Этот класс реализует коллекцию отображений свойств доменного объекта (объекта-связки) в атрибуты объекта IPS (связи IPS).
/// </summary>
/// <remarks>Объекты этого типа являются immutable и thread safe.</remarks>
internal sealed class DataPropertyMappings : ReadOnlyPropertyMap<DataPropertyMapping>
{
  private IDictionary<int, DataPropertyMapping> itemsByDBAttributeId;

  public DataPropertyMappings(Type entityType, IDictionary<string, DataPropertyMapping> mappings)
    : base(entityType, mappings)
  {
    this.itemsByDBAttributeId = (IDictionary<int, DataPropertyMapping>) new Dictionary<int, DataPropertyMapping>(this.Count);
    foreach (KeyValuePair<string, DataPropertyMapping> keyValuePair in (IEnumerable<KeyValuePair<string, DataPropertyMapping>>) this.AsDictionary)
    {
      DataPropertyMapping dataPropertyMapping = keyValuePair.Value;
      this.itemsByDBAttributeId.Add(dataPropertyMapping.Id, dataPropertyMapping);
    }
  }

  public DataPropertyMapping GetByDBAttributeId(int dbAttributeId, bool throwIfNotFound)
  {
    if (dbAttributeId == -1)
      throw new ArgumentException("Не задан идентификатор атрибута IPS.", nameof (dbAttributeId));
    DataPropertyMapping byDbAttributeId;
    if (this.itemsByDBAttributeId.TryGetValue(dbAttributeId, out byDbAttributeId))
      return byDbAttributeId;
    if (!throwIfNotFound)
      return (DataPropertyMapping) null;
    throw new InvalidOperationException($"У доменного объекта '{this.EntityType}' отсутствует свойство, отображаемое в атрибут IPS с идентификатором {dbAttributeId}.");
  }
}
