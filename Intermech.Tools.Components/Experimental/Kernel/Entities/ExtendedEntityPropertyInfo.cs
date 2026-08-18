// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.ExtendedEntityPropertyInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class ExtendedEntityPropertyInfo
{
  private string name;
  private PropertyInfo basicInfo;
  private EntityPropertyDefinition definition;

  public ExtendedEntityPropertyInfo(PropertyInfo propertyInfo)
  {
    this.name = propertyInfo.Name;
    this.basicInfo = propertyInfo;
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }

  public PropertyInfo BasicInfo
  {
    [DebuggerStepThrough] get => this.basicInfo;
  }

  public EntityPropertyDefinition Definition
  {
    get
    {
      if (this.definition == null)
        this.definition = EntityPropertyDefinition.FromReflectionInfo(this.basicInfo);
      return this.definition;
    }
  }

  public T GetAnnotationAttribute<T>(bool inherit) where T : Attribute
  {
    return this.BasicInfo.GetCustomAttribute<T>(inherit);
  }
}
