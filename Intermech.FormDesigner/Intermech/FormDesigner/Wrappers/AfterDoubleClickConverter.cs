// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AfterDoubleClickConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Localization;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Конвертер для свойства "Список".</summary>
public class AfterDoubleClickConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public AfterDoubleClickConverter()
    : base(typeof (AfterDoubleClickAction))
  {
    this._hash.Add((object) AfterDoubleClickAction.Card, (object) LocalizationHolder.rm.GetString("FormDesigner_AfterDoubleClickConverter_Card"));
    this._hash.Add((object) AfterDoubleClickAction.InTree, (object) LocalizationHolder.rm.GetString("FormDesigner_AfterDoubleClickConverter_InTree"));
  }
}
