// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmContextType
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Вид контекста конфигуратора составов</summary>
[Serializable]
public enum PdmContextType
{
  /// <summary>Тип контекста неизвестен</summary>
  Unknown = -1, // 0xFFFFFFFF
  /// <summary>
  /// Редактируется атрибут связи "Контекст конфигуратора составов"
  /// </summary>
  ContextRelation = 0,
  /// <summary>
  /// Редактируется атрибут объекта "Контекст конфигуратора составов"
  /// </summary>
  ContextObject = 1,
  /// <summary>
  /// Редактируется контекст конфигурируемого типа объекта, сохранение в базу не требуется, информация хранится в кэше
  /// </summary>
  ConfigurableObject = 2,
}
