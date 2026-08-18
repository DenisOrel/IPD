// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.SelectionModeConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>SelectionMode конвертер для русификации.</summary>
public class SelectionModeConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public SelectionModeConverter()
    : base(typeof (SelectionMode))
  {
    this._hash.Add((object) SelectionMode.MultiExtended, (object) LocalizationHolder.rm.GetString("FormDesigner_58"));
    this._hash.Add((object) SelectionMode.MultiSimple, (object) LocalizationHolder.rm.GetString("FormDesigner_59"));
    this._hash.Add((object) SelectionMode.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
    this._hash.Add((object) SelectionMode.One, (object) LocalizationHolder.rm.GetString("FormDesigner_60"));
  }
}
