// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.ImGlobals
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Перечислитель позволяет определить, к какому типу относится какой-то элемент метаданных
/// </summary>
[Serializable]
public enum ImGlobals
{
  /// <summary>Неизвестные метаданные</summary>
  Unknown = 0,
  /// <summary>Тип атрибута</summary>
  IMSAttributeType = 1,
  /// <summary>Группа атрибутов</summary>
  IMSAttributeGroup = 2,
  /// <summary>Уровень продвижения</summary>
  IMSLifeCycleLevel = 10, // 0x0000000A
  /// <summary>Схема жизненного цикла</summary>
  IMSLifeCycleScheme = 11, // 0x0000000B
  /// <summary>Шаг жизненного цикла</summary>
  IMSLifeCycleStep = 12, // 0x0000000C
  /// <summary>Тип объекта</summary>
  IMSObjectType = 20, // 0x00000014
  /// <summary>Тип связи</summary>
  IMSRelationType = 30, // 0x0000001E
}
