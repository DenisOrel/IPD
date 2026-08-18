// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationBool
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Статусы</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum TechNumerationBool
{
  /// <summary>
  /// 
  /// </summary>
  [Description("True")] Yes,
  /// <summary>
  /// 
  /// </summary>
  [Description("False")] No,
}
