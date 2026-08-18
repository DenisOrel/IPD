// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DateTimePickerFormatConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>DateTimePickerFormat конвертер для русификации.</summary>
public class DateTimePickerFormatConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public DateTimePickerFormatConverter()
    : base(typeof (DateTimePickerFormat))
  {
    this._hash.Add((object) DateTimePickerFormat.Custom, (object) LocalizationHolder.rm.GetString("FormDesigner_DateTime_Format_AttributeMask"));
    this._hash.Add((object) DateTimePickerFormat.Long, (object) LocalizationHolder.rm.GetString("FormDesigner_98"));
    this._hash.Add((object) DateTimePickerFormat.Short, (object) LocalizationHolder.rm.GetString("FormDesigner_99"));
    this._hash.Add((object) DateTimePickerFormat.Time, (object) LocalizationHolder.rm.GetString("FormDesigner_97"));
  }
}
