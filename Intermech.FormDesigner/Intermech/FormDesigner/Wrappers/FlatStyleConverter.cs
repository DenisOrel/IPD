// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FlatStyleConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>FlatStyle конвертер для русификации.</summary>
public class FlatStyleConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public FlatStyleConverter()
    : base(typeof (FlatStyle))
  {
    this._hash.Add((object) FlatStyle.Flat, (object) LocalizationHolder.rm.GetString("FormDesigner_40"));
    this._hash.Add((object) FlatStyle.Popup, (object) LocalizationHolder.rm.GetString("FormDesigner_41"));
    this._hash.Add((object) FlatStyle.Standard, (object) LocalizationHolder.rm.GetString("FormDesigner_42"));
    this._hash.Add((object) FlatStyle.System, (object) LocalizationHolder.rm.GetString("FormDesigner_43"));
  }
}
