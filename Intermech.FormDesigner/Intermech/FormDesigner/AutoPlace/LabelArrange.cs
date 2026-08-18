// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.LabelArrange
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>Позиционирование метки.</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum LabelArrange
{
  /// <summary>"Attribute.FormDesigner_1" = Нет</summary>
  [CustomDescription("Attribute.FormDesigner_1")] laNone,
  /// <summary>"Attribute.FormDesigner_2" = Слева</summary>
  [CustomDescription("Attribute.FormDesigner_2")] laLeft,
  /// <summary>"Attribute.FormDesigner_3" = Сверху</summary>
  [CustomDescription("Attribute.FormDesigner_3")] laTop,
}
