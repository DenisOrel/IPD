// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDisplayName
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
internal class CustomDisplayName : DisplayNameAttribute
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="displayName"></param>
  public CustomDisplayName(string displayName)
  {
    this.DisplayNameValue = LocalizationHolder.rma.GetString(displayName);
  }
}
