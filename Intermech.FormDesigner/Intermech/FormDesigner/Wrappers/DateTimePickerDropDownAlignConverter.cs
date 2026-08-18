// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DateTimePickerDropDownAlignConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// DateTimePickerDropDownAlign конвертер для русификации.
/// </summary>
public class DateTimePickerDropDownAlignConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public DateTimePickerDropDownAlignConverter()
    : base(typeof (LeftRightAlignment))
  {
    this._hash.Add((object) LeftRightAlignment.Left, (object) LocalizationHolder.rm.GetString("FormDesigner.DateTimePickerDropDownAlignConverter.Left"));
    this._hash.Add((object) LeftRightAlignment.Right, (object) LocalizationHolder.rm.GetString("FormDesigner.DateTimePickerDropDownAlignConverter.Right"));
  }
}
