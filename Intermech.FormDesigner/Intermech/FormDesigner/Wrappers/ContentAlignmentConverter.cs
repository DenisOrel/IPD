// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ContentAlignmentConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Drawing;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>ContentAlignmen конвертер для русификации.</summary>
public class ContentAlignmentConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public ContentAlignmentConverter()
    : base(typeof (ContentAlignment))
  {
    this._hash.Add((object) ContentAlignment.TopLeft, (object) LocalizationHolder.rm.GetString("FormDesigner_44"));
    this._hash.Add((object) ContentAlignment.TopCenter, (object) LocalizationHolder.rm.GetString("FormDesigner_45"));
    this._hash.Add((object) ContentAlignment.TopRight, (object) LocalizationHolder.rm.GetString("FormDesigner_46"));
    this._hash.Add((object) ContentAlignment.MiddleLeft, (object) LocalizationHolder.rm.GetString("FormDesigner_47"));
    this._hash.Add((object) ContentAlignment.MiddleCenter, (object) LocalizationHolder.rm.GetString("FormDesigner_48"));
    this._hash.Add((object) ContentAlignment.MiddleRight, (object) LocalizationHolder.rm.GetString("FormDesigner_49"));
    this._hash.Add((object) ContentAlignment.BottomLeft, (object) LocalizationHolder.rm.GetString("FormDesigner_50"));
    this._hash.Add((object) ContentAlignment.BottomCenter, (object) LocalizationHolder.rm.GetString("FormDesigner_51"));
    this._hash.Add((object) ContentAlignment.BottomRight, (object) LocalizationHolder.rm.GetString("FormDesigner_52"));
  }
}
