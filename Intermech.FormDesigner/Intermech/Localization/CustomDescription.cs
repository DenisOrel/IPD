// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// 
/// </summary>
internal class CustomDescription : DescriptionAttribute
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="description"></param>
  public CustomDescription(string description)
  {
    this.DescriptionValue = LocalizationHolder.rma.GetString(description) ?? string.Empty;
  }
}
