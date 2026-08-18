// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.PictureSelectModeConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Localization;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>PictureSelectMode конвертер для русификации.</summary>
public class PictureSelectModeConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public PictureSelectModeConverter()
    : base(typeof (PictureSelectMode))
  {
    this._hash.Add((object) PictureSelectMode.Fixed, (object) LocalizationHolder.rm.GetString("FormDesigner_p01"));
    this._hash.Add((object) PictureSelectMode.UserRuntime, (object) LocalizationHolder.rm.GetString("FormDesigner_p02"));
  }
}
