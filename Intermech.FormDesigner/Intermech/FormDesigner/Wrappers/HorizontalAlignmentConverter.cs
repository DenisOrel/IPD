// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.HorizontalAlignmentConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>HorizontalAlignment конвертер для русификации.</summary>
public class HorizontalAlignmentConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public HorizontalAlignmentConverter()
    : base(typeof (HorizontalAlignment))
  {
    this._hash.Add((object) HorizontalAlignment.Center, (object) LocalizationHolder.rm.GetString("FormDesigner_93"));
    this._hash.Add((object) HorizontalAlignment.Left, (object) LocalizationHolder.rm.GetString("FormDesigner_66"));
    this._hash.Add((object) HorizontalAlignment.Right, (object) LocalizationHolder.rm.GetString("FormDesigner_67"));
  }
}
