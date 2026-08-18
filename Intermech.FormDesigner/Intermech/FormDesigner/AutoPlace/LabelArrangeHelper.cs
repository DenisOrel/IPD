// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.LabelArrangeHelper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>
/// 
/// </summary>
internal class LabelArrangeHelper
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="la"></param>
  /// <returns></returns>
  public static string GetCaption(LabelArrange la) => EnumTypeHelper.GetCaption((Enum) la);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <returns></returns>
  public static LabelArrange GetEnumValue(string s)
  {
    return (LabelArrange) EnumTypeHelper.GetEnumValue(typeof (LabelArrange), s, (object) LabelArrange.laNone);
  }
}
