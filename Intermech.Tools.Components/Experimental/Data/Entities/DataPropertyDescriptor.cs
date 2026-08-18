// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.DataPropertyDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Reflection;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Реализует дескриптор свойства доменного объекта или объекта-связки. Дескриптор содержит все необходимые сведения для
/// работы со свойством доменного объекта. Реализация является immutable и thread safe.
/// </summary>
public class DataPropertyDescriptor
{
  public DataPropertyDescriptor(EntityPropertyDefinition definition, PropertyInfo reflectionInfo)
  {
    this.Definition = definition;
    this.ReflectionInfo = reflectionInfo;
  }

  public EntityPropertyDefinition Definition { get; private set; }

  protected PropertyInfo ReflectionInfo { get; private set; }

  public EntityPropertyData GetValue(object entity)
  {
    return entity != null ? new EntityPropertyData(EntityMemberPresenceStatus.Present, this.ReflectionInfo.GetValue(entity)) : throw new ArgumentNullException(nameof (entity));
  }

  public void SetValue(object entity, object propertyValue)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.ReflectionInfo.SetValue(entity, propertyValue);
  }
}
