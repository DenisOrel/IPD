// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DuplicateDBObjectTypeMappingCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DuplicateDBObjectTypeMappingCheck
{
  private List<DuplicateDBObjectTypeMappingCheck.Mapping> mappings;

  public DuplicateDBObjectTypeMappingCheck()
  {
    this.mappings = new List<DuplicateDBObjectTypeMappingCheck.Mapping>();
  }

  public void Clear() => this.mappings.Clear();

  public void AddDBObjectType(Type entityType, Guid dbObjectTypeGuid)
  {
    DuplicateDBObjectTypeMappingCheck.Mapping mapping = !(entityType == (Type) null) ? new DuplicateDBObjectTypeMappingCheck.Mapping(entityType, dbObjectTypeGuid) : throw new ArgumentNullException(nameof (entityType));
    if (this.mappings.Contains(mapping))
      throw new InvalidOperationException();
    this.mappings.Add(mapping);
  }

  public void Perform()
  {
    int num = this.mappings.Count - 1;
    for (int index1 = 0; index1 <= num; ++index1)
    {
      DuplicateDBObjectTypeMappingCheck.Mapping mapping1 = this.mappings[index1];
      for (int index2 = index1 + 1; index2 <= num; ++index2)
      {
        DuplicateDBObjectTypeMappingCheck.Mapping mapping2 = this.mappings[index2];
        if (mapping2.DBObjectTypeGuid == mapping1.DBObjectTypeGuid)
          throw new ModelConfigurationException(6, $"Типы доменных объектов '{mapping1.EntityType}' и '{mapping2.EntityType}' отображаются в один и тот же тип объектов IPS. Проверьте значения атрибута '{typeof (DBObjectTypeAttribute)}' у указанных типов.");
      }
    }
  }

  private struct Mapping : IEquatable<DuplicateDBObjectTypeMappingCheck.Mapping>
  {
    public Mapping(Type entityType, Guid dbObjectTypeGuid)
      : this()
    {
      this.EntityType = entityType;
      this.DBObjectTypeGuid = dbObjectTypeGuid;
    }

    public bool Equals(DuplicateDBObjectTypeMappingCheck.Mapping other)
    {
      return this.EntityType == other.EntityType && this.DBObjectTypeGuid == other.DBObjectTypeGuid;
    }

    public override bool Equals(object obj)
    {
      return !(obj is DuplicateDBObjectTypeMappingCheck.Mapping other) ? base.Equals(obj) : this.Equals(other);
    }

    public override int GetHashCode()
    {
      return this.EntityType.GetHashCode() ^ this.DBObjectTypeGuid.GetHashCode();
    }

    public Type EntityType { get; private set; }

    public Guid DBObjectTypeGuid { get; private set; }
  }
}
