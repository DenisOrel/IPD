// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NavigationPropertyDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Реализует дескриптор навигационного свойства доменного объекта или объекта-связки. Дескриптор содержит все необходимые сведения для
/// работы со свойством доменного объекта. Реализация является immutable и thread safe.
/// </summary>
public class NavigationPropertyDescriptor
{
  public NavigationPropertyDescriptor(
    EntityPropertyDefinition definition,
    PropertyInfo reflectionInfo)
  {
    this.Definition = definition;
    this.ReflectionInfo = reflectionInfo;
    this.ValueWriter = this.CreateContainerWriter(this.Definition.IsContainer, this.Definition.ContainerType);
  }

  public EntityPropertyDefinition Definition { get; private set; }

  protected PropertyInfo ReflectionInfo { get; private set; }

  public INavigationPropertyWriter ValueWriter { get; private set; }

  public EntityPropertyData GetValue(object entity)
  {
    object propertyValue = entity != null ? this.ReflectionInfo.GetValue(entity) : throw new ArgumentNullException(nameof (entity));
    return propertyValue != null ? new EntityPropertyData(EntityMemberPresenceStatus.Present, propertyValue) : new EntityPropertyData(EntityMemberPresenceStatus.NotPresent, (object) null);
  }

  public void SetValue(object entity, object propertyValue)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.ReflectionInfo.SetValue(entity, propertyValue);
  }

  private INavigationPropertyWriter CreateContainerWriter(bool isContainer, Type containerType)
  {
    if (isContainer)
    {
      if (containerType.IsGenericType)
      {
        Type[] genericArguments = containerType.GetGenericArguments();
        if (genericArguments.Length == 1)
        {
          if (typeof (List<>).MakeGenericType(genericArguments[0]).IsAssignableFrom(containerType))
            return (INavigationPropertyWriter) Activator.CreateInstance(typeof (ListPropertyWriter<>).MakeGenericType(genericArguments[0]), (object) this);
          if (typeof (ICollection<>).MakeGenericType(genericArguments[0]).IsAssignableFrom(containerType))
            return (INavigationPropertyWriter) Activator.CreateInstance(typeof (CollectionPropertyWriter<>).MakeGenericType(genericArguments[0]), (object) this);
        }
      }
      throw new NotSupportedException();
    }
    return (INavigationPropertyWriter) new ReferencePropertyWriter(this);
  }
}
