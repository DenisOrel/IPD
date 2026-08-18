// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DockStyleConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>DockStyle конвертер для русификации.</summary>
public class DockStyleConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public DockStyleConverter()
    : base(typeof (DockStyle))
  {
    this._hash.Add((object) DockStyle.Bottom, (object) LocalizationHolder.rm.GetString("FormDesigner_64"));
    this._hash.Add((object) DockStyle.Fill, (object) LocalizationHolder.rm.GetString("FormDesigner_65"));
    this._hash.Add((object) DockStyle.Left, (object) LocalizationHolder.rm.GetString("FormDesigner_66"));
    this._hash.Add((object) DockStyle.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
    this._hash.Add((object) DockStyle.Right, (object) LocalizationHolder.rm.GetString("FormDesigner_67"));
    this._hash.Add((object) DockStyle.Top, (object) LocalizationHolder.rm.GetString("FormDesigner_68"));
  }
}
