// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.IBasicEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Интерфейс простейшего дескриптора для доменных объектов. Он позволяет получать имена и значения свойств доменных объектов.
/// </summary>
public interface IBasicEntityTypeDescriptor : IEntityTypeDescriptor
{
  ICollection<EntityPropertyDefinition> GetDataPropertyDefinitions();

  EntityPropertyDefinition GetDataPropertyDefinition(string propertyName, bool throwIfNotFound = true);

  EntityPropertyData GetDataProperty(object entity, string propertyName);

  ICollection<EntityPropertyDefinition> GetNavigationPropertyDefinitions();

  EntityPropertyDefinition GetNavigationPropertyDefinition(
    string propertyName,
    bool throwIfNotFound = true);

  EntityPropertyData GetNavigationProperty(object entity, string propertyName);
}
