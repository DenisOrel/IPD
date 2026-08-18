// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabAlignmentConverter
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
public class TabAlignmentConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public TabAlignmentConverter()
    : base(typeof (TabAlignment))
  {
    this._hash.Add((object) TabAlignment.Bottom, (object) LocalizationHolder.rm.GetString("FormDesigner_64"));
    this._hash.Add((object) TabAlignment.Left, (object) LocalizationHolder.rm.GetString("FormDesigner_66"));
    this._hash.Add((object) TabAlignment.Right, (object) LocalizationHolder.rm.GetString("FormDesigner_67"));
    this._hash.Add((object) TabAlignment.Top, (object) LocalizationHolder.rm.GetString("FormDesigner_68"));
  }
}
