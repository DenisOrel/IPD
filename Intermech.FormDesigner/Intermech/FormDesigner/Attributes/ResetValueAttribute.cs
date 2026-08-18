// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Attributes.ResetValueAttribute
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;

#nullable disable
namespace Intermech.FormDesigner.Attributes;

/// <summary>
/// 
/// </summary>
internal class ResetValueAttribute : Attribute
{
  /// <summary>
  /// 
  /// </summary>
  public bool CanResetValue { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="canResetValue"></param>
  public ResetValueAttribute(bool canResetValue) => this.CanResetValue = canResetValue;
}
