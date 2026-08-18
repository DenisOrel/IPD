
// Type: Intermech.Navigator.Data.NodeDelayedEnumValue
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Data;

/// <summary>
/// 
/// </summary>
public sealed class NodeDelayedEnumValue : NodeDelayedValue
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return !(this.Value is Enum) ? base.ToString() : EnumTypeHelper.GetCaption((Enum) this.Value);
  }

  public NodeDelayedEnumValue()
    : base()
  {
  }
}
