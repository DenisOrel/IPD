// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ParentPointConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Localization;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class ParentPointConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public ParentPointConverter()
    : base(typeof (AttributeDestinationPoint))
  {
    this._hash.Add((object) AttributeDestinationPoint.Default, (object) LocalizationHolder.rm.GetString("FormDesigner.Attribute.AttributeDestinationPoint.Default"));
    this._hash.Add((object) AttributeDestinationPoint.Relation, (object) LocalizationHolder.rm.GetString("FormDesigner.Attribute.AttributeDestinationPoint.Relation"));
  }
}
