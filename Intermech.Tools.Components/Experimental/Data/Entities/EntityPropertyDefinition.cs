// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityPropertyDefinition
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Experimental.Data.Entities;

public class EntityPropertyDefinition
{
  public EntityPropertyDefinition(string propertyName, Type propertyType)
    : this(propertyName, propertyType, propertyType)
  {
  }

  public EntityPropertyDefinition(string propertyName, Type containerType, Type containerItemType)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (containerType == (Type) null)
      throw new ArgumentNullException(nameof (containerType));
    if (containerItemType == (Type) null)
      throw new ArgumentNullException(nameof (containerItemType));
    this.Name = propertyName;
    this.ContainerType = containerType;
    this.ContainerItemType = containerItemType;
    this.IsContainer = this.ContainerType != this.ContainerItemType;
  }

  public static EntityPropertyDefinition FromReflectionInfo(PropertyInfo propertyInfo)
  {
    Type type1 = !(propertyInfo == (PropertyInfo) null) ? propertyInfo.PropertyType : throw new ArgumentNullException(nameof (propertyInfo));
    if (type1.IsArray)
      return new EntityPropertyDefinition(propertyInfo.Name, type1, type1.GetElementType());
    if (type1.IsGenericType)
    {
      foreach (Type type2 in type1.GetInterfaces())
      {
        Type genericTypeDefinition = type2.GetGenericTypeDefinition();
        if (genericTypeDefinition == typeof (IList<>) || genericTypeDefinition == typeof (ICollection<>) || genericTypeDefinition == typeof (IEnumerable<>))
          return new EntityPropertyDefinition(propertyInfo.Name, type1, type1.GetGenericArguments()[0]);
      }
    }
    return new EntityPropertyDefinition(propertyInfo.Name, type1);
  }

  public string Name { get; private set; }

  public Type PropertyType => this.ContainerType;

  public Type ContainerType { get; private set; }

  public Type ContainerItemType { get; private set; }

  public bool IsContainer { get; private set; }
}
