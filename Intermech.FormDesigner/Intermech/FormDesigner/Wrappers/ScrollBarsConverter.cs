// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ScrollBarsConverter
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
public class ScrollBarsConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public ScrollBarsConverter()
    : base(typeof (ScrollBars))
  {
    this._hash.Add((object) ScrollBars.Both, (object) LocalizationHolder.rm.GetString("FormDesigner_94"));
    this._hash.Add((object) ScrollBars.Horizontal, (object) LocalizationHolder.rm.GetString("FormDesigner_95"));
    this._hash.Add((object) ScrollBars.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
    this._hash.Add((object) ScrollBars.Vertical, (object) LocalizationHolder.rm.GetString("FormDesigner_96"));
  }
}
