// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.IEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Общий интерфейс для всех дескрипторов доменныъ объектов.
/// </summary>
public interface IEntityTypeDescriptor
{
  Type EntityType { get; }
}
