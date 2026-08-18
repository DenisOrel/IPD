
// Type: Intermech.Redline.RedToolAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Reflection;


namespace Intermech.Redline;

[AttributeUsage(AttributeTargets.Field)]
public class RedToolAttribute : Attribute
{
  public RedToolAttribute(Type tool) => this.Tool = tool;

  /// <summary>соответствующий MapTool</summary>
  public Type Tool { get; protected set; }

  public static Type Get(IRedToolType value)
  {
    FieldInfo field = value.GetType().GetField(value.ToString());
    if (field != (FieldInfo) null)
    {
      object[] customAttributes = field.GetCustomAttributes(typeof (RedToolAttribute), false);
      if (customAttributes.Length != 0)
        return !(customAttributes[0] is RedToolAttribute redToolAttribute) ? (Type) null : redToolAttribute.Tool;
    }
    return (Type) null;
  }

  public static bool Find(Type tool, out IRedToolType ret)
  {
    if (tool != (Type) null)
    {
      Type enumType = typeof (IRedToolType);
      foreach (string name in Enum.GetNames(enumType))
      {
        object[] customAttributes = enumType.GetField(name).GetCustomAttributes(typeof (RedToolAttribute), false);
        if (customAttributes.Length != 0 && (customAttributes[0] is RedToolAttribute redToolAttribute ? redToolAttribute.Tool : (Type) null) == tool)
        {
          ret = (IRedToolType) Enum.Parse(enumType, name);
          return true;
        }
      }
    }
    ret = IRedToolType.tNone;
    return false;
  }
}
