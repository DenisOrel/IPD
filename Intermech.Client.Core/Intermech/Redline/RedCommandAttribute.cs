
// Type: Intermech.Redline.RedCommandAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Reflection;


namespace Intermech.Redline;

[AttributeUsage(AttributeTargets.Field)]
public class RedCommandAttribute : Attribute
{
  public RedCommandAttribute(string command) => this.Command = command;

  /// <summary>команда кнопки</summary>
  public string Command { get; protected set; }

  public static string Get(IRedToolType value)
  {
    FieldInfo field = value.GetType().GetField(value.ToString());
    return field != (FieldInfo) null && field.GetCustomAttributes(typeof (RedCommandAttribute), false) is RedCommandAttribute[] customAttributes && customAttributes.Length != 0 ? customAttributes[0].Command : (string) null;
  }

  public static bool Find(string command, out IRedToolType ret)
  {
    if (command != null)
    {
      Type enumType = typeof (IRedToolType);
      foreach (string name in Enum.GetNames(enumType))
      {
        if (enumType.GetField(name).GetCustomAttributes(typeof (RedCommandAttribute), false) is RedCommandAttribute[] customAttributes && customAttributes.Length != 0 && customAttributes[0].Command == command)
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
