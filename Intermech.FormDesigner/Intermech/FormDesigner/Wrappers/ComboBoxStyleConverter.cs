// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ComboBoxStyleConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>ComboBoxStyle конвертер для русификации.</summary>
public class ComboBoxStyleConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public ComboBoxStyleConverter()
    : base(typeof (ComboBoxStyle))
  {
    this._hash.Add((object) ComboBoxStyle.DropDown, (object) LocalizationHolder.rm.GetString("FormDesigner_61"));
    this._hash.Add((object) ComboBoxStyle.DropDownList, (object) LocalizationHolder.rm.GetString("FormDesigner_62"));
    this._hash.Add((object) ComboBoxStyle.Simple, (object) LocalizationHolder.rm.GetString("FormDesigner_63"));
  }
}
