// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.ExtendedEntityTypeInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class ExtendedEntityTypeInfo
{
  private Type entityType;

  public ExtendedEntityTypeInfo(Type entityType) => this.entityType = entityType;

  public Type EntityType
  {
    [DebuggerStepThrough] get => this.entityType;
  }

  public T GetAnnotationAttribute<T>(bool inherit) where T : Attribute
  {
    return this.EntityType.GetCustomAttribute<T>(inherit);
  }
}
