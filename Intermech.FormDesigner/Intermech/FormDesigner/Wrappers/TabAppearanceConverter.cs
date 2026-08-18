// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabAppearanceConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class TabAppearanceConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public TabAppearanceConverter()
    : base(typeof (TabAppearance))
  {
    this._hash.Add((object) TabAppearance.Buttons, (object) LocalizationHolder.rm.GetString("FormDesigner_88"));
    this._hash.Add((object) TabAppearance.FlatButtons, (object) LocalizationHolder.rm.GetString("FormDesigner_89"));
    this._hash.Add((object) TabAppearance.Normal, (object) LocalizationHolder.rm.GetString("FormDesigner_54"));
  }
}
