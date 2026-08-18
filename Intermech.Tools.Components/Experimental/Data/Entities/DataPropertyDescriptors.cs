// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.DataPropertyDescriptors
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Реализует коллекцию дескрипторов свойств доменного объекта или объекта-связки.
/// Реализация является immutable и thread safe.
/// </summary>
public sealed class DataPropertyDescriptors(
  Type entityType,
  IDictionary<string, DataPropertyDescriptor> descriptors) : 
  ReadOnlyPropertyMap<DataPropertyDescriptor>(entityType, descriptors)
{
}
