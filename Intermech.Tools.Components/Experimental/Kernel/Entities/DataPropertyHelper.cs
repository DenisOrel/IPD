// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DataPropertyHelper
{
  private static DataPropertyHelper defaultInstance = new DataPropertyHelper();

  public bool IsAllowedDataPropertyType(Type propertyType)
  {
    return propertyType == typeof (string) || propertyType == typeof (MeasuredValue) || propertyType == typeof (DBFileValue) || propertyType == typeof (int) || propertyType == typeof (int?) || propertyType == typeof (long) || propertyType == typeof (long?) || propertyType == typeof (double) || propertyType == typeof (double?) || propertyType == typeof (bool) || propertyType == typeof (bool?) || propertyType == typeof (DateTime) || propertyType == typeof (DateTime?) || propertyType == typeof (Guid) || propertyType == typeof (Guid?);
  }

  public object GetDefaultValueForValueType(Type valueType)
  {
    if (valueType == typeof (int))
      return (object) 0;
    if (valueType == typeof (long))
      return (object) 0L;
    if (valueType == typeof (double))
      return (object) 0.0;
    if (valueType == typeof (bool))
      return (object) false;
    if (valueType == typeof (DateTime))
      return (object) DateTime.MinValue;
    if (valueType == typeof (Guid))
      return (object) Guid.Empty;
    throw new NotSupportedException($"Тип '{valueType}' не поддерживается.");
  }

  public static DataPropertyHelper DefaultInstance
  {
    [DebuggerStepThrough] get => DataPropertyHelper.defaultInstance;
    [DebuggerStepThrough] set => DataPropertyHelper.defaultInstance = value;
  }
}
