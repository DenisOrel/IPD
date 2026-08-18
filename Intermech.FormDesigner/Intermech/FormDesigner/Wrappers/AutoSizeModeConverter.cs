// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AutoSizeModeConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>AutoSizeMode конвертер для русификации.</summary>
public class AutoSizeModeConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public AutoSizeModeConverter()
    : base(typeof (AutoSizeMode))
  {
    this._hash.Add((object) AutoSizeMode.GrowOnly, (object) LocalizationHolder.rm.GetString("FormDesigner.AutoSizeModeConverter.GrowOnly"));
    this._hash.Add((object) AutoSizeMode.GrowAndShrink, (object) LocalizationHolder.rm.GetString("FormDesigner.AutoSizeModeConverter.GrowAndShrink"));
  }
}
