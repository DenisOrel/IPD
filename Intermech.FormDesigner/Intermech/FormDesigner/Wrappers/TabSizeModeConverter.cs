// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabSizeModeConverter
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
public class TabSizeModeConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public TabSizeModeConverter()
    : base(typeof (TabSizeMode))
  {
    this._hash.Add((object) TabSizeMode.FillToRight, (object) LocalizationHolder.rm.GetString("FormDesigner_91"));
    this._hash.Add((object) TabSizeMode.Fixed, (object) LocalizationHolder.rm.GetString("FormDesigner_92"));
    this._hash.Add((object) TabSizeMode.Normal, (object) LocalizationHolder.rm.GetString("FormDesigner_54"));
  }
}
