
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AttributeExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal static class AttributeExtensions
{
  public static TValue GetAttributeValue<TAttribute, TValue>(
    this Type type,
    Func<TAttribute, TValue> valueSelector,
    bool checkBaseType = false)
    where TAttribute : Attribute
  {
    if (type == (Type) null)
      return default (TValue);
    if (((IEnumerable<object>) type.GetCustomAttributes(typeof (TAttribute), true)).FirstOrDefault<object>() is TAttribute attribute)
      return valueSelector(attribute);
    return !checkBaseType ? default (TValue) : type.BaseType.GetAttributeValue<TAttribute, TValue>(valueSelector, true);
  }
}
