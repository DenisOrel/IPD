// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.BorderStyleConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>BorderStyle конвертер для русификации.</summary>
public class BorderStyleConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public BorderStyleConverter()
    : base(typeof (BorderStyle))
  {
    this._hash.Add((object) BorderStyle.Fixed3D, (object) LocalizationHolder.rm.GetString("FormDesigner_34"));
    this._hash.Add((object) BorderStyle.FixedSingle, (object) LocalizationHolder.rm.GetString("FormDesigner_36"));
    this._hash.Add((object) BorderStyle.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
  }
}
