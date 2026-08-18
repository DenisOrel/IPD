// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectNavigationPropertyMappings
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
/// Этот класс реализует коллекцию отображений навигационных свойств доменного объекта в связи IPS с другими объектами IPS.
/// </summary>
/// <remarks>Объекты этого являются immutable и thread safe.</remarks>
internal sealed class DBObjectNavigationPropertyMappings(
  Type entityType,
  IDictionary<string, DBObjectNavigationPropertyMapping> mappings) : 
  ReadOnlyPropertyMap<DBObjectNavigationPropertyMapping>(entityType, mappings)
{
}
