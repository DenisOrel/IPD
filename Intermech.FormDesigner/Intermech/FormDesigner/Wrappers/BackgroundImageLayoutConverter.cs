// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.BackgroundImageLayoutConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>BackgroundImageLayout конвертер для русификации</summary>
public class BackgroundImageLayoutConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public BackgroundImageLayoutConverter()
    : base(typeof (ImageLayout))
  {
    this._hash.Add((object) ImageLayout.None, (object) LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayoutConverter.None"));
    this._hash.Add((object) ImageLayout.Center, (object) LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayoutConverter.Center"));
    this._hash.Add((object) ImageLayout.Stretch, (object) LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayoutConverter.Stretch"));
    this._hash.Add((object) ImageLayout.Tile, (object) LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayoutConverter.Tile"));
    this._hash.Add((object) ImageLayout.Zoom, (object) LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayoutConverter.Zoom"));
  }
}
